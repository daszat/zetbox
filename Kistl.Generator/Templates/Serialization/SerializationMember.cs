
namespace Kistl.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class SerializationMember
    {
        public SerializationMember(string templatename, SerializerType type, string xmlns, string xmlname, params object[] templateparams)
        {
            this.TemplateName = templatename;
            this.SerializerType = type;
            this.TemplateParams = templateparams;
            this.XmlNamespace = xmlns;
            this.XmlName = xmlname;
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
        /// XML Namespace
        /// </summary>
        public string XmlNamespace { get; private set; }

        /// <summary>
        /// XML Tag name
        /// </summary>
        public string XmlName { get; private set; }

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
}
