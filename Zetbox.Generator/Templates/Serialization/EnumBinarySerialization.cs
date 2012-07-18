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

namespace Zetbox.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public partial class EnumBinarySerialization
    {
        public static void AddToSerializers(SerializationMembersList list,
            EnumerationProperty prop,
            string backingStoreName)
        {
            string xmlnamespace = prop.Module.Namespace;
            string xmlname = prop.Name;
            string enumerationType = prop.GetElementTypeString();

            AddToSerializers(list,
                prop.DisableExport == true ? Templates.Serialization.SerializerType.Binary : SerializerType.All,
                xmlnamespace,
                xmlname,
                backingStoreName,
                enumerationType);
        }

        public static void AddToSerializers(SerializationMembersList list,
            SerializerType type,
            string xmlnamespace,
            string xmlname,
            string backingStoreName,
            string enumerationType)
        {
            if (list != null)
            {
                list.Add("Serialization.EnumBinarySerialization", type, xmlnamespace, xmlname, backingStoreName, enumerationType);
            }
        }
    }
}