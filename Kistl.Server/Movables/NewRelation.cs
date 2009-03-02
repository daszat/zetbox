//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Kistl.App.Base;
//using Kistl.API;
//using Kistl.Server.Generators;
//using Kistl.Server.Generators.Extensions;

//namespace Kistl.Server.Movables
//{
//    /// <summary>
//    /// Describes a relation between two entities. The B entity always is a ObjectClass.
//    /// </summary>
//    public class NewRelation
//    {

//        public NewRelation(RelationEnd a, RelationEnd b)
//        {
//            if (a.Container != null)
//                throw new ArgumentException("a", "cannot re-use RelationEnd");
//            if (b.Container != null)
//                throw new ArgumentException("b", "cannot re-use RelationEnd");

//            if (a.Role != RelationEndRole.A)
//                throw new ArgumentException("a", "wrong role");
//            if (b.Role != RelationEndRole.B)
//                throw new ArgumentException("b", "wrong role");

//            this.ID = MaxId++;
//            b.Container = a.Container = this;

//            this.A = a;
//            this.A.Other = b;

//            this.B = b;
//            this.B.Other = a;

//        }

//        private static int MaxId = 1;
//        public int ID { get; private set; }

//        public RelationEnd A { get; private set; }
//        public RelationEnd B { get; private set; }

//        public RelationEnd GetEnd(RelationEndRole role)
//        {
//            switch (role)
//            {
//                case RelationEndRole.A:
//                    return this.A;
//                case RelationEndRole.B:
//                    return this.B;
//                default:
//                    throw new ArgumentOutOfRangeException("role");
//            }
//        }
//        public RelationEnd GetEnd(Property p)
//        {
//            if (A.Navigator.ID == p.ID)
//            {
//                return A;
//            }
//            else if (B.Navigator.ID == p.ID)
//            {
//                return B;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        public RelationEnd GetOtherEnd(Property p)
//        {
//            if (A.Navigator.ID == p.ID)
//            {
//                return B;
//            }
//            else if (B.Navigator.ID == p.ID)
//            {
//                return A;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// Calculates a set of allowed StorageHints for this Relation.
//        /// </summary>
//        public ICollection<StorageHint> GetAllowedStoragePossibilities()
//        {
//            if (B.Multiplicity.UpperBound() == 1 && A.Multiplicity.UpperBound() == 1)
//            {
//                // 1:1 relations can be hinted left, right, both or separate
//                return new HashSet<StorageHint>() { 
//                    StorageHint.MergeA, 
//                    StorageHint.MergeB, 
//                    StorageHint.Replicate, 
//                    StorageHint.Separate 
//                };
//            }
//            else if (B.Multiplicity.UpperBound() > 1 && A.Multiplicity.UpperBound() == 1)
//            {
//                // 1:N relations can only be attached to the right end
//                return new HashSet<StorageHint>() { 
//                    StorageHint.MergeB, 
//                    StorageHint.Separate 
//                };
//            }
//            else if (B.Multiplicity.UpperBound() == 1 && A.Multiplicity.UpperBound() > 1)
//            {
//                // N:1 relations can only be attached to the left end
//                return new HashSet<StorageHint>() { 
//                    StorageHint.MergeA, 
//                    StorageHint.Separate 
//                };
//            }
//            else if (B.Multiplicity.UpperBound() > 1 && A.Multiplicity.UpperBound() > 1)
//            {
//                // N:M relations can only be implemented with a separate relation store
//                return new HashSet<StorageHint>() { 
//                    StorageHint.Separate 
//                };
//            }

//            // this means that UpperBound() < 1 for some end
//            return new HashSet<StorageHint>() { StorageHint.NoHint };
//        }



//        public static IQueryable<NewRelation> GetAll(IKistlContext ctx)
//        {
//            if (_AllRels == null)
//                _AllRels = CalculateAllObjectObjectRelations(ctx).Concat(CalculateAllObjectValueRelations(ctx)).ToList();

//            // clone list to avoid external changes
//            return _AllRels.ToList().AsQueryable();
//        }

//        private static List<NewRelation> _AllRels = null;

//        private static List<NewRelation> CalculateAllObjectValueRelations(IKistlContext ctx)
//        {
//            var result = new List<NewRelation>();

//            //foreach (var prop in ctx.GetQuery<ValueTypeProperty>().Where(p => p.IsList))
//            //{
//            //    result.Add(new NewRelation(
//            //        // value end
//            //        new RelationEnd()
//            //        {
//            //            Navigator = null,
//            //            Type = new TypeMoniker(prop.GetPropertyTypeString()),
//            //            Multiplicity = Multiplicity.ZeroOrMore,
//            //            RoleName = prop.PropertyName,
//            //            HasPersistentOrder = prop.IsIndexed,
//            //            DebugCreationSite = "A, value list, prop ID=" + prop.ID
//            //        },
//            //        prop.ToRelationEnd("A_Parent", "B, value list, prop ID=" + prop.ID))
//            //        );
//            //}

//            return result;
//        }

//        private static List<NewRelation> CalculateAllObjectObjectRelations(IKistlContext ctx)
//        {
//            HashSet<Relation> processedRels = new HashSet<Relation>();
//            var result = new List<NewRelation>();

//            // ignore non-ObjectClass Properties for now
//            foreach (var prop in ctx.GetQuery<ObjectReferenceProperty>().Where(p => p.ObjectClass is ObjectClass))
//            {
//                var relation = prop.GetRelation();
//                if (relation == null)
//                {
//                    // No "other" property defined 
//                    result.Add(new NewRelation(
//                        // IsList or not, but contains the Navigator
//                        new RelationEnd(RelationEndRole.A)
//                        {
//                            Navigator = prop,
//                            Type = ((ObjectClass)prop.ObjectClass).GetTypeMoniker(),
//                            RootType = ((ObjectClass)prop.ObjectClass).GetRootClass().GetTypeMoniker(),
//                            Multiplicity = Multiplicity.ZeroOrMore,
//                            RoleName = prop.ObjectClass.ClassName,
//                            HasPersistentOrder = false,
//                            DebugCreationSite = "A, no Relation, prop ID=" + prop.ID
//                        },
//                        // "open" end
//                        new RelationEnd(RelationEndRole.B)
//                        {
//                            Navigator = null,
//                            Type = ((ObjectClass)prop.ReferenceObjectClass).GetTypeMoniker(),
//                            RootType = ((ObjectClass)prop.ReferenceObjectClass).GetRootClass().GetTypeMoniker(),
//                            Multiplicity = prop.IsList ? Multiplicity.ZeroOrMore : Multiplicity.ZeroOrOne,
//                            RoleName = prop.PropertyName,
//                            HasPersistentOrder = prop.IsIndexed,
//                            DebugCreationSite = "B, no Relation, prop ID=" + prop.ID
//                        }
//                    ));
//                }
//                else
//                {
//                    if (!processedRels.Contains(relation))
//                    {
//                        processedRels.Add(relation);
//                        result.Add(relation.ToNewRelation());
//                    }
//                    else
//                    {
//                        // skip already processed relation
//                    }
//                }

//            }

//            return result;
//        }

//        private static IDictionary<int, NewRelation> _PropertyToRelation;

//        public static NewRelation Lookup(IKistlContext ctx, Property prop)
//        {
//            if (_PropertyToRelation == null)
//            {
//                var result = new Dictionary<int, NewRelation>();
//                foreach (var rel in GetAll(ctx))
//                {
//                    result[rel.A.Navigator.ID] = rel;
//                    if (rel.B.Navigator != null)
//                        result[rel.B.Navigator.ID] = rel;

//                }
//                _PropertyToRelation = result;
//            }

//            if (_PropertyToRelation.ContainsKey(prop.ID))
//            {
//                return _PropertyToRelation[prop.ID];
//            }
//            else
//            {
//                return null;
//            }
//        }

//    }


//}
