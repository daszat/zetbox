using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API.Utils;
using System.Reflection;

namespace Kistl.API
{
    public interface IAssemblyConfiguration
    {
        string InterfaceAssemblyName { get; }
        IEnumerable<string> AllImplementationAssemblyNames { get; }
    }

    public interface ITypeTransformations
    {
        InterfaceType AsInterfaceType(Type t);
        InterfaceType AsInterfaceType(string name);
        ImplementationType AsImplementationType(Type t);
        IAssemblyConfiguration AssemblyConfiguration { get; }
    }

    public class TypeTransformations : ITypeTransformations
    {
        public TypeTransformations(InterfaceType.Factory ifTypeFactory, InterfaceType.NameFactory ifNameFactory, ImplementationType.Factory imTypeFactory, IAssemblyConfiguration assemblyConfig)
        {
            _ifTypeFactory = ifTypeFactory;
            _ifNameFactory = ifNameFactory;
            _imTypeFactory = imTypeFactory;
            this.AssemblyConfiguration = assemblyConfig;
        }

        private readonly InterfaceType.Factory _ifTypeFactory;
        private readonly InterfaceType.NameFactory _ifNameFactory;
        private readonly ImplementationType.Factory _imTypeFactory;

        public InterfaceType AsInterfaceType(Type t) { try { return _ifTypeFactory(t); } catch (TargetInvocationException ex) { throw ex.InnerException; } }
        public InterfaceType AsInterfaceType(string name) { try { return _ifNameFactory(name); } catch (TargetInvocationException ex) { throw ex.InnerException; } }
        public ImplementationType AsImplementationType(Type t) { try { return _imTypeFactory(t); } catch (TargetInvocationException ex) { throw ex.InnerException; } }
        public IAssemblyConfiguration AssemblyConfiguration { get; private set; }
    }

    public struct InterfaceType
    {
        public delegate InterfaceType Factory(Type type);
        public delegate InterfaceType NameFactory(string name);

        /// <summary>
        /// The wrapped <see cref="System.Type"/>. Guaranteed to be a valid InterfaceType (see <see cref="IsValid"/>).
        /// </summary>
        public Type Type { get; private set; }
        private readonly ITypeTransformations typeTrans;

        public InterfaceType(Type type, ITypeTransformations typeTrans)
            : this()
        {
            if (type == null) throw new ArgumentNullException("type");
            if (typeTrans == null) throw new ArgumentNullException("typeTrans");

            if (!IsValid(type, typeTrans.AssemblyConfiguration)) { throw new ArgumentOutOfRangeException("type"); }

            this.Type = type;
            this.typeTrans = typeTrans;
        }

        public InterfaceType(string name, ITypeTransformations typeTrans)
            : this()
        {
            if (name == null) throw new ArgumentNullException("name");
            if (typeTrans == null) throw new ArgumentNullException("typeTrans");

            var type = Type.GetType(name + "," + typeTrans.AssemblyConfiguration.InterfaceAssemblyName, true);
            if (!IsValid(type, typeTrans.AssemblyConfiguration)) { throw new InvalidOperationException("type is not a valid interface type"); }

            this.Type = type;
            this.typeTrans = typeTrans;
        }

        /// <summary>
        /// Checks whether <paramref name="type"/> is a provider independent Type from the 
        /// current InterfaceAssembly -OR- a generic (collection-)interface only referencing 
        /// <see cref="InterfaceType"/>s -OR- any other type neither coming from the 
        /// InterfaceAssembly or the ImplementationAssembly (this is convenience functionality 
        /// to pass through system types unscathed)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assemblyConfig"></param>
        /// <returns></returns>
        private static bool IsValid(Type type, IAssemblyConfiguration assemblyConfig)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (type.IsGenericType)
                return type.GetGenericArguments().All(t => IsValid(t, assemblyConfig));

            if (type.IsValueType) return true;

            if (type.Assembly.FullName == assemblyConfig.InterfaceAssemblyName
                && type.IsInterface)
                return true;

            if (type.Assembly.FullName != assemblyConfig.InterfaceAssemblyName
                && !assemblyConfig.AllImplementationAssemblyNames.Contains(type.Assembly.FullName))
                return true;

            return false;
        }

        private static readonly Type[] BaseInterfaces = new[] { 
            typeof(IDataObject),
            typeof(ICompoundObject),
            typeof(IRelationListEntry<,>),
            typeof(IRelationListEntry),
            typeof(IRelationCollectionEntry<,>),
            typeof(IRelationCollectionEntry),
            typeof(IValueCollectionEntry<,>),
            typeof(IValueCollectionEntry),
            typeof(IPersistenceObject) 
        };

        /// <summary>
        /// Returns the root of the specified InterfaceType's data model. The
        /// root is the top-most interface in this interface's parentage that 
        /// inherits only from IDataObject, or IPersistenceObject. Interfaces 
        /// that do not inherit from IDataObject, or IPersistenceObject are 
        /// excluded from all considerations.
        /// </summary>
        /// <returns>the root InterfaceType of this InterfaceType's data model</returns>
        public InterfaceType GetRootType()
        {
            var self = this.Type.IsGenericType ? this.Type.GetGenericTypeDefinition() : this.Type;
            // the base of the interface we're looking for
            var baseInterface = BaseInterfaces.Where(t => t.IsAssignableFrom(self)).First();
            var allInherited = GetInterestingInterfaces(baseInterface, this.Type);
            var candidates = allInherited
                .Select(intf => new { Interface = intf, Inherited = GetInterestingInterfaces(baseInterface, intf) })
                .ToList();
            candidates.Add(new { Interface = this.Type, Inherited = allInherited });

            return new InterfaceType(candidates.OrderBy(i => i.Inherited.Length).First().Interface, typeTrans);
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
            return new SerializableType(this, typeTrans);
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

    public struct ImplementationType
    {
        public delegate ImplementationType Factory(Type type);

        /// <summary>
        /// The wrapped <see cref="System.Type"/>. Guaranteed to be a valid ImplementationType (see <see cref="IsValid"/>).
        /// </summary>
        public Type Type { get; private set; }

        private readonly ITypeTransformations typeTrans;

        /// <summary>
        /// Wrap a given ImplementationType
        /// </summary>
        /// <param name="type">A valid ImplementationType</param>
        /// <param name="typeTrans"></param>
        /// <exception cref="ArgumentOutOfRangeException">if <paramref name="type"/> doesn't 
        /// fulfill all constraints</exception>
        /// <exception cref="ArgumentNullException">if <paramref name="type"/> is null</exception>
        public ImplementationType(Type type, ITypeTransformations typeTrans)
            : this()
        {
            if (type == null) throw new ArgumentNullException("type");
            if (typeTrans == null) throw new ArgumentNullException("typeTrans");
            if (!IsValid(type, typeTrans.AssemblyConfiguration)) { throw new ArgumentOutOfRangeException("type"); }
            this.typeTrans = typeTrans;
            this.Type = type;
        }

        /// <summary>
        /// Checks whether <paramref name="type"/> is a provider dependent Type from the 
        /// current ImplementationAssembly -OR- a generic (collection-)type only referencing 
        /// <see cref="ImplementationType"/>s -OR- any other type neither coming from the 
        /// InterfaceAssembly or the ImplementationAssembly (this is convenience functionality 
        /// to pass through system types unscathed)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assemblyConfig"></param>
        /// <returns></returns>
        private static bool IsValid(Type type, IAssemblyConfiguration assemblyConfig)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (type.IsGenericType)
                return type.GetGenericArguments().All(t => ImplementationType.IsValid(t, assemblyConfig));

            if (type.IsValueType) return true;

            if (assemblyConfig.AllImplementationAssemblyNames.Contains(type.Assembly.FullName)
                && !type.IsInterface)
                return true;

            if (type.Assembly.FullName != assemblyConfig.InterfaceAssemblyName
                && !assemblyConfig.AllImplementationAssemblyNames.Contains(type.Assembly.FullName))
                return true;


            return false;
        }

        /// <summary>
        /// Computes the corresponding <see cref="InterfaceType"/>
        /// </summary>
        /// <returns>An <see cref="InterfaceType"/> corresponding to this <see cref="ImplementationType"/></returns>
        public InterfaceType ToInterfaceType()
        {
            // TODO: inline transformation logic to here
            return new InterfaceType(ToInterfaceType(this.Type, typeTrans.AssemblyConfiguration), typeTrans);
        }

        /// <summary>
        /// Returns the most specific implemented Kistl.Objects interface of a given Type.
        /// </summary>
        private static Type ToInterfaceType( Type type, IAssemblyConfiguration assemblyConfig)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            // shortcut and warn when trying to resolve an already resolved type
            if (type.IsInterface && type.IsIPersistenceObject())
            {
                Logging.Log.Error("Tried to convert an interface type a second time");
                return type;
            }

            if (type.IsGenericType)
            {
                // convert args of things like Generic Collections
                Type genericType = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments().Select(t => ToInterfaceType(t, assemblyConfig)).ToArray();
                return genericType.MakeGenericType(genericArguments);
            }
            else if (!type.IsInterface)
            {
                if (type.IsIPersistenceObject() || type.IsICompoundObject())
                {
                    var parts = type.FullName.Split(new string[] { Helper.ImplementationSuffix }, StringSplitOptions.RemoveEmptyEntries);
                    type = Type.GetType(parts[0] + ", " + assemblyConfig.InterfaceAssemblyName, true);
                }
            }
            return type;
        }

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

        public static bool operator ==(ImplementationType a, ImplementationType b) { return a.Equals(b); }
        public static bool operator !=(ImplementationType a, ImplementationType b) { return !a.Equals(b); }
        public static bool operator ==(Type a, ImplementationType b) { return b.Type.Equals(a); }
        public static bool operator !=(Type a, ImplementationType b) { return !b.Type.Equals(a); }
        public static bool operator ==(ImplementationType a, Type b) { return a.Type.Equals(b); }
        public static bool operator !=(ImplementationType a, Type b) { return !a.Type.Equals(b); }

        #endregion

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
