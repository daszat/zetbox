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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Serialization Wrapper for a System.Type object
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://dasz.at/Zetbox/", Name = "Type")]
    public class SerializableType
    {
        /// <summary>
        /// This class is used to place type information on the wire. Since the wire protocol is Provider independent, 
        /// only interface types are stored. Usually this is used to declare the type of the following IPersistenceObject.
        /// </summary>
        /// <remarks>
        /// Since the <see cref="InterfaceType.Factory"/> cannot be serialiezed, this class cannot provide full dehydration.
        /// Use <see cref="GetSystemType"/> and your own factory to retrieve <see cref="InterfaceType"/>.</remarks>
        /// <param name="ifType">System.Type to serialize</param>
        /// <param name="iftFactory"></param>
        internal SerializableType(InterfaceType ifType, InterfaceType.Factory iftFactory)
        {
            var type = ifType.Type;

            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                TypeName = genericType.FullName;
                AssemblyQualifiedName = genericType.AssemblyQualifiedName;

                GenericTypeParameter = type.GetGenericArguments()
                    .Select(t => new SerializableType(iftFactory(t), iftFactory))
                    .ToArray();
            }
            else
            {
                TypeName = type.FullName;
                AssemblyQualifiedName = type.AssemblyQualifiedName;
                GenericTypeParameter = new SerializableType[] { };
            }

            // This is null if the Type is e.g. a Generic Parameter - not supported
            if (string.IsNullOrEmpty(AssemblyQualifiedName))
                throw new NotSupportedException("AssemblyQualifiedName must not be null or empty - maybe this Type is a Generic Parameter or something similarily strange.");
        }

        internal SerializableType(string typeName, string assemblyQualifiedName, SerializableType[] genericTypeParameter)
        {
            TypeName = typeName;
            AssemblyQualifiedName = assemblyQualifiedName;
            GenericTypeParameter = genericTypeParameter;
        }

        /// <summary>
        /// Full Type Name
        /// </summary>
        [DataMember(Name = "TypeName")]
        public string TypeName { get; set; }

        /// <summary>
        /// AssemblyQualifiedName
        /// </summary>
        [DataMember(Name = "AssemblyQualifiedName")]
        public string AssemblyQualifiedName { get; set; }

        /// <summary>
        /// List of Generiy Type Parameter
        /// </summary>
        [DataMember(Name = "GenericTypeParameter")]
        public SerializableType[] GenericTypeParameter { get; set; }

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
            if (GenericTypeParameter.Length > 0)
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
                    GenericTypeParameter.Length > 0 ? "<" + string.Join(", ", GenericTypeParameter.Select(t => t.TypeName).ToArray()) + ">" : String.Empty));
            }

            return result;
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

        private SerializableType()
        {
        }
    }
}
