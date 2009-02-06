using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public abstract partial class Template
    {
        /// <summary>
        /// The list of members to serialize
        /// </summary>
        protected SerializationMembersList MembersToSerialize { get { return _MembersToSerialize; } }
        private SerializationMembersList _MembersToSerialize = new SerializationMembersList();

        protected virtual void ApplyIdPropertyTemplate()
        {
            this.CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);
        }

        protected abstract void ApplyAPropertyTemplate();
        protected abstract void ApplyBPropertyTemplate();


        protected virtual void ApplyAIndexPropertyTemplate()
        {
            this.CallTemplate("Implementation.ObjectClasses.NotifyingValueTypeProperty", ctx,
                "int?", "AIndex");
        }

        protected virtual void ApplyBIndexPropertyTemplate()
        {
            this.CallTemplate("Implementation.ObjectClasses.NotifyingValueTypeProperty", ctx,
                "int?", "BIndex");
        }


    }
}
