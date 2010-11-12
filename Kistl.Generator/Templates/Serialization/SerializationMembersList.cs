
namespace Kistl.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A list of names of members to serialize.
    /// </summary>
    /// This will be filled while building a data type. In the end, the 
    /// ToStream and FromStream methods are built from this list.
    // TODO: enhance with versioning and type information
    public class SerializationMembersList 
        : List<SerializationMember>
    {
        /// <summary>
        /// Implicitely creates a <see cref="SerializationMember"/> to store 
        /// the given parameters.
        /// </summary>
        public void Add(string templatename, SerializerType type, string xmlns, string xmlname, params object[] templateparams)
        {
            this.Add(new SerializationMember(templatename, type, xmlns, xmlname, templateparams));
        }

        /// <summary>
        /// Add a SimpleFieldSerialization entry for this membername
        /// </summary>
        public void Add(SerializerType type, string xmlns, string xmlname, string membername)
        {
            this.Add(new SerializationMember("Serialization.SimpleFieldSerialization", type, xmlns, xmlname, membername));
        }
    }
}
