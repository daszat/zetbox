
namespace Kistl.DalProvider.EF.Generator.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators;
    using Templates = Kistl.Server.Generators.Templates;

    public partial class ObjectCollectionEntry
    {
        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);
            bool eagerLoading = otherEnd.Navigator != null && otherEnd.Navigator.EagerLoading;

            Implementation.ObjectClasses.ObjectReferencePropertyTemplate.Call(Host, ctx,
                this.MembersToSerialize,
                propertyName, rel.GetRelationAssociationName(endRole), relEnd.RoleName,
                relEnd.Type.GetDataTypeString(), relEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix,
                rel.NeedsPositionStorage(endRole), endRole.ToString() + Kistl.API.Helper.PositionSuffix,
                ImplementsIExportable(), rel.Module.Namespace,
                eagerLoading, false, true);
        }

        protected override void ApplyIndexPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEndFromRole(endRole);

            if (rel.NeedsPositionStorage(endRole))
            {
                this.MembersToSerialize.Add(Templates.Implementation.SerializerType.All, rel.Module.Namespace, endRole + Kistl.API.Helper.PositionSuffix, "_" + endRole + Kistl.API.Helper.PositionSuffix);
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
