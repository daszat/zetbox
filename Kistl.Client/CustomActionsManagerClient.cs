using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using System.Reflection;

namespace Kistl.Client
{
    /// <summary>
    /// Implementierung des Clientseitigen CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerClient : API.ICustomActionsManager
    {
        private class InvokeInfo
        {
            public MethodInfo CLRMethod {get; set;}
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
        /// </summary>
        /// <param name="obj"></param>
        public void AttachEvents(IDataObject obj)
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

        /// <summary>
        /// Load Metadata, create an Instance and save
        /// Note: The Assembly type is loaded, but _without_ actions!
        /// </summary>
        public void Init()
        {
            try
            {
                using (KistlContext ctx = new KistlContext())
                {
                    foreach (ObjectClass baseObjClass in Helper.ObjectClasses.Values)
                    {
                        ObjectType objType = new ObjectType(baseObjClass.Module.Namespace, baseObjClass.ClassName);
                        foreach (ObjectClass objClass in Helper.GetObjectHierarchie(baseObjClass))
                        {
                            foreach (MethodInvocation mi in objClass.MethodIvokations)
                            {
                                try
                                {
                                    if (!mi.Assembly.IsClientAssembly) continue;

                                    Type t = Type.GetType(mi.FullTypeName + ", " + mi.Assembly.AssemblyName);
                                    if (t == null) continue;

                                    MethodInfo clrMethod = t.GetMethod(mi.MemberName);
                                    if (clrMethod == null) continue;

                                    EventInfo ei = Type.GetType(objType.FullNameClientDataObject).GetEvent(
                                        "On" + mi.Method.MethodName + "_" + mi.InvokeOnObjectClass.ClassName);

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
                                    System.Diagnostics.Trace.TraceError(ex.ToString());
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
