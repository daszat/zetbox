using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

namespace Kistl.API
{
    /// <summary>
    /// Serialization Wrapper for a System.Type object
    /// </summary>
    [Serializable]
    [DataContract]
    public class SerializableType
    {
        /// <summary>
        /// This class is used to place type information on the wire. Since the wire protocol is Provider independent, 
        /// only interface types are stored. Usually this is used to declare the type of the following IPersistenceObject.
        /// </summary>
        /// <param name="ifType">System.Type to serialize</param>
        public SerializableType(InterfaceType ifType)
        {
            var type = ifType.Type;

            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                TypeName = genericType.FullName;
                AssemblyQualifiedName = genericType.AssemblyQualifiedName;

                GenericTypeParameter = type.GetGenericArguments()
                    .Select(t => new SerializableType(new InterfaceType(t)))
                    .ToList();
            }
            else
            {
                TypeName = type.FullName;
                AssemblyQualifiedName = type.AssemblyQualifiedName;
                GenericTypeParameter = new List<SerializableType>();
            }

            // This is null if the Type is e.g. a Generic Parameter - not supported
            if (string.IsNullOrEmpty(AssemblyQualifiedName))
                throw new NotSupportedException("AssemblyQualifiedName must not be null or empty - maybe this Type is a Generic Parameter or something similarily strange.");
        }

        /// <summary>
        /// Full Type Name
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// AssemblyQualifiedName
        /// </summary>
        [DataMember]
        public string AssemblyQualifiedName { get; set; }

        /// <summary>
        /// List of Generiy Type Parameter
        /// </summary>
        [DataMember]
        public List<SerializableType> GenericTypeParameter { get; set; }

        public InterfaceType GetInterfaceType()
        {
            return new InterfaceType(GetSystemType());
        }

        public ImplementationType GetImplementationType()
        {
            return GetInterfaceType().ToImplementationType();
        }

        /// <summary>
        /// Creates a new implementation instance of this Type
        /// </summary>
        /// <returns>a freshly constructed instance</returns>
        public object CreateInstance()
        {
            return Activator.CreateInstance(GetImplementationType().Type);
        }

        /// <summary>
        /// Returns the serialized System.Type
        /// </summary>
        /// <returns></returns>
        public Type GetSystemType()
        {
            if (!this.AssemblyQualifiedName.StartsWith(TypeName))
            {
                throw new InvalidOperationException("FullName doesn't match AssemblyQualifiedName");
            }
            Type result = null;
            if (GenericTypeParameter.Count > 0)
            {
                Type type = Type.GetType(AssemblyQualifiedName);
                if (type != null) result = type.MakeGenericType(GenericTypeParameter.Select(t => t.GetSystemType()).ToArray());
            }
            else
            {
                result = Type.GetType(AssemblyQualifiedName);
            }

            if (result == null)
            {
                throw new InvalidOperationException(string.Format("Unable to create Type {0}{1}",
                    TypeName,
                    GenericTypeParameter.Count > 0 ? "<" + string.Join(", ", GenericTypeParameter.Select(t => t.TypeName).ToArray()) + ">" : String.Empty));
            }

            return result;
        }

        /// <summary>
        /// Creates a new Object
        /// </summary>
        /// <returns></returns>
        public object NewObject()
        {
            return Activator.CreateInstance(GetSystemType().ToImplementationType());
        }

        #region implement value equality over TypeName and AssemblyQualifiedName

        public override bool Equals(object obj)
        {
            SerializableType b = obj as SerializableType;
            if (b == null) return false;
            return this.TypeName == b.TypeName && this.AssemblyQualifiedName == b.AssemblyQualifiedName;
        }

        public override int GetHashCode()
        {
            return TypeName.GetHashCode() ^ this.AssemblyQualifiedName.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return String.Format(@"Type {{ TypeName=""{0}"", AssemblyQualifiedName=""{1}"" }}", TypeName, AssemblyQualifiedName);
        }
    }
}
