
namespace Kistl.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;

    public partial class ModelSsdlEntityTypeColumnsRel
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass cls, IEnumerable<Relation> relations, string prefix, ISchemaProvider schemaProvider)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("EfModel.ModelSsdlEntityTypeColumnsRel", ctx, cls, relations, prefix, schemaProvider);
        }

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