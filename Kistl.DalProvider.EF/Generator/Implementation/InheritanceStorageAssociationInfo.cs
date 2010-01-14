using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.DalProvider.EF.Generator.Implementation
{
    internal class InheritanceStorageAssociationInfo
    {
        public InheritanceStorageAssociationInfo(ObjectClass cls)
        {
            if (cls.BaseObjectClass == null)
                throw new ArgumentOutOfRangeException("cls", "should be a derived ObjectClass");

            Class = cls;
            Parent = Class.BaseObjectClass.GetTypeMoniker();
            Child = Class.GetTypeMoniker();

            AssociationName = Construct.InheritanceAssociationName(Parent, Child);

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
