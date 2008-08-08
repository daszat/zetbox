using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using Kistl.App.Base;

namespace Kistl.Server.Generators
{
    public static class GeneratorHelper
    {
        #region Misc Helper
        public static string CalcColumnName(this string propName, string parentPropName)
        {
            if (string.IsNullOrEmpty(parentPropName)) return propName;
            return parentPropName + "_" + propName;
        }
        #endregion

        #region ToCodeTypeReference
        public static CodeTypeReference ToCodeTypeReference(this BaseParameter param)
        {
            CodeTypeReference result;

            if (param == null)
            {
                result = new CodeTypeReference(typeof(void));
            }
            else
            {
                if (param.IsList)
                {
                    result = new CodeTypeReference(String.Format("IList<{0}>", param.GetParameterTypeString()));
                }
                else
                {
                    result = new CodeTypeReference(param.GetParameterTypeString());
                }
            }

            return result;
        }

        public static CodeTypeReference ToCodeTypeReference(this BaseProperty p, ClientServerEnum clientServer)
        {
            string propType;
            if (clientServer == ClientServerEnum.Server && p is EnumerationProperty)
            {
                // EF does not support Enums
                // TODO: Change this, when EF Wrapper are implemented
                propType = "System.Int32";
            }
            else
            {
                propType = p.GetPropertyTypeString();
            }

            if (p is Property)
            {
                bool appendNullable;
                if (p is EnumerationProperty)
                {
                    appendNullable = true;
                }
                else if (p is ObjectReferenceProperty)
                {
                    appendNullable = false;
                }
                else if (p is BackReferenceProperty)
                {
                    appendNullable = false;
                }
                else if (p is StructProperty)
                {
                    appendNullable = false;
                }
                else
                {
                    Type t = Type.GetType(p.GetPropertyTypeString());
                    if (t == null) throw new ApplicationException(
                         string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                             p.ObjectClass.ClassName, p.PropertyName, p.GetPropertyTypeString()));

                    appendNullable = t.IsValueType;
                }

                propType += (appendNullable && ((Property)p).IsNullable) ? "?" : "";
            }

            return new CodeTypeReference(propType);
        }
        #endregion

        #region CreateField
        public static CodeMemberField CreateField(this CodeTypeDeclaration c, Type type, string name)
        {
            return CreateField(c, new CodeTypeReference(type), name, null);
        }

        public static CodeMemberField CreateField(this CodeTypeDeclaration c, string type, string name)
        {
            return CreateField(c, new CodeTypeReference(type), name, null);
        }

        public static CodeMemberField CreateField(this CodeTypeDeclaration c, CodeTypeReference type, string name)
        {
            return CreateField(c, type, name, null);
        }

        public static CodeMemberField CreateField(this CodeTypeDeclaration c, Type type, string name, string initExpression)
        {
            return CreateField(c, new CodeTypeReference(type), name, new CodeSnippetExpression(initExpression));
        }

        public static CodeMemberField CreateField(this CodeTypeDeclaration c, string type, string name, string initExpression)
        {
            return CreateField(c, new CodeTypeReference(type), name, new CodeSnippetExpression(initExpression));
        }

        public static CodeMemberField CreateField(this BaseProperty prop, CodeTypeDeclaration c, ClientServerEnum clientServer)
        {
            return c.CreateField(prop.ToCodeTypeReference(clientServer), "_" + prop.PropertyName);
        }

        public static CodeMemberField CreateField(this CodeTypeDeclaration c, CodeTypeReference type, string name, CodeExpression initExpression)
        {
            CodeMemberField f = new CodeMemberField(type, name);
            c.Members.Add(f);
            f.Attributes = MemberAttributes.Private;
            if (initExpression != null)
            {
                f.InitExpression = initExpression;
            }
            return f;
        }
        #endregion

        #region CreateProperty
        /// <summary>
        /// Creates a Property with a Set and Get Accessor and adds it to the given Class
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateProperty(this CodeTypeDeclaration c, Type type, string name)
        {
            return CreateProperty(c, new CodeTypeReference(type), name, true);
        }

        /// <summary>
        /// Creates a Property with a Set and Get Accessor and adds it to the given Class
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateProperty(this CodeTypeDeclaration c, string type, string name)
        {
            return CreateProperty(c, new CodeTypeReference(type), name, true);
        }

        /// <summary>
        /// Creates a Property with a Set and Get Accessor and adds it to the given Class
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateProperty(this CodeTypeDeclaration c, CodeTypeReference type, string name)
        {
            return CreateProperty(c, type, name, true);
        }

        /// <summary>
        /// Creates a Property with a Set and Get Accessor and adds it to the given Class
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <param name="hasSet">true if the Property should have a Set Accessor</param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateProperty(this CodeTypeDeclaration c, Type type, string name, bool hasSet)
        {
            return CreateProperty(c, new CodeTypeReference(type), name, hasSet);
        }

        /// <summary>
        /// Creates a Property with a Set and Get Accessor and adds it to the given Class
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <param name="hasSet">true if the Property should have a Set Accessor</param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateProperty(this CodeTypeDeclaration c, string type, string name, bool hasSet)
        {
            return CreateProperty(c, new CodeTypeReference(type), name, hasSet);
        }

        /// <summary>
        /// Creates a Property with a Set and Get Accessor and adds it to the given Class.
        /// This is the implementation. Derive from it, if you want to change something.
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <param name="hasSet">true if the Property should have a Set Accessor</param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateProperty(this CodeTypeDeclaration c, CodeTypeReference type, string name, bool hasSet)
        {
            CodeMemberProperty p = new CodeMemberProperty();
            c.Members.Add(p);
            p.Type = type;
            p.Name = name;
            p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            p.HasGet = true;
            p.HasSet = hasSet;
            return p;
        }

        public static CodeMemberProperty CreateNotifyingProperty(this BaseProperty prop, CodeTypeDeclaration c, ClientServerEnum clientServer)
        {
            return c.CreateNotifyingProperty(prop.ToCodeTypeReference(clientServer),
                prop.PropertyName, "_" + prop.PropertyName, "_" + prop.PropertyName, prop.PropertyName);
        }

        /// <summary>
        /// Creates a property which calls the appropriate NotifyPropertyChang* Methods.
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <param name="getExpression">a string containing the expression to return on Get</param>
        /// <param name="setLValue">a string containing the LValue where the Set should assign to</param>
        /// <param name="notifier">the Property for which notifications should be sent out, usually the same as <code>name</code></param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateNotifyingProperty(this CodeTypeDeclaration c, Type type, string name,
            string getExpression, string setLValue, string notifier)
        {
            return CreateNotifyingProperty(c, new CodeTypeReference(type), name, getExpression, setLValue, notifier);
        }


        /// <summary>
        /// Creates a property which calls the appropriate NotifyPropertyChang* Methods.
        /// </summary>
        /// <param name="c">Class, where the Property will be added</param>
        /// <param name="type">Type of the Property</param>
        /// <param name="name">Name of the Property</param>
        /// <param name="getExpression">a string containing the expression to return on Get</param>
        /// <param name="setLValue">a string containing the LValue where the Set should assign to</param>
        /// <param name="notifier">the Property for which notifications should be sent out, usually the same as <code>name</code></param>
        /// <returns>Returns a CodeMemberProperty Type</returns>
        public static CodeMemberProperty CreateNotifyingProperty(this CodeTypeDeclaration c, CodeTypeReference type, string name,
            string getExpression, string setLValue, string notifier)
        {
            CodeMemberProperty result = CreateProperty(c, type, name, true);
            result.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return {0}", getExpression)));

            // create a condition to not trigger events if the value doesn't change.
            CodeConditionStatement ccs = new CodeConditionStatement(new CodeSnippetExpression(string.Format("{0} != value", name)));
            ccs.TrueStatements.Add(new CodeSnippetExpression(
                string.Format(@"NotifyPropertyChanging(""{1}""); 
                    {0} = value;
                    NotifyPropertyChanged(""{1}"");", setLValue, notifier)));

            result.SetStatements.Add(ccs);

            return result;
        }
        #endregion

        #region CheckPropertyType
        public static bool IsListProperty(this BaseProperty prop)
        {
            return prop is Property && ((Property)prop).IsList;
        }
        public static bool IsValueTypePropertySingle(this BaseProperty prop)
        {
            return prop is ValueTypeProperty && !((Property)prop).IsList;
        }
        public static bool IsValueTypePropertyList(this BaseProperty prop)
        {
            return prop is ValueTypeProperty && ((Property)prop).IsList;
        }
        public static bool IsEnumerationPropertySingle(this BaseProperty prop)
        {
            return prop is EnumerationProperty && !((Property)prop).IsList;
        }
        public static bool IsEnumerationPropertyPropertyList(this BaseProperty prop)
        {
            return prop is EnumerationProperty && ((Property)prop).IsList;
        }
        public static bool IsObjectReferencePropertySingle(this BaseProperty prop)
        {
            return prop is ObjectReferenceProperty && !((Property)prop).IsList;
        }
        public static bool IsObjectReferencePropertyList(this BaseProperty prop)
        {
            return prop is ObjectReferenceProperty && ((Property)prop).IsList;
        }
        public static bool IsStructPropertySingle(this BaseProperty prop)
        {
            return prop is StructProperty && !((Property)prop).IsList;
        }
        public static bool IsStructPropertyPropertyList(this BaseProperty prop)
        {
            return prop is StructProperty && ((Property)prop).IsList;
        }
        #endregion
    }
}
