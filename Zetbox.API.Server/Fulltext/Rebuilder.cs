﻿// This file is part of zetbox.
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
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Zetbox.API.Common;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    internal class Rebuilder
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Rebuilder));
        private readonly ILifetimeScope _scope;
        private readonly Func<IndexWriter> _indexWriterFactory;
        private readonly Common.Fulltext.DataObjectFormatter _formatter;
        private readonly IMetaDataResolver _resolver;

        public Rebuilder(ILifetimeScope scope, Func<IndexWriter> indexWriterFactory, Common.Fulltext.DataObjectFormatter formatter, IMetaDataResolver resolver)
        {
            if (scope == null) throw new ArgumentNullException("scope");
            if (indexWriterFactory == null) throw new ArgumentNullException("indexWriterFactory");
            if (formatter == null) throw new ArgumentNullException("formatter");
            if (resolver == null) throw new ArgumentNullException("resolver");

            _scope = scope;
            _indexWriterFactory = indexWriterFactory;
            _formatter = formatter;
            _resolver = resolver;
        }

        #region shared helper
        public static async Task<IndexUpdate.Text> ExtractText(IDataObject obj, Common.Fulltext.DataObjectFormatter formatter, IMetaDataResolver resolver)
        {
            var result = new IndexUpdate.Text();
            var customFormatter = obj as ICustomFulltextFormat;
            if (customFormatter != null)
            {
                result.Body = await customFormatter.GetFulltextIndexBody();
            }
            else
            {
                result.Body = await formatter.Format(obj);
            }

            var cls = resolver.GetObjectClass(obj.Context.GetInterfaceType(obj));
            var allProps = await cls.GetAllProperties();
            result.Fields = new Dictionary<string, string>();


            foreach (var prop in allProps.OfType<StringProperty>().OrderBy(p => p.Name))
            {
                var txtVal = obj.GetPropertyValue<string>(prop.Name);
                if (!string.IsNullOrWhiteSpace(txtVal))
                {
                    result.Fields[prop.Name] = txtVal;
                }
            }

            foreach (var prop in allProps.OfType<EnumerationProperty>().OrderBy(p => p.Name))
            {
                var enumVal = obj.GetPropertyValue<int>(prop.Name);
                var txtVal = await prop.Enumeration.GetLabelByValue(enumVal);
                if (!string.IsNullOrWhiteSpace(txtVal))
                {
                    result.Fields[prop.Name] = txtVal;
                }
            }

            return result;
        }
        #endregion

        private List<IDataObject> GetParcelHack<T>(IZetboxServerContext ctx, int lastID, int count)
            where T : class, IDataObject
        {
            // The query translator cannot properly handle the IDataObject cast:
            // return GetQuery<T>().Cast<IDataObject>();

            var result = new List<IDataObject>();
            foreach (var o in ctx.GetQuery<T>().Where(obj => obj.ID > lastID).OrderBy(obj => obj.ID).Take(count))
            {
                result.Add(o);
            }
            return result;
        }

        private List<IDataObject> GetParcel(Type t, IZetboxServerContext ctx, int lastID, int count)
        {
            var mi = this.GetType().FindGenericMethod("GetParcelHack", new[] { t }, new Type[] { typeof(IZetboxServerContext), typeof(int), typeof(int) }, isPrivate: true);
            return (List<IDataObject>)mi.Invoke(this, new object[] { ctx, lastID, count });
        }

        public async Task Rebuild(params string[] classFilter)
        {
            using (Log.InfoTraceMethodCall("Rebuild"))
            using(var idxWriter = _indexWriterFactory())
            {
                idxWriter.DeleteAll();

                var frozenCtx = _scope.Resolve<IFrozenContext>();
                var subContainer = _scope.BeginLifetimeScope();

                try
                {
                    var ctx = subContainer.Resolve<IZetboxServerContext>();
                    int objCounter = 0;
                    foreach (var cls in frozenCtx.GetQuery<ObjectClass>()
                        .Where(c => classFilter == null || classFilter.Length == 0 || classFilter.Contains(string.Format("{0}.{1}", c.Module.Namespace, c.Name)))
                        .OrderBy(c => c.Module.Namespace)
                        .ThenBy(c => c.Name))
                    {
                        Log.InfoFormat("Processing ObjectClass [{0}]", cls.Name);
                        var lastID = 0;
                        var dtType = await cls.GetDataType();
                        List<IDataObject> parcel = null;
                        do
                        {
                            parcel = GetParcel(dtType, ctx, lastID, Helper.MAXLISTCOUNT);
                            foreach (var obj in parcel)
                            {
                                var clsId = string.Format(CultureInfo.InvariantCulture, "{0}#{1}", dtType.FullName, obj.ID);
                                var txt = await ExtractText(obj, _formatter, _resolver);

                                var doc = new Document();
                                doc.Add(new Field(Module.FIELD_CLASS, dtType.FullName, new FieldType() { IsStored = true }));
                                doc.Add(new Field(Module.FIELD_CLASS_ID, clsId, new FieldType() { IsStored = true }));
                                doc.Add(new Field(Module.FIELD_ID, obj.ID.ToString(CultureInfo.InvariantCulture), new FieldType() { IsStored = true }));
                                doc.Add(new Field(Module.FIELD_BODY, txt.Body, new FieldType() { IsStored = false }));
                                if (txt.Fields != null)
                                {
                                    txt.Fields.ForEach(kvp => doc.Add(new Field(kvp.Key.ToLowerInvariant(), kvp.Value, new FieldType() { IsStored = false })));
                                }

                                idxWriter.AddDocument(doc);

                                objCounter++;
                                lastID = obj.ID;
                            }
                            Log.InfoFormat("Updated {0} objects", objCounter);
                            subContainer.Dispose();
                            subContainer = _scope.BeginLifetimeScope();
                            ctx = subContainer.Resolve<IZetboxServerContext>();
                        } while (parcel != null && parcel.Count > 0);
                    }
                    idxWriter.Commit();
                }
                finally
                {
                    subContainer.Dispose();
                }
            }
        }
    }
}
