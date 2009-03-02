//using System;
//using System.Collections.Generic;
//using System.Data.Metadata.Edm;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;

//using Kistl.API;
//using Kistl.App.Base;
//using Kistl.Server.Generators.Extensions;


///*
// * TODO: Actually, all this should die and become a bunch of polymorphic calls.
// */

//namespace Kistl.Server.Generators.EntityFramework.Implementation
//{
//    public enum Role { A, B }

//    /// <summary>
//    /// Immutable descriptor of a side of an association
//    /// </summary>
//    public class AssociationSideInfo
//    {
//        public AssociationSideInfo(Role r, TypeMoniker t, RelationshipMultiplicity mult, string cMult, string sMult, string storageSet)
//        {
//            this.Role = r;
//            this.Type = t;
//            this.Multiplicity = mult;
//            this.ConceptualMultiplicity = cMult;
//            this.StorageMultiplicity = sMult;
//            this.StorageEntitySet = storageSet;
//        }

//        public TypeMoniker Type { get; private set; }
//        public Role Role { get; private set; }
//        public string RoleName { get { return DefaultRoleName(this.Role, this.Type.ClassName); } }

//        /// <summary>
//        /// The multiplicity of this association's side in the EF Attributes
//        /// </summary>
//        public RelationshipMultiplicity Multiplicity { get; private set; }
        
//        /// <summary>
//        /// The multiplicity of this association's side in the conceptual schema
//        /// </summary>
//        public string ConceptualMultiplicity { get; private set; }

//        /// <summary>
//        /// The multiplicity of this association's side in the storage schema
//        /// </summary>
//        public string StorageMultiplicity { get; private set; }
//        public string StorageEntitySet { get; private set; }

//        public override bool Equals(object obj)
//        {
//            AssociationSideInfo other = obj as AssociationSideInfo;
//            if (other == null)
//                return false;

//            if (this == other)
//                return true;

//            return this.Type.Equals(other.Type)
//                && this.Role.Equals(other.Role)
//                && this.ConceptualMultiplicity.Equals(other.ConceptualMultiplicity)
//                && this.StorageMultiplicity.Equals(other.StorageMultiplicity)
//                && this.StorageEntitySet.Equals(other.StorageEntitySet);
//        }

//        public override int GetHashCode()
//        {
//            return this.Type.GetHashCode()
//                ^ this.Role.GetHashCode()
//                ^ this.ConceptualMultiplicity.GetHashCode()
//                ^ this.StorageMultiplicity.GetHashCode()
//                ^ this.StorageEntitySet.GetHashCode();
//        }

//        public static string DefaultRoleName(Role r, string baseName)
//        {
//            string prefix;
//            switch (r)
//            {
//                case Role.A:
//                    prefix = "A_";
//                    break;
//                case Role.B:
//                    prefix = "B_";
//                    break;
//                default:
//                    throw new InvalidOperationException();
//            }
//            return prefix + baseName;
//        }

//    }


//    public abstract class AssociationInfo
//    {

//        public static AssociationInfo CreateInfo(IKistlContext ctx, ObjectReferenceProperty p)
//        {

//            if (p.IsList && p.HasStorage())
//            {
//                return new ObjectListAssociationInfo(ctx, p);
//            }
//            else if (p.IsList && !p.HasStorage())
//            {
//                return new ObjectListParentAssociationInfo(ctx, p);
//            }
//            else if (!p.IsList && p.HasStorage())
//            {
//                return new ObjectReferenceAssociationInfo(ctx, p);
//            }
//            else if (!p.IsList && !p.HasStorage())
//            {
//                return new ObjectReferenceTargetAssociationInfo(ctx, p);
//            }

//            if (!p.IsAssociation())
//                throw new ArgumentOutOfRangeException("p", "Doesn't describe an association");

//            throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
//        }

//        public static AssociationInfo CreateInfo(IKistlContext ctx, ValueTypeProperty p)
//        {
//            if (!p.IsAssociation())
//                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

//            if (p.IsList)
//            {
//                return new ValueTypeAssociationInfo(ctx, p);
//            }

//            throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
//        }

//        public static AssociationInfo CreateInfo(IKistlContext ctx, Property p)
//        {
//            if (!p.IsAssociation())
//                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

//            if (p is ObjectReferenceProperty)
//            {
//                return CreateInfo(ctx, (ObjectReferenceProperty)p);
//            }
//            else if (p is ValueTypeProperty)
//            {
//                return CreateInfo(ctx, (ValueTypeProperty)p);
//            }

//            throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
//        }

//        protected AssociationInfo(IKistlContext ctx, Property p)
//        {
//            if (!p.IsAssociation())
//                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

//            this.Context = ctx;
//            this.Property = p;
//            this.ForeignKeyColumnName = "none";
//        }

//        protected IKistlContext Context { get; private set; }
//        public Property Property { get; private set; }

//        public AssociationSideInfo ASide { get; protected set; }
//        public AssociationSideInfo BSide { get; protected set; }

//        public TypeMoniker CollectionEntry { get { return Construct.PropertyCollectionEntryType(this.Property); } }
//        /// <summary>
//        /// Compatability method to NewRelation's vocabulary
//        /// </summary>
//        /// <returns></returns>
//        public string GetCollectionEntryClassName() { return this.CollectionEntry.ClassName; }

//        public string PropertyName { get; protected set; }

//        public string AssociationName { get { return Construct.AssociationName(ASide.Type, BSide.Type, PropertyName); } }

//        // in some places ("other" end of IsList ?!?) we need to swap parent and child
//        // so, here we define the two ends of an assosciation as A and B side
//        // and dish out the right one as Parent and Child, below

//        protected abstract bool IsStraight { get; }

//        public AssociationSideInfo Parent { get { return IsStraight ? ASide : BSide; } }
//        public AssociationSideInfo Child { get { return IsStraight ? BSide : ASide; } }

//        public string ForeignKeyColumnName { get; protected set; }

//        /// <summary>
//        /// Creates the "other" direction of a bi-directional Association. This 
//        /// function returns an AssociationInfo for the reverse Part when 
//        /// applicable, else null.
//        /// </summary>
//        public virtual AssociationInfo GetReverse() { return null; }

//        /// <summary>
//        /// Associations implemented with a ephemeral entity (i.e. 
//        /// CollectionEntry) have actually two underlying EF Relationships: 
//        /// from the referring class to the CollectionEntry and from the 
//        /// CollectionEntry to the referred class. This returns the second 
//        /// part of this Relationship or null if not applicable.
//        /// </summary>
//        public virtual AssociationInfo GetSecondPart() { return null; }
//    }

//}
