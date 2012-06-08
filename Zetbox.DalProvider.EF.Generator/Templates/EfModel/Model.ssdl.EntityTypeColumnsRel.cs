
namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;

    public partial class ModelSsdlEntityTypeColumnsRel
    {
        private void ProcessRelation(Relation rel)
        {
            if (rel.A.Type == rel.B.Type)
            {
                if (rel.A.Type != cls)
                {
                    throw new ArgumentException(String.Format("contains self-Relation {0} which doesn't match the specified ObjectClass {1}", rel, cls), "rel");
                }

                if (rel.HasStorage(RelationEndRole.A))
                {
                    ProcessRelationEnd(rel, rel.A);
                }

                if (rel.HasStorage(RelationEndRole.B))
                {
                    ProcessRelationEnd(rel, rel.B);
                }
            }
            else if (rel.A.Type == cls)
            {
                if (!rel.HasStorage(RelationEndRole.A))
                {
                    throw new ArgumentException(String.Format("contains Relation {0} which doesn't need storage on the A-side", rel, cls), "rel");
                }

                ProcessRelationEnd(rel, rel.A);
            }
            else if (rel.B.Type == cls)
            {
                if (!rel.HasStorage(RelationEndRole.B))
                {
                    throw new ArgumentException(String.Format("contains Relation {0} which doesn't need storage on the B-side", rel, cls), "rel");
                }

                ProcessRelationEnd(rel, rel.B);
            }
            else
            {
                throw new ArgumentException(String.Format("contains Relation {0} which doesn't match the specified ObjectClass {1}", rel, cls), "rel");
            }
        }

        private void ProcessRelationEnd(Relation rel, RelationEnd relEnd)
        {
            var otherEnd = rel.GetOtherEnd(relEnd);

            string propertyName = rel.GetRelationFkNameToEnd(otherEnd);
            bool needPositionStorage = rel.NeedsPositionStorage(relEnd.GetRole());
            string positionColumnName = Construct.ListPositionColumnName(otherEnd, prefix);

            GenerateProperty(
                propertyName,
                needPositionStorage,
                positionColumnName);
        }
    }
}