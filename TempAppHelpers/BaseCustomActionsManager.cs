
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    /// <summary>
    /// A utility class implementing basic operations and caching needed by all CustomActionsManagers.
    /// </summary>
    public abstract class BaseCustomActionsManager
        : ICustomActionsManager
    {
        protected readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Common.BaseCustomActionsManager");

        protected bool initialized = false;

        /// <summary>
        /// Gets or sets the extra suffix which is used to create the implementation class' name.
        /// </summary>
        protected string ExtraSuffix { get; set; }

        /// <summary>
        /// Gets or sets the name of the assembly containing the implementation classes.
        /// </summary>
        protected string ImplementationAssembly { get; set; }

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
        public virtual void Init(IReadOnlyKistlContext ctx)
        {
            if (initialized) return;
            try
            {
                Log.TraceTotalMemory("Before BaseCustomActionsManager.Init()");

                // Init
                CreateInvokeInfosForObjectClasses(ctx, ExtraSuffix, ImplementationAssembly, ObjectClassFilter);

                Log.TraceTotalMemory("After BaseCustomActionsManager.Init()");
            }
            finally
            {
                initialized = true;
            }
        }

        /// <summary>
        /// Attach static events using Metadata
        /// </summary>
        protected virtual void AttachEvents(MethodInfo clrMethod, EventInfo clrEvent)
        {
            if (clrMethod == null) { throw new ArgumentNullException("clrMethod"); }
            if (clrEvent == null) { throw new ArgumentNullException("clrEvent"); }

            Delegate newDelegate = Delegate.CreateDelegate(
                clrEvent.EventHandlerType,
                clrMethod);

            clrEvent.AddEventHandler(null, newDelegate);
        }

        /// <summary>
        /// Initializes caches for the provider of the given Context
        /// </summary>
        /// <param name="metaCtx">the context used to access the meta data</param>
        /// <param name="extraSuffix">an extra suffix to put into the created implementation class names</param>
        /// <param name="assemblyName">the name of the assembly to load implementation classes from</param>
        private void CreateInvokeInfosForObjectClasses(IReadOnlyKistlContext metaCtx, string extraSuffix, string assemblyName, Func<ObjectClass, bool> filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }
            if (metaCtx == null) { throw new ArgumentNullException("metaCtx"); }

            foreach (ObjectClass objClass in metaCtx.GetQuery<ObjectClass>())
            {
                try
                {
                    if (filter == null || filter(objClass))
                    {
                        CreateInvokeInfosForAssembly(objClass, extraSuffix, assemblyName);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Exception in CreateInvokeInfosForObjectClasses", ex);
                }
            }
        }

        #region Walk through objectclass and create InvocationInfos

        private void CreateInvokeInfosForAssembly(ObjectClass objClass, string extraSuffix, string assemblyName)
        {
            // baseObjClass.GetDataType(); is not possible here, because this
            // Method is currently attaching
            var implTypeName = objClass.Module.Namespace
                + "." + objClass.Name
                + Kistl.API.Helper.ImplementationSuffix
                + extraSuffix
                + ", " + assemblyName;
            var implType = Type.GetType(implTypeName);
            if (implType != null)
            {
                CreateInvokeInfos(objClass, implType);
            }
            else
            {
                Log.ErrorFormat("Cannot find Type {0}\n", implTypeName);
            }
        }

        private void CreateInvokeInfos(ObjectClass objObjClass, Type objType)
        {
            if (objType == null)
            {
                throw new ArgumentNullException("objType");
            }

            foreach (ObjectClass baseObjClass in objObjClass.GetObjectHierarchie())
            {
                // Method invocations
                foreach (MethodInvocation mi in baseObjClass.MethodInvocations)
                {
                    Type[] paramTypes = mi.Method.Parameter
                        .Where(p => !p.IsReturnParameter)
                        .Select(p => p.GuessParameterType())
                        .ToArray();
                    MethodInfo methodInfo = objType.FindMethod(mi.Method.MethodName, paramTypes);
                    if (methodInfo == null)
                    {
                        Log.WarnFormat(
                            "Couldn't find method '{0}.{1}' with parameters: {2}\n",
                            mi.InvokeOnObjectClass.Name,
                            mi.Method.MethodName,
                            String.Join(", ", paramTypes.Select(t => t == null ? "null" : t.FullName).ToArray()));
                    }
                    else
                    {
                        var attr = (EventBasedMethodAttribute)methodInfo.GetCustomAttributes(typeof(EventBasedMethodAttribute), false).Single();
                        CreateInvokeInfo(objType, mi, attr.EventName);
                    }
                }
            }
            // PropertyInvocations
            foreach (Property prop in objObjClass.Properties)
            {
                foreach (PropertyInvocation pi in prop.Invocations)
                {
                    CreateInvokeInfo(objType, pi, "On" + prop.Name + "_" + pi.InvocationType);
                }
            }
        }

        private void CreateInvokeInfo(Type objType, IInvocation invoke, string eventName)
        {
            try
            {
                var restr = invoke.Implementor.Assembly.DeploymentRestrictions;
                if (!IsAcceptableDeploymentRestriction((int)restr))
                {
                    return;
                }

                // as noted above, no methods are 
                // attached yet, so TypeRef.AsType() 
                // and TypeRef.ToString() would be 
                // nice, but aren't available yet.
                Type t = Type.GetType(invoke.Implementor.FullName + ", " + invoke.Implementor.Assembly.Name);
                if (t == null)
                {
                    Log.ErrorFormat("Type {0}, {1} not found", invoke.Implementor.FullName, invoke.Implementor.Assembly.Name);
                    return;
                }

                if (!t.IsStatic())
                {
                    Log.ErrorFormat("Type {0}, {1} is not static", invoke.Implementor.FullName, invoke.Implementor.Assembly.Name);
                    return;
                }

                MethodInfo clrMethod = t.GetMethod(invoke.MemberName);
                if (clrMethod == null)
                {
                    Log.ErrorFormat("CLR Method {0} not found", invoke.MemberName);
                    return;
                }

                EventInfo ei = objType.FindEvent(eventName);
                if (ei == null)
                {
                    Log.ErrorFormat("CLR Event {0} not found", eventName);
                    return;
                }

                AttachEvents(clrMethod, ei);
            }
            catch (Exception ex)
            {
                Log.Error("Exception in CreateInvokeInfo", ex);
            }
        }

        #endregion
    }

    #region FrozenActionsManager
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
        public override void Init(IReadOnlyKistlContext ctx)
        {
            if (!IsInitialised)
            {
                base.Init(ctx);
                // try only once
                IsInitialised = true;
            }
        }

        /// <inheritdoc/>
        protected override bool ObjectClassFilter(ObjectClass cls)
        {
            return cls.IsFrozen();
        }
    }
    #endregion

}
