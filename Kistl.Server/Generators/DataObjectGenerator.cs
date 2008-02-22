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

        public class Current
        {
            public Kistl.API.Server.KistlDataContext ctx;

            public ClientServerEnum clientServer;
            public CodeCompileUnit code;
            public CodeNamespace code_namespace;
            public CodeTypeDeclaration code_class;
            public CodeMemberField code_field;
            public CodeMemberProperty code_property;

            public ObjectClass objClass;
            public BaseProperty property;

            public Current Clone()
            {
                Current result = new Current();

                result.ctx = this.ctx;

                result.clientServer = this.clientServer;
                result.code = this.code;
                result.code_namespace = this.code_namespace;
                result.code_class = this.code_class;
                result.code_field = this.code_field;
                result.code_property = this.code_property;

                result.objClass = this.objClass;
                result.property = this.property;

                return result;
            }
        }

        public virtual void Generate(Kistl.API.Server.KistlDataContext ctx, string codeBasePath)
        {
            this.codeBasePath = codeBasePath + (codeBasePath.EndsWith("\\") ? "" : "\\");
            Directory.CreateDirectory(codeBasePath);

            var objClassList = Generator.GetObjectClassList(ctx);

            Directory.GetFiles(this.codeBasePath, "*.cs", SearchOption.AllDirectories).
                ToList().ForEach(f => File.Delete(f));

            foreach (ObjectClass objClass in objClassList)
            {
                GenerateObjectsInternal(new Current() { ctx = ctx, clientServer = ClientServerEnum.Client, objClass = objClass });
                GenerateObjectsInternal(new Current() { ctx = ctx, clientServer = ClientServerEnum.Server, objClass = objClass });
            }

            GenerateObjectSerializer(ClientServerEnum.Server, objClassList);
            GenerateObjectSerializer(ClientServerEnum.Client, objClassList);

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
        protected virtual void GenerateObjects(Current current)
        {
        }

        private void GenerateObjectsInternal(Current current)
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

            GenerateObjects(current);

            if (current.objClass.BaseObjectClass == null)
            {
                // Create Default Properties
                GenerateDefaultPropertiesInternal(current.Clone());
            }

            // Create Properties
            GeneratePropertiesInternal(current.Clone());

            // Create DataObject Default Methods
            GenerateDefaultMethodsInternal(current.Clone());

            // Create DataObject Methods
            GenerateMethodsInternal(current.Clone());

            // Create DataObject StreamingMethods
            GenerateStreamMethodsInternal(current.Clone());

            // Generate the code & save
            SaveFile(current.code, "Kistl.Objects." + current.clientServer + @"\" + current.objClass.ClassName + "." + current.clientServer + ".Designer.cs");
        }
        #endregion

        #region GenerateDefaultProperties
        protected virtual void GenerateDefaultProperty_ID(Current current)
        {
        }

        private void GenerateDefaultProperty_IDInternal(Current current)
        {
            // Create ID member
            current.code_field = CreateField(current.code_class, typeof(int), "_ID", "Helper.INVALIDID");

            current.code_property = CreateProperty(current.code_class, typeof(int), "ID");
            current.code_property.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            current.code_property.GetStatements.Add(new CodeSnippetExpression("return _ID"));
            current.code_property.SetStatements.Add(new CodeSnippetExpression("_ID = value"));

            GenerateDefaultProperty_ID(current);
        }

        protected virtual void GenerateDefaultProperty_EntitySetName(Current current)
        {
        }

        private void GenerateDefaultProperty_EntitySetNameInternal(Current current)
        {
            current.code_property = CreateProperty(current.code_class, typeof(string), "EntitySetName", false);
            current.code_property.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            current.code_property.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(current.objClass.ClassName)));

            GenerateDefaultProperty_EntitySetName(current);
        }
        
        protected virtual void GenerateDefaultPropertiesInternal(Current current)
        {
            GenerateDefaultProperty_IDInternal(current.Clone());

            if (current.clientServer == ClientServerEnum.Server)
            {
                GenerateDefaultProperty_EntitySetNameInternal(current.Clone());
            }
        }
        #endregion

        #region GenerateProperties
        
        #region GenerateValueTypeProperty
        protected virtual void GenerateProperties_ValueTypeProperty(Current current)
        {
        }

        private void GenerateProperties_ValueTypePropertyInternal(Current current)
        {
            if (string.IsNullOrEmpty(current.property.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     current.objClass.ClassName, current.property.PropertyName));

            Type t = Type.GetType(current.property.GetDataType());
            if (t == null) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                     current.objClass.ClassName, current.property.PropertyName, current.property.GetDataType()));

            current.code_field = CreateField(current.code_class, current.property.GetDataType() + ((t.IsValueType && ((ValueTypeProperty)current.property).IsNullable) ? "?" : ""),
                "_" + current.property.PropertyName);

            current.code_property = CreateProperty(current.code_class, current.code_field.Type, current.property.PropertyName);

            current.code_property.GetStatements.Add(new CodeSnippetExpression("return " + current.code_field.Name));
            current.code_property.SetStatements.Add(new CodeSnippetExpression(current.code_field.Name + " = value"));

            GenerateProperties_ValueTypeProperty(current);
        }
        #endregion

        #region GenerateValueTypeProperty_Collection
        protected virtual void GenerateProperties_ValueTypeProperty_Collection(Current current, Current collectionClass)
        {
        }

        private void GenerateProperties_ValueTypeProperty_CollectionInternal(Current current)
        {
            if (string.IsNullOrEmpty(current.property.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     current.objClass.ClassName, current.property.PropertyName));

            Type t = Type.GetType(current.property.GetDataType());
            if (t == null) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                     current.objClass.ClassName, current.property.PropertyName, current.property.GetDataType()));

            Current collectionClass = current.Clone();

            collectionClass.code_class = CreateClass(collectionClass.code_namespace, Generator.GetPropertyCollectionObjectType((Property)current.property).Classname,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.clientServer));
            
            // Create ID
            GenerateDefaultProperty_IDInternal(collectionClass.Clone());

            // Create Property
            collectionClass.code_field = CreateField(collectionClass.code_class,
               current.property.GetDataType() + ((t.IsValueType && ((ValueTypeProperty)current.property).IsNullable) ? "?" : ""), "_Value");

            collectionClass.code_property = CreateProperty(collectionClass.code_class, collectionClass.code_field.Type, "Value");

            collectionClass.code_property.GetStatements.Add(new CodeSnippetExpression("return _Value"));
            collectionClass.code_property.SetStatements.Add(new CodeSnippetExpression("_Value = value"));

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = CreateProperty(current.code_class, (CodeTypeReference)null, current.property.PropertyName);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference(string.Format("List<{0}>", collectionClass.code_class.Name));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"return _{0}", current.property.PropertyName)));
                current.code_property.SetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"_{0} = value", current.property.PropertyName)));

                current.code_field = CreateField(current.code_class, current.code_property.Type, string.Format(@"_{0}", current.property.PropertyName));
                current.code_field.InitExpression = new CodeSnippetExpression(string.Format("new List<{0}>()", collectionClass.code_class.Name));
            }
            else
            {
                current.code_property.HasSet = false;
            }

            GenerateProperties_ValueTypeProperty_Collection(current, collectionClass);
            GenerateProperties_ValueTypeProperty_Collection_StreamMethods(collectionClass);
        }

        #region GenerateProperties_ValueTypeProperty_Collection_StreamMethods
        private void GenerateProperties_ValueTypeProperty_Collection_StreamMethods(Current current)
        {
            // Create ToStream Method
            CodeMemberMethod m = CreateOverrideMethod(current.code_class, "ToStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));

            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.ToBinary(this.Value, sw)"));

            m = CreateOverrideMethod(current.code_class, "FromStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.Add(new CodeSnippetExpression("base.FromStream(ctx, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._Value, sr)"));

            m = CreateOverrideMethod(current.code_class, "CopyTo", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

            m.Statements.Add(new CodeSnippetExpression("base.CopyTo(obj)"));// TODO: Das ist C# spezifisch
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj).NotifyPropertyChanging(\"Value\")", current.code_class.Name)));
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj)._Value = this._Value", current.code_class.Name)));
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj).NotifyPropertyChanged(\"Value\")", current.code_class.Name)));
        }
        #endregion

        #endregion

        #region GenerateProperties_ObjectReferenceProperty
        protected virtual void GenerateProperties_ObjectReferenceProperty(Current current, Current serializer)
        {
        }

        private void GenerateProperties_ObjectReferencePropertyInternal(Current current)
        {
            // Check if Datatype exits
            if (current.ctx.GetTable<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetDataType(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = CreateProperty(current.code_class, current.property.GetDataType(), current.property.PropertyName);
            current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            // ObjectType otherType = new ObjectType(prop.GetDataType());
            Current serializer = current.Clone();

            // Serializer fk_ Field und Property
            serializer.code_field = CreateField(current.code_class, typeof(int), "_fk_" + current.property.PropertyName, "Helper.INVALIDID");
            serializer.code_property = CreateProperty(current.code_class, typeof(int), "fk_" + current.property.PropertyName);

            GenerateProperties_ObjectReferenceProperty(current, serializer);
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty_Collection
        protected virtual void GenerateProperties_ObjectReferenceProperty_Collection(Current current, Current collectionClass, Current serializer)
        {
        }

        private void GenerateProperties_ObjectReferenceProperty_CollectionInternal(Current current)
        {
            if (string.IsNullOrEmpty(current.property.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     current.objClass.ClassName, current.property.PropertyName));

            // Check if Datatype exits
            if (current.ctx.GetTable<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetDataType(), current.objClass.ClassName, current.property.PropertyName));

            Current collectionClass = current.Clone();

            collectionClass.code_class = CreateClass(collectionClass.code_namespace, Generator.GetPropertyCollectionObjectType((Property)current.property).Classname,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.clientServer));

            // Create ID
            GenerateDefaultProperty_IDInternal(collectionClass.Clone());

            collectionClass.code_property = CreateProperty(collectionClass.code_class, collectionClass.property.GetDataType(), "Value");
            collectionClass.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            Current serializer = collectionClass.Clone();

            // Serializer fk_ Field und Property
            serializer.code_field = CreateField(collectionClass.code_class, typeof(int), "_fk_Value", "Helper.INVALIDID");
            serializer.code_property = CreateProperty(collectionClass.code_class, typeof(int), "fk_Value");

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = CreateProperty(current.code_class, (CodeTypeReference)null, current.property.PropertyName);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference(string.Format("List<{0}>", collectionClass.code_class.Name));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"return _{0}", current.property.PropertyName)));
                current.code_property.SetStatements.Add(
                    new CodeSnippetExpression(string.Format(@"_{0} = value", current.property.PropertyName)));

                current.code_field = CreateField(current.code_class, current.code_property.Type, string.Format(@"_{0}", current.property.PropertyName));
                current.code_field.InitExpression = new CodeSnippetExpression(string.Format("new List<{0}>()", collectionClass.code_class.Name));
            }
            else
            {
                current.code_property.HasSet = false;
            }

            GenerateProperties_ObjectReferenceProperty_Collection(current, collectionClass, serializer);
            GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods(collectionClass, serializer);
        }

        #region GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods
        private void GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods(Current current, Current serializer)
        {
            // Create ToStream Method
            CodeMemberMethod m = CreateOverrideMethod(current.code_class, "ToStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));

            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.ToBinary(this.fk_Value, sw)"));

            m = CreateOverrideMethod(current.code_class, "FromStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.Add(new CodeSnippetExpression("base.FromStream(ctx, sr)"));
            m.Statements.Add(new CodeSnippetExpression("BinarySerializer.FromBinary(out this._fk_Value, sr)"));

            m = CreateOverrideMethod(current.code_class, "CopyTo", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

            m.Statements.Add(new CodeSnippetExpression("base.CopyTo(obj)"));// TODO: Das ist C# spezifisch
            m.Statements.Add(new CodeSnippetExpression(string.Format("(({0})obj).fk_Value = this.fk_Value", current.code_class.Name)));
        }

        #endregion

        #endregion

        #region GenerateProperties_BackReferenceProperty
        protected virtual void GenerateProperties_BackReferenceProperty(Current current)
        {
        }

        private void GenerateProperties_BackReferencePropertyInternal(Current current)
        {
            // Check if Datatype exits
            if (current.ctx.GetTable<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on BackReferenceProperty {1}.{2}",
                    current.property.GetDataType(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = CreateProperty(current.code_class, (CodeTypeReference)null, current.property.PropertyName, false);
            current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            GenerateProperties_BackReferenceProperty(current);
        }
        #endregion

        private void GeneratePropertiesInternal(Current current)
        {
            foreach (BaseProperty baseProp in current.objClass.Properties)
            {
                current.property = baseProp;
                if (baseProp is ValueTypeProperty && ((ValueTypeProperty)baseProp).IsList)
                {
                    // Simple Property Collection
                    GenerateProperties_ValueTypeProperty_CollectionInternal(current.Clone());
                }
                else if (baseProp is ValueTypeProperty)
                {
                    // Simple Property
                    GenerateProperties_ValueTypePropertyInternal(current.Clone());
                }
                else if (baseProp is ObjectReferenceProperty && ((ObjectReferenceProperty)baseProp).IsList)
                {
                    // "pointer" Objekt Collection
                    GenerateProperties_ObjectReferenceProperty_CollectionInternal(current.Clone());
                }
                else if (baseProp is ObjectReferenceProperty)
                {
                    // "pointer" Objekt
                    GenerateProperties_ObjectReferencePropertyInternal(current.Clone());
                }
                else if (baseProp is BackReferenceProperty)
                {
                    // TODO: Das ist eigentlich falsch herum, das sollte generiert werden,
                    // wenn bei der referenzierenden Klasse ein FK eingetragen wurde.
                    // Das sind quasi "AutoProperties"
                    GenerateProperties_BackReferencePropertyInternal(current.Clone());
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
        private void GenerateDefaultMethodsInternal(Current current)
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
                    stmt = string.Format("(({1})obj).{0} = this.{0}", p.PropertyName, current.objClass.ClassName);
                }
                    // TODO: Geht z.Z. nur für den Client!!!!
                else if (p is ValueTypeProperty && ((ValueTypeProperty)p).IsList && current.clientServer == ClientServerEnum.Client)
                {
                    stmt = string.Format("(({1})obj)._{0} = this.{0}.Clone()", p.PropertyName, current.objClass.ClassName);
                }
                else if (p is ObjectReferenceProperty && !((ObjectReferenceProperty)p).IsList)
                {
                    stmt = string.Format("(({1})obj).fk_{0} = this.fk_{0}", p.PropertyName, current.objClass.ClassName);
                }
                // TODO: Geht z.Z. nur für den Client!!!!
                else if (p is ObjectReferenceProperty && ((ObjectReferenceProperty)p).IsList && current.clientServer == ClientServerEnum.Client)
                {
                    stmt = string.Format("(({1})obj)._{0} = this.{0}.Clone()", p.PropertyName, current.objClass.ClassName);
                }

                if (!string.IsNullOrEmpty(stmt))
                {
                    if(p is ValueTypeProperty)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("(({1})obj).NotifyPropertyChanging(\"{0}\")", p.PropertyName, current.objClass.ClassName)));
                    
                    m.Statements.Add(new CodeSnippetExpression(stmt));

                    if (p is ValueTypeProperty)
                        m.Statements.Add(new CodeSnippetExpression(string.Format("(({1})obj).NotifyPropertyChanged(\"{0}\")", p.PropertyName, current.objClass.ClassName)));
                }
            }

            if (current.clientServer == ClientServerEnum.Client)
            {
                // Create AttachToContext Method
                m = CreateOverrideMethod(current.code_class, "AttachToContext", typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("KistlContext"), "ctx"));

                m.Statements.Add(new CodeSnippetExpression("base.AttachToContext(ctx)"));

                foreach (Property p in current.objClass.Properties.OfType<Property>().Where(p => p.IsList))
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format(@"_{0}.ForEach(i => i.AttachToContext(ctx));", p.PropertyName)));
                }
            }
        }
        #endregion

        #region GenerateMethods
        private void GenerateMethodsInternal(Current current)
        {
            ObjectClass baseObjClass = current.objClass;
            ObjectClass objClass = current.objClass;
            while (objClass != null)
            {
                foreach (Method method in objClass.Methods)
                {
                    // TODO: Das ist nicht ganz sauber
                    // Sobald man aber Parameter angeben kann, wird hoffentlich besser
                    // Default Methods, do not generate
                    if (method.Module.ModuleName == "KistlBase")
                    {
                        if (method.MethodName == "ToString"
                            || method.MethodName == "PreSave"
                            || method.MethodName == "PostSave"
                        )
                            continue;
                    }

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
                        d.Parameters.Add(new CodeParameterDeclarationExpression(
                            new CodeTypeReference("MethodReturnEventArgs", new CodeTypeReference(typeof(string))),
                            "e"));
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
                    m.ReturnType = new CodeTypeReference(typeof(string));

                    if (objClass == baseObjClass)
                    {
                        m.Statements.Add(new CodeSnippetExpression(string.Format(@"MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            if (On{1}_{0} != null)
            {{
                On{1}_{0}(this, e);
            }}
            return e.Result", baseObjClass.ClassName, method.MethodName)));// TODO: Das ist C# spezifisch
                    }
                    else
                    {
                        m.Statements.Add(new CodeSnippetExpression(string.Format(@"MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.{1}();
            if (On{1}_{0} != null)
            {{
                On{1}_{0}(this, e);
            }}
            return e.Result", baseObjClass.ClassName, method.MethodName)));// TODO: Das ist C# spezifisch
                    }
                }

                // Nächster bitte
                objClass = objClass.BaseObjectClass;
            }
        }
        #endregion

        #region GenerateStreamMethods
        private void GenerateStreamMethodsInternal(Current current)
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
                if (p is ValueTypeProperty)
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
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(out this._{0}, sr, ctx)", p.PropertyName)));
                    else
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(this.{0}, sr, ctx)", p.PropertyName)));
                }
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
                        m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinaryCollectionEntries(out this._{0}, sr, ctx)", p.PropertyName)));
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
