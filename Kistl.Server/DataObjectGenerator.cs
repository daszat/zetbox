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
            Directory.CreateDirectory(path);
            this.ctx = ctx;

            var objClassList = from c in ctx.GetTable<ObjectClass>()
                               select c;


            foreach (ObjectClass objClass in objClassList)
            {
                GenerateClass(objClass);
            }
        }

        private void GenerateClass(ObjectClass objClass)
        {
            CodeCompileUnit code = new CodeCompileUnit();
            
            // Create Namespace
            CodeNamespace ns = new CodeNamespace(objClass.Namespace);
            code.Namespaces.Add(ns);

            // Add using Statements
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

            // Create ServerBL
            GenerateServerBL(objClass, ns);

            // Create ClientBL
            GenerateClientBL(objClass, ns);

            // Generate the code & save
            string fileName = path + objClass.ClassName + ".Designer.cs";

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    code, sourceWriter, options);
            }

        }

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
            m.Parameters.Add(new CodeParameterDeclarationExpression("KistlDataContext", "ctx"));
            m.Statements.Add(new CodeSnippetExpression(@"if (OnPreSave != null) OnPreSave(ctx, this)"));// TODO: Das ist C# spezifisch

            // Create NotifyPostSave Method
            m = new CodeMemberMethod();
            c.Members.Add(m);
            m.Name = "NotifyPostSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression("KistlDataContext", "ctx"));
            m.Statements.Add(new CodeSnippetExpression(@"if (OnPostSave != null) OnPostSave(ctx, this)"));// TODO: Das ist C# spezifisch

        }

        private void GenerateProperties(ObjectClass objClass, CodeTypeDeclaration c)
        {
            foreach (ObjectProperty prop in objClass.Properties)
            {
                if (!prop.IsList)
                {
                    // Simple Property
                    CodeMemberField f = new CodeMemberField(prop.DataType, "_" + prop.PropertyName);
                    c.Members.Add(f);

                    CodeMemberProperty p = new CodeMemberProperty();
                    c.Members.Add(p);

                    p.Name = prop.PropertyName;
                    p.HasGet = true;
                    p.HasSet = true;
                    p.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    p.Type = new CodeTypeReference(Type.GetType(prop.DataType, true, true));
                    p.CustomAttributes.Add(new CodeAttributeDeclaration("Column",
                        new CodeAttributeArgument("UpdateCheck",
                            new CodeSnippetExpression("UpdateCheck.Never")), // TODO: Das ist C# spezifisch
                    new CodeAttributeArgument("Storage",
                        new CodePrimitiveExpression("_" + prop.PropertyName))));

                    p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_" + prop.PropertyName)));
                    p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_" + prop.PropertyName), new CodePropertySetValueReferenceExpression()));
                }
                else if (prop.IsList && prop.IsAssociation)
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
                    p.CustomAttributes.Add(new CodeAttributeDeclaration("ServerObject",
                        new CodeAttributeArgument("FullName",
                            new CodePrimitiveExpression(prop.DataType + "Server, Kistl.App.Projekte"))));
                    p.CustomAttributes.Add(new CodeAttributeDeclaration("ClientObject",
                        new CodeAttributeArgument("FullName",
                            new CodePrimitiveExpression(prop.DataType + "Client, Kistl.App.Projekte"))));
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

        private void GenerateServerBL(ObjectClass objClass, CodeNamespace ns)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(objClass.ClassName + "Server");
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.BaseTypes.Add(new CodeTypeReference("ServerObject", new CodeTypeReference(objClass.ClassName)));
        }

        private void GenerateClientBL(ObjectClass objClass, CodeNamespace ns)
        {
            CodeTypeDeclaration c = new CodeTypeDeclaration(objClass.ClassName + "Client");
            ns.Types.Add(c);
            c.IsClass = true;
            c.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
            c.BaseTypes.Add(new CodeTypeReference("ClientObject", new CodeTypeReference(objClass.ClassName)));

            // Create GetListOf Methods
            foreach (ObjectProperty prop in objClass.Properties)
            {
                if (prop.IsList)
                {
                    CodeMemberMethod m = new CodeMemberMethod();
                    c.Members.Add(m);
                    m.Comments.Add(new CodeCommentStatement("Autogeneriert, um die gebundenen Listen zu bekommen"));

                    m.Name = "GetArrayOf" + prop.PropertyName + "FromXML";
                    m.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    m.ReturnType = new CodeTypeReference(typeof(IEnumerable));
                    m.Parameters.Add(new CodeParameterDeclarationExpression(
                        new CodeTypeReference(typeof(string)), "xml"));
                    m.Statements.Add(new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(
                                new CodeVariableReferenceExpression("xml"), 
                                "FromXmlString", 
                                new CodeTypeReference[] 
                                    {
                                        new CodeTypeReference("List", new CodeTypeReference(prop.DataType))
                                    }), 
                                new CodeExpression[0])));
                }
            }
        }

    }
}
