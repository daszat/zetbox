using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.ClientObjects.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
    {
        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string backingName = propertyName + Kistl.API.Helper.ImplementationSuffix;
            string fkName = "fk_" + propertyName;

            ObjectClasses.ObjectReferencePropertyTemplate.Call(Host, ctx, MembersToSerialize,
                propertyName, backingName, fkName, "_" + fkName,
                "/* not member of an interface, 'ownInterface' should not be used */", 
                relEnd.Type.GetDataTypeString(), rel, endRole,
                false, rel.NeedsPositionStorage(endRole));
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
    }
}
