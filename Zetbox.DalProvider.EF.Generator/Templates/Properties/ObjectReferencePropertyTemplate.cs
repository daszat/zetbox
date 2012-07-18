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

namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public partial class ObjectReferencePropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string sourceMember, string targetMember, string targetGuidMember, string clsFullName, string assocName, string targetRoleName)
        {
            if (list != null)
            {
                if (relDataTypeExportable && !disableExport)
                {
                    list.Add("Serialization.ObjectReferencePropertySerialization",
                        Templates.Serialization.SerializerType.ImportExport, moduleNamespace, name, sourceMember, targetMember, targetGuidMember, clsFullName, assocName, targetRoleName);
                }
                list.Add("Serialization.ObjectReferencePropertySerialization",
                    Templates.Serialization.SerializerType.Service, moduleNamespace, name, sourceMember, targetMember, targetGuidMember, clsFullName, assocName, targetRoleName);
                if (eagerLoading)
                {
                    list.Add("Serialization.EagerObjectLoadingSerialization",
                        Templates.Serialization.SerializerType.Binary, moduleNamespace, name, sourceMember);
                }
            }
        }
    }
}
