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

            var parent = cls.BaseObjectClass;
            var child = cls;

            AssociationName = Construct.InheritanceAssociationName(parent, child);

            ParentRoleName = Construct.AssociationParentRoleName(parent);
            ChildRoleName = Construct.AssociationChildRoleName(child);

            ParentEntitySetName = parent.Name;
            ChildEntitySetName = child.Name;
        }

        public string AssociationName { get; private set; }

        public string ParentRoleName { get; private set; }
        public string ChildRoleName { get; private set; }

        public string ParentEntitySetName { get; private set; }
        public string ChildEntitySetName { get; private set; }

    }

}
