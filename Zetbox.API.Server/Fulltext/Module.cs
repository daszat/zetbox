// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Server.Fulltext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.De;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.QueryParsers.Classic;
    using Lucene.Net.Search;
    using Lucene.Net.Store;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    public sealed class LuceneSearchDeps
    {
        internal LuceneSearchDeps(SearcherManager searcherManager, QueryParser parser, IMetaDataResolver resolver, IQueue queue)
        {
            this.SearcherManager = searcherManager;
            this.Parser = parser;
            this.Resolver = resolver;
            this.Queue = queue;
        }

        public SearcherManager SearcherManager { get; private set; }
        public QueryParser Parser { get; private set; }
        public IMetaDataResolver Resolver { get; private set; }
        public IQueue Queue { get; private set; }
    }

    [Description("A service for full-text indexing of all objects")]
    [Feature(NotOnFallback = true)]
    public sealed class Module : Autofac.Module
    {
        public static readonly string FIELD_CLASS = "__class";
        public static readonly string FIELD_CLASS_ID = "__class_id";
        public static readonly string FIELD_ID = "__id";
        public static readonly string FIELD_BODY = "__body";

        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register<Service>(c => new Service(c.Resolve<Func<IndexWriter>>(), c.Resolve<SearcherManager>()))
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register<ServiceQueue>(c => new ServiceQueue(c.Resolve<Service>()))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register<Listener>(c => new Listener(c.Resolve<IQueue>(), c.Resolve<Common.Fulltext.DataObjectFormatter>(), c.Resolve<IMetaDataResolver>()))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register<Lucene.Net.Store.Directory>(c =>
                {
                    var cfg = c.Resolve<ZetboxConfig>();
                    return SimpleFSDirectory.Open(Path.Combine(cfg.Server.DocumentStore, "LuceneIndex"));
                })
                .SingleInstance();

            builder
                .Register<StandardAnalyzer>(c => new StandardAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
                .As<Analyzer>()
                .Named<Analyzer>("en")
                .SingleInstance();

            builder
                .Register<GermanAnalyzer>(c => new GermanAnalyzer(Lucene.Net.Util.LuceneVersion.LUCENE_48))
                .Named<Analyzer>("de")
                .SingleInstance();

            builder
                .Register<IndexWriter>(c =>
                {
                    var directory = c.Resolve<Lucene.Net.Store.Directory>();
                    var analyzer = c.IsRegisteredWithName<Analyzer>(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                        ? c.ResolveNamed<Analyzer>(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                        : c.Resolve<Analyzer>();

                    return new IndexWriter(directory, new IndexWriterConfig(Lucene.Net.Util.LuceneVersion.LUCENE_48, analyzer));
                })
                .InstancePerDependency();

            builder
                .Register<SearcherManager>(c => new SearcherManager(c.Resolve<Lucene.Net.Store.Directory>()))
                .SingleInstance();

            builder
               .Register<QueryParser>(c => new QueryParser(Lucene.Net.Util.LuceneVersion.LUCENE_48, FIELD_BODY, c.Resolve<Analyzer>()))
               .InstancePerLifetimeScope();

            builder
                .Register<LuceneSearchDeps>(c => new LuceneSearchDeps(c.Resolve<SearcherManager>(), c.Resolve<QueryParser>(), c.Resolve<IMetaDataResolver>(), c.Resolve<IQueue>()))
                .InstancePerDependency();

            builder
                .Register<Rebuilder>(c => new Rebuilder(c.Resolve<ILifetimeScope>(), c.Resolve<Func<IndexWriter>>(), c.Resolve<Common.Fulltext.DataObjectFormatter>(), c.Resolve<IMetaDataResolver>()))
                .InstancePerDependency();

            builder
                .RegisterCmdLineAction("rebuild-fulltext-index=", "Rebuild and optimize the fulltext index. You can add a comma separated list of class names (including the namespace) to rebuild the index for only those classes. When no filter is given, all classes are rebuilt.",
                (scope, v) =>
                {
                    var rebuilder = scope.Resolve<Rebuilder>();
                    if (!string.IsNullOrWhiteSpace(v))
                    {
                        rebuilder.Rebuild(v.Split(','));
                    }
                    else
                    {
                        rebuilder.Rebuild();
                    }

                    return Task.CompletedTask;
                });
        }
    }
}
