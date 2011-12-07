
namespace Kistl.Generator.Extensions
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;
    using Kistl.App.Extensions;

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
