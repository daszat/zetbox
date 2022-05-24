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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;

    public partial class PropertyEvents
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Property prop,
            bool isReadOnly)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string eventName = "On" + prop.Name;
            string propType = prop.GetElementTypeString().Result;
            string objType = prop.ObjectClass.GetDataTypeString().Result;

            Call(host, ctx, eventName, propType, objType, true, !isReadOnly);
        }

        protected override System.CodeDom.MemberAttributes ModifyMemberAttributes(System.CodeDom.MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) | MemberAttributes.Static;
        }
    }
}
