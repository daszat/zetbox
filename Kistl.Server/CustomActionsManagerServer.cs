using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;
using System.Reflection;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server
{
    /// <summary>
    /// Implementierung des Serverseitigen CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerServer : API.ICustomActionsManager
    {
        private class InvokeInfo
        {
            public MethodInfo CLRMethod { get; set; }
            public object Instance { get; set; }
            public EventInfo CLREvent { get; set; }
        }

        /// <summary>
        /// List of Custom Actions
        /// </summary>
        private Dictionary<ObjectType, List<InvokeInfo>> customAction = new Dictionary<ObjectType, List<InvokeInfo>>();

        /// <summary>
        /// Indicates that initializing is done
        /// </summary>
        private bool initialized = false;

        /// <summary>
        /// Attach using Metadata
        /// Detaching is done through the Garbage Collector
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// Und damit kann man dann auch security machen :-)
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(Kistl.API.IDataObject obj)
        {
            if (!initialized) return;

            // New Method
            if (customAction.ContainsKey(obj.Type))
            {
                foreach (InvokeInfo ii in customAction[obj.Type])
                {
                    ii.CLREvent.AddEventHandler(obj, Delegate.CreateDelegate(
                        ii.CLREvent.EventHandlerType, ii.Instance, ii.CLRMethod));
                }
            }
        }

        public void Init()
        {
            try{
                using (KistlDataContext ctx = KistlDataContext.GetContext())
                {
                    foreach (ObjectClass baseObjClass in ctx.GetTable<ObjectClass>())
                    {
                        ObjectType objType = baseObjClass.GetObjectType();
                        foreach (ObjectClass objClass in baseObjClass.GetObjectHierarchie(ctx))
                        {
                            foreach (MethodInvocation mi in objClass.MethodIvokations)
                            {
                                try
                                {
                                    if (mi.Assembly.IsClientAssembly) continue;

                                    Type t = Type.GetType(mi.FullTypeName + ", " + mi.Assembly.AssemblyName);
                                    System.Diagnostics.Debug.Assert(t != null, string.Format("Type '{0}, {1}' not found", mi.FullTypeName , mi.Assembly.AssemblyName));
                                    if (t == null) continue;

                                    MethodInfo clrMethod = t.GetMethod(mi.MemberName);
                                    System.Diagnostics.Debug.Assert(clrMethod != null, string.Format("CLR Method '{0}' not found", mi.MemberName));
                                    if (clrMethod == null) continue;

                                    EventInfo ei = Type.GetType(objType.FullNameServerDataObject).GetEvent(
                                        "On" + mi.Method.MethodName + "_" + mi.InvokeOnObjectClass.ClassName);
                                    System.Diagnostics.Debug.Assert(ei != null, string.Format("Event 'On{0}_{1}' not found", mi.Method.MethodName, mi.InvokeOnObjectClass.ClassName));
                                    if (ei == null) continue;

                                    InvokeInfo ii = new InvokeInfo();
                                    ii.CLRMethod = clrMethod;
                                    ii.Instance = Activator.CreateInstance(t);
                                    ii.CLREvent = ei;

                                    if (!customAction.ContainsKey(objType))
                                    {
                                        customAction.Add(objType, new List<InvokeInfo>());
                                    }
                                    customAction[objType].Add(ii);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.Assert(false, ex.ToString());
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                initialized = true;
            }
        }
    }
}
