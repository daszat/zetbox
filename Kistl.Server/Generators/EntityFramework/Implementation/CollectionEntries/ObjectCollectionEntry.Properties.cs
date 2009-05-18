using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.EntityFramework.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            Implementation.ObjectClasses.ObjectReferencePropertyTemplate.Call(Host, ctx,
                this.MembersToSerialize,
                propertyName, rel.GetCollectionEntryAssociationName(endRole), relEnd.RoleName,
                relEnd.Type.GetDataTypeString(), relEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix,
                rel.NeedsPositionStorage(endRole), false);
        }

        protected override void ApplyIndexPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);

            if (rel.NeedsPositionStorage(endRole))
            {
                // TODO: XML Namespace
                this.MembersToSerialize.Add(SerializerType.All, "http://dasz.at/Kistl", endRole + Kistl.API.Helper.PositionSuffix, "_" + endRole + Kistl.API.Helper.PositionSuffix);
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

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            ReloadReference(RelationEndRole.A);
            this.WriteLine();

            ReloadReference(RelationEndRole.B);
        }

        private void ReloadReference(RelationEndRole endRole)
        {
            RelationEnd relend = rel.GetEnd(endRole);

            this.WriteObjects("\t\t\tif (_fk_", endRole.ToString(), ".HasValue)");
            this.WriteLine();
            this.WriteObjects("\t\t\t\t", endRole.ToString(), "__Implementation__ = (",
                    relend.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix, 
                    ")Context.Find<",
                    relend.Type.GetDataTypeString(),
                    ">(_fk_", endRole.ToString(), ".Value);");
            this.WriteLine();
            this.WriteObjects("\t\t\telse");
            this.WriteLine();
            this.WriteObjects("\t\t\t\t", endRole.ToString(), "__Implementation__ = null;");
            this.WriteLine();
        }

    }
}
