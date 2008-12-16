using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Extensions
{
    public static class Reflection
    {
        public static string ToCsharp(this MemberAttributes attrs)
        {
            List<string> modifiers = new List<string>();
            switch (attrs & MemberAttributes.AccessMask)
            {
                case MemberAttributes.Public:
                    modifiers.Add("public");
                    break;
                case MemberAttributes.Private:
                    modifiers.Add("private");
                    break;
                case MemberAttributes.Assembly:
                    modifiers.Add("internal");
                    break;
                case MemberAttributes.Family:
                    modifiers.Add("protected");
                    break;
                case 0: // no access modifier
                    break;
                default:
                    throw new NotImplementedException();
            }

            if ((attrs & MemberAttributes.Final) == MemberAttributes.Final)
                modifiers.Add("final");

            if ((attrs & MemberAttributes.New) == MemberAttributes.New)
                modifiers.Add("new");

            if ((attrs & MemberAttributes.Override) == MemberAttributes.Override)
                modifiers.Add("override");

            return String.Join(" ", modifiers.ToArray());
        }
    }
}
