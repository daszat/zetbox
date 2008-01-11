using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des Serverseitigen ObjectBrokers
    /// </summary>
    internal class CustomActionsManagerServer : API.ICustomActionsManager
    {
        /// <summary>
        /// List of Custom Actions
        /// </summary>
        private List<ICustomServerActions> actions = new List<ICustomServerActions>();

        /// <summary>
        /// Attach using Metadata
        /// Und damit kann man dann auch security machen :-)
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(Kistl.API.IDataObject obj)
        {
            // TODO: lt. Metadaten
            // API.Server.ICustomServerActions actions_tmp = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomServerActions, Kistl.App.Projekte.Server")) as API.Server.ICustomServerActions;
            // actions_tmp.Attach(obj);

            // TODO: Handle Detach
            actions.ForEach(a => a.Attach(obj));
        }

        public void DetachEvents(Kistl.API.IDataObject obj)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            using (KistlDataContext ctx = KistlDataContext.InitSession())
            {
                var assemblies = ctx.GetTable<Kistl.App.Base.Assembly>();
                foreach (Kistl.App.Base.Assembly a in assemblies)
                {
                    try
                    {
                        foreach (Type t in System.Reflection.Assembly.Load(a.AssemblyName).GetTypes())
                        {
                            if (t.GetInterfaces().Count(i => i == typeof(ICustomServerActions)) > 0)
                            {
                                actions.Add((ICustomServerActions)Activator.CreateInstance(t));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.HandleError(ex);
                    }
                }

                if (actions.Count == 0)
                {
                    throw new InvalidOperationException("No Custom Actions found");
                }
            }
        }
    }
}
