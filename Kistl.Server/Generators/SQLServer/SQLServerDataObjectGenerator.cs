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

namespace Kistl.Server.Generators.SQLServer
{
    public class SQLServerDataObjectGenerator : BaseDataObjectGenerator
    {
        #region AddDefaultNamespaces
        protected override void AddDefaultNamespaces(CodeNamespace ns)
        {
            base.AddDefaultNamespaces(ns);
            ns.Imports.Add(new CodeNamespaceImport("System.Data.Objects"));
            ns.Imports.Add(new CodeNamespaceImport("System.Data.Objects.DataClasses"));
        }
        #endregion

        #region GenerateAssemblyInfo
        protected override void GenerateAssemblyInfo(CodeCompileUnit code, ClientServerEnum clientServer)
        {
            base.GenerateAssemblyInfo(code, clientServer);

            if (clientServer == ClientServerEnum.Server)
            {
                code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(System.Data.Objects.DataClasses.EdmSchemaAttribute))));
            }
        }
        #endregion

        #region GenerateObjects
        protected override void GenerateObjects(ClientServerEnum clientServer, ObjectClass objClass, CodeCompileUnit code, CodeNamespace n, CodeTypeDeclaration c)
        {
            base.GenerateObjects(clientServer, objClass, code, n, c);

            if (clientServer == ClientServerEnum.Server)
            {
                GenerateEdmRelationshipAttribute(objClass, code);

                c.CustomAttributes.Add(new CodeAttributeDeclaration("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName",
                        new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name",
                        new CodePrimitiveExpression(objClass.ClassName))));
            }
        }
        #endregion

        #region GenerateEdmRelationshipAttribute
        private void GenerateEdmRelationshipAttribute(ObjectClass objClass, CodeCompileUnit code)
        {
            var props = objClass.Properties.OfType<ObjectReferenceProperty>();

            foreach (ObjectReferenceProperty prop in props)
            {
                ObjectType otherType = new ObjectType(prop.GetDataType());
                string assocName = "FK_" + objClass.ClassName + "_" + otherType.Classname;

                code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Data.Objects.DataClasses.EdmRelationshipAttribute",
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("Model")),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(assocName)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("A_" + otherType.Classname)),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(prop.GetDataType())),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("B_" + prop.ObjectClass.ClassName)),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.Many")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.ClassName))
                    ));
            }
        }
        #endregion

        #region GenerateServerDefaultProperties

        protected override void GenerateDefaultProperty_ID(ClientServerEnum clientServer, ObjectClass objClass, CodeTypeDeclaration c, CodeMemberField f, CodeMemberProperty p)
        {
            base.GenerateDefaultProperty_ID(clientServer, objClass, c, f, p);

            if (clientServer == ClientServerEnum.Server)
            {
                p.CustomAttributes.Add(new CodeAttributeDeclaration("EdmScalarPropertyAttribute",
                    new CodeAttributeArgument("EntityKeyProperty",
                        new CodePrimitiveExpression(true)),
                    new CodeAttributeArgument("IsNullable",
                        new CodePrimitiveExpression(false))));
            }
        }

        #endregion

        #region GenerateValueTypeProperty
        protected override void GenerateProperties_ValueTypeProperty(ClientServerEnum clientServer, ObjectClass objClass, ValueTypeProperty prop, CodeTypeDeclaration c, CodeMemberField f, CodeMemberProperty p)
        {
            base.GenerateProperties_ValueTypeProperty(clientServer, objClass, prop, c, f, p);

            if (clientServer == ClientServerEnum.Server)
            {
                p.CustomAttributes.Add(new CodeAttributeDeclaration("EdmScalarPropertyAttribute"));
            }
        }
        #endregion

        protected override void GenerateProperties_ValueTypeProperty_Collection(ClientServerEnum clientServer, CodeCompileUnit code, ObjectClass objClass, ValueTypeProperty prop, CodeTypeDeclaration c, CodeTypeDeclaration collectionClass, CodeMemberField f, CodeMemberProperty p, CodeMemberProperty navigationProperty, ObjectType otherType, string assocName)
        {
            base.GenerateProperties_ValueTypeProperty_Collection(clientServer, code, objClass, prop, c, collectionClass, f, p, navigationProperty, otherType, assocName);

            if (clientServer == ClientServerEnum.Server)
            {
                p.CustomAttributes.Add(new CodeAttributeDeclaration("EdmScalarPropertyAttribute"));

                code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Data.Objects.DataClasses.EdmRelationshipAttribute",
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("Model")),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(assocName)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("A_" + prop.ObjectClass.ClassName)),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.ClassName)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("B_" + otherType.Classname)),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.Many")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(otherType.Namespace + "." + otherType.Classname))
                    ));

                collectionClass.CustomAttributes.Add(new CodeAttributeDeclaration("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName",
                        new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name",
                        new CodePrimitiveExpression(collectionClass.Name))));

                navigationProperty.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(collectionClass.Name));

                navigationProperty.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(assocName)),
                    new CodeAttributeArgument(new CodePrimitiveExpression("B_" + otherType.Classname))));


                navigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""B_{2}"");
                if (!c.IsLoaded) c.Load(); 
                return c", collectionClass.Name, assocName, otherType.Classname)));

                // BackNavigation Property
                /*backNavigationProperty.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(assocName)),
                    new CodeAttributeArgument(new CodePrimitiveExpression("A_" + c.Name))));


                backNavigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", c.Name, assocName, c.Name)));

                backNavigationProperty.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value", c.Name, assocName, c.Name)));*/
            }
        }

        #region GenerateObjectReferenceProperty

        protected override void GenerateProperties_ObjectReferenceProperty(ClientServerEnum clientServer, ObjectClass objClass, ObjectReferenceProperty prop,
            CodeTypeDeclaration c, CodeMemberProperty navigationProperty, CodeMemberField serializerField,
            CodeMemberProperty serializerProperty, ObjectType otherType, string assocName)
        {
            base.GenerateProperties_ObjectReferenceProperty(clientServer, objClass, prop, c, 
                navigationProperty, serializerField, serializerProperty, otherType, assocName);

            if (clientServer == ClientServerEnum.Client)
            {
                navigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_{1})", prop.GetDataType(), prop.PropertyName)));

                navigationProperty.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"_fk_{0} = value.ID", prop.PropertyName)));

                serializerProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return _fk_{0}", prop.PropertyName)));
                serializerProperty.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_fk_" + prop.PropertyName), new CodePropertySetValueReferenceExpression()));
            }
            else
            {
                navigationProperty.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(assocName)),
                    new CodeAttributeArgument(new CodePrimitiveExpression("A_" + otherType.Classname))));


                navigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", prop.GetDataType(), assocName, otherType.Classname)));

                navigationProperty.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value", prop.GetDataType(), assocName, otherType.Classname)));

                serializerProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_{0} == Helper.INVALIDID && {0} != null)
                {{
                    _fk_{0} = {0}.ID;
                }}
                return _fk_{0}", prop.PropertyName)));
                serializerProperty.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_fk_" + prop.PropertyName), new CodePropertySetValueReferenceExpression()));
            }
        }

        #endregion

        #region GenerateBackReferenceProperty

        protected override void GenerateProperties_BackReferenceProperty(ClientServerEnum clientServer, ObjectClass objClass, BackReferenceProperty prop, CodeTypeDeclaration c, CodeMemberProperty navigationProperty, ObjectType otherType, string assocName)
        {
            base.GenerateProperties_BackReferenceProperty(clientServer, objClass, prop, c, navigationProperty, otherType, assocName);

            if (clientServer == ClientServerEnum.Client)
            {
                navigationProperty.Type = new CodeTypeReference("List", new CodeTypeReference(prop.GetDataType()));

                navigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(
                        @"if(_{0} == null) _{0} = Context.GetListOf<{1}>(this, ""{0}"");
                return _{0}",
                    prop.PropertyName, prop.GetDataType())));

                CodeMemberField f = new CodeMemberField(navigationProperty.Type,
                    "_" + prop.PropertyName);

                c.Members.Add(f);
            }
            else
            {
                navigationProperty.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(prop.GetDataType()));

                navigationProperty.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(assocName)),
                    new CodeAttributeArgument(new CodePrimitiveExpression("B_" + otherType.Classname))));


                navigationProperty.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""B_{2}"");
                if (!c.IsLoaded) c.Load(); 
                return c", prop.GetDataType(), assocName, otherType.Classname)));
            }
        }

        #endregion

    }
}
