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

namespace Kistl.Server
{
    public class DataObjectGenerator
    {
        private string path = "";
        private Kistl.API.Server.KistlDataContext ctx = null;

        public void Generate(Kistl.API.Server.KistlDataContext ctx, string path)
        {
            this.path = path + (path.EndsWith("\\") ? "" : "\\");
            this.ctx = ctx;

            var objClassList = from c in ctx.GetTable<ObjectClass>()
                               select c;


            foreach (ObjectClass objClass in objClassList)
            {
                GenerateObjects(objClass);
                GenerateObjectsClient(objClass);
                GenerateObjectsServer(objClass);
            }
        }

        #region Save / Helper
        private void SaveFile(CodeCompileUnit code, string fileName)
        {
            string path = Path.GetDirectoryName(fileName);
            Directory.CreateDirectory(path);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    code, sourceWriter, options);
            }
        }

        private void AddDefaultNamespaces(CodeNamespace ns)
        {
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            ns.Imports.Add(new CodeNamespaceImport("System.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("System.Text"));
            ns.Imports.Add(new CodeNamespaceImport("System.Data.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("System.Data.Linq.Mapping"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
            ns.Imports.Add(new CodeNamespaceImport("System.Xml"));
            ns.Imports.Add(new CodeNamespaceImport("System.Xml.Serialization"));
            ns.Imports.Add(new CodeNamespaceImport("Kistl.API"));
        }
        #endregion

        #region GenerateObjectsClient/Server
        private void GenerateObjectsClient(ObjectClass objClass)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            // Create Namespace
            CodeNamespace ns = new CodeNamespace(objClass.Namespace);
            code.Namespaces.Add(ns);

            AddDefaultNamespaces(ns);
            ns.Imports.Add(new CodeNamespaceImport("Kistl.API.Client"));

            GenerateClientAccessLayer(objClass, ns);

            // Generate the code & save
            SaveFile(code, path + @"Kistl.Objects.Client\" + objClass.ClassName + ".Client.Designer.cs");
        }

        private void GenerateObjectsServer(ObjectClass objClass)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            // Create Namespace
            CodeNamespace ns = new CodeNamespace(objClass.Namespace);
            code.Namespaces.Add(ns);

            AddDefaultNamespaces(ns);
            ns.Imports.Add(new CodeNamespaceImport("Kistl.API.Server"));

            GenerateServerAccessLayer(objClass, ns);

            // Generate the code & save
            SaveFile(code, path + @"Kistl.Objects.Server\" + objClass.ClassName + ".Server.Designer.cs");
        }

        private void GenerateServerAccessLayer(ObjectClass objClass, CodeNamespace ns)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(objClass.ClassName + "Server");
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.BaseTypes.Add(new CodeTypeReference("ServerObject", new CodeTypeReference(objClass.ClassName)));
        }

        private void GenerateClientAccessLayer(ObjectClass objClass, CodeNamespace ns)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(objClass.ClassName + "Client");
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.BaseTypes.Add(new CodeTypeReference("ClientObject", new CodeTypeReference(objClass.ClassName)));

            // Create GetListOf Methods
            foreach (ObjectProperty prop in objClass.Properties)
            {
                if (prop.IsList.Value)
                {
                    CodeMemberMethod m = new CodeMemberMethod();
                    c.Members.Add(m);
                    m.Comments.Add(new CodeCommentStatement("Autogeneriert, um die gebundenen Listen zu bekommen"));

                    m.Name = "GetListOf" + prop.PropertyName;
                    m.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    m.ReturnType = new CodeTypeReference("List", new CodeTypeReference(prop.DataType));
                    m.Parameters.Add(new CodeParameterDeclarationExpression(
                        new CodeTypeReference(typeof(int)), "ID"));
                    m.Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(
                                        new CodeVariableReferenceExpression("Proxy.Service"), // TODO: Das ist C# spezifisch
                                        "GetListOf"),
                                        new CodeExpression[] {
                                            new CodeSnippetExpression("Type"),
                                            new CodeSnippetExpression("ID"),
                                            new CodePrimitiveExpression(prop.PropertyName),
                                        }),
                                "FromXmlString",
                                new CodeTypeReference[] 
                                    {
                                        new CodeTypeReference("List", new CodeTypeReference(prop.DataType))
                                    }),
                                new CodeExpression[0])));
                }
            }
        }

        #endregion

        #region Generate Data Objects
        private void GenerateObjects(ObjectClass objClass)
        {
            CodeCompileUnit code = new CodeCompileUnit();
            
            // Create Namespace
            CodeNamespace ns = new CodeNamespace(objClass.Namespace);
            code.Namespaces.Add(ns);

            // Add using Statements
            AddDefaultNamespaces(ns);

            // Create Class
            CodeTypeDeclaration c = new CodeTypeDeclaration(objClass.ClassName);
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.BaseTypes.Add("BaseDataObject");
            c.CustomAttributes.Add(new CodeAttributeDeclaration("Table", 
                new CodeAttributeArgument("Name", 
                    new CodePrimitiveExpression(objClass.TableName))));

            // Create Default Properties
            GenerateDefaultProperties(objClass, c);

            // Create Properties
            GenerateProperties(objClass, c);

            // Create DataObject Default Methods
            GenerateDefaultMethods(objClass, c);

            // Generate the code & save
            SaveFile(code, path + @"Kistl.Objects\" + objClass.ClassName + ".Designer.cs");
        }

        #region GenerateDefaultProperties
        private void GenerateDefaultProperties(ObjectClass objClass, CodeTypeDeclaration c)
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
            p.CustomAttributes.Add(new CodeAttributeDeclaration("Column",
                new CodeAttributeArgument("IsDbGenerated",
                    new CodePrimitiveExpression(true)), 
                new CodeAttributeArgument("IsPrimaryKey",
                    new CodePrimitiveExpression(true)), 
                new CodeAttributeArgument("UpdateCheck",
                    new CodeSnippetExpression("UpdateCheck.Never")), // TODO: Das ist C# spezifisch
                new CodeAttributeArgument("Storage",
                    new CodePrimitiveExpression("_ID")) ));

            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_ID")));
            p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_ID"), new CodePropertySetValueReferenceExpression()));
        }
        #endregion

        #region GenerateDefaultMethods
        private void GenerateDefaultMethods(ObjectClass objClass, CodeTypeDeclaration c)
        {
            // Create ToString Delegate
            CodeMemberEvent e = new CodeMemberEvent();
            c.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ToStringHandler", new CodeTypeReference(objClass.ClassName));
            e.Name = "OnToString";

            // Create PreSave Delegate
            e = new CodeMemberEvent();
            c.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(objClass.ClassName));
            e.Name = "OnPreSave";

            // Create PostSave Delegate
            e = new CodeMemberEvent();
            c.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(objClass.ClassName));
            e.Name = "OnPostSave";

            // Create ToString Method
            CodeMemberMethod m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "ToString";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(string));
            m.Statements.Add(new CodeSnippetExpression(@"if (OnToString != null)
            {
                ToStringEventArgs e = new ToStringEventArgs();
                OnToString(this, e);
                return e.Result;
            }
            return base.ToString()"));// TODO: Das ist C# spezifisch

            // Create NotifyPreSave Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "NotifyPreSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(@"if (OnPreSave != null) OnPreSave(this)"));// TODO: Das ist C# spezifisch

            // Create NotifyPostSave Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "NotifyPostSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.Add(new CodeSnippetExpression(@"if (OnPostSave != null) OnPostSave(this)"));// TODO: Das ist C# spezifisch

        }
        #endregion

        #region GenerateProperties
        private void GenerateProperties(ObjectClass objClass, CodeTypeDeclaration c)
        {
            foreach (ObjectProperty prop in objClass.Properties)
            {
                if (!prop.IsList.Value)
                {
                    // Simple Property
                    CodeMemberField f = null;
                    if (prop.IsAssociation.Value)
                    {
                        f = new CodeMemberField(typeof(int?), "_" + prop.PropertyName);
                    }
                    else
                    {
                        CodeTypeReference ctr = null;
                        if (Type.GetType(prop.DataType).IsValueType)
                        {
                            ctr = new CodeTypeReference("System.Nullable", new CodeTypeReference(prop.DataType));
                        }
                        else
                        {
                            ctr = new CodeTypeReference(prop.DataType);
                        }
                        f = new CodeMemberField(ctr, 
                            "_" + prop.PropertyName);
                    }
                    c.Members.Add(f);

                    CodeMemberProperty p = new CodeMemberProperty();
                    c.Members.Add(p);

                    p.Name = prop.PropertyName;
                    p.HasGet = true;
                    p.HasSet = true;
                    p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    if (prop.IsAssociation.Value)
                    {
                        p.Type = new CodeTypeReference(typeof(int?));
                    }
                    else
                    {
                        p.Type = f.Type;
                    }
                    p.CustomAttributes.Add(new CodeAttributeDeclaration("Column",
                        new CodeAttributeArgument("UpdateCheck",
                            new CodeSnippetExpression("UpdateCheck.Never")), // TODO: Das ist C# spezifisch
                    new CodeAttributeArgument("Storage",
                        new CodePrimitiveExpression("_" + prop.PropertyName))));

                    p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_" + prop.PropertyName)));
                    p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_" + prop.PropertyName), new CodePropertySetValueReferenceExpression()));

                    if (prop.IsAssociation.Value)
                    {
                        // "Rückwäretspointer"
                        string associationPropName = prop.PropertyName.Replace("fk_", "");
                        f = new CodeMemberField(
                            new CodeTypeReference("EntityRef", new CodeTypeReference(prop.DataType)), "_" + associationPropName);
                        f.InitExpression = new CodeObjectCreateExpression(new CodeTypeReference("EntityRef", new CodeTypeReference(prop.DataType)));
                        c.Members.Add(f);

                        p = new CodeMemberProperty();
                        c.Members.Add(p);

                        p.Name = associationPropName;
                        p.HasGet = true;
                        p.HasSet = true;
                        p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                        p.Type = new CodeTypeReference(prop.DataType);

                        p.CustomAttributes.Add(new CodeAttributeDeclaration("Association",
                            new CodeAttributeArgument("Storage",
                                new CodePrimitiveExpression("_" + associationPropName)),
                            new CodeAttributeArgument("ThisKey",
                                new CodePrimitiveExpression("fk_" + associationPropName))));
                        p.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));


                        p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_" + associationPropName + ".Entity")));
                        p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_" + associationPropName + ".Entity"), new CodePropertySetValueReferenceExpression()));
                    }
                }
                else if (prop.IsList.Value && prop.IsAssociation.Value)
                {
                    // Association
                    // TODO: Das ist eigentlich falsch herum, das sollte generiert werden,
                    // wenn bei der referenzierenden Klasse ein FK eingetragen wurde.
                    // Das sind quasi "AutoProperties"
                    CodeMemberField f = new CodeMemberField(
                        new CodeTypeReference("EntitySet", new CodeTypeReference(prop.DataType)), "_" + prop.PropertyName);
                    f.InitExpression = new CodeObjectCreateExpression(new CodeTypeReference("EntitySet", new CodeTypeReference(prop.DataType)));
                    c.Members.Add(f);

                    CodeMemberProperty p = new CodeMemberProperty();
                    c.Members.Add(p);

                    p.Name = prop.PropertyName;
                    p.HasGet = true;
                    p.HasSet = true;
                    p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    p.Type = new CodeTypeReference("EntitySet", new CodeTypeReference(prop.DataType));

                    p.CustomAttributes.Add(new CodeAttributeDeclaration("Association",
                        new CodeAttributeArgument("Storage",
                            new CodePrimitiveExpression("_" + prop.PropertyName)),
                        new CodeAttributeArgument("OtherKey",
                            new CodePrimitiveExpression("fk_" + objClass.ClassName))));
                    p.CustomAttributes.Add(new CodeAttributeDeclaration("XmlIgnore"));


                    p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_" + prop.PropertyName)));
                    p.SetStatements.Add(new CodeMethodInvokeExpression(new CodeSnippetExpression("_" + prop.PropertyName), "Assign", new CodePropertySetValueReferenceExpression()));

                }
                else
                {
                    // not supported yet
                    // just ignore it for now
                }
            }
        }
        #endregion
        
        #endregion
    }
}
