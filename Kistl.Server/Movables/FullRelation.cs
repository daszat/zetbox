using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.API;

namespace Kistl.Server.Movables
{

    public class ObjectRelationEnd
    {
        public ObjectRelationEnd()
        {
            DebugCreationSite = "unknown";
        }

        /// <summary>
        /// The ORP to navigate FROM this end of the relation. MAY be null.
        /// </summary>
        public ObjectReferenceProperty Navigator { get; set; }

        /// <summary>
        /// The ObjectClass referenced by this end of the relation. MUST NOT be null.
        /// </summary>
        public ObjectClass Referenced { get; set; }

        /// <summary>
        /// The Multiplicity of this end of the relation
        /// </summary>
        public Multiplicity Multiplicity { get; set; }

        /// <summary>
        /// The role of the referenced ObjectClass in this relation
        /// </summary>
        public string RoleName { get; set; }

        public FullRelation Container { get; internal set; }

        /// <summary>
        /// a debug string attached to describe the site where this was FullRelation was created
        /// </summary>
        public string DebugCreationSite { get; set; }
    }

    public class FullRelation
    {
        public FullRelation(ObjectRelationEnd left, ObjectRelationEnd right)
        {
            right.Container = left.Container = this;
            this.Left = left;
            this.Right = right;

            this.ID = MaxId++;
        }

        private static int MaxId = 1;
        public int ID { get; private set; }

        public ObjectRelationEnd Right { get; private set; }
        public ObjectRelationEnd Left { get; private set; }

        public ObjectRelationEnd GetEnd(ObjectReferenceProperty p)
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



        public static IQueryable<FullRelation> GetAll(IKistlContext ctx)
        {
            if (_AllRels == null)
                _AllRels = CalculateAll(ctx);

            // clone list to avoid external changes
            return _AllRels.ToList().AsQueryable();
        }

        private static List<FullRelation> _AllRels = null;
        private static List<FullRelation> CalculateAll(IKistlContext ctx)
        {
            HashSet<Relation> processedRels = new HashSet<Relation>();
            var result = new List<FullRelation>();

            // ignore non-ObjectClass Properties for now
            foreach (var prop in ctx.GetQuery<ObjectReferenceProperty>().Where(p => p.ObjectClass is ObjectClass))
            {
                var relation = prop.GetRelation();
                if (relation == null)
                {
                    // No "other" property defined 
                    result.Add(new FullRelation(
                        // "open" end
                        new ObjectRelationEnd()
                        {
                            Navigator = null,
                            Referenced = (ObjectClass)prop.ReferenceObjectClass,
                            Multiplicity = Multiplicity.ZeroOrMore,
                            RoleName = "B_" + ((ObjectClass)prop.ObjectClass).ClassName,
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
                        result.Add(relation.ToFullRelation());
                    }
                    else
                    {
                        // skip already processed relation
                    }
                }

            }

            return result;
        }

        private static IDictionary<ObjectReferenceProperty, FullRelation> _PropertyToRelation;

        public static FullRelation Lookup(IKistlContext ctx, ObjectReferenceProperty prop)
        {
            if (_PropertyToRelation == null)
            {
                var result = new Dictionary<ObjectReferenceProperty, FullRelation>();
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
