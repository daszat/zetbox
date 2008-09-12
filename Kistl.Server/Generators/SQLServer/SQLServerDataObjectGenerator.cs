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
        protected override void GenerateAssemblyInfo(CodeCompileUnit code, TaskEnum clientServer)
        {
            base.GenerateAssemblyInfo(code, clientServer);

            if (clientServer == TaskEnum.Server)
            {
                code.AddAttribute(typeof(System.Data.Objects.DataClasses.EdmSchemaAttribute));
            }
        }
        #endregion

        #region GenerateObjects
        protected override void GenerateObjects(CurrentObjectClass current)
        {
            base.GenerateObjects(current);

            if (current.task == TaskEnum.Server)
            {
                GenerateEdmRelationshipAttribute(current.objClass, current.code);

                current.code_class.AddAttribute("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName", new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name", new CodePrimitiveExpression(current.objClass.ClassName)));
            }
        }

        protected override void GenerateStructs(CurrentStruct current)
        {
            base.GenerateStructs(current);

            if (current.task == TaskEnum.Server)
            {
                current.code_class.AddAttribute("System.Data.Objects.DataClasses.EdmComplexTypeAttribute",
                    new CodeAttributeArgument("NamespaceName", new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name", new CodePrimitiveExpression(current.@struct.ClassName)));

                CurrentStruct nullStruct = (CurrentStruct)current.Clone();
                nullStruct.code_class = current.code_namespace.CreateClass(current.@struct.ClassName + Kistl.API.Helper.ImplementationSuffix + "__NULL", current.@struct.ClassName + Kistl.API.Helper.ImplementationSuffix);
            }
        }
        #endregion

        #region GenerateEdmRelationshipAttribute
        private void GenerateEdmRelationshipAttribute(TypeMoniker parentType, TypeMoniker childType, CodeCompileUnit code, string propertyName)
        {
            code.AddAttribute(typeof(System.Data.Objects.DataClasses.EdmRelationshipAttribute),
                "Model",
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationName(parentType, childType, propertyName))),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationParentRoleName(parentType))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(parentType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                new CodeAttributeArgument(
                    new CodePrimitiveExpression(Generator.GetAssociationChildRoleName(childType))),
                new CodeAttributeArgument(
                    new CodeSnippetExpression("System.Data.Metadata.Edm.RelationshipMultiplicity.Many")),
                new CodeAttributeArgument(
                    new CodeTypeOfExpression(childType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)));
        }

        private void GenerateEdmRelationshipAttribute(ObjectClass objClass, CodeCompileUnit code)
        {
            var props = objClass.Properties.OfType<ObjectReferenceProperty>();

            foreach (ObjectReferenceProperty prop in props)
            {
                TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
                TypeMoniker childType = Generator.GetAssociationChildType(prop);

                GenerateEdmRelationshipAttribute(parentType, childType, code, prop.PropertyName);
            }
        }
        #endregion

        #region GenerateServerDefaultProperties

        protected override void GenerateDefaultProperty_ID(CurrentObjectClass current)
        {
            base.GenerateDefaultProperty_ID(current);

            if (current.task == TaskEnum.Server)
            {
                current.code_property.AddAttribute("EdmScalarPropertyAttribute",
                    new CodeAttributeArgument("EntityKeyProperty", new CodePrimitiveExpression(true)),
                    new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(false)));
            }
        }

        #endregion

        #region GenerateValueTypeProperty
        protected override void GenerateProperties_ValueTypeProperty(CurrentBase current)
        {
            base.GenerateProperties_ValueTypeProperty(current);

            if (current.task == TaskEnum.Server)
            {
                if (current.property.IsEnumerationPropertySingle())
                {
                    CurrentObjectClass implProperty = (CurrentObjectClass)current.Clone();
                    implProperty.code_property = current.code_class.CreateProperty("System.Int32", current.property.PropertyName + Kistl.API.Helper.ImplementationSuffix);

                    implProperty.code_property.AddAttribute("EdmScalarPropertyAttribute");
                    implProperty.code_property.GetStatements.AddStatement("return (int){0}", current.property.PropertyName);
                    implProperty.code_property.SetStatements.AddStatement("{0} = ({1})value", current.property.PropertyName, current.property.GetPropertyTypeString());
                }
                else
                {
                    current.code_property.AddAttribute("EdmScalarPropertyAttribute");
                }
            }
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty_Collection
        protected override void GenerateProperties_ObjectReferenceProperty_Collection(CurrentObjectClass current, CurrentObjectClass collectionClass, CurrentObjectClass serializerValue, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
            base.GenerateProperties_ObjectReferenceProperty_Collection(current, collectionClass, serializerValue, parent, serializerParent);

            if (current.task == TaskEnum.Server)
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)current.property;

                // Class
                TypeMoniker valueType = objRefProp.ReferenceObjectClass.GetTypeMoniker();
                TypeMoniker collectionType = Generator.GetAssociationChildType((Property)current.property);
                TypeMoniker parentType = current.objClass.GetTypeMoniker();

                // Assembly Code Relation
                GenerateEdmRelationshipAttribute(parentType, collectionType, current.code, "fk_Parent");


                CurrentObjectClass currentEFProperty = (CurrentObjectClass)current.Clone();

                current.code_field = current.code_class.CreateField(
                    new CodeTypeReference("EntityCollectionEntryValueWrapper",
                        new CodeTypeReference(parentType.NameDataObject),
                        new CodeTypeReference(valueType.NameDataObject),
                        new CodeTypeReference(collectionType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                    current.property.PropertyName + "Wrapper");
                current.code_property.GetStatements.AddStatement(@"if ({0}Wrapper == null) {0}Wrapper = new EntityCollectionEntryValueWrapper<{1}, {2}, {3}{4}>(this, {0}{4});
                return {0}Wrapper",
                              current.property.PropertyName,
                              parentType.NameDataObject,
                              valueType.NameDataObject,
                              collectionType.NameDataObject, 
                              Kistl.API.Helper.ImplementationSuffix);

                currentEFProperty.code_property = currentEFProperty.code_class.CreateProperty(
                    new CodeTypeReference("EntityCollection", new CodeTypeReference(collectionType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                    current.code_property.Name + Kistl.API.Helper.ImplementationSuffix, false);

                currentEFProperty.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"),
                    Generator.GetAssociationChildRoleName(Generator.GetPropertyCollectionObjectType((Property)current.property)));


                currentEFProperty.code_property.GetStatements.AddStatement(
              @"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c", collectionClass.code_class.Name, Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"), Generator.GetAssociationChildRoleName(collectionType));

                // Collection Class
                collectionClass.code_property.GetStatements.AddStatement("return ValueImpl");
                collectionClass.code_property.SetStatements.AddStatement("ValueImpl = ({0}{1})value", current.property.GetPropertyTypeString(), Kistl.API.Helper.ImplementationSuffix);

                CurrentObjectClass implCollectionClass = (CurrentObjectClass)collectionClass.Clone();
                implCollectionClass.code_property = collectionClass.code_class.CreateProperty(current.property.GetPropertyTypeString() + Kistl.API.Helper.ImplementationSuffix, "ValueImpl");

                implCollectionClass.code_class.AddAttribute("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName", new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name", new CodePrimitiveExpression(collectionType.Classname)));

                implCollectionClass.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(objRefProp.ReferenceObjectClass, (Property)current.property),
                    Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass));

                implCollectionClass.code_property.GetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", current.property.GetPropertyTypeString(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, (Property)current.property), Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass), Kistl.API.Helper.ImplementationSuffix);

                implCollectionClass.code_property.SetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = ({0}{3})value", current.property.GetPropertyTypeString(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, (Property)current.property), Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass), Kistl.API.Helper.ImplementationSuffix);

                // Collection.Serializer.Value
                serializerValue.code_property.GetStatements.AddStatement(
              @"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Value != null)
                {{
                    _fk_Value = Value.ID;
                }}
                return _fk_Value", current.property.PropertyName);
                serializerValue.code_property.SetStatements.AddStatement("_fk_Value = value");

                // Collection.Serializer.Parent
                serializerParent.code_property.GetStatements.AddStatement(
              @"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Parent != null)
                {{
                    _fk_Parent = Parent.ID;
                }}
                return _fk_Parent", current.property.PropertyName);
                serializerParent.code_property.SetStatements.AddStatement("_fk_Parent = value");

                // Collection.Parent
                parent.code_property.GetStatements.AddStatement("return ParentImpl");
                parent.code_property.SetStatements.AddStatement("ParentImpl = ({0}{1})value", current.objClass.GetTypeMoniker().NameDataObject, Kistl.API.Helper.ImplementationSuffix);

                CurrentObjectClass implParent = (CurrentObjectClass)parent.Clone();
                implParent.code_property = parent.code_class.CreateProperty(current.objClass.GetTypeMoniker().NameDataObject + Kistl.API.Helper.ImplementationSuffix, "ParentImpl");

                implParent.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"),
                    Generator.GetAssociationParentRoleName(current.objClass));

                implParent.code_property.GetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", current.objClass.ClassName, Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"), Generator.GetAssociationParentRoleName(current.objClass), Kistl.API.Helper.ImplementationSuffix);

                implParent.code_property.SetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = ({0}{3})value", current.objClass.ClassName, Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"), Generator.GetAssociationParentRoleName(current.objClass), Kistl.API.Helper.ImplementationSuffix);
            }
        }
        #endregion

        #region GenerateProperties_ValueTypeProperty_Collection
        protected override void GenerateProperties_ValueTypeProperty_Collection(CurrentObjectClass current, CurrentObjectClass collectionClass, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
            base.GenerateProperties_ValueTypeProperty_Collection(current, collectionClass, parent, serializerParent);

            if (current.task == TaskEnum.Server)
            {
                string valueType = current.property.GetPropertyTypeString();
                TypeMoniker collectionType = Generator.GetAssociationChildType((Property)current.property);
                TypeMoniker parentType = current.objClass.GetTypeMoniker();

                collectionClass.code_property.AddAttribute("EdmScalarPropertyAttribute");

                GenerateEdmRelationshipAttribute(parentType, collectionType, current.code, "fk_Parent");

                collectionClass.code_class.AddAttribute("EdmEntityTypeAttribute",
                    new CodeAttributeArgument("NamespaceName", new CodePrimitiveExpression("Model")),
                    new CodeAttributeArgument("Name", new CodePrimitiveExpression(collectionType.Classname)));

                // current.code_property.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(collectionClass.code_class.Name));

                CurrentObjectClass currentEFProperty = (CurrentObjectClass)current.Clone();

                current.code_field = current.code_class.CreateField(
                    new CodeTypeReference("EntityCollectionEntryValueWrapper",
                        new CodeTypeReference(parentType.NameDataObject),
                        new CodeTypeReference(valueType),
                        new CodeTypeReference(collectionType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                    current.property.PropertyName + "Wrapper");
                current.code_property.GetStatements.AddStatement(@"if ({0}Wrapper == null) {0}Wrapper = new EntityCollectionEntryValueWrapper<{1}, {2}, {3}{4}>(this, {0}{4});
                return {0}Wrapper",
                              current.property.PropertyName,
                              parentType.NameDataObject,
                              valueType,
                              collectionType.NameDataObject, 
                              Kistl.API.Helper.ImplementationSuffix);

                currentEFProperty.code_property = currentEFProperty.code_class.CreateProperty(
                    new CodeTypeReference("EntityCollection", new CodeTypeReference(collectionType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                    current.code_property.Name + Kistl.API.Helper.ImplementationSuffix, false);

                currentEFProperty.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"),
                    Generator.GetAssociationChildRoleName(collectionType));


                currentEFProperty.code_property.GetStatements.AddStatement(
              @"EntityCollection<{0}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c", collectionClass.code_class.Name, Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"), Generator.GetAssociationChildRoleName(collectionType));


                // Collection.Serializer.Parent
                serializerParent.code_property.GetStatements.AddStatement(
              @"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Parent != null)
                {{
                    _fk_Parent = Parent.ID;
                }}
                return _fk_Parent", current.property.PropertyName);
                serializerParent.code_property.SetStatements.AddStatement("_fk_Parent = value");

                // Collection.Parent
                parent.code_property.GetStatements.AddStatement("return ParentImpl");
                parent.code_property.SetStatements.AddStatement("ParentImpl = ({0}{1})value", current.objClass.GetTypeMoniker().NameDataObject, Kistl.API.Helper.ImplementationSuffix);

                CurrentObjectClass implParent = (CurrentObjectClass)parent.Clone();
                implParent.code_property = parent.code_class.CreateProperty(current.objClass.GetTypeMoniker().NameDataObject + Kistl.API.Helper.ImplementationSuffix, "ParentImpl");

                implParent.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"),
                    Generator.GetAssociationParentRoleName(current.objClass));

                implParent.code_property.GetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", current.objClass.ClassName, Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"), Generator.GetAssociationParentRoleName(current.objClass), Kistl.API.Helper.ImplementationSuffix);

                implParent.code_property.SetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = ({0}{3})value", current.objClass.ClassName, Generator.GetAssociationName(current.objClass, (Property)current.property, "fk_Parent"), Generator.GetAssociationParentRoleName(current.objClass), Kistl.API.Helper.ImplementationSuffix);
            }
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty

        protected override void GenerateProperties_ObjectReferenceProperty(CurrentObjectClass current, CurrentObjectClass serializer)
        {
            base.GenerateProperties_ObjectReferenceProperty(current, serializer);

            if (current.task == TaskEnum.Server)
            {
                ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)current.property;

                current.code_property.GetStatements.AddStatement("return {0}" + Kistl.API.Helper.ImplementationSuffix, current.property.PropertyName);
                current.code_property.SetStatements.AddStatement("{0}{2} = ({1}{2})value", current.property.PropertyName, current.property.GetPropertyTypeString(), Kistl.API.Helper.ImplementationSuffix);

                CurrentObjectClass implProperty = (CurrentObjectClass)current.Clone();
                implProperty.code_property = current.code_class.CreateProperty(current.property.GetPropertyTypeString() + Kistl.API.Helper.ImplementationSuffix, current.property.PropertyName + Kistl.API.Helper.ImplementationSuffix);

                implProperty.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(objRefProp.ReferenceObjectClass, current.objClass, current.property.PropertyName),
                    Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass));

                implProperty.code_property.GetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value", current.property.GetPropertyTypeString(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, current.objClass, current.property.PropertyName), Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass), Kistl.API.Helper.ImplementationSuffix);

                implProperty.code_property.SetStatements.AddStatement(
              @"EntityReference<{0}{3}> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = ({0}{3})value", current.property.GetPropertyTypeString(), Generator.GetAssociationName(objRefProp.ReferenceObjectClass, current.objClass, current.property.PropertyName), Generator.GetAssociationParentRoleName(objRefProp.ReferenceObjectClass), Kistl.API.Helper.ImplementationSuffix);

                // Serializer
                serializer.code_property.GetStatements.AddStatement(
              @"if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && {0} != null)
                {{
                    _fk_{0} = {0}.ID;
                }}
                return _fk_{0}", current.property.PropertyName);
                serializer.code_property.SetStatements.AddStatement("_fk_{0} = value", current.property.PropertyName);
            }
        }

        #endregion

        #region GenerateProperties_BackReferenceProperty

        protected override void GenerateProperties_BackReferenceProperty(CurrentObjectClass current)
        {
            base.GenerateProperties_BackReferenceProperty(current);

            BackReferenceProperty backRefProp = (BackReferenceProperty)current.property;

            if (current.task == TaskEnum.Server)
            {
                TypeMoniker parentType = current.objClass.GetTypeMoniker();
                TypeMoniker childType = Generator.GetAssociationChildType((BackReferenceProperty)current.property);
                TypeMoniker ownerType = ((BackReferenceProperty)current.property).ReferenceProperty.ObjectClass.GetTypeMoniker();

                CurrentObjectClass currentEFProperty = (CurrentObjectClass)current.Clone();

                if (((BackReferenceProperty)current.property).ReferenceProperty.IsList)
                {
                    current.code_field = current.code_class.CreateField(
                        new CodeTypeReference("EntityCollectionEntryParentWrapper",
                            new CodeTypeReference(ownerType.NameDataObject),
                            new CodeTypeReference(parentType.NameDataObject),
                            new CodeTypeReference(childType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                        current.property.PropertyName + "Wrapper");
                    current.code_property.GetStatements.AddStatement(@"if ({0}Wrapper == null) {0}Wrapper = new EntityCollectionEntryParentWrapper<{1}, {2}, {3}{4}>(this, {0}{4});
                return {0}Wrapper", 
                                  current.property.PropertyName, 
                                  ownerType.NameDataObject,
                                  parentType.NameDataObject,
                                  childType.NameDataObject,
                                  Kistl.API.Helper.ImplementationSuffix);

                    currentEFProperty.code_property = currentEFProperty.code_class.CreateProperty(
                        new CodeTypeReference("EntityCollection", new CodeTypeReference(childType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                        current.code_property.Name + Kistl.API.Helper.ImplementationSuffix, false);

                    currentEFProperty.code_property.GetStatements.AddStatement(
                  @"EntityCollection<{0}{3}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c", childType.NameDataObject,
                             Generator.GetAssociationName(parentType, childType, backRefProp.ReferenceProperty.PropertyName),
                             Generator.GetAssociationChildRoleName(childType), Kistl.API.Helper.ImplementationSuffix);
                }
                else
                {
                    current.code_field = current.code_class.CreateField(
                        new CodeTypeReference("EntityCollectionWrapper",
                            new CodeTypeReference(childType.NameDataObject),
                            new CodeTypeReference(childType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                        current.property.PropertyName + "Wrapper");
                    current.code_property.GetStatements.AddStatement(@"if ({0}Wrapper == null) {0}Wrapper = new EntityCollectionWrapper<{1}, {1}{2}>({0}{2});
                return {0}Wrapper", current.property.PropertyName, childType.NameDataObject, Kistl.API.Helper.ImplementationSuffix);

                    currentEFProperty.code_property = currentEFProperty.code_class.CreateProperty(
                        new CodeTypeReference("EntityCollection", new CodeTypeReference(childType.NameDataObject + Kistl.API.Helper.ImplementationSuffix)),
                        current.code_property.Name + Kistl.API.Helper.ImplementationSuffix, false);

                    currentEFProperty.code_property.GetStatements.AddStatement(
                  @"EntityCollection<{0}{3}> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<{0}{3}>(""Model.{1}"", ""{2}"");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c", childType.NameDataObject,
                             Generator.GetAssociationName(parentType, childType, backRefProp.ReferenceProperty.PropertyName),
                             Generator.GetAssociationChildRoleName(childType), Kistl.API.Helper.ImplementationSuffix);
                }

                // currentEFProperty.code_property.Type = new CodeTypeReference("EntityCollection", new CodeTypeReference(childType.NameDataObject));
                currentEFProperty.code_property.AddAttribute("EdmRelationshipNavigationPropertyAttribute",
                    "Model",
                    Generator.GetAssociationName(parentType, childType, backRefProp.ReferenceProperty.PropertyName),
                    Generator.GetAssociationChildRoleName(childType));
            }
        }

        #endregion

        #region GenerateProperties_StructProperty
        protected override void GenerateProperties_StructProperty(CurrentObjectClass current)
        {
            base.GenerateProperties_StructProperty(current);
            if (current.task == TaskEnum.Server)
            {
                //current.code_property.GetStatements.Insert(0, new CodeSnippetStatement(
                //    string.Format(@"                if (_{0} == null) {{ _{0} = new {1}{2}(); _{0}.AttachToObject(this, ""{0}""); }}",
                //        current.property.PropertyName, current.property.GetPropertyTypeString(), Kistl.API.Helper.ImplementationSuffix)));

                CurrentObjectClass ormProperty = (CurrentObjectClass)current.Clone();
                ormProperty.code_property = current.code_class.CreateProperty(current.property.GetPropertyTypeString() + Kistl.API.Helper.ImplementationSuffix, current.property.PropertyName + Kistl.API.Helper.ImplementationSuffix);
                ormProperty.code_property.AddAttribute("EdmComplexPropertyAttribute");
                
                ormProperty.code_property.GetStatements.AddStatement(@"if (_{0} == null) return new {1}{2}__NULL();
                return ({1}{2}){0}", current.property.PropertyName, current.property.GetPropertyTypeString(), Kistl.API.Helper.ImplementationSuffix);
                ormProperty.code_property.SetStatements.AddStatement("if(!(value is {1}{2}__NULL)) {0} = value", current.property.PropertyName, current.property.GetPropertyTypeString(), Kistl.API.Helper.ImplementationSuffix);
            }
        }
        #endregion
    }
}
