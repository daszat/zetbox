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
                    Parent = p.ObjectClass.GetTypeMoniker();
                    Child = Construct.PropertyCollectionEntryType((ObjectReferenceProperty)p);
                    PropertyName = "fk_Parent";
                }
                else
                {
                    // BackReferenceProperty
                    Parent = p.ObjectClass.GetTypeMoniker();
                    Child = Construct.AssociationChildType(p);
                    PropertyName = ((ObjectReferenceProperty)p).GetOpposite().PropertyName;
                }
            }
            else if (p.IsObjectReferencePropertySingle())
            {
                if (p.HasStorage())
                {
                    // ObjectReferenceProperty
                    Parent = new TypeMoniker(p.GetPropertyTypeString());
                    Child = Construct.AssociationChildType(p as ObjectReferenceProperty);
                    PropertyName = p.PropertyName;
                }
                else
                {
                    // ObjectReferenceProperty
                    Parent = p.ObjectClass.GetTypeMoniker();
                    Child = new TypeMoniker(p.GetPropertyTypeString());
                    PropertyName = ((ObjectReferenceProperty)p).GetOpposite().PropertyName;
                }
            }
            else if (p.IsValueTypePropertyList())
            {
                // ValueTypeProperty List
                Parent = p.ObjectClass.GetTypeMoniker();
                Child = Construct.PropertyCollectionEntryType((ValueTypeProperty)p);
                PropertyName = "fk_Parent";
            }
            else
            {
                throw new InvalidOperationException("Mismatch between IsAssociation() and AssociationInfo's knowledge");
            }
        }

        public Property Property { get; private set; }

        public TypeMoniker Parent { get; private set; }
        public TypeMoniker Child { get; private set; }
        public TypeMoniker CollectionEntry { get { return Construct.PropertyCollectionEntryType(this.Property); } }

        public string PropertyName { get; private set; }

        public string AssociationName { get { return Construct.AssociationName(Parent, Child, PropertyName); } }
        public string ParentRoleName { get { return Construct.AssociationParentRoleName(Parent); } }
        public string ChildRoleName { get { return Construct.AssociationChildRoleName(Child); } }

        public string ParentMultiplicity { get { return "0..1"; } }
        public string ChildMultiplicity
        {
            get
            {
                var orp = this.Property as ObjectReferenceProperty;
                if (orp != null)
                    return orp.GetRelationType() == RelationType.one_one ? "0..1" : "*";
                else
                    return "*";
            }
        }
    }
}
