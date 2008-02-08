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
        protected Kistl.API.Server.KistlDataContext ctx = null;

        public virtual void Generate(Kistl.API.Server.KistlDataContext ctx, string codeBasePath)
        {
            this.ctx = ctx;
            this.codeBasePath = codeBasePath + (codeBasePath.EndsWith("\\") ? "" : "\\");
            Directory.CreateDirectory(codeBasePath);

            var objClassList = from c in ctx.GetTable<ObjectClass>()
                               select c;

            Directory.GetFiles(this.codeBasePath, "*.cs", SearchOption.AllDirectories).
                ToList().ForEach(f => File.Delete(f));

            foreach (ObjectClass objClass in objClassList)
            {
                GenerateObjects(ClientServerEnum.Client, objClass);
                GenerateObjects(ClientServerEnum.Server, objClass);
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
            CodeNamespace ns = new CodeNamespace("Kistl.API");
            code.Namespaces.Add(ns);

            AddDefaultNamespaces(ns);

            // XMLObjectCollection
            CodeTypeDeclaration c = new CodeTypeDeclaration("XMLObjectCollection");
            ns.Types.Add(c);
            c.IsClass = true;
            c.BaseTypes.Add(new CodeTypeReference("IXmlObjectCollection"));
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable"));
            c.CustomAttributes.Add(new CodeAttributeDeclaration("XmlRoot", new CodeAttributeArgument("ElementName", new CodePrimitiveExpression("ObjectCollection"))));

            CodeMemberField f = new CodeMemberField(new CodeTypeReference("List", new CodeTypeReference("Object")), "_Objects");
            c.Members.Add(f);
            f.Attributes = MemberAttributes.Private;
            f.InitExpression = new CodeObjectCreateExpression(
                new CodeTypeReference("List", new CodeTypeReference("Object")));

            CodeMemberProperty p = new CodeMemberProperty();
            c.Members.Add(p);
            p.Name = "Objects";
            p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            p.HasGet = true;
            p.HasSet = false;
            p.Type = f.Type;
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("_Objects")));
            foreach (ObjectClass objClass in objClassList)
            {
                p.CustomAttributes.Add(
                    new CodeAttributeDeclaration("XmlArrayItem",
                        new CodeAttributeArgument("Type", new CodeTypeOfExpression(objClass.Module.Namespace + "." + objClass.ClassName)),
                        new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(objClass.ClassName))
                    ));
            }

            CodeMemberMethod m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            m.Name = "ToList";
            m.ReturnType = new CodeTypeReference("List", new CodeTypeReference("T"));
            CodeTypeParameter ct = new CodeTypeParameter("T");
            ct.Constraints.Add("IDataObject");
            m.TypeParameters.Add(ct);
            m.Statements.Add(new CodeSnippetExpression(@"return new List<T>(Objects.OfType<T>())"));

            // XMLObject
            c = new CodeTypeDeclaration("XMLObject");
            c.BaseTypes.Add(new CodeTypeReference("IXmlObject"));
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable"));
            c.CustomAttributes.Add(new CodeAttributeDeclaration("XmlRoot", new CodeAttributeArgument("ElementName", new CodePrimitiveExpression("Object"))));

            f = new CodeMemberField(new CodeTypeReference("Object"), "_Object");
            c.Members.Add(f);
            f.Attributes = MemberAttributes.Private;

            p = new CodeMemberProperty();
            c.Members.Add(p);
            p.Name = "Object";
            p.Type = new CodeTypeReference("Object");
            p.HasGet = true;
            p.HasSet = true;
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("_Object")));
            p.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("_Object"), new CodePropertySetValueReferenceExpression()));
            p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
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
        protected virtual void GenerateObjects(ClientServerEnum clientServer, ObjectClass objClass,
            CodeCompileUnit code, CodeNamespace n, CodeTypeDeclaration c)
        {
        }

        private void GenerateObjects(ClientServerEnum clientServer, ObjectClass objClass)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            // Create Namespace
            CodeNamespace ns = new CodeNamespace(objClass.Module.Namespace);
            code.Namespaces.Add(ns);

            AddDefaultNamespaces(ns);
            ns.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", clientServer)));

            // Create Class
            CodeTypeDeclaration c = new CodeTypeDeclaration(objClass.ClassName);
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public;
            if (objClass.BaseObjectClass != null)
            {
                c.BaseTypes.Add(objClass.BaseObjectClass.Module.Namespace + "." + objClass.BaseObjectClass.ClassName);
            }
            else
            {
                c.BaseTypes.Add(string.Format("Base{0}DataObject", clientServer));
            }
            c.BaseTypes.Add("ICloneable");

            GenerateObjects(clientServer, objClass, code, ns, c);

            if (objClass.BaseObjectClass == null)
            {
                // Create Default Properties
                GenerateDefaultProperties(clientServer, objClass, c);
            }

            // Create Properties
            GenerateProperties(clientServer, code, objClass, c, ns);

            // Create DataObject Default Methods
            GenerateDefaultMethods(clientServer, objClass, c);

            // Create DataObject Methods
            GenerateMethods(clientServer, objClass, c);

            // Create DataObject StreamingMethods
            GenerateStreamMethods(clientServer, objClass, c);

            // Generate the code & save
            SaveFile(code, "Kistl.Objects." + clientServer + @"\" + objClass.ClassName + "." + clientServer + ".Designer.cs");
        }
        #endregion

        #region GenerateDefaultProperties
        protected virtual void GenerateDefaultProperty_ID(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c,
            CodeMemberField f, CodeMemberProperty p)
        {
        }

        private void GenerateDefaultProperty_ID(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c)
        {
            // Create ID member
            CodeMemberField f = new CodeMemberField(typeof(int), "_ID");
            f.InitExpression = new CodeSnippetExpression("Helper.INVALIDID"); // TODO: Das ist c# Spezifisch
            c.Members.Add(f);

            CodeMemberProperty p = new CodeMemberProperty();
            c.Members.Add(p);

            p.Name = "ID";
            p.HasGet = true;
            p.HasSet = true;
            p.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            p.Type = new CodeTypeReference(typeof(int));

            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_ID")));
            p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_ID"), new CodePropertySetValueReferenceExpression()));

            GenerateDefaultProperty_ID(clientServer, objClass, c, f, p);
        }

        protected virtual void GenerateDefaultProperty_EntitySetName(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c,
            CodeMemberProperty p)
        {
        }

        private void GenerateDefaultProperty_EntitySetName(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c)
        {
            CodeMemberProperty p = new CodeMemberProperty();
            c.Members.Add(p);

            p.Name = "EntitySetName";
            p.HasGet = true;
            p.HasSet = false;
            p.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            p.Type = new CodeTypeReference(typeof(string));
            p.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(objClass.ClassName)));

            GenerateDefaultProperty_EntitySetName(clientServer, objClass, c, p);
        }
        
        protected virtual void GenerateDefaultProperties(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c)
        {
            GenerateDefaultProperty_ID(clientServer, objClass, c);

            if (clientServer == ClientServerEnum.Server)
            {
                GenerateDefaultProperty_EntitySetName(clientServer, objClass, c);
            }
        }
        #endregion

        #region GenerateProperties
        #region GenerateValueTypeProperty
        protected virtual void GenerateProperties_ValueTypeProperty(ClientServerEnum clientServer, ObjectClass objClass, ValueTypeProperty prop,
            CodeTypeDeclaration c, CodeMemberField f, CodeMemberProperty p)
        {
        }

        private void GenerateProperties_ValueTypeProperty(ClientServerEnum clientServer, ObjectClass objClass, ValueTypeProperty prop, CodeTypeDeclaration c)
        {
            if (string.IsNullOrEmpty(prop.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     objClass.ClassName, prop.PropertyName));

            Type t = Type.GetType(prop.GetDataType());
            if (t == null) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                     objClass.ClassName, prop.PropertyName, prop.GetDataType()));

            CodeTypeReference ctr = null;

            if (t.IsValueType && prop.IsNullable)
            {
                ctr = new CodeTypeReference("System.Nullable", new CodeTypeReference(prop.GetDataType()));
            }
            else
            {
                ctr = new CodeTypeReference(prop.GetDataType());
            }
            CodeMemberField f = new CodeMemberField(ctr,
                "_" + prop.PropertyName);

            c.Members.Add(f);

            CodeMemberProperty p = new CodeMemberProperty();
            c.Members.Add(p);

            p.Name = prop.PropertyName;
            p.HasGet = true;
            p.HasSet = true;
            p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            p.Type = f.Type;

            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_" + prop.PropertyName)));
            p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_" + prop.PropertyName), new CodePropertySetValueReferenceExpression()));

            GenerateProperties_ValueTypeProperty(clientServer, objClass, prop, c, f, p);
        }
        #endregion

        #region GenerateValueTypeProperty_Collection
        protected virtual void GenerateProperties_ValueTypeProperty_Collection(ClientServerEnum clientServer, CodeCompileUnit code, ObjectClass objClass, ValueTypeProperty prop, CodeTypeDeclaration c,
            CodeTypeDeclaration collectionClass, CodeMemberField f, CodeMemberProperty p, 
            CodeMemberProperty navigationProperty, ObjectType otherType, string assocName)
        {
        }

        private void GenerateProperties_ValueTypeProperty_Collection(ClientServerEnum clientServer, CodeCompileUnit code, ObjectClass objClass, ValueTypeProperty prop, CodeTypeDeclaration c, CodeNamespace ns)
        {
            if (string.IsNullOrEmpty(prop.GetDataType())) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetDataType()",
                     objClass.ClassName, prop.PropertyName));

            Type t = Type.GetType(prop.GetDataType());
            if (t == null) throw new ApplicationException(
                 string.Format("ValueProperty {0}.{1} has a invalid Datatype of {2}",
                     objClass.ClassName, prop.PropertyName, prop.GetDataType()));

            CodeTypeDeclaration collectionClass = new CodeTypeDeclaration(objClass.ClassName + "_" + prop.PropertyName + "CollectionEntry");
            ns.Types.Add(collectionClass);
            collectionClass.IsClass = true;
            collectionClass.TypeAttributes = TypeAttributes.Public;
            if (clientServer == ClientServerEnum.Client)
            {
                collectionClass.BaseTypes.Add("Kistl.API.Client.BaseClientCollectionEntry");
            }
            else
            {
                collectionClass.BaseTypes.Add("Kistl.API.Server.BaseServerCollectionEntry");
            }

            // Create ID
            GenerateDefaultProperty_ID(clientServer, objClass, collectionClass);

            // Create Property
            CodeTypeReference ctr = null;

            if (t.IsValueType && prop.IsNullable)
            {
                ctr = new CodeTypeReference("System.Nullable", new CodeTypeReference(prop.GetDataType()));
            }
            else
            {
                ctr = new CodeTypeReference(prop.GetDataType());
            }
            CodeMemberField f = new CodeMemberField(ctr,
                "_" + prop.PropertyName);

            collectionClass.Members.Add(f);

            CodeMemberProperty p = new CodeMemberProperty();
            collectionClass.Members.Add(p);

            p.Name = prop.PropertyName;
            p.HasGet = true;
            p.HasSet = true;
            p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            p.Type = f.Type;

            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_" + prop.PropertyName)));
            p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_" + prop.PropertyName), new CodePropertySetValueReferenceExpression()));

            // Create NavigationProperty
            CodeMemberProperty navigationProperty = new CodeMemberProperty();
            c.Members.Add(navigationProperty);

            navigationProperty.Name = prop.PropertyName;
            navigationProperty.HasGet = true;
            navigationProperty.HasSet = false;
            navigationProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            if (clientServer == ClientServerEnum.Client)
            {
                navigationProperty.Type = new CodeTypeReference("List", new CodeTypeReference(collectionClass.Name));

                navigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(
                        @"return _{0}",
                    prop.PropertyName, prop.GetDataType())));

                CodeMemberField pf = new CodeMemberField(navigationProperty.Type,
                    "_" + prop.PropertyName);

                c.Members.Add(pf);
            }

            string assocName = "FK_" + collectionClass.Name + "_" + objClass.ClassName;
            ObjectType otherType = new ObjectType(ns.Name, collectionClass.Name);

            GenerateProperties_ValueTypeProperty_Collection(clientServer, code, objClass, prop, c, collectionClass, f, p,
                navigationProperty, otherType, assocName);
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty
        protected virtual void GenerateProperties_ObjectReferenceProperty(ClientServerEnum clientServer, ObjectClass objClass, ObjectReferenceProperty prop,
            CodeTypeDeclaration c, CodeMemberProperty navigationProperty,
            CodeMemberField serializerField, CodeMemberProperty serializerProperty, ObjectType otherType, string assocName)
        {
        }

        private void GenerateProperties_ObjectReferenceProperty(ClientServerEnum clientServer, ObjectClass objClass, ObjectReferenceProperty prop, CodeTypeDeclaration c)
        {
            // Check if Datatype exits
            if (ctx.GetTable<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == prop.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    prop.GetDataType(), objClass.ClassName, prop.PropertyName));

            CodeMemberProperty navigationProperty = new CodeMemberProperty();
            c.Members.Add(navigationProperty);

            navigationProperty.Name = prop.PropertyName;
            navigationProperty.HasGet = true;
            navigationProperty.HasSet = true;
            navigationProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            navigationProperty.Type = new CodeTypeReference(prop.GetDataType());

            navigationProperty.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            ObjectType otherType = new ObjectType(prop.GetDataType());
            string assocName = "FK_" + objClass.ClassName + "_" + otherType.Classname;

            // Serializer fk_ Field und Property
            CodeMemberField serializerField = new CodeMemberField(typeof(int), "_fk_" + prop.PropertyName);
            serializerField.InitExpression = new CodeSnippetExpression("Helper.INVALIDID"); // TODO: Das ist c# Spezifisch
            c.Members.Add(serializerField);

            CodeMemberProperty serializerProperty = new CodeMemberProperty();
            c.Members.Add(serializerProperty);
            serializerProperty.Name = "fk_" + prop.PropertyName;
            serializerProperty.HasGet = true;
            serializerProperty.HasSet = true;
            serializerProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            serializerProperty.Type = new CodeTypeReference(typeof(int));

            GenerateProperties_ObjectReferenceProperty(clientServer, objClass, prop, c, 
                navigationProperty, serializerField, serializerProperty, otherType, assocName);
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty_Collection
        protected virtual void GenerateProperties_ObjectReferenceProperty_Collection(ClientServerEnum clientServer, ObjectClass objClass, ObjectReferenceProperty prop,
            CodeTypeDeclaration c, CodeMemberProperty navigationProperty,
            CodeMemberField serializerField, CodeMemberProperty serializerProperty, ObjectType otherType, string assocName)
        {
        }

        private void GenerateProperties_ObjectReferenceProperty_Collection(ClientServerEnum clientServer, ObjectClass objClass, ObjectReferenceProperty prop, CodeTypeDeclaration c)
        {
            //GenerateProperties_ObjectReferenceProperty_Collection(clientServer, objClass, prop, c,
            //    navigationProperty, serializerField, serializerProperty, otherType, assocName);
        }
        #endregion

        #region GenerateProperties_BackReferenceProperty
        protected virtual void GenerateProperties_BackReferenceProperty(ClientServerEnum clientServer, ObjectClass objClass, BackReferenceProperty prop,
            CodeTypeDeclaration c, CodeMemberProperty navigationProperty,
            ObjectType otherType, string assocName)
        {
        }

        private void GenerateProperties_BackReferenceProperty(ClientServerEnum clientServer, ObjectClass objClass, BackReferenceProperty prop, CodeTypeDeclaration c)
        {
            // Check if Datatype exits
            if (ctx.GetTable<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == prop.GetDataType()) == null)
                throw new ApplicationException(string.Format("ObjectReference {0} not found on BackReferenceProperty {1}.{2}",
                    prop.GetDataType(), objClass.ClassName, prop.PropertyName));

            CodeMemberProperty navigationProperty = new CodeMemberProperty();
            c.Members.Add(navigationProperty);

            navigationProperty.Name = prop.PropertyName;
            navigationProperty.HasGet = true;
            navigationProperty.HasSet = false;
            navigationProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            navigationProperty.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));

            ObjectType otherType = new ObjectType(prop.GetDataType());
            string assocName = "FK_" + otherType.Classname + "_" + objClass.ClassName;

            GenerateProperties_BackReferenceProperty(clientServer, objClass, prop, c,
                navigationProperty, otherType, assocName);
        }
        #endregion

        protected virtual void GenerateProperties(ClientServerEnum clientServer, CodeCompileUnit code, ObjectClass objClass, CodeTypeDeclaration c, CodeNamespace ns)
        {
            foreach (BaseProperty baseProp in objClass.Properties)
            {
                if (baseProp is ValueTypeProperty && ((ValueTypeProperty)baseProp).IsList)
                {
                    // Simple Property Collection
                    GenerateProperties_ValueTypeProperty_Collection(clientServer, code, objClass, (ValueTypeProperty)baseProp, c, ns);
                }
                else if (baseProp is ValueTypeProperty)
                {
                    // Simple Property
                    GenerateProperties_ValueTypeProperty(clientServer, objClass, (ValueTypeProperty)baseProp, c);
                }
                else if (baseProp is ObjectReferenceProperty && ((ObjectReferenceProperty)baseProp).IsList)
                {
                    // "pointer" Objekt Collection
                    GenerateProperties_ObjectReferenceProperty_Collection(clientServer, objClass, (ObjectReferenceProperty)baseProp, c);
                }
                else if (baseProp is ObjectReferenceProperty)
                {
                    // "pointer" Objekt
                    GenerateProperties_ObjectReferenceProperty(clientServer, objClass, (ObjectReferenceProperty)baseProp, c);
                }
                else if (baseProp is BackReferenceProperty)
                {
                    // TODO: Das ist eigentlich falsch herum, das sollte generiert werden,
                    // wenn bei der referenzierenden Klasse ein FK eingetragen wurde.
                    // Das sind quasi "AutoProperties"
                    GenerateProperties_BackReferenceProperty(clientServer, objClass, (BackReferenceProperty)baseProp, c);
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
        protected virtual void GenerateDefaultMethods(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c)
        {
            // Create ToString Delegate
            CodeMemberEvent e = new CodeMemberEvent();
            c.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ToStringHandler", new CodeTypeReference(objClass.ClassName));
            e.Name = "OnToString_" + objClass.ClassName;

            // Create PreSave Delegate
            e = new CodeMemberEvent();
            c.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(objClass.ClassName));
            e.Name = "OnPreSave_" + objClass.ClassName;

            // Create PostSave Delegate
            e = new CodeMemberEvent();
            c.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(objClass.ClassName));
            e.Name = "OnPostSave_" + objClass.ClassName;

            // Create ToString Method
            CodeMemberMethod m = new CodeMemberMethod();
            c.Members.Add(m);
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
            return e.Result", objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create NotifyPreSave Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "NotifyPreSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"base.NotifyPreSave();
            if (OnPreSave_{0} != null) OnPreSave_{0}(this)", objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create NotifyPostSave Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "NotifyPostSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"base.NotifyPostSave();
            if (OnPostSave_{0} != null) OnPostSave_{0}(this)", objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create Clone Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "Clone";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(object));
            m.Statements.Add(new CodeSnippetExpression(string.Format(@"{0} obj = new {0}();
            CopyTo(obj);
            return obj", objClass.ClassName)));// TODO: Das ist C# spezifisch

            // Create CopyTo Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "CopyTo";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(objClass.ClassName), "obj"));
            m.Statements.Add(new CodeSnippetExpression("base.CopyTo(obj)"));// TODO: Das ist C# spezifisch

            foreach (BaseProperty p in objClass.Properties)
            {
                if (p is ValueTypeProperty && !((ValueTypeProperty)p).IsList)
                {
                    m.Statements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("obj"), p.PropertyName),
                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty && !((ObjectReferenceProperty)p).IsList)
                {
                    m.Statements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("obj"), "fk_" + p.PropertyName),
                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "fk_" + p.PropertyName)));
                }
            }
        }
        #endregion

        #region GenerateMethods
        protected virtual void GenerateMethods(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c)
        {
            ObjectClass baseObjClass = objClass;
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

                        c.Members.Add(d);
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
                    c.Members.Add(e);

                    e.Attributes = MemberAttributes.Public;
                    e.Type = new CodeTypeReference(method.MethodName + "_Handler",
                        new CodeTypeReference(baseObjClass.ClassName));
                    e.Name = "On" + method.MethodName + "_" + baseObjClass.ClassName;

                    // Create Method
                    CodeMemberMethod m = new CodeMemberMethod();
                    c.Members.Add(m);
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
        protected virtual void GenerateStreamMethods(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c)
        {
            // Create ToStream Method
            CodeMemberMethod m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "ToStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));
            m.Statements.Add(new CodeSnippetExpression("base.ToStream(sw)"));// TODO: Das ist C# spezifisch

            foreach (BaseProperty p in objClass.Properties)
            {
                if (p is ValueTypeProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.{0}, sw)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.fk_{0}, sw)", p.PropertyName)));
                }
                else if (p is BackReferenceProperty
                    && clientServer == ClientServerEnum.Server
                    && ((BackReferenceProperty)p).PreFetchToClient)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.ToBinary(this.{0}.OfType<IDataObject>(), sw)", p.PropertyName)));
                }
            }

            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "FromStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));
            m.Statements.Add(new CodeSnippetExpression("base.FromStream(ctx, sr)"));// TODO: Das ist C# spezifisch

            foreach (BaseProperty p in objClass.Properties)
            {
                if (p is Property && ((Property)p).IsList)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(this.{0}, sr)", p.PropertyName)));
                }
                else if (p is ValueTypeProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._{0}, sr)", p.PropertyName)));
                }
                else if (p is ObjectReferenceProperty)
                {
                    m.Statements.Add(new CodeSnippetExpression(string.Format("BinarySerializer.FromBinary(out this._fk_{0}, sr)", p.PropertyName)));
                }
                else if (p is BackReferenceProperty
                    && clientServer == ClientServerEnum.Client
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
