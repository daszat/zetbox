using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Movables;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyIdPropertyTemplate()
        {
            // nothing todo, inherited from BaseFrozenObject
        }

        protected override void ApplyObjectReferenceProperty(RelationEnd relEnd, string propertyName)
        {
            this.Host.CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
                    this.MembersToSerialize,
                    relEnd.Type.NameDataObject,
                    propertyName);

            // HACK
            // TODO clean this up
            if (propertyName == "A")
                this.WriteLine("        public int fk_A { get { return A.ID; } set { throw new ReadOnlyException(); } }");
        }

        protected override void ApplyIndexPropertyTemplate(RelationEnd relEnd, string side)
        {
            if (relEnd.HasPersistentOrder)
            {
                this.MembersToSerialize.Add("_" + side + Kistl.API.Helper.PositionSuffix);
                this.WriteObjects("public int? ", side, "Index { get { return ",
                    side, Kistl.API.Helper.PositionSuffix, "; } set { ",
                    side, Kistl.API.Helper.PositionSuffix, " = value; } }");
            }
            else if (IsOrdered())
            {
                this.WriteLine("/// <summary>ignored implementation for INewListEntry</summary>");
                this.WriteObjects("public int? ", side, "Index { get { return null; } set { } }");
            }
        }
    }
}
