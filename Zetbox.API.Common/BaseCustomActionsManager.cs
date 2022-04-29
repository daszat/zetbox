// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Core.Registration;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    /// <summary>
    /// A utility class implementing basic operations and caching needed by all CustomActionsManagers.
    /// </summary>
    public abstract class BaseCustomActionsManager
        : ICustomActionsManager
    {
        protected readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(BaseCustomActionsManager));

        private readonly List<ImplementorAssembly> _assemblies;
        private readonly ILifetimeScope _container;

        private struct MethodKey
        {
            public MethodKey(string @namespace, string typeName, string methodName, Type[] paramTypes)
            {
                key = string.Format("{0}.{1}Actions.{2}({3})", @namespace, typeName, methodName, string.Join(", ", paramTypes.Select(t => t.FullName)));
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

        private readonly Dictionary<MethodKey, List<MethodInfo>> _reflectedMethods = new Dictionary<MethodKey, List<MethodInfo>>();
        private readonly Dictionary<MethodKey, bool> _attachedMethods = new Dictionary<MethodKey, bool>();
        private readonly IAssetsManager _assetsMgr;

        /// <summary>
        /// This provides a per-dalProvider synchronisation root to protect the initialisation
        /// </summary>
        protected abstract object SyncRoot { get; }
        /// <summary>
        /// This returns per DalProvider whether the custom actions are already initialised. It is set by the Init() function. Only access it while holding the SyncRoot lock.
        /// </summary>
        protected abstract bool IsInitialised { get; set; }

        /// <summary>
        /// Gets or sets the extra suffix which is used to create the implementation class' name.
        /// </summary>
        protected string ExtraSuffix { get; private set; }

        protected string ImplementationAssemblyName { get; private set; }

        protected ZetboxConfig cfg;

        /// <summary>
        /// Initialises a new instance of the BaseCustomActionsManager class 
        /// using the specified extra suffix and the assembly of the actual type of this class.
        /// </summary>
        protected BaseCustomActionsManager(ILifetimeScope container, string extraSuffix, IEnumerable<ImplementorAssembly> assemblies)
        {
            if (container == null) { throw new ArgumentNullException("container"); }
            if (assemblies == null) { throw new ArgumentNullException("assemblies"); }

            _container = container;
            // Each assembly only once. Thus assemblies will be registered by modules throug Autofac it cannot be guaranteed that this will happen only once per assembly.
            _assemblies = assemblies.Distinct().ToList();

            ExtraSuffix = extraSuffix;
            ImplementationAssemblyName = this.GetType().Assembly.FullName;

            container.TryResolve<IAssetsManager>(out _assetsMgr);
            cfg = container.Resolve<ZetboxConfig>();
        }

        /// <summary>
        /// Initializes this CustomActionsManager. This method is thread-safe and won't do
        /// anything if the ActionsManager is already initialized.
        /// </summary>
        public virtual async Task Init(IReadOnlyZetboxContext ctx)
        {
            // TODO: lock (SyncRoot)
            {
                if (IsInitialised) return;

                var implType = this.GetType();
                try
                {
                    using (Log.InfoTraceMethodCallFormat("Init", "Initializing Actions for [{0}] by [{1}]", ExtraSuffix, implType.Name))
                    {
                        Log.TraceTotalMemory("Before BaseCustomActionsManager.Init()");

                        ReflectMethodsAndAssets(ctx);
                        await CreateInvokeInfosForDataTypes(ctx);

                        foreach (var key in _reflectedMethods.Where(i => !_attachedMethods.ContainsKey(i.Key)).Select(i => i.Key))
                        {
                            Log.Warn(string.Format("Couldn't find any method for Invocation {0}", key));
                        }

                        Log.TraceTotalMemory("After BaseCustomActionsManager.Init()");
                    }
                }
                finally
                {
                    IsInitialised = true;
                }
            }
        }

        private void ReflectMethodsAndAssets(IReadOnlyZetboxContext metaCtx)
        {
            if (metaCtx == null) { throw new ArgumentNullException("metaCtx"); }

            // Load all Implementor Types and Invocations
            foreach (var assembly in _assemblies.Select(a => a.Assembly))
            {
                try
                {
                    // Methods
                    foreach (var t in assembly.GetTypes())
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
                                try
                                {
                                    if (m.GetCustomAttributes(typeof(Invocation), false).Length != 0)
                                    {
                                        var key = new MethodKey(t.Namespace, t.Name.Substring(0, t.Name.Length - "Actions".Length), m.Name, m.GetParameters().Select(p => p.ParameterType).ToArray());
                                        if (_reflectedMethods.ContainsKey(key))
                                        {
                                            _reflectedMethods[key].Add(m);
                                        }
                                        else
                                        {
                                            _reflectedMethods[key] = new List<MethodInfo>() { m };
                                        }
                                    }
                                    else
                                    {
                                        Log.Warn(string.Format("Found public method {0}.{1} which has no Invocation attribute. Ignoring this method", t.FullName, m.Name));
                                    }
                                }
                                catch (TypeLoadException tlex)
                                {
                                    if (!cfg.IsFallback)
                                    {
                                        Log.WarnFormat("Failed to reflect over Method [{0}].{1}. Unable to load type [{2}]. Ignoring and continuing", assembly.FullName, m.Name, tlex.TypeName);
                                    }
                                }
                            }
                        }
                    }

                    // Assets
                    if (_assetsMgr != null)
                    {
                        foreach (AssetsFor assetAttribute in assembly.GetCustomAttributes(typeof(AssetsFor), false))
                        {
                            _assetsMgr.AddAssembly(assetAttribute.Module, assembly);
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    if (!cfg.IsFallback)
                    {
                        if (ex.LoaderExceptions.Count() == 1)
                        {
                            Log.Warn(String.Format("Failed to reflect over Assembly [{0}]. Ignoring and continuing.", assembly.FullName), ex.LoaderExceptions.Single());
                        }
                        else
                        {
                            foreach (var lex in ex.LoaderExceptions)
                            {
                                Log.Warn(String.Format("Failed to reflect over Assembly [{0}]. Ignoring and continuing. Multiple Errors:", assembly.FullName), lex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn(String.Format("Error while processing Assembly [{0}]. Ignoring and continuing.", assembly.FullName), ex);
                }
            }
        }
        /// <summary>
        /// Initializes caches for the provider of the given Context
        /// </summary>
        /// <param name="metaCtx">the context used to access the meta data</param>
        private async Task CreateInvokeInfosForDataTypes(IReadOnlyZetboxContext metaCtx)
        {
            if (metaCtx == null) { throw new ArgumentNullException("metaCtx"); }

            foreach (var objClass in metaCtx.GetQuery<CompoundObject>())
            {
                try
                {
                    await CreateInvokeInfosForAssembly(objClass);
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
                    await CreateInvokeInfosForAssembly(objClass);
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

        private async Task CreateInvokeInfosForAssembly(DataType objClass)
        {
            // baseObjClass.GetDataType(); is not possible here, because this
            // Method is currently attaching
            var implTypeName = (await objClass.GetProp_Module()).Namespace
                + "."
                + objClass.Name
                + ExtraSuffix
                + ", " + ImplementationAssemblyName;
            var implType = Type.GetType(implTypeName);
            if (implType != null)
            {
                await CreateInvokeInfos(objClass, implType);
            }
            else
            {
                if (!cfg.IsFallback)
                {
                    Log.WarnFormat("Cannot find Type {0}\n", implTypeName);
                }
            }
        }

        private async Task<EventBasedMethodAttribute> FindEventBasedMethodAttribute(Method method, Type implType)
        {
            Type[] paramTypes = (await method.GetProp_Parameter())
                    .Where(p => !p.IsReturnParameter)
                    .Select(p => p.GuessParameterType())
                    .ToArray();
            MethodInfo methodInfo = implType.FindMethod(method.Name, paramTypes);
            if (methodInfo == null)
            {
                if (!cfg.IsFallback)
                {
                    Log.WarnFormat(
                        "Couldn't find method '{0}.{1}' with parameters: {2}\n",
                        method.ObjectClass.Name,
                        method.Name,
                        String.Join(", ", paramTypes.Select(t => t == null ? "null" : t.FullName).ToArray()));
                }
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
                if (!cfg.IsFallback)
                {
                    Log.WarnFormat(
                        "Couldn't find method '{0}.{1}'\n",
                        method.ObjectClass.Name,
                        method.Name + execSuffix);
                }
                return null;
            }
            else
            {
                // May be null on Methods without events like server side invocatiaons or "embedded" methods
                return (EventBasedMethodAttribute)pi.GetCustomAttributes(typeof(EventBasedMethodAttribute), false).SingleOrDefault();
            }
        }

        private async Task CreateInvokeInfos(DataType dt, Type implType)
        {
            if (implType == null) throw new ArgumentNullException("implType");
            if (dt == null) throw new ArgumentNullException("dt");

            // preload 
            _ = await dt.GetProp_Module();

            var dtType = Type.GetType(string.Format("{0}.{1}, {2}", dt.Module.Namespace, dt.Name, Helper.InterfaceAssembly), false);
            if (dtType == null) throw new ArgumentOutOfRangeException("dt", string.Format("Cannot find type '{0}'", string.Format("{0}.{1}", dt.Module.Namespace, dt.Name)));

            // Reflected Methods
            // New style
            foreach (var method in await GetAllMethods(dt))
            {
                var returnParam = (await method.GetProp_Parameter()).SingleOrDefault(p => p.IsReturnParameter);
                var infrastructureParameters = returnParam == null
                    ? new Type[] { dtType }
                    : new Type[] { dtType, typeof(MethodReturnEventArgs<>).MakeGenericType(returnParam.GuessParameterType()) };

                Type[] paramTypes = infrastructureParameters
                    .Concat(method.Parameter
                        .Where(p => !p.IsReturnParameter)
                        .Select(p => p.GuessParameterType()))
                    .ToArray();

                var key = new MethodKey(dt.Module.Namespace, dt.Name, method.Name, paramTypes);
                if (_reflectedMethods.ContainsKey(key))
                {
                    var methodInfos = _reflectedMethods[key];

                    // May be null on Methods without events like server side invocations or "embedded" methods
                    // or null if not found
                    var attr = await FindEventBasedMethodAttribute(method, implType); // The Method
                    if (attr != null)
                    {
                        foreach (var mi in methodInfos)
                        {
                            CreateInvokeInfo(implType, mi, attr.EventName);
                        }
                    }

                    _attachedMethods[key] = true;
                }

                foreach (var data in new[] {
                    new { Suffix = "CanExec", ReturnType = typeof(MethodReturnEventArgs<bool>) },
                    new { Suffix = "CanExecReason", ReturnType = typeof(MethodReturnEventArgs<string>) }
                })
                {
                    key = new MethodKey(dt.Module.Namespace, dt.Name, method.Name + data.Suffix, new[] { dtType, data.ReturnType });
                    if (_reflectedMethods.ContainsKey(key))
                    {
                        var methodInfos = _reflectedMethods[key];

                        // May be null on Methods without events like server side invocations or "embedded" methods
                        // or null if not found
                        var attr = FindEventBasedMethodAttribute(method, data.Suffix, implType);
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

            if (dt is ObjectClass)
            {
                await CreateDefaultMethodInvocations(implType, dt, "NotifyPreSave", new[] { dtType });
                await CreateDefaultMethodInvocations(implType, dt, "NotifyPostSave", new[] { dtType });
                await CreateDefaultMethodInvocations(implType, dt, "NotifyCreated", new[] { dtType });
                await CreateDefaultMethodInvocations(implType, dt, "NotifyDeleting", new[] { dtType });
            }

            await CreateDefaultMethodInvocations(implType, dt, "ToString", new[] { dtType, typeof(MethodReturnEventArgs<string>) });
            await CreateDefaultMethodInvocations(implType, dt, "ObjectIsValid", new[] { dtType, typeof(ObjectIsValidEventArgs) });

            // Reflected Properties
            // New style
            var props = await dt.GetProp_Properties();
            if(props == null)
            {
                throw new InvalidOperationException("Cant be null");
            }
            foreach (Property prop in props)
            {
                if (!prop.GetIsList())
                {
                    await CreatePropertyInvocations(implType, prop, "get_", "Getter",
                        new[] { dtType, typeof(PropertyGetterEventArgs<>).MakeGenericType(dtType.GetPropertyType(prop.Name)) });
                }

                if (!prop.IsCalculated() && !prop.GetIsList())
                {
                    await CreatePropertyInvocations(implType, prop, "preSet_", "PreSetter",
                        new[] { dtType, typeof(PropertyPreSetterEventArgs<>).MakeGenericType(dtType.GetPropertyType(prop.Name)) });
                    await CreatePropertyInvocations(implType, prop, "postSet_", "PostSetter",
                        new[] { dtType, typeof(PropertyPostSetterEventArgs<>).MakeGenericType(dtType.GetPropertyType(prop.Name)) });
                }

                if (prop.GetIsList())
                {
                    // change notification
                    await CreatePropertyInvocations(implType, prop, "postSet_", "PostSetter", new[] { dtType });
                }

                await CreatePropertyInvocations(implType, prop, "isValid_", "IsValid",
                    new[] { dtType, typeof(PropertyIsValidEventArgs) });
            }
        }

        private async Task CreateDefaultMethodInvocations(Type implType, DataType dt, string methodName, Type[] paramTypes)
        {
            var key = new MethodKey((await dt.GetProp_Module()).Namespace, dt.Name, methodName, paramTypes);
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

        private async Task CreatePropertyInvocations(Type implType, Property prop, string methodPrefix, string invocationType, Type[] paramTypes)
        {
            var key = new MethodKey((await (await prop.GetProp_ObjectClass()).GetProp_Module()).Namespace, prop.ObjectClass.Name, string.Format("{0}{1}", methodPrefix, prop.Name), paramTypes);
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

        private async Task<List<Method>> GetAllMethods(DataType dt)
        {
            var result = new List<Method>();
            if (dt != null)
            {
                if (dt is ObjectClass)
                {
                    result = await GetAllMethods(await ((ObjectClass)dt).GetProp_BaseObjectClass());
                }
                var methods = await dt.GetProp_Methods();
                if(methods == null)
                {
                    throw new InvalidOperationException("Datatype returns a null methods collection");
                }
                result.AddRange(methods);
                return result;
            }
            return result;
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
