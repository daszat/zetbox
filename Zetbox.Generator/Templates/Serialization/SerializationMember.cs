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

namespace Zetbox.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class SerializationMember
    {
        public SerializationMember(string templatename, SerializerType type, string xmlns, string xmlname, params object[] templateparams)
        {
            if (templatename.Contains("EagerLoadingSerialization"))
            {
            }
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
