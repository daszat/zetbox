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

        #region Current Metadata
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

        public class CurrentStruct : CurrentBase
        {
            public Struct @struct { get; set; }

            public override object Clone()
            {
                CurrentStruct result = new CurrentStruct();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentStruct)result).@struct = this.@struct;
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
        #endregion

        #region Generate
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

            var structList = Generator.GetStructList(ctx);

            foreach (Struct s in structList)
            {
                GenerateStructsInternal(new CurrentStruct() { ctx = ctx, clientServer = ClientServerEnum.Client, @struct = s });
                GenerateStructsInternal(new CurrentStruct() { ctx = ctx, clientServer = ClientServerEnum.Server, @struct = s });
            }

            GenerateAssemblyInfo(ClientServerEnum.Server);
            GenerateAssemblyInfo(ClientServerEnum.Client);
        }
        #endregion

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

            CodeMemberField f = c.CreateField(typeof(List<Object>), "_Objects", "new List<Object>()");

            CodeMemberProperty p = c.CreateProperty(f.Type, "Objects", false);
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

            f = c.CreateField(typeof(Object), "_Object");

            p = c.CreateProperty("Object", "Object");
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
                    : string.Format("Base{0}DataObject", current.clientServer));

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
            GenerateStreamMethodsInternal((CurrentObjectClass)current.Clone(), current.objClass.Properties);

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
                current.code_class.CreateProperty(p.ToCodeTypeReference(current.clientServer), p.PropertyName);
            }

            // Methods
            foreach (Method method in current.@interface.Methods)
            {
                BaseParameter returnParam = method.Parameter.SingleOrDefault(p => p.IsReturnParameter);
                CodeMemberMethod m = CreateMethod(current.code_class, method.MethodName, returnParam.ToCodeTypeReference());

                foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter))
                {
                    m.Parameters.Add(new CodeParameterDeclarationExpression(
                        new CodeTypeReference(param.GetParameterTypeString()), param.ParameterName));
                }
            }

            GenerateInterfaces(current);

            // Generate the code & save
            SaveFile(current.code, "Kistl.Objects." + current.clientServer + @"\" + current.@interface.ClassName + "." + current.clientServer + ".Designer.cs");
        }
        #endregion

        #region GenerateStructs
        protected virtual void GenerateStructs(CurrentStruct current)
        {
        }

        private void GenerateStructsInternal(CurrentStruct current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.@struct.Module.Namespace);
            current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.clientServer)));

            // Create Struct class
            current.code_class = CreateClass(current.code_namespace, current.@struct.ClassName, string.Format("Base{0}StructObject", current.clientServer));

            // Create Properties
            GeneratePropertiesInternal((CurrentStruct)current.Clone());

            // Create Structs StreamingMethods
            GenerateStreamMethodsInternal((CurrentStruct)current.Clone(), current.@struct.Properties);

            // Call derived Classes
            GenerateStructs(current);

            // Generate the code & save
            SaveFile(current.code, "Kistl.Objects." + current.clientServer + @"\" + current.@struct.ClassName + "." + current.clientServer + ".Designer.cs");
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
                CodeMemberField mf = current.code_class.CreateField(typeof(int), e.EnumerationEntryName, e.EnumValue.ToString());
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
            // Client does not need such a stupid thing
            if (current.clientServer == ClientServerEnum.Server)
            {
                // Create _Server_ ID member
                current.code_field = current.code_class.CreateField(typeof(int), "_ID");

                current.code_property = current.code_class.CreateProperty(typeof(int), "ID");
                current.code_property.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                current.code_property.GetStatements.Add(new CodeSnippetExpression("return _ID"));
                current.code_property.SetStatements.Add(new CodeSnippetExpression("_ID = value"));

                GenerateDefaultProperty_ID(current);
            }            
        }

        protected virtual void GenerateDefaultPropertiesInternal(CurrentObjectClass current)
        {
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)current.Clone());
        }
        #endregion

        #region GenerateProperties

        #region GenerateValueTypeProperty
        protected virtual void GenerateProperties_ValueTypeProperty(CurrentBase current)
        {
        }

        private void GenerateProperties_ValueTypePropertyInternal(CurrentBase current)
        {
            current.code_field = current.property.CreateField(current.code_class, current.clientServer);
            current.code_property = current.property.CreateNotifyingProperty(current.code_class, current.clientServer);

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
            CurrentObjectClass collectionClass = (CurrentObjectClass)current.Clone();

            collectionClass.code_class = CreateClass(collectionClass.code_namespace, Generator.GetPropertyCollectionObjectType((Property)current.property).Classname,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.clientServer));
            if (current.clientServer == ClientServerEnum.Client)
            {
                collectionClass.code_class.BaseTypes.Add(string.Format("ICollectionEntry<{0}, {1}>",
                    current.property.GetPropertyTypeString(), current.code_class.Name));
                collectionClass.code_class.TypeAttributes = TypeAttributes.NotPublic;
            }

            // Create ID
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)collectionClass.Clone());

            // Create Property
            collectionClass.code_field = collectionClass.code_class.CreateField(
                current.property.ToCodeTypeReference(current.clientServer), "_Value");

            collectionClass.code_property = collectionClass.code_class.CreateNotifyingProperty(
                current.property.ToCodeTypeReference(current.clientServer),
                "Value", "_Value", "_Value", "Value");

            // Create Parent
            CurrentObjectClass parent = (CurrentObjectClass)collectionClass.Clone();
            parent.code_property = collectionClass.code_class.CreateProperty(current.code_class.Name, "Parent");
            parent.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                parent.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Parent)", current.objClass.ClassName)));
                parent.code_property.SetStatements.Add(new CodeCommentStatement(@"TODO: Damit hab ich noch ein Problem. Wenn die Property not nullable ist, dann sollte das eigentlich nicht möglich sein."));
                parent.code_property.SetStatements.Add(new CodeSnippetExpression(@"_fk_Parent = value.ID"));
            }

            // Create SerializerParent
            CurrentObjectClass serializerParent = (CurrentObjectClass)collectionClass.Clone();

            // Serializer Parent fk_ Field und Property
            serializerParent.code_field = collectionClass.code_class.CreateField(typeof(int), "_fk_Parent");
            serializerParent.code_property = collectionClass.code_class.CreateProperty(typeof(int), "fk_Parent");

            if (current.clientServer == ClientServerEnum.Client)
            {
                serializerParent.code_property.GetStatements.Add(new CodeSnippetExpression(@"return _fk_Parent"));
                serializerParent.code_property.SetStatements.Add(new CodeSnippetExpression("_fk_Parent = value"));
            }

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = current.code_class.CreateProperty((CodeTypeReference)null, current.property.PropertyName, false);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference(string.Format("IList<{0}>", current.property.GetPropertyTypeString()));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"return _{0}", current.property.PropertyName)));

                current.code_field = current.code_class.CreateField(
                    string.Format("ListPropertyCollection<{0}, {1}, {2}>",
                        current.property.GetPropertyTypeString(),
                        current.code_class.Name,
                        collectionClass.code_class.Name),
                    string.Format(@"_{0}", current.property.PropertyName));

                current.code_constructor.Statements.Add(new CodeSnippetExpression(
                    string.Format(@"_{0} = new ListPropertyCollection<{1}, {2}, {3}>(this, ""{0}"")",
                        current.property.PropertyName,
                        current.property.GetPropertyTypeString(),
                        current.code_class.Name,
                        collectionClass.code_class.Name)));
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
            //m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.Add(new CodeSnippetExpression("base.FromStream(sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._Value, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Parent, sr)"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                m = CreateOverrideMethod(current.code_class, "ApplyChanges", typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

                m.Statements.Add(new CodeSnippetExpression("base.ApplyChanges(obj)"));
                m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._Value = this._Value", current.code_class.Name)));
                m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._fk_Parent = this._fk_Parent", current.code_class.Name)));
            }
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
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetPropertyTypeString()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetPropertyTypeString(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = current.code_class.CreateProperty(current.property.GetPropertyTypeString(), current.property.PropertyName);
            current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"if (fk_{1} == null) return null;
                return Context.Find<{0}>(fk_{1}.Value)", current.property.GetPropertyTypeString(), current.property.PropertyName)));

                current.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"fk_{0} = value != null ? (int?)value.ID : null", current.property.PropertyName)));
            }

            CurrentObjectClass serializer = (CurrentObjectClass)current.Clone();

            // Serializer fk_ Field und Property
            string fieldName = "_fk_" + current.property.PropertyName;
            serializer.code_field = current.code_class.CreateField(typeof(int?), fieldName, "null");

            if (current.clientServer == ClientServerEnum.Client)
            {
                serializer.code_property = current.code_class.CreateNotifyingProperty(typeof(int?), "fk_" + current.property.PropertyName,
                    fieldName, fieldName, current.property.PropertyName
                    );
            }
            else
            {
                serializer.code_property = current.code_class.CreateProperty(typeof(int?), "fk_" + current.property.PropertyName);
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
            if (string.IsNullOrEmpty(current.property.GetPropertyTypeString())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetPropertyTypeString()",
                     current.objClass.ClassName, current.property.PropertyName));

            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetPropertyTypeString()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetPropertyTypeString(), current.objClass.ClassName, current.property.PropertyName));

            CurrentObjectClass collectionClass = (CurrentObjectClass)current.Clone();

            collectionClass.code_class = CreateClass(collectionClass.code_namespace, Generator.GetPropertyCollectionObjectType((Property)current.property).Classname,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.clientServer));
            if (current.clientServer == ClientServerEnum.Client)
            {
                collectionClass.code_class.BaseTypes.Add(string.Format("ICollectionEntry<{0}, {1}>",
                    current.property.GetPropertyTypeString(), current.code_class.Name));
                collectionClass.code_class.TypeAttributes = TypeAttributes.NotPublic;
            }

            // Create ID
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)collectionClass.Clone());

            // Create Value
            collectionClass.code_property = collectionClass.code_class.CreateProperty(collectionClass.property.GetPropertyTypeString(), "Value");
            collectionClass.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            // Create Parent
            CurrentObjectClass parent = (CurrentObjectClass)collectionClass.Clone();
            parent.code_property = collectionClass.code_class.CreateProperty(current.code_class.Name, "Parent");
            parent.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            // Create SerializerValue
            CurrentObjectClass serializerValue = (CurrentObjectClass)collectionClass.Clone();

            // Serializer fk_ Field und Property
            serializerValue.code_field = collectionClass.code_class.CreateField(typeof(int), "_fk_Value");
            serializerValue.code_property = collectionClass.code_class.CreateProperty(typeof(int), "fk_Value");

            // Create SerializerParent
            CurrentObjectClass serializerParent = (CurrentObjectClass)collectionClass.Clone();

            // Serializer Parent fk_ Field und Property
            serializerParent.code_field = collectionClass.code_class.CreateField(typeof(int), "_fk_Parent");
            serializerParent.code_property = collectionClass.code_class.CreateProperty(typeof(int), "fk_Parent");

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = current.code_class.CreateProperty((CodeTypeReference)null, current.property.PropertyName, false);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference(string.Format("IList<{0}>", current.property.GetPropertyTypeString()));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"return _{0}", current.property.PropertyName)));

                current.code_field = current.code_class.CreateField(
                    string.Format("ListPropertyCollection<{0}, {1}, {2}>",
                        current.property.GetPropertyTypeString(),
                        current.code_class.Name,
                        collectionClass.code_class.Name),
                    string.Format(@"_{0}", current.property.PropertyName));

                current.code_constructor.Statements.Add(new CodeSnippetExpression(
                    string.Format(@"_{0} = new ListPropertyCollection<{1}, {2}, {3}>(this, ""{0}"")",
                        current.property.PropertyName,
                        current.property.GetPropertyTypeString(),
                        current.code_class.Name,
                        collectionClass.code_class.Name)));
            }

            // Get/Set for client
            if (current.clientServer == ClientServerEnum.Client)
            {
                collectionClass.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Value)", current.property.GetPropertyTypeString())));
                collectionClass.code_property.SetStatements.Add(new CodeSnippetExpression(@"fk_Value = value.ID;"));

                parent.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Parent)", current.objClass.ClassName)));
                parent.code_property.SetStatements.Add(new CodeSnippetExpression(@"_fk_Parent = value.ID"));

                serializerValue.code_property.GetStatements.Add(new CodeSnippetExpression(@"return _fk_Value"));
                serializerValue.code_property.SetStatements.Add(new CodeSnippetExpression(@"if(_fk_Value != value)
                {
                    base.NotifyPropertyChanging(""Value"");
                    _fk_Value = value;
                    base.NotifyPropertyChanged(""Value"");
                }"));

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
            //m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.Add(new CodeSnippetExpression("base.FromStream(sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Value, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Parent, sr)"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                m = CreateOverrideMethod(current.code_class, "ApplyChanges", typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

                m.Statements.Add(new CodeSnippetExpression("base.ApplyChanges(obj)"));
                m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._fk_Value = this.fk_Value", current.code_class.Name)));
                m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._fk_Parent = this.fk_Parent", current.code_class.Name)));
            }
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
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetPropertyTypeString()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on BackReferenceProperty {1}.{2}",
                    current.property.GetPropertyTypeString(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = current.code_class.CreateProperty((CodeTypeReference)null, current.property.PropertyName, false);
            current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                TypeMoniker childType = new TypeMoniker(current.property.GetPropertyTypeString());
                current.code_property.Type = new CodeTypeReference("IList", new CodeTypeReference(childType.NameDataObject));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(
                        @"if (_{0} == null)
                {{
                    List<{1}> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<{1}>(this, ""{0}"");
                    else
                        serverList = new List<{1}>();

                    _{0} = new BackReferenceCollection<{1}>(
                         ""{2}"", this, serverList);
                }}
                return _{0}",
                            current.property.PropertyName,
                            childType.NameDataObject,
                            ((BackReferenceProperty)current.property).ReferenceProperty.PropertyName)));

                CodeMemberField f = new CodeMemberField(new CodeTypeReference("BackReferenceCollection", new CodeTypeReference(childType.NameDataObject)),
                    "_" + current.property.PropertyName);

                current.code_class.Members.Add(f);
            }


            GenerateProperties_BackReferenceProperty(current);
        }
        #endregion

        #region GenerateProperties_StructProperty
        protected virtual void GenerateProperties_StructProperty(CurrentObjectClass current)
        {
        }

        private void GenerateProperties_StructPropertyInternal(CurrentObjectClass current)
        {
            current.code_field = current.property.CreateField(current.code_class, current.clientServer);
            current.code_property = current.property.CreateNotifyingProperty(current.code_class, current.clientServer);

            current.code_property.GetStatements.Clear();
            current.code_property.GetStatements.Add(
                new CodeSnippetExpression(string.Format("return _{0} != null ? ({1})_{0}.Clone() : new {1}()", 
                    current.property.PropertyName, current.property.GetPropertyTypeString())));

            GenerateProperties_StructProperty(current);
        }
        #endregion

        #region GeneratePropertiesInternal
        private void GeneratePropertiesInternal(CurrentObjectClass current)
        {
            foreach (BaseProperty baseProp in current.objClass.Properties)
            {
                current.property = baseProp;
                if (baseProp.IsValueTypePropertyList())
                {
                    // Simple Property Collection
                    GenerateProperties_ValueTypeProperty_CollectionInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp.IsValueTypePropertySingle())
                {
                    // Simple Property
                    GenerateProperties_ValueTypePropertyInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp.IsObjectReferencePropertyList())
                {
                    // "pointer" Object Collection
                    GenerateProperties_ObjectReferenceProperty_CollectionInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp.IsObjectReferencePropertySingle())
                {
                    // "pointer" Object
                    GenerateProperties_ObjectReferencePropertyInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp is BackReferenceProperty)
                {
                    // "Backpointer" Object
                    GenerateProperties_BackReferencePropertyInternal((CurrentObjectClass)current.Clone());
                }
                else if (baseProp.IsStructPropertySingle())
                {
                    // Struct Property
                    GenerateProperties_StructPropertyInternal((CurrentObjectClass)current.Clone());
                }
                else
                {
                    // not supported yet
                    throw new NotSupportedException("Unknown Propertytype " + baseProp.GetType().Name);
                }
            }
        }

        private void GeneratePropertiesInternal(CurrentStruct current)
        {
            foreach (BaseProperty baseProp in current.@struct.Properties)
            {
                current.property = baseProp;
                if (baseProp.IsValueTypePropertySingle())
                {
                    // Simple Property
                    GenerateProperties_ValueTypePropertyInternal((CurrentStruct)current.Clone());
                }
                else
                {
                    // not supported yet
                    throw new NotSupportedException("Unknonw Propertytype " + baseProp.GetType().Name);
                }
            }
        }
        #endregion

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
            return e.Result", current.objClass.ClassName)));

            // Create NotifyPreSave Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "NotifyPreSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"base.NotifyPreSave();
            if (OnPreSave_{0} != null) OnPreSave_{0}(this)", current.objClass.ClassName)));

            // Create NotifyPostSave Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "NotifyPostSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"base.NotifyPostSave();
            if (OnPostSave_{0} != null) OnPostSave_{0}(this)", current.objClass.ClassName)));

            if (current.clientServer == ClientServerEnum.Client)
            {
                // Create ApplyChanges Method
                m = new CodeMemberMethod();
                current.code_class.Members.Add(m);
                m.Name = "ApplyChanges";
                m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                m.ReturnType = new CodeTypeReference(typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(IDataObject)), "obj"));
                m.Statements.Add(new CodeSnippetExpression("base.ApplyChanges(obj)"));

                foreach (BaseProperty p in current.objClass.Properties.OfType<BaseProperty>())
                {
                    string stmt = "";

                    if (p.IsValueTypePropertySingle())
                    {
                        stmt = string.Format("(({1})obj).{0} = this.{0}", p.PropertyName, current.objClass.ClassName);
                    }
                    else if (p.IsValueTypePropertyList())
                    {
                        stmt = string.Format("this._{0}.ApplyChanges((({1})obj)._{0})", p.PropertyName, current.objClass.ClassName);
                    }
                    else if (p.IsObjectReferencePropertySingle())
                    {
                        stmt = string.Format("(({1})obj).fk_{0} = this.fk_{0}", p.PropertyName, current.objClass.ClassName);
                    }
                    else if (p.IsObjectReferencePropertyList())
                    {
                        stmt = string.Format("this._{0}.ApplyChanges((({1})obj)._{0})", p.PropertyName, current.objClass.ClassName);
                    }
                    else if (p is BackReferenceProperty)
                    {
                        stmt = string.Format("if(this._{0} != null) this._{0}.ApplyChanges((({1})obj)._{0}); else (({1})obj)._{0} = null; (({1})obj).NotifyPropertyChanged(\"{0}\")", p.PropertyName, current.objClass.ClassName);
                    }

                    if (!string.IsNullOrEmpty(stmt))
                    {
                        m.Statements.Add(new CodeSnippetExpression(stmt));
                    }
                }
            }

            // Create AttachToContext Method
            m = CreateOverrideMethod(current.code_class, "AttachToContext", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("IKistlContext"), "ctx"));

            m.Statements.Add(new CodeSnippetExpression("base.AttachToContext(ctx)"));

            if (current.clientServer == ClientServerEnum.Client)
            {
                foreach (Property p in current.objClass.Properties.OfType<Property>().Where(p => p.IsList))
                {
                    // Use ToList before using foreach - the collection could change
                    m.Statements.Add(new CodeSnippetExpression(string.Format(@"_{0}.AttachToContext(ctx)", p.PropertyName)));
                }
                foreach (BackReferenceProperty p in current.objClass.Properties.OfType<BackReferenceProperty>())
                {
                    // Create a new List after Attach - Object Reference will change, if Object is alredy in tha Context
                    m.Statements.Add(new CodeSnippetExpression(string.Format(@"if(_{0} != null) _{0}.AttachToContext(ctx)", p.PropertyName)));
                }
            }
            else
            {
                foreach (Property p in current.objClass.Properties.OfType<Property>().Where(p => p.IsList))
                {
                    // Use ToList before using foreach - the collection will change in the KistContext.Attach() Method
                    // because EntityFramework will need a Trick to attach CollectionEntries correctly
                    m.Statements.Add(new CodeSnippetExpression(string.Format(@"{0}.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i))", p.PropertyName)));
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
                                new CodeTypeReference("MethodReturnEventArgs", returnParam.ToCodeTypeReference()),
                                "e"));
                        }

                        foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter))
                        {
                            d.Parameters.Add(new CodeParameterDeclarationExpression(
                                param.ToCodeTypeReference(), param.ParameterName));
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

                    m.ReturnType = returnParam.ToCodeTypeReference();

                    // Add Parameter
                    StringBuilder methodCallParameter = new StringBuilder();
                    foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter))
                    {
                        m.Parameters.Add(new CodeParameterDeclarationExpression(
                            param.ToCodeTypeReference(), param.ParameterName));
                        methodCallParameter.AppendFormat(", {0}", param.ParameterName);
                    }

                    if (returnParam != null)
                    {
                        m.Statements.Add(new CodeSnippetExpression(
                            string.Format(@"MethodReturnEventArgs<{0}> e = new MethodReturnEventArgs<{0}>()",
                            returnParam.ToCodeTypeReference().BaseType)));
          
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
        private void GenerateStreamMethodsInternal(CurrentBase current, IEnumerable<BaseProperty> properties)
        {
            // Create ToStream Method
            CodeMemberMethod m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "ToStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));
            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));

            foreach (BaseProperty p in properties)
            {
                if (p.IsEnumerationPropertySingle())
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary((int{1})this._{0}, sw)", p.PropertyName, ((Property)p).IsNullable ? "?": "")));
                }
                else if (p.IsValueTypePropertySingle())
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this._{0}, sw)", p.PropertyName)));
                }
                else if (p.IsValueTypePropertyList())
                {
                    if (current.clientServer == ClientServerEnum.Client)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("this._{0}.ToStream(sw)", p.PropertyName)));
                    else
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.{0}, sw)", p.PropertyName)));
                }
                else if (p.IsStructPropertySingle())
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this._{0}, sw)", p.PropertyName)));
                }
                else if (p.IsObjectReferencePropertySingle())
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.fk_{0}, sw)", p.PropertyName)));
                }
                else if (p.IsObjectReferencePropertyList())
                {
                    if (current.clientServer == ClientServerEnum.Client)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("this._{0}.ToStream(sw)", p.PropertyName)));
                    else
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
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));
            m.Statements.Add(new CodeSnippetExpression("base.FromStream(sr)"));

            foreach (BaseProperty p in properties)
            {
                if (p.IsListProperty())
                {
                    if (current.clientServer == ClientServerEnum.Client)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("this._{0}.FromStream(sr)", p.PropertyName)));
                    else
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(this.{0}, sr)", p.PropertyName)));
                }
                else if (p is EnumerationProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("int{2} tmp{0}; BinarySerializer.FromBinary(out tmp{0}, sr); _{0} = ({1})tmp{0}",
                        p.PropertyName, p.ToCodeTypeReference(current.clientServer).BaseType, ((Property)p).IsNullable ? "?" : "")));
                }
                else if (p is ValueTypeProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._{0}, sr)", p.PropertyName)));
                }
                else if (p.IsStructPropertySingle())
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._{0}, sr)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._fk_{0}, sr)", p.PropertyName)));
                }
                else if (p is BackReferenceProperty
                    && current.clientServer == ClientServerEnum.Client
                    && ((BackReferenceProperty)p).PreFetchToClient)
                {
                    m.Statements.Add(new CodeSnippetExpression(
                        string.Format("this._{0} = new BackReferenceCollection<{1}>(\"{2}\", this); BinarySerializer.FromBinary(this._{0}, sr)",
                            p.PropertyName, p.GetPropertyTypeString(),
                            ((BackReferenceProperty)p).ReferenceProperty.PropertyName)));
                }
            }
        }
        #endregion
    }

    #region DataObjectGeneratorFactory
    public static class DataObjectGeneratorFactory
    {
        public static BaseDataObjectGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDataObjectGenerator();
        }
    }
    #endregion
}
