using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using System.Xml;
using System.Diagnostics;
using Kistl.Server.Generators.Extensions;
using System.Reflection;
using Kistl.API.Utils;
using Kistl.App.Extensions;

namespace Kistl.Server.Packaging
{
    public class Exporter
    {
        public static void Export(string filename, string[] modules)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Starting Export for Modules {0}", string.Join(", ", modules));
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    using (XmlTextWriter xml = new XmlTextWriter(filename, Encoding.UTF8))
                    {
                        var moduleList = new List<Kistl.App.Base.Module>();
                        foreach (var strModule in modules)
                        {
                            var module = ctx.GetQuery<Kistl.App.Base.Module>().Where(m => m.ModuleName == strModule).FirstOrDefault();
                            if (module == null)
                            {
                                Trace.TraceWarning("Module {0} not found", strModule);
                                continue;
                            }
                            moduleList.Add(module);
                        }

                        xml.Formatting = Formatting.Indented;
                        xml.WriteStartDocument();
                        xml.WriteStartElement("KistlPackaging");
                        xml.WriteAttributeString("xmlns", "http://dasz.at/Kistl");
                        foreach (var module in moduleList)
                        {
                            xml.WriteAttributeString("xmlns", module.ModuleName, null, module.Namespace);
                        }

                        xml.WriteAttributeString("date", XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.Utc));

                        foreach (var module in moduleList)
                        {
                            Trace.TraceInformation("  exporting {0}", module.ModuleName);
                            foreach (var objClass in module.DataTypes.OfType<Kistl.App.Base.ObjectClass>()) // TODO: .Where(o => o.Implements("IExportable"))
                            {
                                Trace.TraceInformation("    {0} ", objClass.ClassName);
                                foreach (IDataObject obj in ctx.GetQuery(new InterfaceType(objClass.GetDataType())))
                                {
                                    Console.Write(".");
                                    xml.WriteStartElement(objClass.ClassName, module.Namespace);
                                    obj.ToStream(xml, modules);
                                    xml.WriteEndElement();
                                }
                                Console.WriteLine();
                            }

                            // TODO: Use r.Module - if it's implemented
                            int moduleID = module.ID; // Dont ask
                            foreach (var rel in ctx.GetQuery<Kistl.App.Base.Relation>().Where(r => r.A.Type.Module.ID == moduleID))
                            {
                                if (rel.GetRelationType() != RelationType.n_m) continue;

                                string ifTypeName = string.Format("{0}, {1}", rel.GetCollectionEntryFullName(), ApplicationContext.Current.InterfaceAssembly);
                                Trace.TraceInformation("    {0} ", ifTypeName);
                                Type ifType = Type.GetType(ifTypeName);
                                if (ifType == null)
                                {
                                    Trace.TraceWarning("RelationType {0} not found", ifTypeName);
                                    continue;
                                }

                                MethodInfo mi = ctx.GetType().FindGenericMethod("FetchRelation", new Type[] { ifType }, new Type[] { typeof(int), typeof(RelationEndRole), typeof(IDataObject) });
                                var relations = MagicCollectionFactory.WrapAsCollection<IPersistenceObject>(mi.Invoke(ctx, new object[] { rel.ID, RelationEndRole.A, null }));

                                foreach (var obj in relations)
                                {
                                    Console.Write(".");
                                    xml.WriteStartElement(rel.GetCollectionEntryClassName(), rel.A.Type.Module.Namespace);
                                    obj.ToStream(xml, modules);
                                    xml.WriteEndElement();
                                }
                            }
                        }
                        xml.WriteEndElement();
                        xml.WriteEndDocument();
                    }
                }
                Trace.TraceInformation("Export finished");
            }
        }
    }
}
