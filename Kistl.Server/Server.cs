
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Server;
    using Kistl.API.Utils;
    using System.DirectoryServices;

    /// <summary>
    /// Serversteuerung
    /// </summary>
    public class Server
        : IDisposable
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server");

        public Server(IContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// The IoC container used by this Server.
        /// </summary>
        private IContainer container;

        public void GenerateCode()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.CreateInnerContainer())
            {
                var generator = subContainer.Resolve<Generators.Generator>();
                generator.GenerateCode();
            }
        }

        public void Export(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            using (var subContainer = container.CreateInnerContainer())
            {
                Packaging.Exporter.ExportFromContext(subContainer.Resolve<IKistlContext>(), file, namespaces);
            }
        }

        public void Import(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlServerContext ctx = subContainer.Resolve<IKistlServerContext>();
                Packaging.Importer.LoadFromXml(ctx, file);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void Publish(string file, string[] namespaces)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],namespaces=[{1}]", file, String.Join(";", namespaces ?? new string[] { })))
            using (var subContainer = container.CreateInnerContainer())
            {
                Packaging.Exporter.PublishFromContext(subContainer.Resolve<IKistlContext>(), file, namespaces);
            }
        }

        public void Deploy(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            using (var subContainer = container.CreateInnerContainer())
            using (FileStream fs = File.OpenRead(file))
            {
                var ctx = subContainer.Resolve<IKistlServerContext>();
                Packaging.Importer.Deploy(ctx, fs);
                Log.Info("Submitting changes");
                ctx.SubmitRestore();
            }
        }

        public void CheckSchemaFromCurrentMetaData(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            using (var subContainer = container.CreateInnerContainer())
            {
                var ctx = subContainer.Resolve<IKistlContext>();
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("withRepair=[{0}]", withRepair))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                ISchemaProvider schemaProvider = subContainer.Resolve<ISchemaProvider>();
                SchemaManagement.SchemaManager.LoadSavedSchemaInto(schemaProvider, ctx);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void CheckSchema(string file, bool withRepair)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}],withRepair=[{1}]", file, withRepair))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                Packaging.Importer.LoadFromXml(ctx, file);
                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.CheckSchema(withRepair);
            }
        }

        public void UpdateSchema()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                IKistlContext dbctx = subContainer.Resolve<IKistlContext>();

                // load database contents into local cache
                // to be independent of the database when managing 
                // the schema
                using (MemoryStream ms = new MemoryStream())
                {
                    Packaging.Exporter.PublishFromContext(dbctx, ms, new string[] { "*" });
                    ms.Seek(0, SeekOrigin.Begin);
                    Packaging.Importer.LoadFromXml(ctx, ms);
                }

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void UpdateSchema(string file)
        {
            using (Log.InfoTraceMethodCallFormat("file=[{0}]", file))
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<MemoryContext>();
                Packaging.Importer.LoadFromXml(ctx, file);

                var mgr = subContainer.Resolve<SchemaManagement.SchemaManager>(new NamedParameter("newSchema", ctx));
                mgr.UpdateSchema();
            }
        }

        public void SyncIdentities()
        {
            using (Log.InfoTraceMethodCall())
            using (var subContainer = container.CreateInnerContainer())
            {
                IKistlContext ctx = subContainer.Resolve<IKistlContext>();
                var userList = new Dictionary<string, string>();
                ReadUsers(Environment.UserDomainName, userList);
                ReadUsers(Environment.MachineName, userList);

                var identities = ctx.GetQuery<Kistl.App.Base.Identity>().ToLookup(k => k.WCFAccount.ToUpper());

                foreach (var user in userList)
                {
                    if (!identities.Contains(user.Key.ToUpper()))
                    {
                        var id = ctx.Create<Kistl.App.Base.Identity>();
                        id.WCFAccount = user.Key;
                        id.UserName = user.Value;
                        Log.InfoFormat("Adding Identity {0} ({1})", id.UserName, id.WCFAccount);
                    }
                }

                ctx.SubmitChanges();
            }
        }

        private void ReadUsers(string machine, Dictionary<string, string> userList)
        {
            try
            {
                DirectoryEntry root = new DirectoryEntry("WinNT://" + machine);
                root.Children.SchemaFilter.Add("User");
                foreach (DirectoryEntry d in root.Children)
                {
                    var login = machine + "\\" + d.Name;
                    userList[login] = (d.Properties["FullName"].Value ?? login).ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error reading users from " + machine, ex);
            }
        }

        public void RunFixes()
        {
            using (Log.InfoTraceMethodCall())
            {
            }
        }

        #region IDisposable Members

        // TODO: implement Dispose Pattern after 
        // http://msdn2.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            Log.Info("Disposing");
            {
                if (container != null)
                {
                    container.Dispose();
                    container = null;
                }
            }
        }

        #endregion
    }
}
