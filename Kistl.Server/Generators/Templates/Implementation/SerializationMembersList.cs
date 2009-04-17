using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation
{

    public sealed class SerializationMember
    {
        public SerializationMember(string templatename, SerializerType type, params object[] templateparams)
        {
            this.TemplateName = templatename;
            this.SerializerType = type;
            this.TemplateParams = templateparams;
        }

        /// <summary>
        /// which serializer to use
        /// </summary>
        public string TemplateName { get; private set; }

        /// <summary>
        /// which type of serializer
        /// </summary>
        public SerializerType SerializerType { get; private set; }

        /// <summary>
        /// which member to serialize, will be interpreted by template.
        /// </summary>
        public object[] TemplateParams { get; private set; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;

            var other = obj as SerializationMember;

            return (other != null
                && object.Equals(this.TemplateName, other.TemplateName)
                && object.Equals(this.TemplateParams, other.TemplateParams));
        }

        public override int GetHashCode()
        {
            return this.TemplateName.GetHashCode() ^ this.TemplateParams.GetHashCode();
        }
    }

    /// <summary>
    /// A list of names of members to serialize.
    /// </summary>
    /// This will be filled while building a data type. In the end, the 
    /// ToStream and FromStream methods are built from this list.
    // TODO: enhance with versioning and type information
    public class SerializationMembersList : List<SerializationMember>
    {
        /// <summary>
        /// Implicitely creates a <see cref="SerializationMember"/> to store 
        /// the given parameters.
        /// </summary>
        /// <param name="templatename"></param>
        /// <param name="membername"></param>
        public void Add(string templatename, SerializerType type, params object[] templateparams)
        {
            this.Add(new SerializationMember(templatename, type, templateparams));
        }       
        
        /// <summary>
        /// Add a SimpleBinarySerialization entry for this membername
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="membername"></param>
        public void Add(string membername, SerializerType type)
        {
            this.Add(new SerializationMember("Implementation.ObjectClasses.SimpleBinarySerialization", type, membername));
        }
    }
}
