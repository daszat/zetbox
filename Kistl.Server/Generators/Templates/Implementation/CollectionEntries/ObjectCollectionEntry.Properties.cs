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
