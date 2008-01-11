using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des Serverseitigen CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerServer : API.ICustomActionsManager
    {
        /// <summary>
        /// List of Custom Actions
        /// </summary>
        private List<ICustomServerActions> actions = new List<ICustomServerActions>();

        /// <summary>
        /// Attach using Metadata
        /// Detaching is done through the Garbage Collector
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// Und damit kann man dann auch security machen :-)
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(Kistl.API.IDataObject obj)
        {
            actions.ForEach(a => a.Attach(obj));
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
