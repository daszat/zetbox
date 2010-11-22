
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

    using Autofac;

    /// <summary>
    /// A utility class implementing basic operations and caching needed by all CustomActionsManagers.
    /// </summary>
    public abstract class BaseCustomActionsManager
        : ICustomActionsManager
    {
        protected readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Common.BaseCustomActionsManager");
        private readonly static object _initLock = new object();
        private readonly static Dictionary<Type, Type> _initImpls = new Dictionary<Type, Type>();

        private readonly IDeploymentRestrictor _restrictor;
        private readonly ILifetimeScope _container;


        /// <summary>
        /// Gets or sets the extra suffix which is used to create the implementation class' name.
        /// </summary>
        protected string ExtraSuffix { get; private set; }

        protected string ImplementationAssemblyName { get; private set; }

        /// <summary>
        /// Initialises a new instance of the BaseCustomActionsManager class 
        /// using the specified extra suffix and the assembly of the actual type of this class.
        /// </summary>
        protected BaseCustomActionsManager(ILifetimeScope container, IDeploymentRestrictor restrictor, string extraSuffix)
        {
            if (restrictor == null) { throw new ArgumentNullException("restrictor"); }
            if (container == null) { throw new ArgumentNullException("container"); }

            _restrictor = restrictor;
            _container = container;
            ExtraSuffix = extraSuffix;
            ImplementationAssemblyName = this.GetType().Assembly.FullName;
        }

        /// <summary>
        /// Initializes this CustomActionsManager. This method is thread-safe and won't do
        /// anything if the ActionsManager is already initialized.
        /// </summary>
        public virtual void Init(IReadOnlyKistlContext ctx)
        {
            lock (_initLock)
            {
                var implType = this.GetType();
                if (_initImpls.ContainsKey(implType)) return;
                try
                {
                    Log.InfoFormat("Initialising Actions for [{0}] by [{1}]", ExtraSuffix, implType.Name);
                    Log.TraceTotalMemory("Before BaseCustomActionsManager.Init()");

                    // Init
                    CreateInvokeInfosForObjectClasses(ctx, ExtraSuffix, ImplementationAssemblyName);

                    Log.TraceTotalMemory("After BaseCustomActionsManager.Init()");
                }
                finally
                {
                    _initImpls[implType] = implType;
                }
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
        private void CreateInvokeInfosForObjectClasses(IReadOnlyKistlContext metaCtx, string extraSuffix, string assemblyName)
        {
            if (metaCtx == null) { throw new ArgumentNullException("metaCtx"); }

            foreach (ObjectClass objClass in metaCtx.GetQuery<ObjectClass>())
            {
                try
                {
                    CreateInvokeInfosForAssembly(objClass, extraSuffix, assemblyName);
                }
                catch (Exception ex)
                {
                    Log.Error("Exception in CreateInvokeInfosForObjectClasses", ex);
                    throw;
                }
            }
        }

        #region Walk through objectclass and create InvocationInfos

        private void CreateInvokeInfosForAssembly(ObjectClass objClass, string extraSuffix, string assemblyName)
        {
            // baseObjClass.GetDataType(); is not possible here, because this
            // Method is currently attaching
            var implTypeName = objClass.Module.Namespace
                + "."
                + objClass.Name
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
                foreach (var mi in baseObjClass.MethodInvocations)
                {
                    Type[] paramTypes = mi.Method.Parameter
                        .Where(p => !p.IsReturnParameter)
                        .Select(p => p.GuessParameterType())
                        .ToArray();
                    MethodInfo methodInfo = objType.FindMethod(mi.Method.Name, paramTypes);
                    if (methodInfo == null)
                    {
                        Log.WarnFormat(
                            "Couldn't find method '{0}.{1}' with parameters: {2}\n",
                            mi.InvokeOnObjectClass.Name,
                            mi.Method.Name,
                            String.Join(", ", paramTypes.Select(t => t == null ? "null" : t.FullName).ToArray()));
                    }
                    else
                    {
                        // May be null on Methods without events like server side invocations or "embedded" methods
                        var attr = (EventBasedMethodAttribute)methodInfo.GetCustomAttributes(typeof(EventBasedMethodAttribute), false).SingleOrDefault();
                        if(attr != null) CreateInvokeInfo(objType, mi, attr.EventName);
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
                if (!_restrictor.IsAcceptableDeploymentRestriction((int)restr))
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
                    // Changed -> init singleton
                    // Log.ErrorFormat("Type {0}, {1} is not static", invoke.Implementor.FullName, invoke.Implementor.Assembly.Name);
                    // return;
                    var tmp = _container.Resolve(t);
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
}
