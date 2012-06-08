
namespace Zetbox.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;

    public partial class SerializerTemplate
    {
        protected virtual void ApplySerializer(SerializerDirection direction, SerializationMember member, string streamName)
        {
            this.CallTemplate(member.TemplateName, new object[] { this.ctx, direction, streamName, member.XmlNamespace, member.XmlName }.Concat(member.TemplateParams).ToArray());
        }
    }
}
