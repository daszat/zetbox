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
        protected override void GenerateObjects(Current current)
        {
            base.GenerateObjects(current);

            if (current.clientServer == ClientServerEnum.Server)
            {
                GenerateEdmRelationshipAttribute(current.objClass, current.code);

                current.code_class.CustomAttributes.Add(new CodeAttributeDeclaration("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName",
                        new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name",
                        new CodePrimitiveExpression(current.objClass.ClassName))));
            }
        }
        #endregion

        #region GenerateEdmRelationshipAttribute
        private void GenerateEdmRelationshipAttribute(ObjectClass objClass, CodeCompileUnit code)
        {
            var props = objClass.Properties.OfType<ObjectReferenceProperty>();

            foreach (ObjectReferenceProperty prop in props)
            {
                ObjectType parentType = new ObjectType(prop.GetDataType());
                ObjectType childType = Generator.GetAssociationChildType(prop);

                code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Data.Objects.DataClasses.EdmRelationshipAttribute",
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("Model")),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationName(parentType, childType))),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationParentRoleName(parentType))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(parentType.NameDataObject)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(childType))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.Many")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(childType.NameDataObject))
                    ));
            }
        }
        #endregion

        #region GenerateServerDefaultProperties

        protected override void GenerateDefaultProperty_ID(Current current)
        {
            base.GenerateDefaultProperty_ID(current);

            if (current.clientServer == ClientServerEnum.Server)
            {
                current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("EdmScalarPropertyAttribute",
                    new CodeAttributeArgument("EntityKeyProperty",
                        new CodePrimitiveExpression(true)),
                    new CodeAttributeArgument("IsNullable",
                        new CodePrimitiveExpression(false))));
            }
        }

        #endregion

        #region GenerateValueTypeProperty
        protected override void GenerateProperties_ValueTypeProperty(Current current)
        {
            base.GenerateProperties_ValueTypeProperty(current);

            if (current.clientServer == ClientServerEnum.Server)
            {
                current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("EdmScalarPropertyAttribute"));
            }
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty_Collection
        protected override void GenerateProperties_ObjectReferenceProperty_Collection(Current current, Current collectionClass, Current serializer)
        {
            base.GenerateProperties_ObjectReferenceProperty_Collection(current, collectionClass, serializer);

            if (current.clientServer == ClientServerEnum.Server)
            {
                // Assembly Code Relation
                current.code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Data.Objects.DataClasses.EdmRelationshipAttribute",
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("Model")),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationName(current.code_namespace, current.code_class, collectionClass.code_namespace, collectionClass.code_class))),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationParentRoleName(current.property.ObjectClass))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(current.property.ObjectClass.Module.Namespace + "." + current.property.ObjectClass.ClassName)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(collectionClass.code_namespace, collectionClass.code_class))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.Many")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(collectionClass.code_namespace.Name + "." + collectionClass.code_class.Name))
                    ));

                // ParentClass
                current.code_property.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(collectionClass.code_class.Name));

                current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationName(current.code_namespace, current.code_class, collectionClass.code_namespace, collectionClass.code_class))),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(collectionClass.code_namespace, collectionClass.code_class)))));


                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""B_{2}"");
                if (!c.IsLoaded) c.Load(); 
                return c", collectionClass.code_class.Name, Generator.GetAssociationName(current.code_namespace, current.code_class, collectionClass.code_namespace, collectionClass.code_class), collectionClass.code_class.Name)));

                // Collection Class
                collectionClass.code_class.CustomAttributes.Add(new CodeAttributeDeclaration("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName",
                        new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name",
                        new CodePrimitiveExpression(collectionClass.code_class.Name))));

                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)current.property;

                collectionClass.code_property.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationName(objRefProp.ReferenceObjectClass, collectionClass.code_namespace, collectionClass.code_class))),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass)))));
                
                collectionClass.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", current.property.GetDataType(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, collectionClass.code_namespace, collectionClass.code_class), objRefProp.ReferenceObjectClass.ClassName)));

                collectionClass.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value", current.property.GetDataType(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, collectionClass.code_namespace, collectionClass.code_class), objRefProp.ReferenceObjectClass.ClassName)));

                // Serializer
                serializer.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Value == Helper.INVALIDID && Value != null)
                {{
                    _fk_Value = Value.ID;
                }}
                return _fk_Value", current.property.PropertyName)));
                serializer.code_property.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_fk_Value"), new CodePropertySetValueReferenceExpression()));
            }
            else
            {
                collectionClass.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Value)", current.property.GetDataType(), current.property.PropertyName)));

                collectionClass.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"_fk_Value = value.ID", current.property.PropertyName)));

                serializer.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return _fk_Value", current.property.PropertyName)));
                serializer.code_property.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_fk_Value"), new CodePropertySetValueReferenceExpression()));
            }
        }
        #endregion

        #region GenerateProperties_ValueTypeProperty_Collection
        protected override void GenerateProperties_ValueTypeProperty_Collection(Current current, Current collectionClass)
        {
            base.GenerateProperties_ValueTypeProperty_Collection(current, collectionClass);

            if (current.clientServer == ClientServerEnum.Server)
            {
                collectionClass.code_property.CustomAttributes.Add(new CodeAttributeDeclaration("EdmScalarPropertyAttribute"));

                current.code.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Data.Objects.DataClasses.EdmRelationshipAttribute",
                new CodeAttributeArgument(
                    new CodePrimitiveExpression("Model")),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationName(current.code_namespace, current.code_class, collectionClass.code_namespace, collectionClass.code_class))),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationParentRoleName(current.property.ObjectClass))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(current.property.ObjectClass.Module.Namespace + "." + current.property.ObjectClass.ClassName)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(collectionClass.code_namespace, collectionClass.code_class))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.Many")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(collectionClass.code_namespace.Name + "." + collectionClass.code_class.Name))
                    ));

                collectionClass.code_class.CustomAttributes.Add(new CodeAttributeDeclaration("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName",
                        new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name",
                        new CodePrimitiveExpression(collectionClass.code_class.Name))));

                current.code_property.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(collectionClass.code_class.Name));

                current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationName(current.code_namespace, current.code_class, collectionClass.code_namespace, collectionClass.code_class))),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(collectionClass.code_namespace, collectionClass.code_class)))));


                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""B_{2}"");
                if (!c.IsLoaded) c.Load(); 
                return c", collectionClass.code_class.Name, Generator.GetAssociationName(current.code_namespace, current.code_class, collectionClass.code_namespace, collectionClass.code_class), collectionClass.code_class.Name)));
            }
        }
        #endregion

        #region GenerateObjectReferenceProperty

        protected override void GenerateProperties_ObjectReferenceProperty(Current current, Current serializer)
        {
            base.GenerateProperties_ObjectReferenceProperty(current, serializer);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_{1})", current.property.GetDataType(), current.property.PropertyName)));

                current.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"_fk_{0} = value.ID", current.property.PropertyName)));

                serializer.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"return _fk_{0}", current.property.PropertyName)));
                serializer.code_property.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_fk_" + current.property.PropertyName), new CodePropertySetValueReferenceExpression()));
            }
            else
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)current.property;

                current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationName(objRefProp.ReferenceObjectClass, current.objClass))),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass)))));


                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", current.property.GetDataType(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, current.objClass), objRefProp.ReferenceObjectClass.ClassName)));

                current.code_property.SetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityReference<{0}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}>(""Model.{1}"", ""A_{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value", current.property.GetDataType(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, current.objClass), objRefProp.ReferenceObjectClass.ClassName)));

                serializer.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_{0} == Helper.INVALIDID && {0} != null)
                {{
                    _fk_{0} = {0}.ID;
                }}
                return _fk_{0}", current.property.PropertyName)));
                serializer.code_property.SetStatements.Add(new CodeAssignStatement(new CodeSnippetExpression("_fk_" + current.property.PropertyName), new CodePropertySetValueReferenceExpression()));
            }
        }

        #endregion

        #region GenerateBackReferenceProperty

        protected override void GenerateProperties_BackReferenceProperty(Current current)
        {
            base.GenerateProperties_BackReferenceProperty(current);

            BackReferenceProperty backRefProp = (BackReferenceProperty)current.property;
            ObjectType childType = Generator.GetAssociationChildType((BackReferenceProperty)current.property);

            if (current.clientServer == ClientServerEnum.Client)
            {
                current.code_property.Type = new CodeTypeReference("List", new CodeTypeReference(childType.NameDataObject));

                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(string.Format(
                        @"if(_{0} == null) _{0} = Context.GetListOf<{1}>(this, ""{0}"");
                return _{0}",
                    current.property.PropertyName, childType.NameDataObject)));

                CodeMemberField f = new CodeMemberField(current.code_property.Type,
                    "_" + current.property.PropertyName);

                current.code_class.Members.Add(f);
            }
            else
            {
                current.code_property.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(childType.NameDataObject));
                current.code_property.CustomAttributes.Add(new CodeAttributeDeclaration(
                    "EdmRelationshipNavigationPropertyAttribute",
                    new CodeAttributeArgument(new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationName(current.objClass, childType))),
                    new CodeAttributeArgument(new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(childType)))));


                current.code_property.GetStatements.Add(
                    new CodeSnippetExpression(
                        string.Format(@"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""{2}"");
                if (!c.IsLoaded) c.Load(); 
                return c", childType.NameDataObject, 
                         Generator.GetAssociationName(current.objClass, childType), 
                         Generator.GetAssociationChildRoleName(childType))));
            }
        }

        #endregion

    }
}
