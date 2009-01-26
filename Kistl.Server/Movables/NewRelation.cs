using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.API;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Movables
{

    /// <summary>
    /// Describes a relation between two entities. The Right entity always is a ObjectClass.
    /// </summary>
    public class NewRelation 
    {

        public NewRelation(RelationEnd left, RelationEnd right)
        {
            if (left.Container != null)
                throw new ArgumentException("left", "cannot re-use RelationEnd");
            if (right.Container != null)
                throw new ArgumentException("right", "cannot re-use RelationEnd");

            right.Container = left.Container = this;
            this.Left = left;
            this.Right = right;

            this.ID = MaxId++;
        }

        private static int MaxId = 1;
        public int ID { get; private set; }

        public RelationEnd Right { get; private set; }
        public RelationEnd Left { get; private set; }

        public RelationEnd GetEnd(ObjectReferenceProperty p)
        {
            if (Right.Navigator == p)
            {
                return Right;
            }
            else if (Left.Navigator == p)
            {
                return Left;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Calculates a set of allowed StorageHints for this Relation.
        /// </summary>
        public ICollection<StorageHint> GetAllowedStoragePossibilities()
        {
            if (Right.Multiplicity.UpperBound() == 1 && Left.Multiplicity.UpperBound() == 1)
            {
                // 1:1 relations can be hinted left, right, both or separate
                return new HashSet<StorageHint>() { 
                    StorageHint.MergeLeft, 
                    StorageHint.MergeRight, 
                    StorageHint.Replicate, 
                    StorageHint.Separate 
                };
            }
            else if (Right.Multiplicity.UpperBound() > 1 && Left.Multiplicity.UpperBound() == 1)
            {
                // 1:N relations can only be attached to the right end
                return new HashSet<StorageHint>() { 
                    StorageHint.MergeRight, 
                    StorageHint.Separate 
                };
            }
            else if (Right.Multiplicity.UpperBound() == 1 && Left.Multiplicity.UpperBound() > 1)
            {
                // N:1 relations can only be attached to the left end
                return new HashSet<StorageHint>() { 
                    StorageHint.MergeLeft, 
                    StorageHint.Separate 
                };
            }
            else if (Right.Multiplicity.UpperBound() > 1 && Left.Multiplicity.UpperBound() > 1)
            {
                // N:M relations can only be implemented with a separate relation store
                return new HashSet<StorageHint>() { 
                    StorageHint.Separate 
                };
            }

            // this means that UpperBound() < 1 for some end
            return new HashSet<StorageHint>() { StorageHint.NoHint };
        }



        public static IQueryable<NewRelation> GetAll(IKistlContext ctx)
        {
            if (_AllRels == null)
                _AllRels = CalculateAllObjectObjectRelations(ctx).Concat(CalculateAllObjectValueRelations(ctx)).ToList();

            // clone list to avoid external changes
            return _AllRels.ToList().AsQueryable();
        }

        private static List<NewRelation> _AllRels = null;

        private static List<NewRelation> CalculateAllObjectValueRelations(IKistlContext ctx)
        {
            var result = new List<NewRelation>();

            foreach (var prop in ctx.GetQuery<ValueTypeProperty>().Where(p => p.IsList))
            {
                result.Add(new NewRelation(
                    // value end
                    new RelationEnd() {
                        Navigator = null,
                        Referenced = new TypeMoniker(prop.GetPropertyTypeString()),
                        Multiplicity = Multiplicity.ZeroOrMore,
                        RoleName = "B_" + prop.PropertyName,
                        DebugCreationSite = "Left, value list, prop ID=" + prop.ID
                    },
                    prop.ToRelationEnd("A_Parent" , "Right, value list, prop ID=" + prop.ID))
                    );
            }

            return result;
        }

        private static List<NewRelation> CalculateAllObjectObjectRelations(IKistlContext ctx)
        {
            HashSet<Relation> processedRels = new HashSet<Relation>();
            var result = new List<NewRelation>();

            // ignore non-ObjectClass Properties for now
            foreach (var prop in ctx.GetQuery<ObjectReferenceProperty>().Where(p => p.ObjectClass is ObjectClass))
            {
                var relation = prop.GetRelation();
                if (relation == null)
                {
                    // No "other" property defined 
                    result.Add(new NewRelation(
                        // "open" end
                        new RelationEnd()
                        {
                            Navigator = null,
                            Referenced = ((ObjectClass)prop.ReferenceObjectClass).GetTypeMoniker(),
                            Multiplicity = Multiplicity.ZeroOrMore,
                            RoleName = "B_" + prop.PropertyName,
                            DebugCreationSite = "Left, no Relation, prop ID=" + prop.ID
                        },
                        // IsList or not, but contains the Navigator
                        prop.ToRelationEnd("A_" + prop.ObjectClass.ClassName, "Right, no Relation, prop ID=" + prop.ID)
                    ));
                }
                else
                {
                    if (!processedRels.Contains(relation))
                    {
                        processedRels.Add(relation);
                        result.Add(relation.ToNewRelation());
                    }
                    else
                    {
                        // skip already processed relation
                    }
                }

            }

            return result;
        }

        private static IDictionary<Property, NewRelation> _PropertyToRelation;

        public static NewRelation Lookup(IKistlContext ctx, Property prop)
        {
            if (_PropertyToRelation == null)
            {
                var result = new Dictionary<Property, NewRelation>();
                foreach (var rel in GetAll(ctx))
                {
                    result[rel.Right.Navigator] = rel;
                    if (rel.Left.Navigator != null)
                        result[rel.Left.Navigator] = rel;

                }
                _PropertyToRelation = result;
            }
            return _PropertyToRelation[prop];
        }

    }


}
