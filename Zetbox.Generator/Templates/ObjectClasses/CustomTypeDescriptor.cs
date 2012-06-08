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

namespace Zetbox.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Zetbox.API;
    using Zetbox.App.Base;

    public partial class CustomTypeDescriptor
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, ObjectClass cls, string ifName, string implName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            string propertyDescriptorName = host.Settings["propertydescriptorname"];
            host.CallTemplate("ObjectClasses.CustomTypeDescriptor", ctx, cls, ifName, implName, propertyDescriptorName);
        }
    }
}
