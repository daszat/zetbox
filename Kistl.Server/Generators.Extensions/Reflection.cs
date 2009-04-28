using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

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

            if ((attrs & MemberAttributes.New) == MemberAttributes.New)
                modifiers.Add("new");

            if ((attrs & MemberAttributes.Override) == MemberAttributes.Override)
            {
                if ((attrs & MemberAttributes.Final) == MemberAttributes.Final)
                {
                    throw new ArgumentException("attrs", "don't know how to handle Final & Override");
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

        public static string ReferencedTypeAsCSharp(this Property prop)
        {
            if (prop is BoolProperty)
            {
                return "bool" + (prop.IsNullable ? "?" : "");
            }
            else if (prop is IntProperty)
            {
                return "int" + (prop.IsNullable ? "?" : "");
            }
            else if (prop is DoubleProperty)
            {
                return "double" + (prop.IsNullable ? "?" : "");
            }
            else if (prop is DateTimeProperty)
            {
                return "DateTime" + (prop.IsNullable ? "?" : "");
            }
            else if (prop is GuidProperty)
            {
                return "Guid" + (prop.IsNullable ? "?" : "");
            }
            else if (prop is EnumerationProperty)
            {
                return prop.GetPropertyTypeString() + (prop.IsNullable ? "?" : "");
            }
            else if (prop is StringProperty)
            {
                return "string";
            }
            else
            {
                return prop.GetPropertyTypeString();
            }
        }

        public static string ReturnedTypeAsCSharp(this BaseParameter param)
        {
            string result;
            if (param is BoolParameter)
            {
                result = "bool"; // +(param.IsNullable ? "?" : "");
            }
            else if (param is IntParameter)
            {
                result = "int"; // + (param.IsNullable ? "?" : "");
            }
            else if (param is DoubleParameter)
            {
                result = "double"; // + (param.IsNullable ? "?" : "");
            }
            else if (param is DateTimeParameter)
            {
                result = "DateTime"; // + (param.IsNullable ? "?" : "");
            }
            //else if (param is EnumerationParameter)
            //{
            //    result = param.GetPropertyTypeString(); // + (param.IsNullable ? "?" : "");
            //}
            else if (param is StringParameter)
            {
                result = "string";
            }
            else
            {
                result = param.GetParameterTypeString();
            }

            if (param.IsList)
            {
                result = "IList<" + result + ">";
            }

            return result;
        }

    }
}
