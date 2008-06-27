using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kistl.API
{
    [Serializable]
    public class SerializableType
    {
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
                _AssemblyQualifiedName = genericType.AssemblyQualifiedName;

                type.GetGenericArguments().ForEach<Type>(t => GenericTypeParameter.Add(new SerializableType(t)));
            }
            else
            {
                TypeName = type.FullName;
                _AssemblyQualifiedName = type.AssemblyQualifiedName;
            }

            // This is null if the Typ is e.g. a Generic Parameter - not supported
            if (string.IsNullOrEmpty(AssemblyQualifiedName)) throw new NotSupportedException("AssemblyQualifiedName must not be null or empty - maybe this Type is a Generic Parameter or something similar.");
        }

        public string TypeName { get; private set; }

        private string _AssemblyQualifiedName;
        /// <summary>
        /// TODO: This could be more optimal
        /// </summary>
        public string AssemblyQualifiedName 
        { 
            get 
            {
                switch (APIInit.HostType)
                {
                    case HostType.Server:
                        return _AssemblyQualifiedName.Replace(".Client", ".Server");
                        break;
                    case HostType.Client:
                        return _AssemblyQualifiedName.Replace(".Server", ".Client");
                        break;
                    default:
                        throw new InvalidOperationException("APIInit: Invalid Host Type " + APIInit.HostType);
                }
            } 
        }

        public List<SerializableType> GenericTypeParameter { get; private set; }

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

        public object NewObject()
        {
            return Activator.CreateInstance(GetSerializedType());
        }

        public override bool Equals(object obj)
        {
            SerializableType b = obj as SerializableType;
            if (b == null) return false;
            return this.TypeName == b.TypeName && this.AssemblyQualifiedName == b.AssemblyQualifiedName;
        }

        public override int GetHashCode()
        {
            return TypeName.GetHashCode();
        }
    }
}
