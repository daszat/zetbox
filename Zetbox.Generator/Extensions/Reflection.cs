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

namespace Zetbox.Generator.Extensions
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public static class Reflection
    {
        public static string ToCsharp(this MemberAttributes attrs)
        {
            List<string> modifiers = new List<string>();
            if ((attrs & MemberAttributes.Public) == MemberAttributes.Public)
            {
                modifiers.Add("public");
            }
            if ((attrs & MemberAttributes.Private) == MemberAttributes.Private)
            {
                modifiers.Add("private");
            }
            if ((attrs & MemberAttributes.Assembly) == MemberAttributes.Assembly)
            {
                modifiers.Add("internal");
            }
            if ((attrs & MemberAttributes.Family) == MemberAttributes.Family)
            {
                modifiers.Add("protected");
            }

            if ((attrs & MemberAttributes.Static) == MemberAttributes.Static)
                modifiers.Add("static");

            if ((attrs & MemberAttributes.New) == MemberAttributes.New)
                modifiers.Add("new");

            if ((attrs & MemberAttributes.Override) == MemberAttributes.Override)
            {
                if ((attrs & MemberAttributes.Final) == MemberAttributes.Final)
                {
                    throw new ArgumentOutOfRangeException("attrs", "don't know how to handle Final & Override");
                }
                modifiers.Add("override");
            }
            else
            {
                // need virtual only on non-overriding members

                // "obviously", having _not_ specified Final, yields virtual
                // see e.g. http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/61ac4430-fa5a-4cf2-9932-ad3ae193a6bf/
                if ((attrs & MemberAttributes.Final) != MemberAttributes.Final)
                    modifiers.Add("virtual");
            }

            return String.Join(" ", modifiers.ToArray());
        }
    }
}
