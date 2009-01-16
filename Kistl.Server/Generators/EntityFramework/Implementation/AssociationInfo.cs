using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server;
using Kistl.Server.Generators.Extensions;
using System.Diagnostics;


/*
 * TODO: Actually, all this should die and become a bunch of polymorphic calls.
 */

namespace Kistl.Server.Generators.EntityFramework.Implementation
{
    public enum Role { A, B }

    /// <summary>
    /// Immutable descriptor of a side of an association
    /// </summary>
    public class AssociationSideInfo
    {
        public AssociationSideInfo(TypeMoniker t, Role r, string cMult, string sMult, string storageSet)
        {
            this.Type = t;
            this.Role = r;
            this.ConceptualMultiplicity = cMult;
            this.StorageMultiplicity = sMult;
            this.StorageEntitySet = storageSet;
        }

        public TypeMoniker Type { get; private set; }
        public Role Role { get; private set; }
        public string RoleName { get { return DefaultRoleName(this.Role, this.Type.ClassName); } }
        
        /// <summary>
        /// The multiplicity of this association's side in the conceptual schema
        /// </summary>
        public string ConceptualMultiplicity { get; private set; }

        /// <summary>
        /// The multiplicity of this association's side in the storage schema
        /// </summary>
        public string StorageMultiplicity { get; private set; }
        public string StorageEntitySet { get; private set; }

        public override bool Equals(object obj)
        {
            AssociationSideInfo other = obj as AssociationSideInfo;
            if (other == null)
                return false;

            if (this == other)
                return true;

            return this.Type.Equals(other.Type)
                && this.Role.Equals(other.Role)
                && this.ConceptualMultiplicity.Equals(other.ConceptualMultiplicity)
                && this.StorageMultiplicity.Equals(other.StorageMultiplicity)
                && this.StorageEntitySet.Equals(other.StorageEntitySet);
        }

        public override int GetHashCode()
        {
            return this.Type.GetHashCode()
                ^ this.Role.GetHashCode()
                ^ this.ConceptualMultiplicity.GetHashCode()
                ^ this.StorageMultiplicity.GetHashCode()
                ^ this.StorageEntitySet.GetHashCode();
        }

        public static string DefaultRoleName(Role r, string baseName)
        {
            string prefix;
            switch (r)
            {
                case Role.A:
                    prefix = "A_";
                    break;
                case Role.B:
                    prefix = "B_";
                    break;
                default:
                    throw new InvalidOperationException();
            }
            return prefix + baseName;
        }

    }

    /// <summary>
    /// An association from the list items to the parent
    /// </summary>
    public class ObjectListAssociationInfo : AssociationInfo
    {
        public ObjectListAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
            : base(p)
        {
            Debug.Assert(p.HasStorage() && p.IsList);

            this.ASide = new AssociationSideInfo(p.ObjectClass.GetTypeMoniker(), Role.A, "0..1", "0..1", p.ObjectClass.ClassName);

            var bSideType = Construct.PropertyCollectionEntryType(p);
            // TODO: redundant data here: IsList <-> RelationType?
            this.BSide = new AssociationSideInfo(bSideType, Role.B, p.GetRelationType() == RelationType.one_one ? "0..1" : "*", "*", bSideType.ClassName);

            this.PropertyName = "fk_Parent";
            this.ForeignKeyColumnName = Construct.ForeignKeyColumnNameReferencing(p.ObjectClass);
        }

        protected override bool IsStraight { get { return true; } }

    }

    /// <summary>
    /// An association from the parent to the list items
    /// </summary>
    public class ObjectListParentAssociationInfo : AssociationInfo
    {
        public ObjectListParentAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
            : base(p)
        {
            Debug.Assert(!p.HasStorage() && p.IsList);

            this.ASide = new AssociationSideInfo(p.ObjectClass.GetTypeMoniker(), Role.A, "0..1", "0..1", "none");

            var bSideType = p.GetOpposite().IsList ? Construct.PropertyCollectionEntryType(p.GetOpposite()) : new TypeMoniker(p.GetPropertyTypeString());
            // TODO: redundant data here: IsList <-> RelationType?
            this.BSide = new AssociationSideInfo(bSideType, Role.B, p.GetRelationType() == RelationType.one_one ? "0..1" : "*", "0..1", "none");

            this.PropertyName = p.GetOpposite().PropertyName;
        }

        protected override bool IsStraight { get { return true; } }

    }

    /// <summary>
    /// An association describing a list of values
    /// </summary>
    public class ValueTypeAssociationInfo : AssociationInfo
    {
        public ValueTypeAssociationInfo(IKistlContext ctx, ValueTypeProperty p)
            : base(p)
        {
            Debug.Assert(p.IsList);

            this.ASide = new AssociationSideInfo(p.ObjectClass.GetTypeMoniker(), Role.A, "0..1", "0..1", ((ObjectClass)Property.ObjectClass).GetRootClass().ClassName);

            TypeMoniker bSideCollectionEntryType = Construct.PropertyCollectionEntryType(p);
            this.BSide = new AssociationSideInfo(bSideCollectionEntryType, Role.B, "*", "*", bSideCollectionEntryType.ClassName);

            this.PropertyName = "fk_Parent";
            this.ForeignKeyColumnName = Construct.ForeignKeyColumnNameReferencing(p.ObjectClass);
        }

        protected override bool IsStraight { get { return true; } }
    }

    /// <summary>
    /// An association referencing an object
    /// </summary>
    public class ObjectReferenceAssociationInfo : AssociationInfo
    {
        public ObjectReferenceAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
            : base(p)
        {
            Debug.Assert(p.HasStorage() && !p.IsList);

            this.ASide = new AssociationSideInfo(p.ReferenceObjectClass.GetTypeMoniker(), Role.A, "0..1", "0..1", p.ReferenceObjectClass.ClassName);

            var bSideType = p.ObjectClass.GetTypeMoniker();
            // TODO: redundant data here: IsList <-> RelationType?
            this.BSide = new AssociationSideInfo(
                bSideType,
                Role.B,
                p.GetRelationType() == RelationType.one_one ? "0..1" : "*", 
                "*", 
                ctx.GetQuery<ObjectClass>().First(c => c.ClassName == bSideType.ClassName).ClassName
                );

            this.PropertyName = p.PropertyName;
            this.ForeignKeyColumnName = Construct.ForeignKeyColumnName(p);

        }

        // TODO: check whether A and B side can be swapped here and IsStraight removed
        protected override bool IsStraight { get { return false; } }

    }

    /// <summary>
    /// An association of an object being referenced
    /// </summary>
    public class ObjectReferenceTargetAssociationInfo : AssociationInfo
    {
        public ObjectReferenceTargetAssociationInfo(IKistlContext ctx, ObjectReferenceProperty p)
            : base(p)
        {
            Debug.Assert(!p.HasStorage() && !p.IsList);

            this.ASide = new AssociationSideInfo(p.ObjectClass.GetTypeMoniker(), Role.A, "0..1", "0..1", p.ReferenceObjectClass.GetRootClass().ClassName);

            var bSideType = p.ReferenceObjectClass.GetTypeMoniker();
            // TODO: redundant data here: IsList <-> RelationType?
            this.BSide = new AssociationSideInfo(bSideType, Role.B, p.GetRelationType() == RelationType.one_one ? "0..1" : "*", "0..1", p.Context.GetQuery<ObjectClass>().First(c => c.ClassName == bSideType.ClassName).GetRootClass().ClassName);

            this.PropertyName = p.GetOpposite().PropertyName;
        }

        protected override bool IsStraight { get { return true; } }

    }


    public abstract class AssociationInfo
    {

        public static AssociationInfo CreateInfo(IKistlContext ctx, ObjectReferenceProperty p)
        {
            if (!p.IsAssociation())
                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

            if (p.IsList && p.HasStorage())
            {
                return new ObjectListAssociationInfo(ctx, p);
            }
            else if ( p.IsList && !p.HasStorage())
            {
                return new ObjectListParentAssociationInfo(ctx, p);
            }
            else if (!p.IsList && p.HasStorage())
            {
                return new ObjectReferenceAssociationInfo(ctx, p);
            }
            else if (!p.IsList && !p.HasStorage())
            {
                return new ObjectReferenceTargetAssociationInfo(ctx, p);
            }

            throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
        }

        public static AssociationInfo CreateInfo(IKistlContext ctx, ValueTypeProperty p)
        {
            if (!p.IsAssociation())
                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

            if (p.IsList)
            {
                return new ValueTypeAssociationInfo(ctx, p);
            }

            throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
        }

        public static AssociationInfo CreateInfo(IKistlContext ctx, Property p)
        {
            if (!p.IsAssociation())
                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

            if (p is ObjectReferenceProperty)
            {
                return CreateInfo(ctx, (ObjectReferenceProperty)p);
            }
            else if (p is ValueTypeProperty)
            {
                return CreateInfo(ctx, (ValueTypeProperty)p);
            }

            throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
        }

        protected AssociationInfo(Property p)
        {
            if (!p.IsAssociation())
                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

            this.Property = p;
            this.ForeignKeyColumnName = "none";
        }

        public Property Property { get; private set; }

        public AssociationSideInfo ASide { get; protected set; }
        public AssociationSideInfo BSide { get; protected set; }

        public TypeMoniker CollectionEntry { get { return Construct.PropertyCollectionEntryType(this.Property); } }

        public string PropertyName { get; protected set; }

        public string AssociationName { get { return Construct.AssociationName(ASide.Type, BSide.Type, PropertyName); } }

        // in some places ("other" end of IsList ?!?) we need to swap parent and child
        // so, here we define the two ends of an assosciation as A and B side
        // and dish out the right one as Parent and Child, below

        protected abstract bool IsStraight { get; }

        public AssociationSideInfo Parent { get { return IsStraight ? ASide : BSide; } }
        public AssociationSideInfo Child { get { return IsStraight ? BSide : ASide; } }

        public string ForeignKeyColumnName { get; protected set; }

    }

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
