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

namespace Zetbox.DalProvider.NHibernate.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.Generator;
    using System.Collections.Specialized;

    public class NHibernateGenerator
        : AbstractBaseGenerator
    {
        public NHibernateGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        // TODO: #1569 Why not using const Suffix?
        public override string ExtraSuffix { get { return "NHibernate"; } }
        public override string Description { get { return "NHibernate"; } }
        public override string TargetNameSpace { get { return "Zetbox.Objects.NHibernate"; } }
        public override string BaseName { get { return "NHibernate"; } }
        public override string ProjectGuid { get { return "{5514C9AF-6C2E-4713-8EAC-FAAADFFDB029}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        public override IEnumerable<string> RequiredNamespaces
        {
            get
            {
                return new string[] {
                   "Zetbox.API.Utils", "Zetbox.DalProvider.Base", "Zetbox.DalProvider.NHibernate",
                };
            }
        }

        protected override IEnumerable<string> Generate_Other(Zetbox.API.IZetboxContext ctx)
        {
            using (log4net.NDC.Push("NhGenerateOther"))
            {
                var otherFileNames = new List<string>();

                otherFileNames.AddRange(CreateMappings(ctx));

                return base.Generate_Other(ctx).Concat(otherFileNames);
            }
        }

        private List<string> CreateMappings(Zetbox.API.IZetboxContext ctx)
        {
            this.RunTemplateWithExtension(ctx, "Mappings.CollectionEntriesHbm", "CollectionEntries", "hbm.xml");
            this.RunTemplateWithExtension(ctx, "Mappings.ClassesHbm", "Classes", "hbm.xml", ExtraSuffix);

            // Mapping files are picked up automatically by the ProjectFile as EmbeddedResources
            // so we must not need to keep track of them as Compiles
            return new List<string>();
        }
    }
}
