using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyAPropertyTemplate()
        {
            ApplyObjectReferenceProperty(rel, RelationEndRole.A, "A");
        }

        protected override void ApplyBPropertyTemplate()
        {
            ApplyObjectReferenceProperty(rel, RelationEndRole.B, "B");
        }

        protected override void ApplyAIndexPropertyTemplate()
        {
            ApplyIndexPropertyTemplate(rel, RelationEndRole.A);
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            ApplyIndexPropertyTemplate(rel, RelationEndRole.B);
        }

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            ReloadReferences(RelationEndRole.A);
            this.WriteLine();

            ReloadReferences(RelationEndRole.B);
        }

        private void ReloadReferences(RelationEndRole endRole)
        {
            RelationEnd relend = rel.GetEndFromRole(endRole);
            Templates.Implementation.ObjectClasses.ReloadOneReference.Call(Host, ctx,
                relend.Type.GetDataTypeString(),
                relend.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix + Settings["extrasuffix"],
                endRole.ToString(),
                endRole.ToString() + Kistl.API.Helper.ImplementationSuffix,
                "_fk_" + endRole.ToString(),
                "_fk_guid_" + endRole.ToString());
        }

        /// <summary>
        /// Creates a object reference property with the given propertyName for this RelationEnd
        /// </summary>
        protected abstract void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName);


        /// <summary>
        /// Creates a index property for this RelationEnd
        /// </summary>
        protected abstract void ApplyIndexPropertyTemplate(Relation rel, RelationEndRole endRole);
    }
}
