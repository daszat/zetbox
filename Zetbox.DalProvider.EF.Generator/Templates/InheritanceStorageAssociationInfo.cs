// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;

namespace Zetbox.DalProvider.Ef.Generator.Templates
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
