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
            Implementation.ObjectClasses.IdProperty.Call(Host, ctx);
        }

        protected virtual void ApplyExportGuidPropertyTemplate()
        {
            // Will be implemented by relations, not by value collections
        }

        protected abstract void ApplyAPropertyTemplate();
        protected abstract void ApplyBPropertyTemplate();


        protected virtual void ApplyAIndexPropertyTemplate()
        {
            //Implementation.ObjectClasses.NotifyingValueTypeProperty.Call(Host, ctx,
            //    "int?", "AIndex");
        }

        protected virtual void ApplyBIndexPropertyTemplate()
        {
            //    Implementation.ObjectClasses.NotifyingValueTypeProperty.Call(Host, ctx,
            //        "int?", "BIndex");
        }


    }
}
