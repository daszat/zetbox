using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.EntityFramework.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyObjectReferenceProperty(RelationEnd relEnd, string propertyName)
        {
            CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                this.MembersToSerialize,
                propertyName, rel.GetCollectionEntryAssociationName(relEnd), relEnd.RoleName,
                relEnd.Type.NameDataObject, relEnd.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
                relEnd.HasPersistentOrder);
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
