using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;

namespace Kistl.Client
{
    /// <summary>
    /// Implementation of the client-side CustomActionsManager
    /// TODO: due to interface dependencies, this cannot go to either the App.Base or the API.Client;
    ///       still, this shouldn't be left to the actual client to be implemented.
    /// </summary>
    internal class CustomActionsManagerClient : API.ICustomActionsManager
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
        private Dictionary<Type, List<InvokeInfo>> customAction = new Dictionary<Type, List<InvokeInfo>>();

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
            if (customAction.ContainsKey(obj.GetType()))
            {
                foreach (InvokeInfo ii in customAction[obj.GetType()])
                {
                    // TODO: Fix Case 316
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
                using (TraceClient.TraceHelper.TraceMethodCall())
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        /// Prepare Methods
                        List<Method> mList = ctx.GetQuery<Method>().ToList();

                        /// Prepare Assemblies
                        List<Kistl.App.Base.Assembly> aList = ctx.GetQuery<Kistl.App.Base.Assembly>().ToList();

                        StringBuilder warnings = new StringBuilder();

                        foreach (ObjectClass baseObjClass in ClientHelper.ObjectClasses.Values)
                        {
                            Type objType = baseObjClass.GetDataCLRType();
                            foreach (ObjectClass objClass in baseObjClass.GetObjectHierarchie())
                            {
                                foreach (MethodInvocation mi in objClass.MethodIvokations)
                                {
                                    try
                                    {
                                        if (!mi.Assembly.IsClientAssembly) continue;

                                        Type t = Type.GetType(mi.FullTypeName + ", " + mi.Assembly.AssemblyName);
                                        if (t == null)
                                        {
                                            warnings.AppendLine(string.Format("Warning: Type {0}, {1} not found", mi.FullTypeName, mi.Assembly.AssemblyName));
                                            continue;
                                        }

                                        MethodInfo clrMethod = t.GetMethod(mi.MemberName);
                                        if (clrMethod == null)
                                        {
                                            warnings.AppendLine(string.Format("Warning: CLR Method {0} not found", mi.MemberName));
                                            continue;
                                        }

                                        EventInfo ei = objType.GetEvent(
                                            "On" + mi.Method.MethodName + "_" + mi.InvokeOnObjectClass.ClassName);

                                        if (ei == null)
                                        {
                                            warnings.AppendLine(string.Format("Warning: CLR Event On{0}_{1} not found", mi.Method.MethodName, mi.InvokeOnObjectClass.ClassName));
                                            continue;
                                        }

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
                                        ClientHelper.HandleError(ex);
                                        return;
                                    }
                                }
                            }
                        }

                        if (warnings.Length > 0)
                        {
                            Manager.Renderer.ShowMessage(warnings.ToString());
                        }
                    }
                }
            }
            finally
            {
                initialized = true;
                // Clean up Helper Caches
                ClientHelper.CleanCaches();
            }
        }
    }
}
