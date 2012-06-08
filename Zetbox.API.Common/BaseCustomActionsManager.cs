
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Autofac;
    using Autofac.Core.Registration;
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
        private readonly static object _initLock = new object();
        private readonly static Dictionary<Type, Type> _initImpls = new Dictionary<Type, Type>();

        private readonly IDeploymentRestrictor _restrictor;
        private readonly ILifetimeScope _container;

        private struct MethodKey
        {
            public MethodKey(string @namespace, string typeName, string methodName)
            {
                key = string.Format("{0}.{1}Actions.{2}", @namespace, typeName, methodName);
            }

            private string key;

            public override bool Equals(object obj)
            {
                if (obj is MethodKey)
                    return this.key.Equals(((MethodKey)obj).key);
                else
                    return false;
            }

            public override int GetHashCode()
            {
                return key.GetHashCode();
            }

            public override string ToString()
            {
                return key;
            }
        }

        Dictionary<MethodKey, List<MethodInfo>> _reflectedMethods = new Dictionary<MethodKey, List<MethodInfo>>();
        Dictionary<MethodKey, bool> _attachedMethods = new Dictionary<MethodKey, bool>();


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
                    Log.InfoFormat("Initializing Actions for [{0}] by [{1}]", ExtraSuffix, implType.Name);
                    Log.TraceTotalMemory("Before BaseCustomActionsManager.Init()");

                    ReflectMethods(ctx);
                    CreateInvokeInfosForObjectClasses(ctx);

                    foreach (var key in _reflectedMethods.Where(i => !_attachedMethods.ContainsKey(i.Key)).Select(i => i.Key))
                    {
                        Log.Warn(string.Format("Couldn't find any method for Invocation {0}", key));
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
                try
                {
                    var restr = assembly.DeploymentRestrictions;
                    if (!_restrictor.IsAcceptableDeploymentRestriction((int)restr))
                    {
                        continue;
                    }

                    System.Reflection.Assembly a;
                    a = System.Reflection.Assembly.Load(assembly.Name);

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
                                    var key = new MethodKey(t.Namespace, t.Name.Substring(0, t.Name.Length - "Actions".Length), m.Name);
                                    if (_reflectedMethods.ContainsKey(key))
                                    {
                                        _reflectedMethods[key].Add(m);
                                    }
                                    else
                                    {
                                        _reflectedMethods[key] = new List<MethodInfo>() { m };
                                    }
                                }
                                else if (m.GetCustomAttributes(typeof(Kistl.API.Constraint), false).Length != 0)
                                {
                                    // TODO: Check if Invoking Constraint is valid
                                }
                                else
                                {
                                    Log.Warn(string.Format("Found public method {0}.{1} which has no Invocation attribute. Ignoring this method", t.FullName, m.Name));
                                }
                            }
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    if (ex.LoaderExceptions.Count() == 1)
                    {
                        Log.Warn(String.Format("Failed to reflect over Assembly [{0}]. Ignoring and continuing.", assembly.Name), ex.LoaderExceptions.Single());
                    }
                    else
                    {
                        foreach (var lex in ex.LoaderExceptions)
                        {
                            Log.Warn(String.Format("Failed to reflect over Assembly [{0}]. Ignoring and continuing. Multiple Errors:", assembly.Name), lex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn(String.Format("Error while processing Assembly [{0}]. Ignoring and continuing.", assembly.Name), ex);
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

            foreach (var objClass in metaCtx.GetQuery<CompoundObject>())
            {
                try
                {
                    CreateInvokeInfosForAssembly(objClass);
                }
                catch (Exception ex)
                {
                    Log.Warn("Exception in CreateInvokeInfosForObjectClasses", ex);
                }
            }

            // put this in separate loop to avoid mono bug #701187
            // see https://bugzilla.novell.com/show_bug.cgi?id=701187
            foreach (var objClass in metaCtx.GetQuery<ObjectClass>())
            {
                try
                {
                    CreateInvokeInfosForAssembly(objClass);
                }
                catch (Exception ex)
                {
                    Log.Warn("Exception in CreateInvokeInfosForObjectClasses", ex);
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

        private void CreateInvokeInfosForAssembly(DataType objClass)
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
                Log.WarnFormat("Cannot find Type {0}\n", implTypeName);
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

        private EventBasedMethodAttribute FindEventBasedMethodAttribute(Method method, string execSuffix, Type implType)
        {
            var pi = implType.FindProperty(method.Name + execSuffix).SingleOrDefault();
            if (pi == null)
            {
                Log.WarnFormat(
                    "Couldn't find method '{0}.{1}'\n",
                    method.ObjectClass.Name,
                    method.Name + execSuffix);
                return null;
            }
            else
            {
                // May be null on Methods without events like server side invocatiaons or "embedded" methods
                return (EventBasedMethodAttribute)pi.GetCustomAttributes(typeof(EventBasedMethodAttribute), false).SingleOrDefault();
            }
        }

        private void CreateInvokeInfos(DataType dt, Type implType)
        {
            if (implType == null) throw new ArgumentNullException("implType");
            if (dt == null) throw new ArgumentNullException("dt");


            if (dt is ObjectClass)
            {
                var objClass = (ObjectClass)dt;
                // Reflected Methods
                // New style
                foreach (var method in GetAllMethods(objClass))
                {
                    foreach (var methodSuffix in new[] { string.Empty, "CanExec", "CanExecReason" })
                    {
                        var key = new MethodKey(objClass.Module.Namespace, objClass.Name, method.Name + methodSuffix);
                        if (_reflectedMethods.ContainsKey(key))
                        {
                            var methodInfos = _reflectedMethods[key];

                            // May be null on Methods without events like server side invocatiaons or "embedded" methods
                            // or null if not found
                            EventBasedMethodAttribute attr;
                            if(string.IsNullOrEmpty(methodSuffix))
                                attr = FindEventBasedMethodAttribute(method, implType); // The Method
                            else
                                attr = FindEventBasedMethodAttribute(method, methodSuffix, implType); // For can execute & reason
                            if (attr != null)
                            {
                                foreach (var mi in methodInfos)
                                {
                                    CreateInvokeInfo(implType, mi, attr.EventName);
                                }
                            }

                            _attachedMethods[key] = true;
                        }
                    }
                }
                CreateDefaultMethodInvocations(implType, dt, "NotifyPreSave");
                CreateDefaultMethodInvocations(implType, dt, "NotifyPostSave");
                CreateDefaultMethodInvocations(implType, dt, "NotifyCreated");
                CreateDefaultMethodInvocations(implType, dt, "NotifyDeleting");
            }

            CreateDefaultMethodInvocations(implType, dt, "ToString");
            CreateDefaultMethodInvocations(implType, dt, "ObjectIsValid");

            // Reflected Properties
            // New style
            foreach (Property prop in dt.Properties)
            {
                CreatePropertyInvocations(implType, prop, "get_", "Getter");
                CreatePropertyInvocations(implType, prop, "preSet_", "PreSetter");
                CreatePropertyInvocations(implType, prop, "postSet_", "PostSetter");
                CreatePropertyInvocations(implType, prop, "isValid_", "IsValid");
            }
        }

        private void CreateDefaultMethodInvocations(Type implType, DataType dt, string methodName)
        {
            var key = new MethodKey(dt.Module.Namespace, dt.Name, methodName);
            if (_reflectedMethods.ContainsKey(key))
            {
                var methodInfos = _reflectedMethods[key];
                foreach (var mi in methodInfos)
                {
                    CreateInvokeInfo(implType, mi, string.Format(CultureInfo.InvariantCulture, "On{0}_{1}", methodName, dt.Name));
                }
                _attachedMethods[key] = true;
            }
        }

        private void CreatePropertyInvocations(Type implType, Property prop, string methodPrefix, string invocationType)
        {
            var key = new MethodKey(prop.ObjectClass.Module.Namespace, prop.ObjectClass.Name, string.Format("{0}{1}", methodPrefix, prop.Name));
            if (_reflectedMethods.ContainsKey(key))
            {
                var methodInfos = _reflectedMethods[key];
                foreach (var mi in methodInfos)
                {
                    CreateInvokeInfo(implType, mi, string.Format(CultureInfo.InvariantCulture, "On{0}_{1}", prop.Name, invocationType));
                }
                _attachedMethods[key] = true;
            }
        }

        private List<Method> GetAllMethods(ObjectClass objClass)
        {
            if (objClass != null)
            {
                var result = GetAllMethods(((ObjectClass)objClass).BaseObjectClass);
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
                    Log.WarnFormat("CLR Event {0}.{1} not found", implType.FullName, eventName);
                    return;
                }

                AttachEvents(clrMethod, ei);
            }
            catch (ComponentNotRegisteredException ex)
            {
                Log.Error(string.Format("Non-static handler for {0} or its dependencies not registered with autofac. Continuing without business logic", clrMethod.DeclaringType.AssemblyQualifiedName), ex);
            }
            catch (Exception ex)
            {
                Log.Warn(string.Format("Exception in CreateInvokeInfo: type: {0}, method: {1} in {2}", implType.FullName, clrMethod, clrMethod.DeclaringType.Assembly.FullName), ex);
            }
        }
        #endregion
    }
}
