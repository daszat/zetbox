
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
    using System.IO;

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
        Dictionary<string, MethodInfo> _reflectedMethods = new Dictionary<string, MethodInfo>();

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

                    ReflectMethods(ctx);
                    CreateInvokeInfosForObjectClasses(ctx);

                    foreach (var mi in _reflectedMethods.Values)
                    {
                        Log.Error(string.Format("Couldn't find any method for Invocation {0}.{1}", mi.DeclaringType.FullName, mi.Name));
                    }

                    Log.TraceTotalMemory("After BaseCustomActionsManager.Init()");
                }
                finally
                {
                    _initImpls[implType] = implType;
                }
            }
        }

        private void ReflectMethods(IReadOnlyKistlContext metaCtx)
        {
            if (metaCtx == null) { throw new ArgumentNullException("metaCtx"); }

            // Load all Implementor Types and Invocations
            foreach (var assembly in metaCtx.GetQuery<Kistl.App.Base.Assembly>())
            {
                var restr = assembly.DeploymentRestrictions;
                if (!_restrictor.IsAcceptableDeploymentRestriction((int)restr))
                {
                    continue;
                }

                System.Reflection.Assembly a;
                try
                {
                    a = System.Reflection.Assembly.Load(assembly.Name);
                }
                catch (FileLoadException)
                {
                    // Microsoft's Version
                    // don't care
                    continue;
                }
                catch (FileNotFoundException)
                {
                    // Mono's Version
                    // don't care
                    continue;
                }

                foreach (var t in a.GetTypes())
                {
                    if (t.GetCustomAttributes(typeof(Implementor), false).Length != 0)
                    {
                        if (!t.Name.EndsWith("Actions"))
                        {
                            Log.Warn(string.Format("Type {0} does not end with 'Actions'. Ignoring this type", t.FullName));
                            continue;
                        }

                        // Found Implementor Type
                        foreach (var m in t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                        {
                            if (m.GetCustomAttributes(typeof(Invocation), false).Length != 0)
                            {
                                var key = string.Format("{0}.{1}", t.FullName, m.Name);
                                _reflectedMethods[key] = m;
                            }
                            else
                            {
                                Log.Warn(string.Format("Found public method {0}.{1} which has no Invocation attribute. Ignoring this method", t.FullName, m.Name));
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Initializes caches for the provider of the given Context
        /// </summary>
        /// <param name="metaCtx">the context used to access the meta data</param>
        private void CreateInvokeInfosForObjectClasses(IReadOnlyKistlContext metaCtx)
        {
            if (metaCtx == null) { throw new ArgumentNullException("metaCtx"); }

            foreach (ObjectClass objClass in metaCtx.GetQuery<ObjectClass>())
            {
                try
                {
                    CreateInvokeInfosForAssembly(objClass);
                }
                catch (Exception ex)
                {
                    Log.Error("Exception in CreateInvokeInfosForObjectClasses", ex);
                    throw;
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

        #region Walk through objectclass and create InvocationInfos

        private void CreateInvokeInfosForAssembly(ObjectClass objClass)
        {
            // baseObjClass.GetDataType(); is not possible here, because this
            // Method is currently attaching
            var implTypeName = objClass.Module.Namespace
                + "."
                + objClass.Name
                + ExtraSuffix
                + ", " + ImplementationAssemblyName;
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

        private EventBasedMethodAttribute FindEventBasedMethodAttribute(Method method, Type implType)
        {
            Type[] paramTypes = method.Parameter
                    .Where(p => !p.IsReturnParameter)
                    .Select(p => p.GuessParameterType())
                    .ToArray();
            MethodInfo methodInfo = implType.FindMethod(method.Name, paramTypes);
            if (methodInfo == null)
            {
                Log.WarnFormat(
                    "Couldn't find method '{0}.{1}' with parameters: {2}\n",
                    method.ObjectClass.Name,
                    method.Name,
                    String.Join(", ", paramTypes.Select(t => t == null ? "null" : t.FullName).ToArray()));
                return null;
            }
            else
            {
                // May be null on Methods without events like server side invocatiaons or "embedded" methods
                return (EventBasedMethodAttribute)methodInfo.GetCustomAttributes(typeof(EventBasedMethodAttribute), false).SingleOrDefault();
            }
        }

        private void CreateInvokeInfos(ObjectClass objClass, Type implType)
        {
            if (implType == null) throw new ArgumentNullException("implType");
            if (objClass == null) throw new ArgumentNullException("objClass");


            // Reflected Methods
            // New style
            foreach (var method in GetAllMethods(objClass))
            {
                string key = string.Format("{0}.{1}Actions.{2}", objClass.Module.Namespace, objClass.Name, method.Name);
                if (_reflectedMethods.ContainsKey(key))
                {
                    var reflectedMethod = _reflectedMethods[key];

                    // May be null on Methods without events like server side invocatiaons or "embedded" methods
                    // or null if not found
                    var attr = FindEventBasedMethodAttribute(method, implType);
                    if (attr != null) CreateInvokeInfo(implType, reflectedMethod, attr.EventName);

                    _reflectedMethods.Remove(key);
                }
            }

            // Reflected Properties
            // New style
            foreach (Property prop in objClass.Properties)
            {
                CreatePropertyInvocations(implType, prop, PropertyInvocationType.Getter);
                CreatePropertyInvocations(implType, prop, PropertyInvocationType.PreSetter);
                CreatePropertyInvocations(implType, prop, PropertyInvocationType.PostSetter);
            }
        }

        private void CreatePropertyInvocations(Type implType, Property prop, PropertyInvocationType invocationType)
        {
            string methodPrefix;
            switch(invocationType)
            {
                case PropertyInvocationType.Getter:
                    methodPrefix = "get_";
                    break;
                case PropertyInvocationType.PreSetter:
                    methodPrefix = "preSet_";
                    break;
                case PropertyInvocationType.PostSetter:
                    methodPrefix = "postSet_";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("invocationType");
            }

            string key = string.Format("{0}.{1}Actions.{2}{3}", prop.ObjectClass.Module.Namespace, prop.ObjectClass.Name, methodPrefix, prop.Name);
            if (_reflectedMethods.ContainsKey(key))
            {
                var reflectedMethod = _reflectedMethods[key];
                CreateInvokeInfo(implType, reflectedMethod, "On" + prop.Name + "_" + invocationType);
                _reflectedMethods.Remove(key);
            }
        }

        private List<Method> GetAllMethods(ObjectClass objClass)
        {
            if (objClass != null)
            {
                var result = GetAllMethods(objClass.BaseObjectClass);
                objClass.Methods.ForEach<Method>(m => result.Add(m));
                return result;
            }
            else
            {
                return new List<Method>();
            }
        }

        private void CreateInvokeInfo(Type implType, MethodInfo clrMethod, string eventName)
        {
            try
            {
                Type t = clrMethod.DeclaringType;
                if (!t.IsStatic())
                {
                    // initialize t's handler
                    _container.Resolve(t);
                }

                EventInfo ei = implType.FindEvent(eventName);
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
