using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class SerializerTemplate
    {
        protected virtual void ApplySerializer(SerializerDirection direction, SerializationMember member, string streamName)
        {
            this.CallTemplate(member.TemplateName, new object[] { this.ctx, direction, streamName }.Concat(member.TemplateParams).ToArray());
        }
    }
}
