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
        private Dictionary<Type, List<InvokeInfo>> customAction = new Dictionary<Type, List<InvokeInfo>>();

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
            if (customAction.ContainsKey(obj.GetType()))
            {
                foreach (InvokeInfo ii in customAction[obj.GetType()])
                {
                    // TODO: Fix Case 316
                    try
                    {
                        Delegate d = Delegate.CreateDelegate(ii.CLREvent.EventHandlerType, ii.Instance, ii.CLRMethod);
                        ii.CLREvent.AddEventHandler(obj, d);
                    }
                    catch (Exception ex) { 
                        // HACK
                        // TODO : Fix Case 316 properly!
                    }
                }
            }
        }

        public void Init()
        {
            try
            {
                using (IKistlContext ctx = KistlDataContext.GetContext())
                {
                    StringBuilder warnings = new StringBuilder();

                    foreach (ObjectClass baseObjClass in ctx.GetQuery<ObjectClass>())
                    {
                        try
                        {
                            // baseObjClass.GetDataType(); is not possible here, because this
                            // Method is currently attaching
                            Type objType = Type.GetType(baseObjClass.Module.Namespace + "." + baseObjClass.ClassName + "Impl, Kistl.Objects.Server");
                            if (objType == null)
                            {
                                warnings.AppendLine(string.Format("DataType '{0}, Kistl.Objects.Server' not found", baseObjClass.Module.Namespace + "." + baseObjClass.ClassName));
                                continue;
                            }
                            foreach (ObjectClass objClass in baseObjClass.GetObjectHierarchie())
                            {
                                foreach (MethodInvocation mi in objClass.MethodIvokations)
                                {
                                    try
                                    {
                                        if (mi.Assembly.IsClientAssembly) continue;

                                        Type t = Type.GetType(mi.FullTypeName + ", " + mi.Assembly.AssemblyName);
                                        if (t == null)
                                        {
                                            warnings.AppendLine(string.Format("Type '{0}, {1}' not found", mi.FullTypeName, mi.Assembly.AssemblyName));
                                            continue;
                                        }

                                        MethodInfo clrMethod = t.GetMethod(mi.MemberName);
                                        if (clrMethod == null)
                                        {
                                            warnings.AppendLine(string.Format("CLR Method '{0}' not found", mi.MemberName));
                                            continue;
                                        }

                                        EventInfo ei = objType.GetEvent(
                                            "On" + mi.Method.MethodName + "_" + mi.InvokeOnObjectClass.ClassName);
                                        if (ei == null)
                                        {
                                            warnings.AppendLine(string.Format("Event 'On{0}_{1}' not found", mi.Method.MethodName, mi.InvokeOnObjectClass.ClassName));
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
                                        warnings.AppendLine(ex.Message);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Can happen if Type is not yet compiled into Assembly
                            warnings.AppendLine(ex.Message);
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
