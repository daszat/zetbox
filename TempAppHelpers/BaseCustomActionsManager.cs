
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// A utility class implementing basic operations and caching needed by all CustomActionsManagers.
    /// </summary>
    public abstract class BaseCustomActionsManager
        : ICustomActionsManager
    {
        /// <summary>
        /// A small container holding necessary infos for caching Invokations
        /// </summary>
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
        /// <param name="obj">the object on which to attach events</param>
        public void AttachEvents(IDataObject obj)
        {
            if (!initialized)
            {
                return;
            }

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
        /// Initializes this CustomActionsManager
        /// </summary>
        public void Init()
        {
            try
            {
                InitCore();
            }
            finally
            {
                initialized = true;
            }

            InitFrozen();
        }

        /// <summary>
        /// By default, intializes the Provider using the ImplementationAssembly, no extra suffix and the FrozenContext as meta store.
        /// Override this to specialize intialization.
        /// </summary>
        protected virtual void InitCore()
        {
            InitializeProvider(FrozenContext.Single, String.Empty, ApplicationContext.Current.ImplementationAssembly);
        }

        /// <summary>
        /// Override this to modify behaviour for the frozen context.
        /// </summary>
        /// This is called after initializing everything else
        protected virtual void InitFrozen()
        {
            foreach (IDataObject obj in FrozenContext.Single.AttachedObjects.OfType<IDataObject>())
            {
                AttachEvents(obj);
            }
        }

        /// <summary>
        /// Initializes caches for the provider of the given Context
        /// </summary>
        /// <param name="metaCtx">the context used to access the meta data</param>
        /// <param name="extraSuffix">an extra suffix to put into the created implementation class names</param>
        /// <param name="assemblyName">the name of the assembly to load implementation classes from</param>
        protected void InitializeProvider(IKistlContext metaCtx, string extraSuffix, string assemblyName)
        {
            // some shortcuts
            var classes = metaCtx.GetQuery<ObjectClass>();

            StringBuilder warnings = new StringBuilder();

            foreach (ObjectClass baseObjClass in classes)
            {
                try
                {
                    CreateInvokeInfosForAssembly(warnings, baseObjClass, extraSuffix, assemblyName);
                    if (baseObjClass.IsFrozen())
                    {
                        CreateInvokeInfosForAssembly(warnings, baseObjClass, "Frozen", "Kistl.Objects.Frozen");
                    }
                }
                catch (Exception ex)
                {
                    warnings.AppendLine(ex.Message);
                }
            }

            // TODO create and use Logging API to invert control here
            if (warnings.Length > 0)
            {
                ProcessWarnings(warnings.ToString());
            }
        }

        /// <summary>
        /// This method is called to let the implementor customize processing of warnings.
        /// </summary>
        /// <param name="warnings">a human readable string containing the warnings</param>
        protected virtual void ProcessWarnings(string warnings)
        {
            // do nothing
        }

        ///// <summary>
        ///// Override this method to modify the acceptable DeploymentRestrictions. By default only None is accepted.
        ///// </summary>
        ///// <param name="r">the restriction to check</param>
        ///// <returns>whether or not the given deployment restriction is acceptable for the implementor</returns>
        //protected virtual bool IsAcceptableDeploymentRestriction(DeploymentRestriction r)
        //{
        //    return r == DeploymentRestriction.None;
        //}

        protected abstract bool IsAcceptableDeploymentRestriction(int r);

        private void CreateInvokeInfosForAssembly(StringBuilder warnings, ObjectClass baseObjClass, string extraSuffix, string assemblyName)
        {
            // baseObjClass.GetDataType(); is not possible here, because this
            // Method is currently attaching
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
            {
                throw new ArgumentNullException("objType");
            }

            foreach (ObjectClass objClass in baseObjClass.GetObjectHierarchie())
            {
                foreach (MethodInvocation mi in objClass.MethodInvocations)
                {
                    Type[] paramTypes = mi.Method.Parameter
                        .Where(p => !p.IsReturnParameter)
                        .Select(p => p.GuessParameterType())
                        .ToArray();
                    MethodInfo methodInfo = objType.FindMethod(mi.Method.MethodName, paramTypes);
                    if (methodInfo == null)
                    {
                        warnings.AppendFormat(
                            "Couldn't find method '{0}.{1}' with parameters: {2}\n",
                            mi.InvokeOnObjectClass.ClassName,
                            mi.Method.MethodName,
                            String.Join(", ", paramTypes.Select(t => t == null ? "null" : t.FullName).ToArray()));
                        methodInfo = objType.FindMethod("Notify" + mi.Method.MethodName, paramTypes);
                    }
                    else
                    {
                        var attr = (EventBasedMethodAttribute)methodInfo.GetCustomAttributes(typeof(EventBasedMethodAttribute), false).Single();
                        CreateInvokeInfo(warnings, objType, mi, attr.EventName);
                    }
                }

                foreach (Property prop in objClass.Properties)
                {
                    foreach (PropertyInvocation pi in prop.Invocations)
                    {
                        CreateInvokeInfo(warnings, objType, pi, "On" + prop.PropertyName + "_" + pi.InvocationType);
                    }
                }
            }
        }

        private void CreateInvokeInfo(StringBuilder warnings, Type objType, IInvocation invoke, string eventName)
        {
            try
            {
                var restr = invoke.Implementor.Assembly.DeploymentRestrictions ?? DeploymentRestriction.None;
                if (!IsAcceptableDeploymentRestriction((int)restr))
                {
                    return;
                }

                // as noted above, no methods are 
                // attached yet, so TypeRef.AsType() 
                // and TypeRef.ToString() would be 
                // nice, but aren't available yet.
                Type t = Type.GetType(invoke.Implementor.FullName + ", " + invoke.Implementor.Assembly.AssemblyName);
                if (t == null)
                {
                    warnings.AppendLine(string.Format("Warning: Type {0}, {1} not found", invoke.Implementor.FullName, invoke.Implementor.Assembly.AssemblyName));
                    return;
                }

                MethodInfo clrMethod = t.GetMethod(invoke.MemberName);
                if (clrMethod == null)
                {
                    warnings.AppendLine(string.Format("Warning: CLR Method {0} not found", invoke.MemberName));
                    return;
                }

                EventInfo ei = objType.GetEvent(eventName);

                if (ei == null)
                {
                    warnings.AppendLine(string.Format("Warning: CLR Event {0} not found", eventName));
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
