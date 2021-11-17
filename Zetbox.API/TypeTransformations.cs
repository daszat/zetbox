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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API.Utils;

    public interface IInterfaceTypeChecker
    {
        bool IsInterfaceType(Type t);
    }

    public interface IImplementationTypeChecker
    {
        bool IsImplementationType(Type t);
    }

    public abstract class BaseTypeChecker
    {
        private readonly object _lock = new object();
        private readonly Func<IEnumerable<IImplementationTypeChecker>> _implTypeCheckersFactory;
        private ReadOnlyCollection<IImplementationTypeChecker> _implTypeCheckers;
        protected ReadOnlyCollection<IImplementationTypeChecker> ImplTypeCheckers
        {
            get
            {
                lock (_lock)
                    if (_implTypeCheckers == null)
                        _implTypeCheckers = _implTypeCheckersFactory().ToList().AsReadOnly();

                return _implTypeCheckers;
            }
        }

        public BaseTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)
        {
            _implTypeCheckersFactory = implTypeCheckersFactory;
        }
    }

    public abstract class BaseInterfaceTypeChecker
        : BaseTypeChecker, IInterfaceTypeChecker
    {
        public BaseInterfaceTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)
            : base(implTypeCheckersFactory)
        {
        }

        protected abstract Assembly GetAssembly();

        public bool IsInterfaceType(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            // Allow all interface types from the generated assembly
            if (type.IsInterface && type.Assembly == GetAssembly())
                return true;

            // Allow all enumeration types from the generated assembly
            if (type.IsEnum && type.Assembly == GetAssembly())
                return true;

            // Allow all generic types which have only interface types as arguments
            if (type.IsGenericType)
                return type.GetGenericArguments().All(t => IsInterfaceType(t));

            // Allow all value types from mscorlib
            if (type.IsValueType && type.Assembly == typeof(int).Assembly)
                return true;

            // Allow strings (not a value type)
            if (type == typeof(string))
                return true;

            // Hack: Allow all types that are not generated at all
            if (type.Assembly != GetAssembly()
                && !ImplTypeCheckers.Any(checker => checker.IsImplementationType(type)))
            {
                Logging.Log.WarnOnce(String.Format("Allowing non-generated type [{0}] as interface type", type.AssemblyQualifiedName));
                return true;
            }

            return false;
        }
    }

    public abstract class BaseImplementationTypeChecker
        : BaseTypeChecker
    {
        public BaseImplementationTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)
            : base(implTypeCheckersFactory)
        {
        }

        protected abstract Assembly GetAssembly();

        public bool IsImplementationType(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            // Allow all top-level non-interface types from this Assembly
            if (!type.IsInterface && type.Assembly == GetAssembly() && type.DeclaringType == null)
                return true;

            // Allow all generic types which have only implementation types as arguments
            if (type.IsGenericType)
                return type.GetGenericArguments().All(t => IsImplementationType(t));

            //// Allow all value types from mscorlib
            //if (type.IsValueType && type.Assembly == typeof(int).Assembly)
            //    return true;

            //// Hack: Allow all types that are not generated at all
            //if (type.Assembly != GetAssembly()
            //    && !ImplTypeCheckers.Any(checker => checker.IsImplementationType(type)))
            //{
            //    Zetbox.API.Utils.Logging.Log.WarnFormat("Allowing non-generated type [{0}] as implementation type", type.AssemblyQualifiedName);
            //    return true;
            //}

            return false;
        }
    }

    public struct InterfaceType
    {
        public delegate InterfaceType Factory(Type type);

        /// <summary>
        /// Small helper to avoid autofac accesses.
        /// </summary>
        /// <remarks>This is used by the proxy, where off-thread accesses may deadlock on
        /// autofac when a query is run from within an autofac resolution on the main thread.
        /// Also, this is a basic operation that does not profit from the Autofac lifecycle
        /// management. Removing this from Autofac should help in reducing the overhead.</remarks>
        public sealed class FactoryImpl
        {
            private readonly IInterfaceTypeChecker _typeChecker;
            public FactoryImpl(IInterfaceTypeChecker typeChecker)
            {
                if (typeChecker == null) throw new ArgumentNullException("typeChecker");
                _typeChecker = typeChecker;
            }

            public InterfaceType Invoke(Type type)
            {
                return Create(type, _typeChecker);
            }
        }

        private static readonly object _cacheLock = new object();
        private static Dictionary<Type, InterfaceType> _cache = new Dictionary<Type, InterfaceType>();
        private static Dictionary<InterfaceType, InterfaceType> _rootTypeCache = new Dictionary<InterfaceType, InterfaceType>();

        // TODO: Mit david nochmals besprechen
        internal static InterfaceType Create(Type type, IInterfaceTypeChecker typeChecker)
        {
            lock (_cacheLock)
            {
                if (type == null) return new InterfaceType(); // Possible, because a Type could be loaded from an XML File which does not exists in this Zetbox instance
                if (_cache.ContainsKey(type)) return _cache[type];
                var ift = new InterfaceType(type, typeChecker);
                _cache[type] = ift;
                return ift;
            }
        }

        /// <summary>
        /// The wrapped <see cref="Type"/>. Guaranteed to be a valid InterfaceType (see <see cref="IInterfaceTypeChecker"/>).
        /// </summary>
        public Type Type { get { return _type; } }
        private readonly Type _type;
        private readonly IInterfaceTypeChecker _typeChecker;

        private InterfaceType(Type type, IInterfaceTypeChecker typeChecker)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (typeChecker == null) throw new ArgumentNullException("typeChecker");

            if (!typeChecker.IsInterfaceType(type))
            {
                Logging.Log.ErrorFormat("[{0}] is not an interface type", type.AssemblyQualifiedName);
                throw new ArgumentOutOfRangeException("type");
            }

            this._type = type;
            this._typeChecker = typeChecker;
        }

        private static readonly Type[] BaseInterfaces = new[] { 
            typeof(IDataObject),
            typeof(ICompoundObject),
            typeof(IRelationListEntry<,>),
            typeof(IRelationListEntry),
            typeof(IRelationEntry<,>),
            typeof(IRelationEntry),
            typeof(IValueCollectionEntry<,>),
            typeof(IValueCollectionEntry),
            typeof(IValueListEntry<,>),
            typeof(IValueListEntry),
            typeof(IPersistenceObject) 
        };

        /// <summary>
        /// Returns the root of the specified InterfaceType's data model. The
        /// root is the top-most interface in this interface's parentage that 
        /// inherits only from one of the base interfaces. Interfaces 
        /// that do not inherit from one of the base interfaces are 
        /// excluded from all considerations.
        /// </summary>
        /// <returns>the root InterfaceType of this InterfaceType's data model</returns>
        public InterfaceType GetRootType()
        {
            lock (_cacheLock)
            {
                if (_rootTypeCache.ContainsKey(this)) return _rootTypeCache[this];

                var self = this.Type.IsGenericType ? this.Type.GetGenericTypeDefinition() : this.Type;
                // the base of the interface we're looking for
                var baseInterface = BaseInterfaces.Where(t => t.IsAssignableFrom(self)).First();
                var allInherited = GetInterestingInterfaces(baseInterface, this.Type);
                var candidates = allInherited
                    .Select(intf => new { Interface = intf, Inherited = GetInterestingInterfaces(baseInterface, intf) })
                    .ToList();
                candidates.Add(new { Interface = this.Type, Inherited = allInherited });

                return _rootTypeCache[this] = Create(candidates.OrderBy(i => i.Inherited.Length).First().Interface, _typeChecker);
            }
        }

        /// <summary>
        /// Returns an array of interfaces that are potential base interfaces 
        /// of type <paramref name="baseInterface"/> for the specified Type 
        /// <paramref name="t"/>.
        /// </summary>
        /// <param name="baseInterface">the basic interface that is the parent of the searched base</param>
        /// <param name="t">the interface to inspect</param>
        /// <returns>an array of interfaces</returns>
        private static Type[] GetInterestingInterfaces(Type baseInterface, Type t)
        {
            return t.GetInterfaces()
                .Where(i => baseInterface.IsAssignableFrom(i) && !BaseInterfaces.Contains(i.IsGenericType ? i.GetGenericTypeDefinition() : i))
                .ToArray();
        }

        public SerializableType ToSerializableType()
        {
            // local copy for lambda
            var tc = this._typeChecker;
            return new SerializableType(this, (t) => new InterfaceType(t, tc));
        }

        #region implement content equality over the Type property

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InterfaceType)
                return this.Type.Equals(((InterfaceType)obj).Type);
            else
                return false;
        }

        public static bool operator ==(InterfaceType a, InterfaceType b) { return a.Equals(b); }
        public static bool operator !=(InterfaceType a, InterfaceType b) { return !a.Equals(b); }
        public static bool operator ==(Type a, InterfaceType b) { return b.Type.Equals(a); }
        public static bool operator !=(Type a, InterfaceType b) { return !b.Type.Equals(a); }
        public static bool operator ==(InterfaceType a, Type b) { return a.Type.Equals(b); }
        public static bool operator !=(InterfaceType a, Type b) { return !a.Type.Equals(b); }

        #endregion

        public bool IsAssignableFrom(InterfaceType b)
        {
            return this.Type.IsAssignableFrom(b.Type);
        }

        public override string ToString()
        {
            return Type != null ? Type.ToString() : "<Type is NULL>";
        }
    }

    public abstract class ImplementationType
    {
        private readonly InterfaceType.Factory _iftFactory;
        protected InterfaceType.Factory IftFactory { get { return _iftFactory; } }

        /// <summary>
        /// The wrapped <see cref="Type"/>. Guaranteed to be a valid ImplementationType (see <see cref="IInterfaceTypeChecker"/>).
        /// </summary>
        public Type Type { get { return _type; } }
        private readonly Type _type;

        /// <summary>
        /// Wrap a given ImplementationType
        /// </summary>
        /// <param name="type">A valid ImplementationType</param>
        /// <param name="iftFactory"></param>
        /// <param name="implTypeChecker"></param>
        /// <exception cref="ArgumentOutOfRangeException">if <paramref name="type"/> doesn't 
        /// fulfill all constraints</exception>
        /// <exception cref="ArgumentNullException">if <paramref name="type"/> is null</exception>
        protected ImplementationType(Type type, InterfaceType.Factory iftFactory, IImplementationTypeChecker implTypeChecker)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (iftFactory == null) throw new ArgumentNullException("iftFactory");
            if (implTypeChecker == null) throw new ArgumentNullException("implTypeChecker");
            if (!implTypeChecker.IsImplementationType(type)) { throw new ArgumentOutOfRangeException("type", String.Format("Type {0} is not an ImplementationType", type.AssemblyQualifiedName)); }

            this._type = type;
            this._iftFactory = iftFactory;
        }

        /// <summary>
        /// Computes the corresponding <see cref="InterfaceType"/>
        /// </summary>
        /// <returns>An <see cref="InterfaceType"/> corresponding to this <see cref="ImplementationType"/></returns>
        public abstract InterfaceType ToInterfaceType();

        #region implement content equality over the Type property

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ImplementationType)
                return this.Type.Equals(((ImplementationType)obj).Type);
            else
                return false;
        }

        public static bool operator ==(ImplementationType a, ImplementationType b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }
            else if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
            {
                return false;
            }
            else
            {
                return a.Equals(b);
            }
        }
        public static bool operator !=(ImplementationType a, ImplementationType b) { return !(a == b); }

        #endregion

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
