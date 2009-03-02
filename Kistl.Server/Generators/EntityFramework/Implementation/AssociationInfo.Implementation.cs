using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{

//    /// <summary>
//    /// An association from the list items to the parent
//    /// </summary>
//    public class ObjectListAssociationInfo : AssociationInfo
//    {
//        public ObjectListAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
//            : base(ctx, p)
//        {
//            Debug.Assert(p.HasStorage() && p.IsList);

//            OrProperty = p;

//            this.ASide = new AssociationSideInfo(
//                Role.A,
//                p.ObjectClass.GetTypeMoniker(),
//                RelationshipMultiplicity.ZeroOrOne,
//                "0..1",
//                "0..1",
//                p.ObjectClass.ClassName);

//            var bSideType = Construct.PropertyCollectionEntryType(p);
//            // TODO: redundant data here: IsList <-> RelationType?
//            this.BSide = new AssociationSideInfo(
//                Role.B,
//                bSideType,
//                p.GetRelationType() == RelationType.one_one ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many,
//                p.GetRelationType() == RelationType.one_one ? "0..1" : "*",
//                "*",
//                bSideType.ClassName);

//            this.PropertyName = "fk_Parent";
//            this.ForeignKeyColumnName = Construct.ForeignKeyColumnNameReferencing(p.ObjectClass);
//        }

//        protected ObjectReferenceProperty OrProperty { get; private set; }

//        protected override bool IsStraight { get { return true; } }

//        public override AssociationInfo GetReverse()
//        {
//            var opposite = this.OrProperty.GetOpposite();
//            if (opposite != null)
//            {
//                return AssociationInfo.CreateInfo(this.Context, opposite);
//            }
//            else
//            {
//                return base.GetReverse();
//            }
//        }

//        public override AssociationInfo GetSecondPart()
//        {
//            if (OrProperty.IsList)
//            {
//                // TODO: Create correct AssociationInfo. Probably needs a new Implementation
//                return null;
//            }
//            else
//            {
//                return base.GetSecondPart();
//            }
//        }
//    }

//    /// <summary>
//    /// An association from the parent to the list items
//    /// </summary>
//    public class ObjectListParentAssociationInfo : AssociationInfo
//    {
//        public ObjectListParentAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
//            : base(ctx, p)
//        {
//            Debug.Assert(!p.HasStorage() && p.IsList);

//            this.ASide = new AssociationSideInfo(
//                Role.A,
//                p.ObjectClass.GetTypeMoniker(),
//                RelationshipMultiplicity.ZeroOrOne,
//                "0..1",
//                "0..1",
//                p.ObjectClass.ClassName);

//            var bSideType = p.GetOpposite().IsList ? Construct.PropertyCollectionEntryType(p.GetOpposite()) : new TypeMoniker(p.GetPropertyTypeString());
//            // TODO: redundant data here: IsList <-> RelationType?
//            this.BSide = new AssociationSideInfo(
//                Role.B,
//                bSideType,
//                p.GetRelationType() == RelationType.one_one ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many,
//                p.GetRelationType() == RelationType.one_one ? "0..1" : "*",
//                "0..1",
//                bSideType.ClassName);

//            this.PropertyName = p.GetOpposite().PropertyName;
//        }

//        protected override bool IsStraight { get { return true; } }

//    }

//    /// <summary>
//    /// An association describing a list of values
//    /// </summary>
//    public class ValueTypeAssociationInfo : AssociationInfo
//    {
//        public ValueTypeAssociationInfo(IKistlContext ctx, ValueTypeProperty p)
//            : base(ctx, p)
//        {
//            Debug.Assert(p.IsList);

//            this.ASide = new AssociationSideInfo(
//                Role.A,
//                p.ObjectClass.GetTypeMoniker(),
//                RelationshipMultiplicity.ZeroOrOne,
//                "0..1",
//                "0..1",
//                ((ObjectClass)Property.ObjectClass).GetRootClass().ClassName
//                );

//            TypeMoniker bSideCollectionEntryType = Construct.PropertyCollectionEntryType(p);
//            this.BSide = new AssociationSideInfo(
//                Role.B,
//                bSideCollectionEntryType,
//                RelationshipMultiplicity.Many,
//                "*",
//                "*",
//                bSideCollectionEntryType.ClassName);

//            this.PropertyName = "fk_Parent";
//            this.ForeignKeyColumnName = Construct.ForeignKeyColumnNameReferencing(p.ObjectClass);
//        }

//        protected override bool IsStraight { get { return true; } }
//    }

//    /// <summary>
//    /// An association referencing an object
//    /// </summary>
//    public class ObjectReferenceAssociationInfo : AssociationInfo
//    {
//        public ObjectReferenceAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
//            : base(ctx, p)
//        {
//            Debug.Assert(p.HasStorage() && !p.IsList);

//            this.ASide = new AssociationSideInfo(
//                Role.A,
//                p.ReferenceObjectClass.GetTypeMoniker(),
//                RelationshipMultiplicity.ZeroOrOne,
//                "0..1",
//                "0..1",
//                p.ReferenceObjectClass.ClassName);

//            var bSideType = p.ObjectClass.GetTypeMoniker();
//            // TODO: redundant data here: IsList <-> RelationType?
//            this.BSide = new AssociationSideInfo(
//                Role.B,
//                bSideType,
//                p.GetRelationType() == RelationType.one_one ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many,
//                p.GetRelationType() == RelationType.one_one ? "0..1" : "*",
//                "*",
//                bSideType.ClassName
//                );

//            this.PropertyName = p.PropertyName;
//            this.ForeignKeyColumnName = Construct.ForeignKeyColumnName(p);

//        }

//        // TODO: check whether A and B side can be swapped here and IsStraight removed
//        protected override bool IsStraight { get { return false; } }

//    }

//    /// <summary>
//    /// An association of an object being referenced
//    /// </summary>
//    public class ObjectReferenceTargetAssociationInfo : AssociationInfo
//    {
//        public ObjectReferenceTargetAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
//            : base(ctx, p)
//        {
//            Debug.Assert(!p.HasStorage() && !p.IsList);

//            this.ASide = new AssociationSideInfo(
//                Role.A,
//                p.ObjectClass.GetTypeMoniker(),
//                RelationshipMultiplicity.ZeroOrOne,
//                "0..1",
//                "0..1",
//                p.ReferenceObjectClass.GetRootClass().ClassName);

//            var bSideType = p.ReferenceObjectClass.GetTypeMoniker();
//            // TODO: redundant data here: IsList <-> RelationType?
//            this.BSide = new AssociationSideInfo(
//                Role.B,
//                bSideType,
//                p.GetRelationType() == RelationType.one_one ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many,
//                p.GetRelationType() == RelationType.one_one ? "0..1" : "*",
//                "0..1",
//                p.Context.GetQuery<ObjectClass>().First(c => c.ClassName == bSideType.ClassName).GetRootClass().ClassName);

//            this.PropertyName = p.GetOpposite().PropertyName;
//        }

//        protected override bool IsStraight { get { return true; } }

//    }

    public class InheritanceStorageAssociationInfo
    {
        public InheritanceStorageAssociationInfo(ObjectClass cls)
        {
            if (cls.BaseObjectClass == null)
                throw new ArgumentOutOfRangeException("cls", "should be a derived ObjectClass");

            Class = cls;
            Parent = Class.BaseObjectClass.GetTypeMoniker();
            Child = Class.GetTypeMoniker();

            AssociationName = Construct.AssociationName(Parent, Child, "ID");

            ParentRoleName = "A_" + Parent.ClassName;
            ChildRoleName = "B_" + Child.ClassName;

            ParentEntitySetName = Parent.ClassName;
            ChildEntitySetName = Child.ClassName;
        }

        public ObjectClass Class { get; private set; }
        public TypeMoniker Parent { get; private set; }
        public TypeMoniker Child { get; private set; }

        public string AssociationName { get; private set; }

        public string ParentRoleName { get; private set; }
        public string ChildRoleName { get; private set; }

        public string ParentEntitySetName { get; private set; }
        public string ChildEntitySetName { get; private set; }


    }

}
