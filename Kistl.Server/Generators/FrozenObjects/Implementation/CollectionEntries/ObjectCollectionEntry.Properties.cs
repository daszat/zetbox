using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyIdPropertyTemplate()
        {
            // nothing todo, inherited from BaseFrozenObject
        }

        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            Templates.Implementation.ObjectClasses.NotifyingValueProperty.Call(Host, ctx,
                    this.MembersToSerialize,
                    relEnd.Type.GetDataTypeString(),
                    propertyName, this.rel.A.Type.Module.Namespace);

            // HACK
            // TODO clean this up
            if (propertyName == "A")
                this.WriteLine("        public int fk_A { get { return A.ID; } set { throw new ReadOnlyException(); } }");
        }

        protected override void ApplyIndexPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);

            if (rel.NeedsPositionStorage(endRole))
            {
                this.MembersToSerialize.Add(SerializerType.All, this.rel.A.Type.Module.Namespace, endRole + Kistl.API.Helper.PositionSuffix, "_" + endRole + Kistl.API.Helper.PositionSuffix);
                this.WriteObjects("public int? ", endRole, "Index { get { return ",
                    endRole, Kistl.API.Helper.PositionSuffix, "; } set { ",
                    endRole, Kistl.API.Helper.PositionSuffix, " = value; } }");
            }
            else if (IsOrdered())
            {
                this.WriteLine("/// <summary>ignored implementation for INewListEntry</summary>");
                this.WriteObjects("public int? ", endRole, "Index { get { return null; } set { } }");
            }
        }
    }
}
