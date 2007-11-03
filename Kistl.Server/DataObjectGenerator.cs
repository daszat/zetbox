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

namespace Kistl.Server
{
    public class DataObjectGenerator
    {
        private string path = "";
        private DataContext ctx = null;

        public void Generate(DataContext ctx, string path)
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
            ns.Imports.Add(new CodeNamespaceImport("System.Data.Linq.Mapping"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
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
                    new CodeSnippetExpression("UpdateCheck.Never")), // TODO: Das ist c# Spezifisch
                new CodeAttributeArgument("Storage",
                    new CodePrimitiveExpression("_ID")) ));

            p.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression("_ID")));
            p.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_ID"), new CodePropertySetValueReferenceExpression()));
        }

        private void GenerateProperties(ObjectClass objClass, CodeTypeDeclaration c)
        {
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
        }

    }
}
