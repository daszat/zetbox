using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API.Utils;

namespace Kistl.API
{
    public struct InterfaceType
    {
        /// <summary>
        /// The wrapped <see cref="System.Type"/>. Guaranteed to be a valid InterfaceType (see <see cref="IsValid"/>).
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Wrap a given InterfaceType
        /// </summary>
        /// <param name="type">A valid InterfaceType</param>
        /// <exception cref="ArgumentOutOfRangeException">if <paramref name="type"/> doesn't 
        /// fulfill all constraints</exception>
        /// <exception cref="ArgumentNullException">if <paramref name="type"/> is null</exception>
        public InterfaceType(Type type)
            : this()
        {
            if (!IsValid(type)) { throw new ArgumentOutOfRangeException("type"); }

            this.Type = type;
        }

        /// <summary>
        /// Checks whether <paramref name="type"/> is a provider independent Type from the 
        /// current InterfaceAssembly -OR- a generic (collection-)interface only referencing 
        /// <see cref="InterfaceType"/>s -OR- any other type neither coming from the 
        /// InterfaceAssembly or the ImplementationAssembly (this is convenience functionality 
        /// to pass through system types unscathed)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValid(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (type.IsGenericType)
                return type.GetGenericArguments().All(t => InterfaceType.IsValid(t));

            if (type.IsValueType) return true;

            if (type.Assembly.FullName == ApplicationContext.Current.InterfaceAssembly
                && type.IsInterface)
                return true;

            if (type.Assembly.FullName != ApplicationContext.Current.InterfaceAssembly
                && type.Assembly.FullName != ApplicationContext.Current.ImplementationAssembly)
                return true;

            return false;
        }

        /// <summary>
        /// Computes the corresponding <see cref="ImplementationType"/>
        /// </summary>
        /// <returns>An <see cref="ImplementationType"/> corresponding to this <see cref="InterfaceType"/></returns>
        public ImplementationType ToImplementationType()
        {
            // TODO: inline transformation logic to here
            return new ImplementationType(this.Type.ToImplementationType());
        }

        /// <summary>
        /// Forces the given Type into an Interface shape.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static InterfaceType Transform(Type type)
        {
            if (InterfaceType.IsValid(type))
                return new InterfaceType(type);
            else
                return new ImplementationType(type).ToInterfaceType();
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
            return Type.ToString();
        }
    }

    public struct ImplementationType
    {
        /// <summary>
        /// The wrapped <see cref="System.Type"/>. Guaranteed to be a valid ImplementationType (see <see cref="IsValid"/>).
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Wrap a given ImplementationType
        /// </summary>
        /// <param name="type">A valid ImplementationType</param>
        /// <exception cref="ArgumentOutOfRangeException">if <paramref name="type"/> doesn't 
        /// fulfill all constraints</exception>
        /// <exception cref="ArgumentNullException">if <paramref name="type"/> is null</exception>
        public ImplementationType(Type type)
            : this()
        {
            if (!IsValid(type)) { throw new ArgumentOutOfRangeException("type"); }

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
        /// <returns></returns>
        public static bool IsValid(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (type.IsGenericType)
                return type.GetGenericArguments().All(t => ImplementationType.IsValid(t));

            if (type.IsValueType) return true;

            if (type.Assembly.FullName == ApplicationContext.Current.ImplementationAssembly
                && !type.IsInterface)
                return true;

            if (type.Assembly.FullName != ApplicationContext.Current.InterfaceAssembly
                && type.Assembly.FullName != ApplicationContext.Current.ImplementationAssembly)
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
            return new InterfaceType(this.Type.ToInterfaceType());
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

    public static class TypeTransformations
    {

        /// <summary>
        /// Returns the most specific implemented Kistl.Objects interface of a given Type.
        /// </summary>
        public static Type ToInterfaceType(this Type type)
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
                var genericArguments = type.GetGenericArguments().Select(t => t.ToInterfaceType()).ToArray();
                return genericType.MakeGenericType(genericArguments);
            }
            else if (!type.IsInterface)
            {
                if (type.IsIPersistenceObject() || type.IsICompoundObject())
                {
                    var parts = type.FullName.Split(new string[] { Helper.ImplementationSuffix }, StringSplitOptions.RemoveEmptyEntries);
                    type = Type.GetType(parts[0] + ", " + ApplicationContext.Current.InterfaceAssembly, true);
                }
            }
            return type;
        }

        /// <summary>
        /// Returns the Type implementing a given Kistl.Objects interface from the current ImplementationAssembly
        /// </summary>
        public static Type ToImplementationType(this Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            // shortcut and warn when trying to resolve an already resolved type
            if (type.FullName.Contains(Kistl.API.Helper.ImplementationSuffix) && type.IsIPersistenceObject())
            {
                Logging.Log.Error("Tried to convert an implementation type a second time");
                return type;
            }

            if (type.IsGenericType)
            {
                // convert args of things like Generic Collections
                Type genericType = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments().Select(t => t.ToImplementationType()).ToArray();
                return genericType.MakeGenericType(genericArguments);
            }
            else
            {
                if (type == typeof(IDataObject))
                {
                    return ApplicationContext.Current.BaseDataObjectType;
                }
                else if (type == typeof(IPersistenceObject))
                {
                    return ApplicationContext.Current.BasePersistenceObjectType;
                }
                else if (type == typeof(ICompoundObject))
                {
                    return ApplicationContext.Current.BaseCompoundObjectType;
                }
                else if (type == typeof(IRelationCollectionEntry))
                {
                    return ApplicationContext.Current.BaseCollectionEntryType;
                }
                else if (type == typeof(IValueCollectionEntry))
                {
                    return ApplicationContext.Current.BaseCollectionEntryType;
                }
                else if (type.IsInterface)
                {
                    if (type.IsIPersistenceObject() || type.IsICompoundObject())
                    {
                        // add ImplementationSuffix
                        string newType = type.FullName + Kistl.API.Helper.ImplementationSuffix + ", " + ApplicationContext.Current.ImplementationAssembly;
                        return Type.GetType(newType, true);
                    }
                }
                return type;
            }
        }
    }
}
