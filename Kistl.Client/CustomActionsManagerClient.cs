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

            var key = obj.GetType();

            // New Method
            if (customAction.ContainsKey(key))
            {
                foreach (InvokeInfo ii in customAction[key])
                {
                    // TODO: Fix Case 316
                    Delegate newDelegate = Delegate.CreateDelegate(
                        ii.CLREvent.EventHandlerType,
                        ii.Instance,
                        ii.CLRMethod);

                    ii.CLREvent.AddEventHandler(obj, newDelegate);
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
                    using (IKistlContext ctx = Kistl.API.FrozenContext.Single)
                    //using (IKistlContext ctx = KistlContext.GetContext())
                    {

                        // some shortcuts
                        var modules = ctx.GetQuery<Kistl.App.Base.Module>();
                        var classes = ctx.GetQuery<ObjectClass>();
                        var methods = ctx.GetQuery<Method>();
                        var assemblies = ctx.GetQuery<Kistl.App.Base.Assembly>();

                        StringBuilder warnings = new StringBuilder();

                        foreach (ObjectClass baseObjClass in classes)
                        {
                            try
                            {
                                // baseObjClass.GetDataType(); is not possible here, because this
                                // Method is currently attaching
                                CreateInvokeInfosForAssembly(warnings, baseObjClass, "", ApplicationContext.Current.ImplementationAssembly);
                                if (baseObjClass.IsFrozenObject)
                                    CreateInvokeInfosForAssembly(warnings, baseObjClass, "Frozen", "Kistl.Objects.Frozen");
                            }
                            catch (Exception ex)
                            {
                                warnings.AppendLine(ex.Message);
                            }
                        }

                        // TODO create and use Logging API to invert control here
                        if (warnings.Length > 0)
                        {
                            System.Diagnostics.Debug.WriteLine(warnings.ToString());
                            if (GuiApplicationContext.Current.Renderer != null)
                            {
                                GuiApplicationContext.Current.Renderer.ShowMessage(warnings.ToString());
                            }
                        }
                    }
                }
            }
            finally
            {
                initialized = true;
                // Clean up Helper Caches
                //ClientHelper.CleanCaches();
            }

            // Attach Methods to objects in FrozenContext
            foreach (IDataObject obj in FrozenContext.Single.AttachedObjects.OfType<IDataObject>())
            {
                AttachEvents(obj);
            }
        }

        private void CreateInvokeInfosForAssembly(StringBuilder warnings, ObjectClass baseObjClass, string extraSuffix, string assemblyName)
        {
            var implTypeName = baseObjClass.Module.Namespace
                + "." + baseObjClass.ClassName
                + Kistl.API.Helper.ImplementationSuffix
                + extraSuffix
                + ", " + assemblyName;
            var implType = Type.GetType(implTypeName);
            if (implType != null)
            {
                CreateInvokeInfos(warnings, baseObjClass, implType);
            }
            else
            {
                warnings.AppendFormat("Cannot find Type {0}\n", implTypeName);
            }
        }

        private void CreateInvokeInfos(StringBuilder warnings, ObjectClass baseObjClass, Type objType)
        {
            if (objType == null)
                throw new ArgumentNullException("objType");

            foreach (ObjectClass objClass in baseObjClass.GetObjectHierarchie())
            {
                foreach (MethodInvocation mi in objClass.MethodInvocations)
                {
                    try
                    {
                        if (!mi.Implementor.Assembly.IsClientAssembly) continue;

                        // as noted above, no methods are 
                        // attached yet, so TypeRef.AsType() 
                        // and TypeRef.ToString() would be 
                        // nice, but aren't available yet.
                        Type t = Type.GetType(mi.Implementor.FullName + ", " + mi.Implementor.Assembly.AssemblyName);
                        if (t == null)
                        {
                            warnings.AppendLine(string.Format("Warning: Type {0}, {1} not found", mi.Implementor.FullName, mi.Implementor.Assembly.AssemblyName));
                            return;
                        }

                        MethodInfo clrMethod = t.GetMethod(mi.MemberName);
                        if (clrMethod == null)
                        {
                            warnings.AppendLine(string.Format("Warning: CLR Method {0} not found", mi.MemberName));
                            return;
                        }

                        EventInfo ei = objType.GetEvent(
                            "On" + mi.Method.MethodName + "_" + mi.InvokeOnObjectClass.ClassName);

                        if (ei == null)
                        {
                            warnings.AppendLine(string.Format("Warning: CLR Event On{0}_{1} not found", mi.Method.MethodName, mi.InvokeOnObjectClass.ClassName));
                            return;
                        }

                        InvokeInfo ii = new InvokeInfo()
                        {
                            CLRMethod = clrMethod,
                            Instance = Activator.CreateInstance(t),
                            CLREvent = ei
                        };

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
    }
}
