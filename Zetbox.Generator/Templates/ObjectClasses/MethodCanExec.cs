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
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;

    public partial class MethodCanExec
    {
        protected MethodCanExec(Arebis.CodeGeneration.IGenerationHost host)
            : base(host)
        {
            throw new NotSupportedException("this constructor only exists to allow overrinding in a CST");
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            // Methods are usually virtual ...
            var result = base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;

            // ... and override on derived types
            if (this.m.ObjectClass != this.dt)
                result = result | MemberAttributes.Override;

            return result;
        }
    }
}
