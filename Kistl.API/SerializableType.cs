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
        /// This class is used to place type information on the wire. Usually this is used to wrap the interface type information of the following IPersistenceObject.
        /// </summary>
        /// <param name="type">System.Type to serialize</param>
        public SerializableType(Type type)
        {
            GenericTypeParameter = new List<SerializableType>();

            if (type.IsGenericParameter)
            {
                throw new NotSupportedException("Generic Parameter cannot be serialized");
            }
            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                TypeName = genericType.FullName;
                AssemblyQualifiedName = genericType.AssemblyQualifiedName;

                type.GetGenericArguments().ForEach<Type>(t => GenericTypeParameter.Add(new SerializableType(t)));
            }
            else
            {
                TypeName = type.FullName;
                AssemblyQualifiedName = type.AssemblyQualifiedName;
            }

            // This is null if the Typ is e.g. a Generic Parameter - not supported
            if (string.IsNullOrEmpty(AssemblyQualifiedName)) throw new NotSupportedException("AssemblyQualifiedName must not be null or empty - maybe this Type is a Generic Parameter or something similar.");
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

        /// <summary>
        /// Returns the serialized System.Type
        /// </summary>
        /// <returns></returns>
        public Type GetSerializedType()
        {
            Type result = null;
            if (GenericTypeParameter.Count > 0)
            {
                Type type = Type.GetType(AssemblyQualifiedName);
                if (type != null) result = type.MakeGenericType(GenericTypeParameter.Select(t => t.GetSerializedType()).ToArray());
            }
            else
            {
                result = Type.GetType(AssemblyQualifiedName);
            }

            if (result == null)
            {
                throw new InvalidOperationException(string.Format("Unable to create Type {0}{1}",
                    TypeName,
                    GenericTypeParameter.Count > 0 ? "<" + string.Join(", ", GenericTypeParameter.Select(t => t.TypeName).ToArray()) + ">" : ""));
            }

            return result;
        }

        /// <summary>
        /// Creates a new Object
        /// </summary>
        /// <returns></returns>
        public object NewObject()
        {
            return Activator.CreateInstance(GetSerializedType());
        }

        /// <summary>
        /// Equals....
        /// </summary>
        /// <param name="obj">the other object</param>
        /// <returns>Dont know ;-)</returns>
        public override bool Equals(object obj)
        {
            SerializableType b = obj as SerializableType;
            if (b == null) return false;
            return this.TypeName == b.TypeName && this.AssemblyQualifiedName == b.AssemblyQualifiedName;
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <returns>Dont know ;-)</returns>
        public override int GetHashCode()
        {
            return TypeName.GetHashCode() ^ this.AssemblyQualifiedName.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format(@"Type {{ TypeName=""{0}"", AssemblyQualifiedName=""{1}"" }}", TypeName, AssemblyQualifiedName);
        }
    }
}
