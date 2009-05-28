using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API;
using Kistl.API.Server;
using System.Xml;
using System.IO;

namespace Kistl.Server.Packaging
{
    public class Importer
    {
        public static void Import(IKistlContext ctx, Stream s)
        {
            // TODO: Das muss ich z.Z. machen, weil die erste Query eine Entity Query ist und noch nix geladen wurde....
            var testObj = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
            Debug.WriteLine(testObj != null ? testObj.ToString() : "");

            List<IPersistenceObject> objects = new List<IPersistenceObject>();
            using (XmlTextReader xml = new XmlTextReader(s))
            {
                // Find Root Element
                while (xml.Read() && xml.NodeType != XmlNodeType.Element && xml.LocalName != "KistlPackaging" && xml.NamespaceURI != "http://dasz.at/Kistl") ;

                // Read content
                while (xml.Read())
                {
                    if (xml.NodeType != XmlNodeType.Element) continue;

                    // TODO: Change to GUID
                    Guid exportGuid = xml.GetAttribute("ExportGuid").ParseGuidValue();
                    if (exportGuid != Guid.Empty)
                    {
                        Console.Write(".");
                        string ifTypeName = string.Format("{0}.{1}, {2}", xml.NamespaceURI, xml.LocalName, ApplicationContext.Current.InterfaceAssembly);
                        Type t = Type.GetType(ifTypeName);
                        if (t == null)
                        {
                            Trace.TraceWarning("Type {0} not found", ifTypeName);
                            continue;
                        }

                        InterfaceType ifType = new InterfaceType(t);

                        IPersistenceObject obj;
                        obj = (IPersistenceObject)ctx.FindPersistenceObject(ifType, exportGuid);
                        if(obj == null)
                        {
                            if (typeof(IDataObject).IsAssignableFrom(ifType.Type))
                            {
                                obj = ctx.Create(ifType);
                            }
                            else if (typeof(IRelationCollectionEntry).IsAssignableFrom(ifType.Type))
                            {
                                obj = ctx.CreateRelationCollectionEntry(ifType);
                            }
                            else
                            {
                                throw new NotSupportedException("Interfacetype " + ifTypeName + " is not supported");
                            }
                            ((Kistl.App.Base.IExportable)obj).ExportGuid = exportGuid;
                        }
                        objects.Add(obj);

                        var children = xml.ReadSubtree();
                        while (children.Read())
                        {
                            if (children.NodeType == XmlNodeType.Element)
                            {
                                ((IExportableInternal)obj).MergeImport(xml);
                            }
                        }
                    }
                }
            }

            Trace.TraceInformation("Reloading References");
            foreach (var obj in objects)
            {
                obj.ReloadReferences();
            }
        }

        public static void Import(string filename)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Starting Import from {0}", filename);
                using (IKistlContext  ctx = KistlContext.GetContext())
                {
                    using (FileStream fs = File.OpenRead(filename))
                    {
                        Import(ctx, fs);
                    }
                    Trace.TraceInformation("Submitting changes");
                    ctx.SubmitChanges();
                }
                Trace.TraceInformation("Import finished");
            }
        }
    }
}
