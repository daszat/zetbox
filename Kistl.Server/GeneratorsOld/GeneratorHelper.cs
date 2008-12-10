using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using Kistl.API;
using Kistl.App.Base;
using System.Reflection;

namespace Kistl.Server.GeneratorsOld.Helper
{
    public static class GeneratorHelper
    {
        #region Misc Helper
        public static string CalcColumnName(this string propName, string parentPropName)
        {
            if (string.IsNullOrEmpty(parentPropName)) return propName;
            return parentPropName + "_" + propName;
        }

        public static string CalcForeignKeyColumnName(this string propName, string parentPropName)
        {
            return "fk_" + propName.CalcColumnName(parentPropName);
        }

        public static string CalcListPositionColumnName(this string propName, string parentPropName)
        {
            return propName.CalcForeignKeyColumnName(parentPropName) + "_pos";
        }

        public static string GetKistObjectsName(this TaskEnum task)
        {
            if (task == TaskEnum.Interface)
            {
                return "Kistl.Objects";
            }
            else
            {
                return string.Format(@"Kistl.Objects.{0}", task);
            }
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

        public static CodeTypeReference ToCodeTypeReference(this BaseProperty p, TaskEnum clientServer)
        {
            string propType = p.GetPropertyTypeString();

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
                //else if (p is BackReferenceProperty)
                //{
                //    appendNullable = false;
                //}
                else if (p is StructProperty)
                {
                    appendNullable = false;
                }
                else
                {
                    Type t = Type.GetType(p.GetPropertyTypeString());
                    if (t == null) throw new ArgumentOutOfRangeException(
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

        public static CodeMemberField CreateField(this Property prop, CodeTypeDeclaration c, TaskEnum clientServer)
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

        public static CodeMemberProperty CreateNotifyingProperty(this BaseProperty prop, CodeTypeDeclaration c, TaskEnum clientServer)
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

        public static CodeMemberProperty CreateNotifyingProperty(this CodeTypeDeclaration c, string type, string name,
            string getExpression, string setLValue, string notifier)
        {
            return CreateNotifyingProperty(c, new CodeTypeReference(type), name, getExpression, setLValue, notifier);
        }

        public static CodeMemberProperty CreateNotifyingProperty(this CodeTypeDeclaration c, string type, string name)
        {
            return CreateNotifyingProperty(c, new CodeTypeReference(type), name, "_" + name, "_" + name, name);
        }

        public static CodeMemberProperty CreateNotifyingProperty(this CodeTypeDeclaration c, Type type, string name)
        {
            return CreateNotifyingProperty(c, new CodeTypeReference(type), name, "_" + name, "_" + name, name);
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
            result.GetStatements.AddExpression(@"return {0}", getExpression);

            result.SetStatements.AddExpression("if (IsReadonly) throw new ReadOnlyObjectException()");

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

        public static void AddParamComment(this CodeMemberMethod m, string name, string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            AddComment(m, string.Format("<param name=\"{0}\">", name));
            text.Split('\n').ForEach<string>(s => AddComment(m, s));
            AddComment(m, "</param>");
        }

        public static void AddReturnsComment(this CodeMemberMethod m, string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            AddComment(m, "<returns>");
            text.Split('\n').ForEach<string>(s => AddComment(m, s));
            AddComment(m, "</returns>");
        }

        public static void AddSummaryComment(this CodeTypeDeclaration t, string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            AddComment(t, "<summary>");
            text.Split('\n').ForEach<string>(s => AddComment(t, s));
            AddComment(t, "</summary>");
        }

        public static void AddSummaryComment(this CodeMemberProperty p, string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            AddComment(p, "<summary>");
            text.Split('\n').ForEach<string>(s => AddComment(p, s));
            AddComment(p, "</summary>");
        }

        public static void AddSummaryComment(this CodeMemberField f, string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            AddComment(f, "<summary>");
            text.Split('\n').ForEach<string>(s => AddComment(f, s));
            AddComment(f, "</summary>");
        }

        public static void AddSummaryComment(this CodeMemberMethod m, string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            AddComment(m, "<summary>");
            text.Split('\n').ForEach<string>(s => AddComment(m, s));
            AddComment(m, "</summary>");
        }

        public static CodeCommentStatement AddComment(this CodeTypeDeclaration t, string text)
        {
            CodeCommentStatement comment = new CodeCommentStatement(text, true);
            t.Comments.Add(comment);
            return comment;
        }
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
        public static CodeCommentStatement AddComment(this CodeMemberMethod m, string text)
        {
            CodeCommentStatement comment = new CodeCommentStatement(text, true);
            m.Comments.Add(comment);
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

        // collectionClass.code_property.AddAttribute("System.Diagnostics.DebuggerDisplay", "ID = {_fk_Value}");
        // http://blogs.msdn.com/greggm/archive/2005/11/18/494648.aspx
        // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=91772
        // http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=344925
        public static CodeAttributeDeclaration SetNeverDebuggerBrowsable(this CodeMemberProperty prop)
        {
            return AddAttribute(prop, "System.Diagnostics.DebuggerBrowsable", 
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Diagnostics.DebuggerBrowsableState.Never")));
        }

        public static CodeAttributeDeclaration SetXmlIgnore(this CodeMemberProperty prop)
        {
            return AddAttribute(prop, "XmlIgnore");
        }

        public static void SetIgnoreAttributes(this CodeMemberProperty prop)
        {
            SetXmlIgnore(prop);
            SetNeverDebuggerBrowsable(prop);
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

        #region CreateStatement
        public static CodeSnippetExpression AddExpression(this CodeStatementCollection statements, string text, params object[] args)
        {
            CodeSnippetExpression expression;
            if (args != null && args.Length > 0)
            {
                expression = new CodeSnippetExpression(string.Format(text, args));
            }
            else
            {
                expression = new CodeSnippetExpression(text);
            }

            statements.Add(expression);
            return expression;
        }
        public static CodeSnippetStatement AddStatement(this CodeStatementCollection statements, string text, params object[] args)
        {
            CodeSnippetStatement expression;
            if (args != null && args.Length > 0)
            {
                expression = new CodeSnippetStatement(string.Format(text, args));
            }
            else
            {
                expression = new CodeSnippetStatement(text);
            }

            statements.Add(expression);
            return expression;
        }
        #endregion

        #region CreateInterface
        public static CodeTypeDeclaration CreateInterface(this CodeNamespace ns, string name,
            TypeAttributes typeAttributes, params CodeTypeReference[] baseInterfaces)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(name);
            ns.Types.Add(c);
            c.IsClass = false;
            c.IsInterface = true;
            c.TypeAttributes = typeAttributes | TypeAttributes.Interface;
            baseInterfaces.ForEach<CodeTypeReference>(b => c.BaseTypes.Add(b));
            return c;
        }
        public static CodeTypeDeclaration CreateInterface(this CodeNamespace ns, string name, params string[] baseInterfaces)
        {
            return CreateInterface(ns, name, TypeAttributes.Public, baseInterfaces.Select(b => new CodeTypeReference(b)).ToArray());
        }
        #endregion

        #region CreateEnum
        public static CodeTypeDeclaration CreateEnum(this CodeNamespace ns, string name, TypeAttributes typeAttributes)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(name);
            ns.Types.Add(c);
            c.IsClass = false;
            c.IsEnum = true;
            c.TypeAttributes = typeAttributes;
            return c;
        }
        public static CodeTypeDeclaration CreateEnum(this CodeNamespace ns, string name)
        {
            return CreateEnum(ns, name, TypeAttributes.Public);
        }
        #endregion

        #region CreateClass
        public static CodeTypeDeclaration CreateClass(this CodeNamespace ns, string name, params string[] baseClasses)
        {
            List<CodeTypeReference> baseClassesList = new List<CodeTypeReference>();
            baseClasses.ForEach<string>(b => baseClassesList.Add(new CodeTypeReference(b)));
            return CreateClass(ns, name, TypeAttributes.Public, baseClassesList.ToArray());
        }

        public static CodeTypeDeclaration CreateClass(this CodeNamespace ns, string name,
            params CodeTypeReference[] baseClasses)
        {
            return CreateClass(ns, name, TypeAttributes.Public, baseClasses);
        }

        public static CodeTypeDeclaration CreateSealedClass(this CodeNamespace ns, string name, params string[] baseClasses)
        {
            List<CodeTypeReference> baseClassesList = new List<CodeTypeReference>();
            baseClasses.ForEach<string>(b => baseClassesList.Add(new CodeTypeReference(b)));
            return CreateClass(ns, name, TypeAttributes.Public | TypeAttributes.Sealed, baseClassesList.ToArray());
        }

        public static CodeTypeDeclaration CreateClass(this CodeNamespace ns, string name,
            TypeAttributes typeAttributes, params CodeTypeReference[] baseClasses)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(name);
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = typeAttributes;
            baseClasses.ForEach<CodeTypeReference>(b => c.BaseTypes.Add(b));
            // Add a DebuggerDisplay. The Debugger will not call ToString() anymore on this class
            c.AddAttribute("System.Diagnostics.DebuggerDisplay", ns.Name + "." + name.Replace(API.Helper.ImplementationSuffix, ""));
            return c;
        }

        #endregion

        #region CreateConstructor
        public static CodeConstructor CreateConstructor(this CodeTypeDeclaration c)
        {
            CodeConstructor ctor = new CodeConstructor();
            c.Members.Add(ctor);
            ctor.Attributes = MemberAttributes.Public;
            return ctor;
        }
        #endregion

        #region CheckMethod
        public static bool IsDefaultMethod(this Method method)
        {
            if (method.Module.ModuleName == "KistlBase")
            {
                if (method.MethodName == "ToString"
                    || method.MethodName == "PreSave"
                    || method.MethodName == "PostSave"
                )
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region NeedsPositionColumn
        public static bool NeedsPositionColumn(this Property p)
        {
            if (!p.HasStorage()) return false;

            if (p is ObjectReferenceProperty)
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)p;
                if (objRefProp.IsList == false &&
                    objRefProp.GetRelation() != null &&
                    objRefProp.GetRelationType() == Kistl.API.RelationType.one_n &&
                    objRefProp.GetOpposite().IsIndexed) return true;
            }
            if (p.IsList == true &&
                p.IsIndexed) return true;
            return false;
        }
        #endregion
    }
}
