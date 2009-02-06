using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyAPropertyTemplate()
        {
            ApplyObjectReferenceProperty(rel.A, "A");
        }

        protected override void ApplyBPropertyTemplate()
        {
            ApplyObjectReferenceProperty(rel.B, "B");
        }

        protected override void ApplyAIndexPropertyTemplate()
        {
            ApplyIndexPropertyTemplate(rel.A, "A");
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            ApplyIndexPropertyTemplate(rel.B, "B");
        }

        /// <summary>
        /// Creates a object reference property with the given propertyName for this RelationEnd
        /// </summary>
        protected abstract void ApplyObjectReferenceProperty(RelationEnd relEnd, string propertyName);


        /// <summary>
        /// Creates a index property for this RelationEnd
        /// </summary>
        /// <param name="side">"A" or "B"</param>
        protected abstract void ApplyIndexPropertyTemplate(RelationEnd relEnd, string side);
    }
}
