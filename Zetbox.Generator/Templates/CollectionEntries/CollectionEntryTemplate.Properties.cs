
namespace Zetbox.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract partial class CollectionEntryTemplate
    {
        /// <summary>
        /// The list of members to serialize
        /// </summary>
        protected Serialization.SerializationMembersList MembersToSerialize { get { return _MembersToSerialize; } }
        private Serialization.SerializationMembersList _MembersToSerialize = new Serialization.SerializationMembersList();

        protected virtual void ApplyIdPropertyTemplate()
        {
            Properties.IdProperty.Call(Host, ctx);
        }

        protected virtual void ApplyExportGuidPropertyTemplate()
        {
            Properties.ExportGuidProperty.Call(Host, ctx, this.MembersToSerialize, GetExportGuidBackingStoreReference());
        }

        protected abstract void ApplyAPropertyTemplate();
        protected abstract void ApplyBPropertyTemplate();

        protected abstract void ApplyAIndexPropertyTemplate();
        protected abstract void ApplyBIndexPropertyTemplate();
    }
}
