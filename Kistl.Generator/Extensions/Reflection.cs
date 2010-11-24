
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

        public static string ReferencedTypeAsCSharp(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            if (prop is BoolProperty)
            {
                return "bool" + (prop.IsNullable() ? "?" : String.Empty);
            }
            else if (prop is IntProperty)
            {
                return "int" + (prop.IsNullable() ? "?" : String.Empty);
            }
            else if (prop is DecimalProperty)
            {
                return "decimal" + (prop.IsNullable() ? "?" : String.Empty);
            }
            else if (prop is DoubleProperty)
            {
                return "double" + (prop.IsNullable() ? "?" : String.Empty);
            }
            else if (prop is DateTimeProperty)
            {
                return "DateTime" + (prop.IsNullable() ? "?" : String.Empty);
            }
            else if (prop is GuidProperty)
            {
                return "Guid" + (prop.IsNullable() ? "?" : String.Empty);
            }
            else if (prop is EnumerationProperty)
            {
                return prop.GetPropertyTypeString() + (prop.IsNullable() ? "?" : String.Empty);
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

        public static string InterfaceTypeAsCSharp(this Property prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            bool isList = false;
            bool isValueType = false;

            if (prop is ValueTypeProperty)
            {
                isList = ((ValueTypeProperty)prop).IsList;
                isValueType = true;
            }
            else if (prop is CompoundObjectProperty)
            {
                isList = ((CompoundObjectProperty)prop).IsList;
            }
            else if (prop is ObjectReferenceProperty)
            {
                isList = ((ObjectReferenceProperty)prop).IsList();
            }

            string result = isList
                ? prop.GetCollectionTypeString()
                : prop.GetPropertyTypeString();

            if (isValueType && prop.IsNullable() && !(prop is StringProperty))
            {
                result += "?";
            }

            return result;
        }

        public static string ReturnedTypeAsCSharp(this BaseParameter param)
        {
            if (param == null) { throw new ArgumentNullException("param"); }

            string result;
            if (param is BoolParameter)
            {
                result = "bool";
            }
            else if (param is IntParameter)
            {
                result = "int";
            }
            else if (param is DoubleParameter)
            {
                result = "double";
            }
            else if (param is DateTimeParameter)
            {
                result = "DateTime";
            }
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
