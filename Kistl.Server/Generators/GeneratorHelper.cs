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

        #region CreateMethod
        public static CodeMemberMethod CreateMethod(this CodeTypeDeclaration c, string name, string returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Final);
        }

        public static CodeMemberMethod CreateVirtualMethod(this CodeTypeDeclaration c, string name, string returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public);
        }

        public static CodeMemberMethod CreateOverrideMethod(this CodeTypeDeclaration c, string name, string returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Override);
        }

        public static CodeMemberMethod CreateMethod(this CodeTypeDeclaration c, string name, Type returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Final);
        }

        public static CodeMemberMethod CreateMethod(this CodeTypeDeclaration c, string name, CodeTypeReference returnType)
        {
            return CreateMethod(c, name, returnType, MemberAttributes.Public | MemberAttributes.Final);
        }

        public static CodeMemberMethod CreateVirtualMethod(this CodeTypeDeclaration c, string name, Type returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public);
        }

        public static CodeMemberMethod CreateOverrideMethod(this CodeTypeDeclaration c, string name, Type returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Override);
        }

        public static CodeMemberMethod CreateMethod(this CodeTypeDeclaration c, string name, CodeTypeReference returnType, MemberAttributes memberAttributes)
        {
            CodeMemberMethod m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Attributes = memberAttributes;
            m.Name = name;
            m.ReturnType = returnType;
            return m;
        }
        #endregion

        #region CreateComments
        public static CodeCommentStatement AddComment(this CodeMemberField f, string text)
        {
            CodeCommentStatement comment = new CodeCommentStatement(text, true);
            f.Comments.Add(comment);
            return comment;
        }
        public static CodeCommentStatement AddComment(this CodeMemberProperty p, string text)
        {
            CodeCommentStatement comment = new CodeCommentStatement(text, true);
            p.Comments.Add(comment);
            return comment;
        }
        public static CodeCommentStatement AddComment(this CodeStatementCollection statements, string text)
        {
            CodeCommentStatement comment = new CodeCommentStatement(text, true);
            statements.Add(comment);
            return comment;
        }
        #endregion

        #region CreateAttributes
        /// <summary>
        /// Add Arguments to CodeAttributeDeclaration. CodeAttributeArgument are added without any further translation. 
        /// Any other Expression will be converted to a CodePrimitiveExpression!
        /// </summary>
        /// <param name="attribute">CodeAttributeDeclaration to add arguments to</param>
        /// <param name="expressions">CodeAttributeArgument or any expression which will be converted to a CodePrimitiveExpression</param>
        public static void AddArguments(this CodeAttributeDeclaration attribute, params object[] expressions)
        {
            if (expressions != null)
            {
                foreach (object e in expressions)
                {
                    if (e is CodeAttributeArgument)
                    {
                        attribute.Arguments.Add((CodeAttributeArgument)e);
                    }
                    else
                    {
                        attribute.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(e)));
                    }
                }
            }
        }

        /// <summary>
        /// Add a CodeAttributeDeclaration. 
        /// <remarks>
        /// <typeparamref name="expressions"/>: CodeAttributeArgument are added without any further translation. 
        /// Any other Expression will be converted to a CodePrimitiveExpression!
        /// </remarks>
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="name"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static CodeAttributeDeclaration AddAttribute(this CodeMemberProperty prop, string name, params object[] expressions)
        {
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(name);
            attribute.AddArguments(expressions);
            prop.CustomAttributes.Add(attribute);
            return attribute;
        }

        /// <summary>
        /// Add a CodeAttributeDeclaration. 
        /// <remarks>
        /// <typeparamref name="expressions"/>: CodeAttributeArgument are added without any further translation. 
        /// Any other Expression will be converted to a CodePrimitiveExpression!
        /// </remarks>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="name"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static CodeAttributeDeclaration AddAttribute(this CodeMemberMethod method, string name, params object[] expressions)
        {
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(name);
            attribute.AddArguments(expressions);
            method.CustomAttributes.Add(attribute);
            return attribute;
        }

        /// <summary>
        /// Add a CodeAttributeDeclaration. 
        /// <remarks>
        /// <typeparamref name="expressions"/>: CodeAttributeArgument are added without any further translation. 
        /// Any other Expression will be converted to a CodePrimitiveExpression!
        /// </remarks>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static CodeAttributeDeclaration AddAttribute(this CodeTypeDeclaration type, string name, params object[] expressions)
        {
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(name);
            attribute.AddArguments(expressions);
            type.CustomAttributes.Add(attribute);
            return attribute;
        }

        /// <summary>
        /// Add a CodeAttributeDeclaration. 
        /// <remarks>
        /// <typeparamref name="expressions"/>: CodeAttributeArgument are added without any further translation. 
        /// Any other Expression will be converted to a CodePrimitiveExpression!
        /// </remarks>
        /// </summary>
        /// <param name="code"></param>
        /// <param name="attributeType"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static CodeAttributeDeclaration AddAttribute(this CodeCompileUnit code, Type attributeType, params object[] expressions)
        {
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(new CodeTypeReference(attributeType));
            attribute.AddArguments(expressions);
            code.AssemblyCustomAttributes.Add(attribute);
            return attribute;
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
