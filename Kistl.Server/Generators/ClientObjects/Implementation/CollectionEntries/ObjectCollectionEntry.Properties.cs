using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyObjectReferenceProperty(RelationEnd relEnd, string propertyName)
        {
            ObjectClasses.ObjectReferencePropertyTemplate.Call(Host, ctx, MembersToSerialize,
               propertyName, relEnd.Type.NameDataObject, relEnd.Container, relEnd);
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
