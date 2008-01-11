using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client
{
    /// <summary>
    /// Implementierung des Clientseitigen CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerClient : API.ICustomActionsManager
    {
        /// <summary>
        /// List of Custom Actions
        /// </summary>
        private List<ICustomClientActions> actions = new List<ICustomClientActions>();

        /// <summary>
        /// Attach using Metadata
        /// Detaching is done through the Garbage Collector
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(IDataObject obj)
        {
            actions.ForEach(a => a.Attach(obj));
        }

        /// <summary>
        /// Load Metadata, create an Instance and save
        /// Note: The Assembly type is loaded, but _without_ actions!
        /// </summary>
        public void Init()
        {
            using (KistlContext ctx = new KistlContext())
            {
                var assemblies = ctx.GetQuery<Kistl.App.Base.Assembly>();
                foreach (Kistl.App.Base.Assembly a in assemblies)
                {
                    try
                    {
                        foreach (Type t in System.Reflection.Assembly.Load(a.AssemblyName).GetTypes())
                        {
                            if (t.GetInterfaces().Count(i => i == typeof(ICustomClientActions)) > 0)
                            {
                                actions.Add((ICustomClientActions)Activator.CreateInstance(t));
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
