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
                        Trace.TraceInformation("found XMLNode: {0}", xml.Name);
                        string ifTypeName = string.Format("{0}.{1}, {2}", xml.NamespaceURI, xml.LocalName, ApplicationContext.Current.InterfaceAssembly);
                        Type t = Type.GetType(ifTypeName);
                        if (t == null)
                        {
                            Trace.TraceWarning("Type {0} not found", ifTypeName);
                            continue;
                        }

                        InterfaceType ifType = new InterfaceType(t);

                        // TODO: Change this!!! Frage: Soll FindPersistenceObject null liefern, wenn es ein Objekt nicht kennt?
                        // bzw: Wie komme ich an Objekte, wenn ich nur die Guid kenne - ich würde dann ein GetPersistenceObjectQuery brauchen??
                        // David: FindByGuid vielleicht?
                        IPersistenceObject obj;
                        obj = (IPersistenceObject)ctx.GetQuery(ifType).Cast<Kistl.App.Base.IExportable>().FirstOrDefault(i => i.ExportGuid == exportGuid);
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
                                obj.FromStream(xml);
                            }
                        }
                    }
                }
            }

            Trace.TraceInformation("Submitting changes");
            foreach (var obj in objects)
            {
                obj.ReloadReferences();
            }

            Trace.TraceInformation("Submitting changes");
            ctx.SubmitChanges();
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
                }
                Trace.TraceInformation("Import finished");
            }
        }
    }
}
