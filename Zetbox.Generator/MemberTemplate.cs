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

namespace Zetbox.Generator
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Generator.Extensions;

    public class MemberTemplate
        : ResourceTemplate
    {
        protected MemberTemplate(Arebis.CodeGeneration.IGenerationHost host)
            : base(host)
        {
        }

        protected string GetModifiers()
        {
            MemberAttributes attrs = ModifyMemberAttributes(MemberAttributes.Public | MemberAttributes.Final);
            return attrs.ToCsharp();
        }

        /// <summary>
        /// Gets a set of <see cref="MemberAttributes"/> for this method and returns an appropriate set for output.
        /// </summary>
        protected virtual MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return memberAttributes;
        }
    }
}
