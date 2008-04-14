using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;

using Kistl.App.Base;
using System.IO;
using System.Reflection;
using System.Collections;
using Kistl.API;

namespace Kistl.Server.Generators
{
    public class BaseDataObjectGenerator
    {
        private string codeBasePath = "";

        public abstract class CurrentBase : ICloneable
        {
            public Kistl.API.IKistlContext ctx { get; set; }

            public ClientServerEnum clientServer { get; set; }
            public CodeCompileUnit code { get; set; }
            public CodeNamespace code_namespace { get; set; }
            public CodeTypeDeclaration code_class { get; set; }
            public CodeMemberField code_field { get; set; }
            public CodeMemberProperty code_property { get; set; }

            public BaseProperty property { get; set; }

            public abstract object Clone();

            protected virtual void CloneInternal(CurrentBase result)
            {
                result.ctx = this.ctx;

                result.clientServer = this.clientServer;
                result.code = this.code;
                result.code_namespace = this.code_namespace;
                result.code_class = this.code_class;
                result.code_field = this.code_field;
                result.code_property = this.code_property;

                result.property = this.property;
            }
        }

        public class CurrentObjectClass : CurrentBase
        {
            public ObjectClass objClass { get; set; }
            public CodeConstructor code_constructor { get; set; }

            public override object Clone()
            {
                CurrentObjectClass result = new CurrentObjectClass();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentObjectClass)result).code_constructor = this.code_constructor;
                ((CurrentObjectClass)result).objClass = this.objClass;
            }
        }

        public class CurrentInterface : CurrentBase
        {
            public Interface @interface { get; set; }

            public override object Clone()
            {
                CurrentInterface result = new CurrentInterface();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentInterface)result).@interface = this.@interface;
            }
        }

        public class CurrentEnumeration : CurrentBase
        {
            public Enumeration enumeration { get; set; }

            public override object Clone()
            {
                CurrentEnumeration result = new CurrentEnumeration();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentEnumeration)result).enumeration = this.enumeration;
            }
        }


        public virtual void Generate(Kistl.API.IKistlContext ctx, string codeBasePath)
        {
            this.codeBasePath = codeBasePath + (codeBasePath.EndsWith("\\") ? "" : "\\");
            Directory.CreateDirectory(codeBasePath);

            Directory.GetFiles(this.codeBasePath, "*.cs", SearchOption.AllDirectories).
                ToList().ForEach(f => File.Delete(f));

            var objClassList = Generator.GetObjectClassList(ctx);

            foreach (ObjectClass objClass in objClassList)
            {
                GenerateObjectsInternal(new CurrentObjectClass() { ctx = ctx, clientServer = ClientServerEnum.Client, objClass = objClass });
                GenerateObjectsInternal(new CurrentObjectClass() { ctx = ctx, clientServer = ClientServerEnum.Server, objClass = objClass });
            }

            GenerateObjectSerializer(ClientServerEnum.Server, objClassList);
            GenerateObjectSerializer(ClientServerEnum.Client, objClassList);

            var interfaceList = Generator.GetInterfaceList(ctx);

            foreach (Interface i in interfaceList)
            {
                GenerateInterfacesInternal(new CurrentInterface() { ctx = ctx, clientServer = ClientServerEnum.Client, @interface = i });
                GenerateInterfacesInternal(new CurrentInterface() { ctx = ctx, clientServer = ClientServerEnum.Server, @interface = i });
            }

            var enumList = Generator.GetEnumList(ctx);

            foreach (Enumeration e in enumList)
            {
                GenerateEnumerationsInternal(new CurrentEnumeration() { ctx = ctx, clientServer = ClientServerEnum.Client, enumeration = e });
                GenerateEnumerationsInternal(new CurrentEnumeration() { ctx = ctx, clientServer = ClientServerEnum.Server, enumeration = e });
            }

            GenerateAssemblyInfo(ClientServerEnum.Server);
            GenerateAssemblyInfo(ClientServerEnum.Client);
        }

        #region Save / Helper
        protected virtual void SaveFile(CodeCompileUnit code, string fileName)
        {
            string path = Path.GetDirectoryName(codeBasePath + fileName);
            Directory.CreateDirectory(path);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(codeBasePath + fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    code, sourceWriter, options);
            }
        }

        protected virtual void AddDefaultNamespaces(CodeNamespace ns)
        {
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.ObjectModel"));
            ns.Imports.Add(new CodeNamespaceImport("System.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("System.Text"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
            ns.Imports.Add(new CodeNamespaceImport("System.Xml"));
            ns.Imports.Add(new CodeNamespaceImport("System.Xml.Serialization"));
            ns.Imports.Add(new CodeNamespaceImport("Kistl.API"));
        }

        #region CreateField
        protected CodeMemberField CreateField(CodeTypeDeclaration c, Type type, string name)
        {
            return CreateField(c, new CodeTypeReference(type), name, null);
        }

        protected CodeMemberField CreateField(CodeTypeDeclaration c, string type, string name)
        {
            return CreateField(c, new CodeTypeReference(type), name, null);
        }

        protected CodeMemberField CreateField(CodeTypeDeclaration c, CodeTypeReference type, string name)
        {
            return CreateField(c, type, name, null);
        }

        protected CodeMemberField CreateField(CodeTypeDeclaration c, Type type, string name, string initExpression)
        {
            return CreateField(c, new CodeTypeReference(type), name, new CodeSnippetExpression(initExpression));
        }

        protected CodeMemberField CreateField(CodeTypeDeclaration c, string type, string name, string initExpression)
        {
            return CreateField(c, new CodeTypeReference(type), name, new CodeSnippetExpression(initExpression));
        }

        protected virtual CodeMemberField CreateField(CodeTypeDeclaration c, CodeTypeReference type, string name, CodeExpression initExpression)
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
        protected CodeMemberProperty CreateProperty(CodeTypeDeclaration c, Type type, string name)
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
        protected CodeMemberProperty CreateProperty(CodeTypeDeclaration c, string type, string name)
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
        protected CodeMemberProperty CreateProperty(CodeTypeDeclaration c, CodeTypeReference type, string name)
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
        protected CodeMemberProperty CreateProperty(CodeTypeDeclaration c, Type type, string name, bool hasSet)
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
        protected CodeMemberProperty CreateProperty(CodeTypeDeclaration c, string type, string name, bool hasSet)
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
        protected virtual CodeMemberProperty CreateProperty(CodeTypeDeclaration c, CodeTypeReference type, string name, bool hasSet)
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
        #endregion

        #region CreateInterface
        protected virtual CodeTypeDeclaration CreateInterface(CodeNamespace ns, string name,
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
        protected virtual CodeTypeDeclaration CreateInterface(CodeNamespace ns, string name, params CodeTypeReference[] baseInterfaces)
        {
            return CreateInterface(ns, name, TypeAttributes.Public, baseInterfaces);
        }
        #endregion

        #region CreateEnum
        protected virtual CodeTypeDeclaration CreateEnum(CodeNamespace ns, string name, TypeAttributes typeAttributes)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(name);
            ns.Types.Add(c);
            c.IsClass = false;
            c.IsEnum = true;
            c.TypeAttributes = typeAttributes;
            return c;
        }
        protected virtual CodeTypeDeclaration CreateEnum(CodeNamespace ns, string name)
        {
            return CreateEnum(ns, name, TypeAttributes.Public);
        }
        #endregion

        #region CreateClass
        protected CodeTypeDeclaration CreateClass(CodeNamespace ns, string name, params string[] baseClasses)
        {
            List<CodeTypeReference> baseClassesList = new List<CodeTypeReference>();
            baseClasses.ForEach<string>(b => baseClassesList.Add(new CodeTypeReference(b)));
            return CreateClass(ns, name, TypeAttributes.Public, baseClassesList.ToArray());
        }

        protected CodeTypeDeclaration CreateSealedClass(CodeNamespace ns, string name, params string[] baseClasses)
        {
            List<CodeTypeReference> baseClassesList = new List<CodeTypeReference>();
            baseClasses.ForEach<string>(b => baseClassesList.Add(new CodeTypeReference(b)));
            return CreateClass(ns, name, TypeAttributes.Public | TypeAttributes.Sealed, baseClassesList.ToArray());
        }

        protected virtual CodeTypeDeclaration CreateClass(CodeNamespace ns, string name, 
            TypeAttributes typeAttributes, params CodeTypeReference[] baseClasses)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(name);
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = typeAttributes;
            baseClasses.ForEach<CodeTypeReference>(b => c.BaseTypes.Add(b));
            return c;
        }

        #endregion

        #region CreateConstructor
        protected virtual CodeConstructor CreateConstructor(CodeTypeDeclaration c)
        {
            CodeConstructor ctor = new CodeConstructor();
            c.Members.Add(ctor);
            ctor.Attributes = MemberAttributes.Public;
            return ctor;
        }
        #endregion

        #region CreateMethod
        protected CodeMemberMethod CreateMethod(CodeTypeDeclaration c, string name, string returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Final);
        }

        protected CodeMemberMethod CreateVirtualMethod(CodeTypeDeclaration c, string name, string returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public);
        }

        protected CodeMemberMethod CreateOverrideMethod(CodeTypeDeclaration c, string name, string returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Override);
        }

        protected CodeMemberMethod CreateMethod(CodeTypeDeclaration c, string name, Type returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Final);
        }

        protected CodeMemberMethod CreateMethod(CodeTypeDeclaration c, string name, CodeTypeReference returnType)
        {
            return CreateMethod(c, name, returnType, MemberAttributes.Public | MemberAttributes.Final);
        }

        protected CodeMemberMethod CreateVirtualMethod(CodeTypeDeclaration c, string name, Type returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public);
        }

        protected CodeMemberMethod CreateOverrideMethod(CodeTypeDeclaration c, string name, Type returnType)
        {
            return CreateMethod(c, name, new CodeTypeReference(returnType), MemberAttributes.Public | MemberAttributes.Override);
        }

        protected virtual CodeMemberMethod CreateMethod(CodeTypeDeclaration c, string name, CodeTypeReference returnType, MemberAttributes memberAttributes)
        {
            CodeMemberMethod m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Attributes = memberAttributes;
            m.Name = name;
            m.ReturnType = returnType;
            return m;
        }
        #endregion

        #region CreateNamespace
        protected CodeNamespace CreateNamespace(CodeCompileUnit code, string name)
        {
            return CreateNamespace(code, name, true);
        }

        protected virtual CodeNamespace CreateNamespace(CodeCompileUnit code, string name, bool addDefaultNamespaces)
        {
            CodeNamespace ns = new CodeNamespace(name);
            code.Namespaces.Add(ns);
            if (addDefaultNamespaces) AddDefaultNamespaces(ns);

            return ns;
        }
        #endregion

        #endregion

        #region GenerateAssemblyInfo
        protected virtual void GenerateAssemblyInfo(CodeCompileUnit code, ClientServerEnum clientServer)
        {
            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Reflection.AssemblyTitleAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression(string.Format("Kistl.Objects.{0}", clientServer)))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Reflection.AssemblyCompanyAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression("dasz.at"))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Reflection.AssemblyProductAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression("Kistl"))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Reflection.AssemblyCopyrightAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression("Copyright © dasz.at 2008"))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Runtime.InteropServices.ComVisibleAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression(false))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Reflection.AssemblyVersionAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression("1.0.0.0"))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(System.Reflection.AssemblyFileVersionAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression("1.0.0.0"))));

            code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(CLSCompliantAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression(true))));
        }

        private void GenerateAssemblyInfo(ClientServerEnum clientServer)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            GenerateAssemblyInfo(code, clientServer);

            // Generate the code & save
            SaveFile(code, string.Format(@"Kistl.Objects.{0}\AssemblyInfo.cs", clientServer));
        }
        #endregion

        #region GenerateObjectSerializer
        protected virtual void GenerateObjectSerializer(ClientServerEnum clientServer, IQueryable<ObjectClass> objClassList)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            // Create Namespace
            CodeNamespace ns = CreateNamespace(code, "Kistl.API");
            

            // XMLObjectCollection
            CodeTypeDeclaration c = CreateSealedClass(ns, "XMLObjectCollection", "IXmlObjectCollection");
            c.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable"));
            c.CustomAttributes.Add(new CodeAttributeDeclaration("XmlRoot", new CodeAttributeArgument("ElementName", new CodePrimitiveExpression("ObjectCollection"))));

            CodeMemberField f = CreateField(c, "List<Object>", "_Objects", "new List<Object>()");

            CodeMemberProperty p = CreateProperty(c, f.Type, "Objects", false);
            p.GetStatements.Add(new CodeSnippetExpression("return _Objects"));
            foreach (ObjectClass objClass in objClassList)
            {
                p.CustomAttributes.Add(
                    new CodeAttributeDeclaration("XmlArrayItem",
                        new CodeAttributeArgument("Type", new CodeTypeOfExpression(objClass.Module.Namespace + "." + objClass.ClassName)),
                        new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(objClass.ClassName))
                    ));
            }

            CodeMemberMethod m = CreateMethod(c, "ToList", "List<T>");
            CodeTypeParameter ct = new CodeTypeParameter("T");
            ct.Constraints.Add("IDataObject");
            m.TypeParameters.Add(ct);
            
            m.Statements.Add(new CodeSnippetExpression(@"return new List<T>(Objects.OfType<T>())"));

            // XMLObject
            c = CreateSealedClass(ns, "XMLObject", "IXmlObject");
            c.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable"));
            c.CustomAttributes.Add(new CodeAttributeDeclaration("XmlRoot", new CodeAttributeArgument("ElementName", new CodePrimitiveExpression("Object"))));

            f = CreateField(c, "Object", "_Object");

            p = CreateProperty(c, "Object", "Object");
            p.GetStatements.Add(new CodeSnippetExpression(" return _Object"));
            p.SetStatements.Add(new CodeSnippetExpression("_Object = value"));
            foreach (ObjectClass objClass in objClassList)
            {
                p.CustomAttributes.Add(
                    new CodeAttributeDeclaration("XmlElement",
                        new CodeAttributeArgument("Type", new CodeTypeOfExpression(objClass.Module.Namespace + "." + objClass.ClassName)),
                        new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(objClass.ClassName))
                    ));
            }

            // Generate the code & save
            SaveFile(code, string.Format(@"Kistl.Objects.{0}\ObjectSerializer.cs", clientServer));
        }
        #endregion

        #region GenerateObjects
        protected virtual void GenerateObjects(CurrentObjectClass current)
        {
        }

        private void GenerateObjectsInternal(CurrentObjectClass current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.objClass.Module.Namespace);
            current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.clientServer)));

            // Create Class
            current.code_class = CreateClass(current.code_namespace, current.objClass.ClassName,
                current.objClass.BaseObjectClass != null
                    ? current.objClass.BaseObjectClass.Module.Namespace + "." + current.objClass.BaseObjectClass.ClassName
                    : string.Format("Base{0}DataObject", current.clientServer),
                "ICloneable");

            foreach (Interface i in current.objClass.ImplementsInterfaces.Select(i => i.Value))
            {
                current.code_class.BaseTypes.Add(i.Module.Namespace + "." + i.ClassName);
            }

            // Constructor
            current.code_constructor = CreateConstructor(current.code_class);

            GenerateObjects(current);

            if (current.objClass.BaseObjectClass == null)
            {
                // Create Default Properties
                GenerateDefaultPropertiesInternal((CurrentObjectClass)current.Clone());
            }

            // Create Properties
            GeneratePropertiesInternal((CurrentObjectClass)current.Clone());

            // Create DataObject Default Methods
            GenerateDefaultMethodsInternal((CurrentObjectClass)current.Clone());

            // Create DataObject Methods
            GenerateMethodsInternal((CurrentObjectClass)current.Clone());

            // Create DataObject StreamingMethods
            GenerateStreamMethodsInternal((CurrentObjectClass)current.Clone());

            // Generate the code & save
            SaveFile(current.code, "Kistl.Objects." + current.clientServer + @"\" + current.objClass.ClassName + "." + current.clientServer + ".Designer.cs");
        }
        #endregion

        #region GenerateInterfaces
        protected virtual void GenerateInterfaces(CurrentInterface current)
        {
        }

        private void GenerateInterfacesInternal(CurrentInterface current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.@interface.Module.Namespace);
            current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.clientServer)));

            // Create Class
            current.code_class = CreateInterface(current.code_namespace, current.@interface.ClassName);

            // Properties
            foreach (BaseProperty p in current.@interface.Properties)
            {
                CreateProperty(current.code_class, p.GetDataType(), p.PropertyName);
            }

            // Methods
            foreach (Method method in current.@interface.Methods)
            {
                BaseParameter returnParam = method.Parameter.SingleOrDefault(p => p.IsReturnParameter);
                CodeMemberMethod m = CreateMethod(current.code_class, method.MethodName, 
                    returnParam != null ? new CodeTypeReference(returnParam.GetDataType()) : new CodeTypeReference(typeof(void)));

                foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter))
                {
                    m.Parameters.Add(new CodeParameterDeclarationExpression(
                        new CodeTypeReference(param.GetDataType()), param.ParameterName));
                }
            }

            GenerateInterfaces(current);

            // Generate the code & save
            SaveFile(current.code, "Kistl.Objects." + current.clientServer + @"\" + current.@interface.ClassName + "." + current.clientServer + ".Designer.cs");
        }
        #endregion

        #region GenerateEnumerations
        protected virtual void GenerateEnumerations(CurrentEnumeration current)
        {
        }

        private void GenerateEnumerationsInternal(CurrentEnumeration current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.enumeration.Module.Namespace);
            current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.clientServer)));

            // Create Class
            current.code_class = CreateEnum(current.code_namespace, current.enumeration.ClassName);

            foreach (EnumerationEntry e in current.enumeration.EnumerationEntries)
            {
                CodeMemberField mf = CreateField(current.code_class, typeof(int), e.EnumerationEntryName, e.EnumValue.ToString());
                mf.Comments.Add(new CodeCommentStatement(e.EnumerationEntryName, true));
            }

            GenerateEnumerations(current);

            // Generate the code & save
            SaveFile(current.code, "Kistl.Objects." + current.clientServer + @"\" + current.enumeration.ClassName + "." + current.clientServer + ".Designer.cs");
        }
        #endregion

        #region GenerateDefaultProperties
        protected virtual void GenerateDefaultProperty_ID(CurrentObjectClass current)
        {
        }

        private void GenerateDefaultProperty_IDInternal(CurrentObjectClass current)
        {
            // Create ID member
            current.code_field = CreateField(current.code_class, typeof(int), "_ID", "Helper.INVALIDID");

            current.code_property = CreateProperty(current.code_class, typeof(int), "ID");
            current.code_property.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            current.code_property.GetStatements.Add(new CodeSnippetExpression("return _ID"));
            current.code_property.SetStatements.Add(new CodeSnippetExpression("_ID = value"));

            GenerateDefaultProperty_ID(current);
        }

        protected virtual void GenerateDefaultProperty_EntitySetName(CurrentObjectClass current)
        {
        }

        private void GenerateDefaultProperty_EntitySetNameInternal(CurrentObjectClass current)
        {
            current.code_property = CreateProperty(current.code_class, typeof(string), "EntitySetName", false);
            current.code_property.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            current.code_property.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(current.objClass.ClassName)));

            GenerateDefaultProperty_EntitySetName(current);
        }
        
        protected virtual void GenerateDefaultPropertiesInternal(CurrentObjectClass current)
        {
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)current.Clone());

            if (current.clientServer == ClientServerEnum.Server)
            {
                GenerateDefaultProperty_EntitySetNameInternal((CurrentObjectClass)current.Clone());
            }
        }
        #endregion

        #region GenerateProperties
        
        #region GenerateValueTypeProperty
        protected virtual void GenerateProperties_ValueTypeProperty(CurrentObjectClass current)
        {
        }

        private void GenerateProperties_ValueTypePropertyInternal(CurrentObjectClass current)
        {
            if (string.IsNullOrEmpty(current.property.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     current.objClass.ClassName, current.property.PropertyName));

            bool isValueType;
            if (current.property is EnumerationProperty)
            {
                isValueType = true;
            }
            else
            {
                Type t = Type.GetType(current.property.GetDataType());
                if (t == null) throw new ApplicationException(
                     string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                         current.objClass.ClassName, current.property.PropertyName, current.property.GetDataType()));

                isValueType = t.IsValueType;
            }

            current.code_field = CreateField(current.code_class, current.property.GetDataType() + ((isValueType && ((ValueTypeProperty)current.property).IsNullable) ? "?" : ""),
                "_" + current.property.PropertyName);

            current.code_property = CreateProperty(current.code_class, current.code_field.Type, current.property.PropertyName);

            current.code_property.GetStatements.Add(new CodeSnippetExpression("return " + current.code_field.Name));
            current.code_property.SetStatements.Add(new CodeSnippetExpression(
                string.Format(@"NotifyPropertyChanging(""{0}""); 
                {1} = value; 
                NotifyPropertyChanged(""{0}"");", 
                current.property.PropertyName, current.code_field.Name)));

            GenerateProperties_ValueTypeProperty(current);
        }
        #endregion

        #region GenerateValueTypeProperty_Collection
        protected virtual void GenerateProperties_ValueTypeProperty_Collection(CurrentObjectClass current,
            CurrentObjectClass collectionClass, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
        }

        private void GenerateProperties_ValueTypeProperty_CollectionInternal(CurrentObjectClass current)
        {
            if (string.IsNullOrEmpty(current.property.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     current.objClass.ClassName, current.property.PropertyName));

            Type t = Type.GetType(current.property.GetDataType());
            if (t == null) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                     current.objClass.ClassName, current.property.PropertyName, current.property.GetDataType()));

            CurrentObjectClass collectionClass = (CurrentObjectClass)current.Clone();

            collectionClass.code_class = CreateClass(collectionClass.code_namespace, Generator.GetPropertyCollectionObjectType((Property)current.property).Classname,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.clientServer));
            
            // Create ID
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)collectionClass.Clone());

            // Create Property
            collectionClass.code_field = CreateField(collectionClass.code_class,
               current.property.GetDataType() + ((t.IsValueType && ((ValueTypeProperty)current.property).IsNullable) ? "?" : ""), "_Value");

            collectionClass.code_property = CreateProperty(collectionClass.code_class, collectionClass.code_field.Type, "Value");

            collectionClass.code_property.GetStatements.Add(new CodeSnippetExpression("return _Value"));
            collectionClass.code_property.SetStatements.Add(new CodeSnippetExpression(@"base.NotifyPropertyChanging(""Value"");
                _Value = value;
                base.NotifyPropertyChanged(""Value"");"));

            // Create Parent
            CurrentObjectClass parent = (CurrentObjectClass)collectionClass.Clone();
            parent.code_property = CreateProperty(collectionClass.code_class, current.code_class.Name, "Parent");
            parent.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                parent.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Parent)", current.objClass.ClassName)));
                parent.code_property.SetStatements.Add(new CodeSnippetExpression(@"_fk_Parent = value.ID"));
            }

            // Create SerializerParent
            CurrentObjectClass serializerParent = (CurrentObjectClass)collectionClass.Clone();

            // Serializer Parent fk_ Field und Property
            serializerParent.code_field = CreateField(collectionClass.code_class, typeof(int), "_fk_Parent", "Helper.INVALIDID");
            serializerParent.code_property = CreateProperty(collectionClass.code_class, typeof(int), "fk_Parent");

            if (current.clientServer == ClientServerEnum.Client)
            {
                serializerParent.code_property.GetStatements.Add(new CodeSnippetExpression(@"return _fk_Parent"));
                serializerParent.code_property.SetStatements.Add(new CodeSnippetExpression("_fk_Parent = value"));
            }

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = CreateProperty(current.code_class, (CodeTypeReference)null, current.property.PropertyName, false);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference(string.Format("NotifyingObservableCollection<{0}>", collectionClass.code_class.Name));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"return _{0}", current.property.PropertyName)));

                current.code_field = CreateField(current.code_class, current.code_property.Type, string.Format(@"_{0}", current.property.PropertyName));

                current.code_constructor.Statements.Add(new CodeSnippetExpression(
                    string.Format(@"_{0} = new NotifyingObservableCollection<{1}>(this, ""{0}"")",
                        current.property.PropertyName, collectionClass.code_class.Name)));
            }

            GenerateProperties_ValueTypeProperty_Collection(current, collectionClass, parent, serializerParent);
            GenerateProperties_ValueTypeProperty_Collection_StreamMethods(collectionClass, parent, serializerParent);
        }

        #region GenerateProperties_ValueTypeProperty_Collection_StreamMethods
        private void GenerateProperties_ValueTypeProperty_Collection_StreamMethods(CurrentObjectClass current, 
            CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
            // Create ToStream Method
            CodeMemberMethod m = CreateOverrideMethod(current.code_class, "ToStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));

            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.ToBinary(this.Value, sw)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.ToBinary(this.fk_Parent, sw)"));

            m = CreateOverrideMethod(current.code_class, "FromStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.Add(new CodeSnippetExpression("base.FromStream(ctx, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._Value, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Parent, sr)"));

            m = CreateOverrideMethod(current.code_class, "CopyTo", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

            m.Statements.Add(new CodeSnippetExpression("base.CopyTo(obj)"));
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._Value = this._Value", current.code_class.Name)));
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._fk_Parent = this._fk_Parent", current.code_class.Name)));
        }
        #endregion

        #endregion

        #region GenerateProperties_ObjectReferenceProperty
        protected virtual void GenerateProperties_ObjectReferenceProperty(CurrentObjectClass current, CurrentObjectClass serializer)
        {
        }

        private void GenerateProperties_ObjectReferencePropertyInternal(CurrentObjectClass current)
        {
            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetDataType(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = CreateProperty(current.code_class, current.property.GetDataType(), current.property.PropertyName);
            current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_{1})", current.property.GetDataType(), current.property.PropertyName)));

                current.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"NotifyPropertyChanging(""{0}""); 
                _fk_{0} = value.ID;
                NotifyPropertyChanged(""{0}""); ", current.property.PropertyName)));
            }

            CurrentObjectClass serializer = (CurrentObjectClass)current.Clone();

            // Serializer fk_ Field und Property
            serializer.code_field = CreateField(current.code_class, typeof(int), "_fk_" + current.property.PropertyName, "Helper.INVALIDID");
            serializer.code_property = CreateProperty(current.code_class, typeof(int), "fk_" + current.property.PropertyName);

            if (current.clientServer == ClientServerEnum.Client)
            {
                serializer.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return _fk_{0}", current.property.PropertyName)));
                serializer.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"NotifyPropertyChanging(""{0}""); 
                _fk_{0} = value;
                NotifyPropertyChanged(""{0}""); ", current.property.PropertyName)));
            }

            GenerateProperties_ObjectReferenceProperty(current, serializer);
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty_Collection
        protected virtual void GenerateProperties_ObjectReferenceProperty_Collection(CurrentObjectClass current, CurrentObjectClass collectionClass,
            CurrentObjectClass serializerValue, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
        }

        private void GenerateProperties_ObjectReferenceProperty_CollectionInternal(CurrentObjectClass current)
        {
            if (string.IsNullOrEmpty(current.property.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     current.objClass.ClassName, current.property.PropertyName));

            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetDataType(), current.objClass.ClassName, current.property.PropertyName));

            CurrentObjectClass collectionClass = (CurrentObjectClass)current.Clone();

            collectionClass.code_class = CreateClass(collectionClass.code_namespace, Generator.GetPropertyCollectionObjectType((Property)current.property).Classname,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.clientServer));

            // Create ID
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)collectionClass.Clone());

            // Create Value
            collectionClass.code_property = CreateProperty(collectionClass.code_class, collectionClass.property.GetDataType(), "Value");
            collectionClass.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            // Create Parent
            CurrentObjectClass parent = (CurrentObjectClass)collectionClass.Clone();
            parent.code_property = CreateProperty(collectionClass.code_class, current.code_class.Name, "Parent");
            parent.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            // Create SerializerValue
            CurrentObjectClass serializerValue = (CurrentObjectClass)collectionClass.Clone();

            // Serializer fk_ Field und Property
            serializerValue.code_field = CreateField(collectionClass.code_class, typeof(int), "_fk_Value", "Helper.INVALIDID");
            serializerValue.code_property = CreateProperty(collectionClass.code_class, typeof(int), "fk_Value");

            // Create SerializerParent
            CurrentObjectClass serializerParent = (CurrentObjectClass)collectionClass.Clone();

            // Serializer Parent fk_ Field und Property
            serializerParent.code_field = CreateField(collectionClass.code_class, typeof(int), "_fk_Parent", "Helper.INVALIDID");
            serializerParent.code_property = CreateProperty(collectionClass.code_class, typeof(int), "fk_Parent");

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = CreateProperty(current.code_class, (CodeTypeReference)null, current.property.PropertyName, false);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference(string.Format("NotifyingObservableCollection<{0}>", collectionClass.code_class.Name));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"return _{0}", current.property.PropertyName)));

                current.code_field = CreateField(current.code_class, current.code_property.Type, string.Format(@"_{0}", current.property.PropertyName));

                current.code_constructor.Statements.Add(new CodeSnippetExpression(
                    string.Format(@"_{0} = new NotifyingObservableCollection<{1}>(this, ""{0}"")",
                        current.property.PropertyName, collectionClass.code_class.Name)));
            }

            // Get/Set for client
            if (current.clientServer == ClientServerEnum.Client)
            {
                collectionClass.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Value)", current.property.GetDataType())));
                collectionClass.code_property.SetStatements.Add(new CodeSnippetExpression(@"base.NotifyPropertyChanging(""Value"");
                _fk_Value = value.ID;
                base.NotifyPropertyChanged(""Value"")"));

                parent.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Parent)", current.objClass.ClassName)));
                parent.code_property.SetStatements.Add(new CodeSnippetExpression(@"_fk_Parent = value.ID"));

                serializerValue.code_property.GetStatements.Add(new CodeSnippetExpression(@"return _fk_Value"));
                serializerValue.code_property.SetStatements.Add(new CodeSnippetExpression(@"base.NotifyPropertyChanging(""Value"");
                _fk_Value = value;
                base.NotifyPropertyChanged(""Value"")"));

                serializerParent.code_property.GetStatements.Add(new CodeSnippetExpression(@"return _fk_Parent"));
                serializerParent.code_property.SetStatements.Add(new CodeSnippetExpression("_fk_Parent = value"));
            }

            GenerateProperties_ObjectReferenceProperty_Collection(current, collectionClass, serializerValue, parent, serializerParent);
            GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods(collectionClass, serializerValue, parent, serializerParent);
        }

        #region GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods
        private void GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods(CurrentObjectClass current, 
            CurrentObjectClass serializerValue, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
            // Create ToStream Method
            CodeMemberMethod m = CreateOverrideMethod(current.code_class, "ToStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));

            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.ToBinary(this.fk_Value, sw)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.ToBinary(this.fk_Parent, sw)"));

            m = CreateOverrideMethod(current.code_class, "FromStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.Add(new CodeSnippetExpression("base.FromStream(ctx, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Value, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Parent, sr)"));

            m = CreateOverrideMethod(current.code_class, "CopyTo", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

            m.Statements.Add(new CodeSnippetExpression("base.CopyTo(obj)"));
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._fk_Value = this.fk_Value", current.code_class.Name)));
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._fk_Parent = this.fk_Parent", current.code_class.Name)));
        }

        #endregion

        #endregion

        #region GenerateProperties_BackReferenceProperty
        protected virtual void GenerateProperties_BackReferenceProperty(CurrentObjectClass current)
        {
        }

        private void GenerateProperties_BackReferencePropertyInternal(CurrentObjectClass current)
        {
            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on BackReferenceProperty {1}.{2}",
                    current.property.GetDataType(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = CreateProperty(current.code_class, (CodeTypeReference)null, current.property.PropertyName, false);
            current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                ObjectType childType = new ObjectType(current.property.GetDataType());
                current.code_property.Type = new CodeTypeReference("List", new CodeTypeReference(childType.NameDataObject));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(
                        @"if(_{0} == null) _{0} = Context.GetListOf<{1}>(this, ""{0}"");
                return _{0}", current.property.PropertyName, childType.NameDataObject)));

                CodeMemberField f = new CodeMemberField(current.code_property.Type,
                    "_" + current.property.PropertyName);

                current.code_class.Members.Add(f);
            }


            GenerateProperties_BackReferenceProperty(current);
        }
        #endregion

        private void GeneratePropertiesInternal(CurrentObjectClass current)
        {
            foreach (BaseProperty baseProp in current.objClass.Properties)
            {
                current.property = baseProp;
                if (baseProp is ValueTypeProperty && ((ValueTypeProperty)baseProp).IsList)
                {
                    // Simple Property Collection
                    GenerateProperties_ValueTypeProperty_CollectionInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp is ValueTypeProperty)
                {
                    // Simple Property
                    GenerateProperties_ValueTypePropertyInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp is ObjectReferenceProperty && ((ObjectReferenceProperty)baseProp).IsList)
                {
                    // "pointer" Object Collection
                    GenerateProperties_ObjectReferenceProperty_CollectionInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp is ObjectReferenceProperty)
                {
                    // "pointer" Object
                    GenerateProperties_ObjectReferencePropertyInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp is BackReferenceProperty)
                {
                    // "Backpointer" Object
                    GenerateProperties_BackReferencePropertyInternal((CurrentObjectClass)current.Clone());
                }
                else
                {
                    // not supported yet
                    // just ignore it for now
                    throw new ApplicationException("Unknonw Propertytype " + baseProp.GetType().Name);
                }
            }
        }
        #endregion

        #region GenerateDefaultMethods
        private void GenerateDefaultMethodsInternal(CurrentObjectClass current)
        {
            // Create ToString Delegate
            CodeMemberEvent e = new CodeMemberEvent();
            current.code_class.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ToStringHandler", new CodeTypeReference(current.objClass.ClassName));
            e.Name = "OnToString_" + current.objClass.ClassName;

            // Create PreSave Delegate
            e = new CodeMemberEvent();
            current.code_class.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(current.objClass.ClassName));
            e.Name = "OnPreSave_" + current.objClass.ClassName;

            // Create PostSave Delegate
            e = new CodeMemberEvent();
            current.code_class.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(current.objClass.ClassName));
            e.Name = "OnPostSave_" + current.objClass.ClassName;

            // Create ToString Method
            CodeMemberMethod m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.Diagnostics.DebuggerHidden")));
            m.Name = "ToString";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(string));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_{0} != null)
            {{
                OnToString_{0}(this, e);
            }}
            return e.Result", current.objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create NotifyPreSave Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "NotifyPreSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"base.NotifyPreSave();
            if (OnPreSave_{0} != null) OnPreSave_{0}(this)", current.objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create NotifyPostSave Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "NotifyPostSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"base.NotifyPostSave();
            if (OnPostSave_{0} != null) OnPostSave_{0}(this)", current.objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create Clone Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "Clone";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(object));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"{0} obj = new {0}();
            CopyTo(obj);
            return obj", current.objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create CopyTo Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "CopyTo";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(IDataObject)), "obj"));
            m.Statements.Add(new CodeSnippetExpression("base.CopyTo(obj)"));// TODO: Das ist C# spezifisch

            foreach (Property p in current.objClass.Properties.OfType<Property>())
            {
                string stmt = "";

                if (p is ValueTypeProperty && !((ValueTypeProperty)p).IsList)
                {
                    stmt = string.Format("(({1})obj)._{0} = this._{0}", p.PropertyName, current.objClass.ClassName);
                }
                // TODO: Geht z.Z. nur für den Client!!!!
                else if (p is ValueTypeProperty && ((ValueTypeProperty)p).IsList && current.clientServer == ClientServerEnum.Client)
                {
                    stmt = string.Format("(({1})obj)._{0} = this._{0}.Clone(obj)", p.PropertyName, current.objClass.ClassName);
                }
                else if (p is ObjectReferenceProperty && !((ObjectReferenceProperty)p).IsList)
                {
                    stmt = string.Format("(({1})obj)._fk_{0} = this._fk_{0}", p.PropertyName, current.objClass.ClassName);
                }
                // TODO: Geht z.Z. nur für den Client!!!!
                else if (p is ObjectReferenceProperty && ((ObjectReferenceProperty)p).IsList && current.clientServer == ClientServerEnum.Client)
                {
                    stmt = string.Format("(({1})obj)._{0} = this._{0}.Clone(obj)", p.PropertyName, current.objClass.ClassName);
                }

                if (!string.IsNullOrEmpty(stmt))
                {
                    m.Statements.Add(new CodeSnippetExpression(stmt));
                }
            }

            if (current.clientServer == ClientServerEnum.Client)
            {
                // Create AttachToContext Method
                m = CreateOverrideMethod(current.code_class, "AttachToContext", typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("IKistlContext"), "ctx"));

                m.Statements.Add(new CodeSnippetExpression("base.AttachToContext(ctx)"));

                foreach (Property p in current.objClass.Properties.OfType<Property>().Where(p => p.IsList))
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format(@"_{0}.ForEach(i => i.AttachToContext(ctx));", p.PropertyName)));
                }
            }
        }
        #endregion

        #region GenerateMethods
        private void GenerateMethodsInternal(CurrentObjectClass current)
        {
            ObjectClass baseObjClass = current.objClass;
            ObjectClass objClass = current.objClass;
            while (objClass != null)
            {
                foreach (Method method in objClass.Methods)
                {
                    // TODO: I dont know if there's another way
                    // Default Methods, do not generate
                    if (method.Module.ModuleName == "KistlBase")
                    {
                        if (method.MethodName == "ToString"
                            || method.MethodName == "PreSave"
                            || method.MethodName == "PostSave"
                        )
                            continue;
                    }

                    BaseParameter returnParam = method.Parameter.SingleOrDefault(p => p.IsReturnParameter);

                    if (objClass == baseObjClass)
                    {
                        // Create Delegate
                        // HACK!!! Die TypeParameter scheinen nicht zu funktionieren
                        CodeTypeDelegate d = new CodeTypeDelegate(method.MethodName + "_Handler<T>");

                        current.code_class.Members.Add(d);
                        d.Attributes = MemberAttributes.Public;

                        // HACK!!! Die TypeParameter scheinen nicht zu funktionieren
                        CodeTypeParameter ct = new CodeTypeParameter("T");
                        ct.Constraints.Add(new CodeTypeReference("IDataObject"));
                        d.TypeParameters.Add(ct);
                        // HACK!!! Die TypeParameter scheinen nicht zu funktionieren


                        d.Parameters.Add(new CodeParameterDeclarationExpression("T", "obj"));

                        if (returnParam != null)
                        {
                            d.Parameters.Add(new CodeParameterDeclarationExpression(
                                new CodeTypeReference("MethodReturnEventArgs", new CodeTypeReference(returnParam.GetDataType())),
                                "e"));
                        }

                        foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter))
                        {
                            d.Parameters.Add(new CodeParameterDeclarationExpression(
                                new CodeTypeReference(param.GetDataType()), param.ParameterName));
                        }
                    }

                    // Create event
                    CodeMemberEvent e = new CodeMemberEvent();
                    current.code_class.Members.Add(e);

                    e.Attributes = MemberAttributes.Public;
                    e.Type = new CodeTypeReference(method.MethodName + "_Handler",
                        new CodeTypeReference(baseObjClass.ClassName));
                    e.Name = "On" + method.MethodName + "_" + baseObjClass.ClassName;

                    // Create Method
                    CodeMemberMethod m = new CodeMemberMethod();
                    current.code_class.Members.Add(m);
                    m.Name = method.MethodName;
                    m.Attributes = (objClass == baseObjClass) ? (MemberAttributes.Public) : (MemberAttributes.Public | MemberAttributes.Override);

                    if (returnParam != null)
                    {
                        m.ReturnType = new CodeTypeReference(returnParam.GetDataType());
                    }
                    else
                    {
                        m.ReturnType = new CodeTypeReference(typeof(void));
                    }

                    // Add Parameter
                    StringBuilder methodCallParameter = new StringBuilder();
                    foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter))
                    {
                        m.Parameters.Add(new CodeParameterDeclarationExpression(
                            new CodeTypeReference(param.GetDataType()), param.ParameterName));
                        methodCallParameter.AppendFormat(", {0}", param.ParameterName);
                    }

                    if (returnParam != null)
                    {
                        m.Statements.Add(new CodeSnippetExpression(string.Format(@"MethodReturnEventArgs<{0}> e = new MethodReturnEventArgs<{0}>()", returnParam.GetDataType())));
                    }

                    if (objClass != baseObjClass)
                    {
                        m.Statements.Add(new CodeSnippetExpression(string.Format(@"{2}base.{0}({1})", 
                            method.MethodName, 
                            methodCallParameter.ToString(),
                            returnParam != null ? "e.Result = " : "")));
                    }

                    m.Statements.Add(new CodeSnippetExpression(string.Format(@"if (On{1}_{0} != null)
            {{
                On{1}_{0}(this{2}{3});
            }}", 
               baseObjClass.ClassName, 
               method.MethodName, 
               returnParam != null ? ", e" : "",
               methodCallParameter.ToString())));

                    if (returnParam != null)
                    {
                        m.Statements.Add(new CodeSnippetExpression("return e.Result"));
                    }
                }

                // Nächster bitte
                objClass = objClass.BaseObjectClass;
            }
        }
        #endregion

        #region GenerateStreamMethods
        private void GenerateStreamMethodsInternal(CurrentObjectClass current)
        {
            // Create ToStream Method
            CodeMemberMethod m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "ToStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));
            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));// TODO: Das ist C# spezifisch

            foreach (BaseProperty p in current.objClass.Properties)
            {
                if (p is ValueTypeProperty && !((ValueTypeProperty)p).IsList)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this._{0}, sw)", p.PropertyName)));
                }
                else if (p is ValueTypeProperty && ((ValueTypeProperty)p).IsList)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.{0}, sw)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty && !((ObjectReferenceProperty)p).IsList)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.fk_{0}, sw)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty && ((ObjectReferenceProperty)p).IsList)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.{0}, sw)", p.PropertyName)));
                }
                else if (p is BackReferenceProperty
                    && current.clientServer == ClientServerEnum.Server
                    && ((BackReferenceProperty)p).PreFetchToClient)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.{0}.OfType<IDataObject>(), sw)", p.PropertyName)));
                }
            }

            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "FromStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));
            m.Statements.Add(new CodeSnippetExpression("base.FromStream(ctx, sr)"));// TODO: Das ist C# spezifisch

            foreach (BaseProperty p in current.objClass.Properties)
            {
                if (p is Property && ((Property)p).IsList)
                {
                    if (current.clientServer == ClientServerEnum.Client)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(out this._{0}, sr, ctx, this, \"{0}\")", p.PropertyName)));
                    else
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(this.{0}, sr, ctx)", p.PropertyName)));
                }
                /* TODO: Reimplement when Interfaces for DataTypes are implemented to wrap this whole damm shit thing!
                 * else if (p is EnumerationProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("Enum tmp{0}; BinarySerializer.FromBinary(out tmp{0}, sr); _{0} = ({1})tmp{0}", 
                        p.PropertyName, p.GetDataType() + (((EnumerationProperty)p).IsNullable ? "?" : ""))));
                }*/
                else if (p is ValueTypeProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._{0}, sr)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty && !((ObjectReferenceProperty)p).IsList)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._fk_{0}, sr)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty && ((ObjectReferenceProperty)p).IsList)
                {
                    if (current.clientServer == ClientServerEnum.Client)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(out this._{0}, sr, ctx, this, \"{0}\")", p.PropertyName)));
                    else
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(this.{0}, sr, ctx)", p.PropertyName)));
                }
                else if (p is BackReferenceProperty
                    && current.clientServer == ClientServerEnum.Client
                    && ((BackReferenceProperty)p).PreFetchToClient)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._{0}, sr, ctx)", p.PropertyName)));
                }
            }
        }
        #endregion
    }

    public sealed class DataObjectGeneratorFactory
    {
        public static BaseDataObjectGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDataObjectGenerator();
        }

        /// <summary>
        /// prevent this class from being instantiated
        /// </summary>
        private DataObjectGeneratorFactory() { }
    }
}
