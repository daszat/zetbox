
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.API.Utils;

    /// <summary>
    /// A utility class implementing basic operations and caching needed by all CustomActionsManagers.
    /// </summary>
    public abstract class BaseCustomActionsManager
        : ICustomActionsManager
    {
        /// <summary>
        /// Gets or sets a value indicating that initializing is done
        /// </summary>
        protected InvocationCache Cache { get; set; }

        /// <summary>
        /// Gets or sets the extra suffix which is used to create the implementation class' name.
        /// </summary>
        protected string ExtraSuffix { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly containing the implementation classes.
        /// </summary>
        protected string ImplementationAssembly { get; set; }

        /// <summary>
        /// Initialises a new instance of the BaseCustomActionsManager class 
        /// using the specified extra suffix and implementation assembly name.
        /// </summary>
        /// <param name="extraSuffix"></param>
        /// <param name="implementationAssembly"></param>
        protected BaseCustomActionsManager(string extraSuffix, string implementationAssembly)
        {
            ExtraSuffix = extraSuffix;
            ImplementationAssembly = implementationAssembly;
        }

        /// <summary>
        /// Initializes this CustomActionsManager
        /// </summary>
        public virtual void Init(IKistlContext ctx)
        {
            if (Cache != null) { return; }
            var warnings = new StringBuilder();
            Cache = new InvocationCache(IsAcceptableDeploymentRestriction);
            Cache.InitializeCache(ctx, ExtraSuffix, ImplementationAssembly, ObjectClassFilter, warnings);
            // TODO create and use Logging API in the cache to invert control here and remove clumsy ProcessWarnings calls
            if (warnings.Length > 0)
            {
                ProcessWarnings(warnings.ToString());
            }
        }

        /// <summary>
        /// Attach using Metadata
        /// Detaching is done through the Garbage Collector
        /// see Unsubscribing at http://msdn2.microsoft.com/en-us/library/ms366768.aspx
        /// </summary>
        /// <param name="obj">the object on which to attach events</param>
        public virtual void AttachEvents(IDataObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            // defer attaching if there is no cache
            if (Cache == null) { return; }

            var key = obj.GetType();

            // New Method
            foreach (InvokeInfo ii in Cache.Lookup(key))
            {
                // TODO: Fix Case 316
                Delegate newDelegate = Delegate.CreateDelegate(
                    ii.CLREvent.EventHandlerType,
                    ii.Instance,
                    ii.CLRMethod);

                ii.CLREvent.AddEventHandler(obj, newDelegate);
            }
        }

        /// <summary>
        /// This method is called to let the implementor customize processing of warnings.
        /// </summary>
        /// <param name="warnings">a human readable string containing the warnings</param>
        protected virtual void ProcessWarnings(string warnings)
        {
            Logging.Log.Warn(warnings);
        }

        /// <summary>
        /// Override this method to modify the acceptable DeploymentRestrictions. By default only None is accepted.
        /// </summary>
        /// <param name="r">the restriction to check (This parameter is int because <see cref="DeploymentRestriction"/> might not yet be loaded)</param>
        /// <returns>whether or not the given deployment restriction is acceptable for the implementor</returns>
        protected abstract bool IsAcceptableDeploymentRestriction(int r);

        /// <summary>
        /// Retruns true on ObjectClasses that should be managed
        /// </summary>
        /// <param name="cls"></param>
        /// <returns>true</returns>
        protected virtual bool ObjectClassFilter(ObjectClass cls) { return true; }
    }

    /// <summary>
    /// A CustomActionsManager for the FrozenContext.
    /// </summary>
    public abstract class FrozenActionsManager
        : BaseCustomActionsManager
    {
        /// <summary>
        /// Gets a value indicating whether the frozen actions are already initialised.
        /// </summary>
        public static bool IsInitialised { get; private set; }

        static FrozenActionsManager()
        {
            IsInitialised = false;
        }

        /// <summary>
        /// Initialises a new instane of the FrozenActionsManager.
        /// </summary>
        protected FrozenActionsManager()
            : base("Frozen", "Kistl.Objects.Frozen")
        {
        }

        /// <inheritdoc/>
        public override void Init(IKistlContext ctx)
        {
            if (!IsInitialised)
            {
                try
                {
                    base.Init(ctx);
                }
                finally
                {
                    // try only once
                    IsInitialised = true;
                }

                try
                {
                    // attach all frozen objects
                    foreach (IDataObject obj in FrozenContext.Single.AttachedObjects.OfType<IDataObject>())
                    {
                        AttachEvents(obj);
                    }
                }
                finally
                {
                    // drop cache
                    Cache = null;
                }
            }
        }

        public override void AttachEvents(IDataObject obj)
        {
            if (IsInitialised)
            {
                base.AttachEvents(obj);
            }
        }

        /// <inheritdoc/>
        protected override bool ObjectClassFilter(ObjectClass cls)
        {
            return cls.IsFrozen();
        }
    }

    public sealed class InvocationCache
    {
        public InvocationCache(Func<int, bool> isAcceptableDeploymentRestriction)
        {
            this.IsAcceptableDeploymentRestriction = isAcceptableDeploymentRestriction;
        }

        /// <summary>
        /// A filter for <see cref="DeploymentRestriction"/>s on Implementors
        /// </summary>
        private Func<int, bool> IsAcceptableDeploymentRestriction;

        /// <summary>
        /// List of Custom Actions
        /// </summary>
        private Dictionary<Type, List<InvokeInfo>> _cache = new Dictionary<Type, List<InvokeInfo>>();

        /// <summary>
        /// An empty list of InvokeInfos
        /// </summary>
        private static ReadOnlyCollection<InvokeInfo> Empty = new List<InvokeInfo>().AsReadOnly();

        /// <summary>
        /// Returns the list of InvokeInfos for the given Type or an empty list if the Type is not cached.
        /// </summary>
        /// <param name="t">the type to lookup</param>
        /// <returns></returns>
        public ReadOnlyCollection<InvokeInfo> Lookup(Type t)
        {
            if (_cache.ContainsKey(t))
            {
                return _cache[t].AsReadOnly();
            }
            else
            {
                return Empty;
            }
        }

        /// <summary>
        /// Initializes caches for the provider of the given Context
        /// </summary>
        /// <param name="metaCtx">the context used to access the meta data</param>
        /// <param name="extraSuffix">an extra suffix to put into the created implementation class names</param>
        /// <param name="assemblyName">the name of the assembly to load implementation classes from</param>
        public void InitializeCache(IKistlContext metaCtx, string extraSuffix, string assemblyName, Func<ObjectClass, bool> filter, StringBuilder warnings)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            foreach (ObjectClass baseObjClass in metaCtx.GetQuery<ObjectClass>())
            {
                try
                {
                    if (filter == null || filter(baseObjClass))
                    {
                        CreateInvokeInfosForAssembly(warnings, baseObjClass, extraSuffix, assemblyName);
                    }
                }
                catch (Exception ex)
                {
                    warnings.AppendLine(ex.Message);
                }
            }
        }

        #region Walk through objectclass and create InvocationInfos

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

                if (!_cache.ContainsKey(objType))
                {
                    _cache.Add(objType, new List<InvokeInfo>());
                }
                _cache[objType].Add(ii);
            }
            catch (Exception ex)
            {
                warnings.AppendLine(ex.Message);
            }
        }

        #endregion

    }

    /// <summary>
    /// A small container holding necessary infos for caching Invokations
    /// </summary>
    [DebuggerDisplay("Invoke {CLRMethod} on {Instance} using {CLREvent}")]
    public class InvokeInfo
    {
        public MethodInfo CLRMethod { get; set; }
        public object Instance { get; set; }
        public EventInfo CLREvent { get; set; }
    }
}
