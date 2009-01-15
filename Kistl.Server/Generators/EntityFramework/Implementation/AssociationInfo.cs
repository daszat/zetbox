using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation
{
    public class AssociationInfo
    {
        public AssociationInfo(Property p)
        {
            if (!p.IsAssociation())
                throw new ArgumentOutOfRangeException("p", "Doesn't describe a relation");

            this.Property = p;

            /*
             * TODO: Actually, all this should die and become a bunch of polymorphic calls.
             */

            if (p.IsObjectReferencePropertyList())
            {
                if (p.HasStorage())
                {
                    // ObjectReferenceProperty List
                    ASide = p.ObjectClass.GetTypeMoniker();
                    BSide = Construct.PropertyCollectionEntryType((ObjectReferenceProperty)p);
                    PropertyName = "fk_Parent";
                }
                else
                {
                    // BackReferenceProperty
                    ASide = p.ObjectClass.GetTypeMoniker();
                    BSide = Construct.AssociationChildType(p);
                    PropertyName = ((ObjectReferenceProperty)p).GetOpposite().PropertyName;
                }
            }
            else if (p.IsObjectReferencePropertySingle())
            {
                if (p.HasStorage())
                {
                    // ObjectReferenceProperty
                    ASide = new TypeMoniker(p.GetPropertyTypeString());
                    BSide = Construct.AssociationChildType(p as ObjectReferenceProperty);
                    PropertyName = p.PropertyName;
                }
                else
                {
                    // ObjectReferenceProperty
                    ASide = p.ObjectClass.GetTypeMoniker();
                    BSide = new TypeMoniker(p.GetPropertyTypeString());
                    PropertyName = ((ObjectReferenceProperty)p).GetOpposite().PropertyName;
                }
            }
            else if (p.IsValueTypePropertyList())
            {
                // ValueTypeProperty List
                ASide = p.ObjectClass.GetTypeMoniker();
                BSide = Construct.PropertyCollectionEntryType((ValueTypeProperty)p);
                PropertyName = "fk_Parent";
            }
            else
            {
                throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
            }
        }

        public Property Property { get; private set; }

        public TypeMoniker ASide { get; private set; }
        public TypeMoniker BSide { get; private set; }

        public TypeMoniker CollectionEntry { get { return Construct.PropertyCollectionEntryType(this.Property); } }

        public string PropertyName { get; private set; }

        public string AssociationName { get { return Construct.AssociationName(ASide, BSide, PropertyName); } }

        // in some places ("other" end of IsList ?!?) we need to swap parent and child
        // so, here we define the two ends of an assosciation as A and B side
        // and dish out the right one as Parent and Child, below

        public string ASideRoleName { get { return "A_" + ASide.ClassName; } }
        public string BSideRoleName { get { return "B_" + BSide.ClassName; } }

        public string ASideMultiplicity { get { return "0..1"; } }
        public string BSideMultiplicity         {
            get
            {
                var orp = this.Property as ObjectReferenceProperty;
                if (orp != null)
                    return orp.GetRelationType() == RelationType.one_one ? "0..1" : "*";
                else
                    return "*";
            }
        }

        private bool IsAParent { get { return Property.IsList || !Property.HasStorage(); } }

        public TypeMoniker Parent { get { return IsAParent ? ASide : BSide; } }
        public TypeMoniker Child { get { return IsAParent ? BSide : ASide; } }

        public string ParentRoleName { get { return IsAParent ? ASideRoleName : BSideRoleName; } }
        public string ChildRoleName { get { return IsAParent ? BSideRoleName : ASideRoleName; } }

        public string ParentMultiplicity { get { return IsAParent ? ASideMultiplicity : BSideMultiplicity; ; } }
        public string ChildMultiplicity { get { return IsAParent ? BSideMultiplicity : ASideMultiplicity; ; } }
    }
}
