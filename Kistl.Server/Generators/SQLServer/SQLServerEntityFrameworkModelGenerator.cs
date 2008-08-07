using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API;

namespace Kistl.Server.Generators.SQLServer
{
    public class SQLServerEntityFrameworkModelGenerator : IMappingGenerator
    {
        private string path = "";
        private Kistl.API.IKistlContext ctx = null;

        public void Generate(Kistl.API.IKistlContext ctx, string path)
        {
            System.Diagnostics.Trace.TraceInformation("Generating EF-Model...");

            this.path = path + (path.EndsWith("\\") ? "" : "\\");
            this.ctx = ctx;

            IQueryable<ObjectClass> objClassList = Generator.GetObjectClassList(ctx);
            IQueryable<Struct> structList = Generator.GetStructList(ctx);
            GenerateCSDL(objClassList, structList);
            GenerateMSL(objClassList);
            GenerateSSDL(objClassList);

            System.Diagnostics.Trace.TraceInformation("...finished!");
        }

        #region Helper
        private static ObjectClass GetRootClass(ObjectClass c)
        {
            while (c.BaseObjectClass != null)
            {
                c = c.BaseObjectClass;
            }

            return c;
        }

        private static string GetAssociationChildEntitySetName(Property prop, IQueryable<ObjectClass> objClassList)
        {
            TypeMoniker childType = Generator.GetAssociationChildType(prop);
            if (!prop.IsList)
            {
                return GetRootClass(objClassList.First(c => childType.Classname == c.ClassName)).ClassName;
            }
            else
            {
                return childType.Classname;
            }
        }

        private static string GetAssociationParentTypeName(ObjectReferenceProperty prop)
        {
            return GetAssociationParentTypeName(new TypeMoniker(prop.GetPropertyTypeString()));
        }

        private static string GetAssociationParentTypeName(TypeMoniker type)
        {
            return "Model." + type.Classname;
        }

        private static string GetAssociationChildTypeName(Property prop)
        {
            if (!prop.IsList)
            {
                return "Model." + prop.ObjectClass.ClassName;
            }
            else
            {
                return "Model." + Generator.GetPropertyCollectionObjectType(prop).Classname;
            }
        }
        #endregion

        #region GenerateCSDL
        private void GenerateCSDL(IQueryable<ObjectClass> objClassList, IQueryable<Struct> structList)
        {
            using (System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(path + @"Kistl.Objects.Server\Model.csdl", Encoding.UTF8))
            {
                xml.Indentation = 2;
                xml.IndentChar = ' ';
                xml.Formatting = System.Xml.Formatting.Indented;

                // Prepare
                var listProperties = Generator.GetCollectionProperties(ctx);
                var objectReferenceProperties = Generator.GetObjectReferenceProperties(ctx);

                // Maintag
                xml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                xml.WriteStartElement("Schema");
                xml.WriteAttributeString("Namespace", "Model");
                xml.WriteAttributeString("Alias", "Self");
                xml.WriteAttributeString("xmlns", "http://schemas.microsoft.com/ado/2006/04/edm");

                GenerateCSDL_EntityContainer(xml, objClassList, listProperties, objectReferenceProperties);
                GenerateCSDL_EntityTypeObjects(xml, objClassList);
                GenerateCSDL_ComplexTypeStructs(xml, structList);
                GenerateCSDL_EntityTypeListProperties(xml, listProperties);
                GenerateCSDL_AssociationObjectRelations(xml, objectReferenceProperties);
                GenerateCSDL_AssociationListPropertiesRelations(xml, listProperties);

                xml.WriteEndElement(); // </Schema>
            }
        }

        #region GenerateCSDL_AssociationListPropertiesRelations
        private void GenerateCSDL_AssociationListPropertiesRelations(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                xml.WriteStartElement("Association");

                TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
                TypeMoniker childType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "fk_Parent"));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(prop.ObjectClass));
                xml.WriteAttributeString("Type", GetAssociationParentTypeName(parentType));
                xml.WriteAttributeString("Multiplicity", "0..1");
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("Type", GetAssociationChildTypeName(prop));
                xml.WriteAttributeString("Multiplicity", "*");
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateCSDL_AssociationObjectRelations

        private void GenerateCSDL_AssociationObjectRelations(System.Xml.XmlTextWriter xml, IQueryable<ObjectReferenceProperty> objectReferenceProperties)
        {
            foreach (ObjectReferenceProperty prop in objectReferenceProperties)
            {
                xml.WriteStartElement("Association");

                TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
                TypeMoniker childType = Generator.GetAssociationChildType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, prop.PropertyName));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("Type", GetAssociationParentTypeName(prop)); 
                xml.WriteAttributeString("Multiplicity", "0..1");
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("Type", GetAssociationChildTypeName(prop));
                xml.WriteAttributeString("Multiplicity", "*");
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateCSDL_EntityTypeListProperties
        private void GenerateCSDL_EntityTypeListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                TypeMoniker collectionType = Generator.GetPropertyCollectionObjectType(prop);
                TypeMoniker parentType;
                TypeMoniker childType;

                xml.WriteStartElement("EntityType");
                xml.WriteAttributeString("Name", collectionType.Classname);

                xml.WriteStartElement("Key");
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Key>

                // ID
                xml.WriteStartElement("Property");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("Type", "Int32");
                xml.WriteAttributeString("Nullable", "false");
                xml.WriteEndElement(); // </Property>

                // Value
                if (prop is ObjectReferenceProperty)
                {
                    // ObjectReferenceProperty
                    xml.WriteStartElement("NavigationProperty");
                    xml.WriteAttributeString("Name", "Value");
                    parentType = new TypeMoniker(prop.GetPropertyTypeString());
                    childType = collectionType;
                    xml.WriteAttributeString("Relationship", "Model." + Generator.GetAssociationName(parentType, childType, prop.PropertyName));
                    xml.WriteAttributeString("FromRole", Generator.GetAssociationChildRoleName(childType));
                    xml.WriteAttributeString("ToRole", Generator.GetAssociationParentRoleName(parentType));
                    // TODO: IsNullable??
                    xml.WriteEndElement(); // </NavigationProperty>
                }
                else if (prop is ValueTypeProperty)
                {
                    xml.WriteStartElement("Property");
                    xml.WriteAttributeString("Name", "Value");
                    xml.WriteAttributeString("Type", Type.GetType(prop.GetPropertyTypeString()).Name);
                    if (prop is StringProperty)
                    {
                        xml.WriteAttributeString("MaxLength", ((StringProperty)prop).Length.ToString());
                    }
                    xml.WriteAttributeString("Nullable", prop.IsNullable.ToString().ToLowerInvariant());
                    xml.WriteEndElement(); // </Property>
                }

                // Parent
                xml.WriteStartElement("NavigationProperty");
                xml.WriteAttributeString("Name", "Parent");
                parentType = prop.ObjectClass.GetTypeMoniker(); //new TypeMoniker(prop.ObjectClass.Module.Namespace, prop.ObjectClass.ClassName);
                childType = collectionType;
                xml.WriteAttributeString("Relationship", "Model." + Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                xml.WriteAttributeString("FromRole", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("ToRole", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteEndElement(); // </NavigationProperty>

                xml.WriteEndElement(); // </EntityType>
            }
        }
        #endregion

        #region GenerateCSDL_ComplexTypeStructs
        private void GenerateCSDL_ComplexTypeStructs(System.Xml.XmlTextWriter xml, IQueryable<Struct> structList)
        {
            foreach (Struct obj in structList)
            {
                xml.WriteStartElement("ComplexType");
                xml.WriteAttributeString("Name", obj.ClassName);

                foreach (BaseProperty p in obj.Properties)
                {
                    if (p is ValueTypeProperty && !((ValueTypeProperty)p).IsList)
                    {
                        // ValueTypeProperty
                        xml.WriteStartElement("Property");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        xml.WriteAttributeString("Type", p is EnumerationProperty ? "Int32" : Type.GetType(p.GetPropertyTypeString()).Name);
                        if (p is StringProperty)
                        {
                            xml.WriteAttributeString("MaxLength", ((StringProperty)p).Length.ToString());
                        }
                        xml.WriteAttributeString("Nullable", ((ValueTypeProperty)p).IsNullable.ToString().ToLowerInvariant());
                        xml.WriteEndElement(); // </Property>
                    }
                    else
                    {
                        throw new NotSupportedException("Propertytype " + p.GetPropertyTypeString() + " is not supported");
                    }
                }

                xml.WriteEndElement(); // </EntityType>
            }
        }
        #endregion

        #region GenerateCSDL_EntityTypeObjects
        private void GenerateCSDL_EntityTypeObjects(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList)
        {
            foreach (ObjectClass obj in objClassList)
            {
                xml.WriteStartElement("EntityType");
                xml.WriteAttributeString("Name", obj.ClassName);
                if (obj.BaseObjectClass != null)
                {
                    xml.WriteAttributeString("BaseType", "Model." + obj.BaseObjectClass.ClassName);
                }
                else
                {
                    xml.WriteStartElement("Key");
                    xml.WriteStartElement("PropertyRef");
                    xml.WriteAttributeString("Name", "ID");
                    xml.WriteEndElement(); // </PropertyRef>
                    xml.WriteEndElement(); // </Key>

                    xml.WriteStartElement("Property");
                    xml.WriteAttributeString("Name", "ID");
                    xml.WriteAttributeString("Type", "Int32");
                    xml.WriteAttributeString("Nullable", "false");
                    xml.WriteEndElement(); // </Property>
                }

                foreach (BaseProperty p in obj.Properties)
                {
                    // TODO: implement IsNullable everywhere
                    if (p is BackReferenceProperty)
                    {
                        // BackReferenceProperty
                        xml.WriteStartElement("NavigationProperty");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        TypeMoniker parentType = p.ObjectClass.GetTypeMoniker();
                        TypeMoniker childType = Generator.GetAssociationChildType((BackReferenceProperty)p);
                        xml.WriteAttributeString("Relationship", "Model." + Generator.GetAssociationName(parentType, childType, ((BackReferenceProperty)p).ReferenceProperty.PropertyName));
                        xml.WriteAttributeString("FromRole", Generator.GetAssociationParentRoleName(parentType));
                        xml.WriteAttributeString("ToRole", Generator.GetAssociationChildRoleName(childType));
                        xml.WriteEndElement(); // </NavigationProperty>
                    }
                    else if (p is ObjectReferenceProperty && !((ObjectReferenceProperty)p).IsList)
                    {
                        // ObjectReferenceProperty
                        xml.WriteStartElement("NavigationProperty");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        TypeMoniker parentType = new TypeMoniker(p.GetPropertyTypeString());
                        TypeMoniker childType = Generator.GetAssociationChildType(p as ObjectReferenceProperty);
                        xml.WriteAttributeString("Relationship", "Model." + Generator.GetAssociationName(parentType, childType, p.PropertyName));
                        xml.WriteAttributeString("FromRole", Generator.GetAssociationChildRoleName(childType));
                        xml.WriteAttributeString("ToRole", Generator.GetAssociationParentRoleName(parentType));
                        xml.WriteEndElement(); // </NavigationProperty>
                    }
                    else if (p is ObjectReferenceProperty && ((ObjectReferenceProperty)p).IsList)
                    {
                        // ObjectReferenceProperty List
                        xml.WriteStartElement("NavigationProperty");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        TypeMoniker parentType = p.ObjectClass.GetTypeMoniker();
                        TypeMoniker childType = Generator.GetPropertyCollectionObjectType((ObjectReferenceProperty)p);
                        xml.WriteAttributeString("Relationship", "Model." + Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                        xml.WriteAttributeString("FromRole", Generator.GetAssociationParentRoleName(parentType));
                        xml.WriteAttributeString("ToRole", Generator.GetAssociationChildRoleName(childType));
                        xml.WriteEndElement(); // </NavigationProperty>
                    }
                    else if (p is ValueTypeProperty && !((ValueTypeProperty)p).IsList)
                    {
                        // ValueTypeProperty
                        xml.WriteStartElement("Property");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        xml.WriteAttributeString("Type", p is EnumerationProperty ? "Int32" : Type.GetType(p.GetPropertyTypeString()).Name);
                        if (p is StringProperty)
                        {
                            xml.WriteAttributeString("MaxLength", ((StringProperty)p).Length.ToString());
                        }
                        xml.WriteAttributeString("Nullable", ((ValueTypeProperty)p).IsNullable.ToString().ToLowerInvariant());
                        xml.WriteEndElement(); // </Property>
                    }
                    else if (p is ValueTypeProperty && ((ValueTypeProperty)p).IsList)
                    {
                        // ValueTypeProperty List
                        xml.WriteStartElement("NavigationProperty");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        TypeMoniker parentType = p.ObjectClass.GetTypeMoniker();
                        TypeMoniker childType = Generator.GetPropertyCollectionObjectType((ValueTypeProperty)p);
                        xml.WriteAttributeString("Relationship", "Model." + Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                        xml.WriteAttributeString("FromRole", Generator.GetAssociationParentRoleName(parentType));
                        xml.WriteAttributeString("ToRole", Generator.GetAssociationChildRoleName(childType));
                        xml.WriteEndElement(); // </NavigationProperty>
                    }
                }

                xml.WriteEndElement(); // </EntityType>
            }
        }
        #endregion

        #region GenerateCSDL_EntityContainer
        private void GenerateCSDL_EntityContainer(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList, IQueryable<Property> listProperties, IQueryable<ObjectReferenceProperty> objectReferenceProperties)
        {
            // Write EntityContainer
            xml.WriteStartElement("EntityContainer");
            xml.WriteAttributeString("Name", "Entities");

            foreach (ObjectClass obj in objClassList)
            {
                if (obj.BaseObjectClass != null) continue;
                xml.WriteStartElement("EntitySet");
                xml.WriteAttributeString("Name", obj.ClassName);
                xml.WriteAttributeString("EntityType", "Model." + obj.ClassName);
                xml.WriteEndElement(); // </EntitySet>
            }

            foreach (Property prop in listProperties)
            {
                TypeMoniker otherType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteStartElement("EntitySet");
                xml.WriteAttributeString("Name", otherType.Classname);
                xml.WriteAttributeString("EntityType", "Model." + otherType.Classname);
                xml.WriteEndElement(); // </EntitySet>
            }


            #region Relations
            foreach (ObjectReferenceProperty prop in objectReferenceProperties)
            {
                xml.WriteStartElement("AssociationSet");
                TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
                TypeMoniker childType = Generator.GetAssociationChildType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, prop.PropertyName));
                xml.WriteAttributeString("Association", "Model." + Generator.GetAssociationName(parentType, childType, prop.PropertyName));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("EntitySet", GetRootClass(prop.ReferenceObjectClass).ClassName);
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("EntitySet", GetAssociationChildEntitySetName(prop, objClassList));
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
            #endregion

            #region List Properties Relations

            foreach (Property prop in listProperties)
            {
                TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker();
                TypeMoniker childType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteStartElement("AssociationSet");
                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                xml.WriteAttributeString("Association", "Model." + Generator.GetAssociationName(parentType, childType, "fk_Parent"));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("EntitySet", GetRootClass((ObjectClass)prop.ObjectClass).ClassName);
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("EntitySet", GetAssociationChildEntitySetName(prop, objClassList));
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
            #endregion

            xml.WriteEndElement(); // </EntityContainer>
        }
        #endregion

        #endregion

        #region GenerateMSL

        #region AddEntityTypeMapping
        private void AddEntityTypeMapping(System.Xml.XmlTextWriter xml, ObjectClass obj)
        {
            xml.WriteStartElement("EntityTypeMapping");
            xml.WriteAttributeString("TypeName", "IsTypeOf(Model." + obj.ClassName + ")");

            xml.WriteStartElement("MappingFragment");
            xml.WriteAttributeString("StoreEntitySet", obj.ClassName);

            xml.WriteStartElement("ScalarProperty");
            xml.WriteAttributeString("Name", "ID");
            xml.WriteAttributeString("ColumnName", "ID");
            xml.WriteEndElement(); // </ScalarProperty>

            foreach (ValueTypeProperty p in obj.Properties.OfType<ValueTypeProperty>().Where(p => p.IsList == false))
            {
                xml.WriteStartElement("ScalarProperty");
                xml.WriteAttributeString("Name", p.PropertyName);
                xml.WriteAttributeString("ColumnName", p.PropertyName);
                xml.WriteEndElement(); // </ScalarProperty>
            }

            xml.WriteEndElement(); // </MappingFragment>

            xml.WriteEndElement(); // </EntityTypeMapping>

            // Und rekursiv runter...
            obj.SubClasses.ToList().ForEach(
                subObj => AddEntityTypeMapping(xml, subObj));
        }
        #endregion

        public void GenerateMSL(IQueryable<ObjectClass> objClassList)
        {
            using (System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(path + @"Kistl.Objects.Server\Model.msl", Encoding.UTF8))
            {
                xml.Indentation = 2;
                xml.IndentChar = ' ';
                xml.Formatting = System.Xml.Formatting.Indented;

                var listProperties = Generator.GetCollectionProperties(ctx);
                var objectReferenceProperties = Generator.GetObjectReferenceProperties(ctx);

                // Maintag
                xml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                xml.WriteStartElement("Mapping");
                xml.WriteAttributeString("Space", "C-S");
                xml.WriteAttributeString("xmlns", "urn:schemas-microsoft-com:windows:storage:mapping:CS");

                xml.WriteStartElement("EntityContainerMapping");
                xml.WriteAttributeString("StorageEntityContainer", "dbo");
                xml.WriteAttributeString("CdmEntityContainer", "Entities");

                GenerateMSL_EntitySetMapping_Objects(xml, objClassList);
                GenerateMSL_EntitySetMapping_ListProperties(xml, listProperties);
                GenerateMSL_AssociationSetMapping_ObjectReference(xml, objectReferenceProperties);
                GenerateMSL_AssociationSetMapping_ListProperties(xml, listProperties);

                xml.WriteEndElement(); // </EntityContainerMapping>

                xml.WriteEndElement(); // </Mapping>
            }
        }

        #region GenerateMSL_AssociationSetMapping_ListProperties
        private void GenerateMSL_AssociationSetMapping_ListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                xml.WriteStartElement("AssociationSetMapping");
                TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker(); // new TypeMoniker(prop.ObjectClass.Module.Namespace, prop.ObjectClass.ClassName);
                TypeMoniker childType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                xml.WriteAttributeString("TypeName", "Model." + Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                xml.WriteAttributeString("StoreEntitySet", childType.Classname);

                xml.WriteStartElement("EndProperty");
                xml.WriteAttributeString("Name", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteStartElement("ScalarProperty");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("ColumnName", "fk_" + prop.ObjectClass.ClassName);
                xml.WriteEndElement(); // </ScalarProperty>
                xml.WriteEndElement(); // </EndProperty>

                xml.WriteStartElement("EndProperty");
                xml.WriteAttributeString("Name", Generator.GetAssociationChildRoleName(childType));
                xml.WriteStartElement("ScalarProperty");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("ColumnName", "ID");
                xml.WriteEndElement(); // </ScalarProperty>
                xml.WriteEndElement(); // </EndProperty>

                xml.WriteStartElement("Condition");
                xml.WriteAttributeString("ColumnName", "fk_" + prop.ObjectClass.ClassName);
                xml.WriteAttributeString("IsNull", "false");
                xml.WriteEndElement(); // </Condition>


                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateMSL_AssociationSetMapping_ObjectReference
        private void GenerateMSL_AssociationSetMapping_ObjectReference(System.Xml.XmlTextWriter xml, IQueryable<ObjectReferenceProperty> objectReferenceProperties)
        {
            foreach (ObjectReferenceProperty prop in objectReferenceProperties)
            {
                xml.WriteStartElement("AssociationSetMapping");
                TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
                TypeMoniker childType = Generator.GetAssociationChildType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, prop.PropertyName));
                xml.WriteAttributeString("TypeName", "Model." + Generator.GetAssociationName(parentType, childType, prop.PropertyName));
                xml.WriteAttributeString("StoreEntitySet", childType.Classname);

                xml.WriteStartElement("EndProperty");
                xml.WriteAttributeString("Name", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteStartElement("ScalarProperty");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("ColumnName", "fk_" + prop.PropertyName);
                xml.WriteEndElement(); // </ScalarProperty>
                xml.WriteEndElement(); // </EndProperty>

                xml.WriteStartElement("EndProperty");
                xml.WriteAttributeString("Name", Generator.GetAssociationChildRoleName(childType));
                xml.WriteStartElement("ScalarProperty");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("ColumnName", "ID");
                xml.WriteEndElement(); // </ScalarProperty>
                xml.WriteEndElement(); // </EndProperty>

                xml.WriteStartElement("Condition");
                xml.WriteAttributeString("ColumnName", "fk_" + prop.PropertyName);
                xml.WriteAttributeString("IsNull", "false");
                xml.WriteEndElement(); // </Condition>


                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateMSL_EntitySetMapping_ListProperties
        private void GenerateMSL_EntitySetMapping_ListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                TypeMoniker otherType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteStartElement("EntitySetMapping");
                xml.WriteAttributeString("Name", otherType.Classname);

                xml.WriteStartElement("EntityTypeMapping");
                xml.WriteAttributeString("TypeName", "IsTypeOf(Model." + otherType.Classname + ")");

                xml.WriteStartElement("MappingFragment");
                xml.WriteAttributeString("StoreEntitySet", otherType.Classname);

                xml.WriteStartElement("ScalarProperty");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("ColumnName", "ID");
                xml.WriteEndElement(); // </ScalarProperty>

                if (prop is ValueTypeProperty)
                {
                    xml.WriteStartElement("ScalarProperty");
                    xml.WriteAttributeString("Name", "Value");
                    xml.WriteAttributeString("ColumnName", prop.PropertyName);
                    xml.WriteEndElement(); // </ScalarProperty>
                }

                xml.WriteEndElement(); // </MappingFragment>

                xml.WriteEndElement(); // </EntityTypeMapping>


                xml.WriteEndElement(); // </EntitySetMapping>
            }
        }
        #endregion

        #region GenerateMSL_EntitySetMapping_Objects
        private void GenerateMSL_EntitySetMapping_Objects(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList)
        {
            foreach (ObjectClass obj in objClassList)
            {
                // Nur für Basisobjekte
                if (obj.BaseObjectClass != null) continue;

                xml.WriteStartElement("EntitySetMapping");
                xml.WriteAttributeString("Name", obj.ClassName);

                AddEntityTypeMapping(xml, obj);

                xml.WriteEndElement(); // </EntitySetMapping>
            }
        }
        #endregion

        #endregion

        #region GenerateSSDL
        public void GenerateSSDL(IQueryable<ObjectClass> objClassList)
        {
            using (System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(path + @"Kistl.Objects.Server\Model.ssdl", Encoding.UTF8))
            {
                xml.Indentation = 2;
                xml.IndentChar = ' ';
                xml.Formatting = System.Xml.Formatting.Indented;

                var listProperties = Generator.GetCollectionProperties(ctx);
                var objectReferenceProperties = Generator.GetObjectReferenceProperties(ctx);

                // Maintag
                xml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                xml.WriteStartElement("Schema");
                xml.WriteAttributeString("Namespace", "Model.Store");
                xml.WriteAttributeString("Alias", "Self");
                xml.WriteAttributeString("xmlns", "http://schemas.microsoft.com/ado/2006/04/edm/ssdl");

                // Write EntityContainer
                xml.WriteStartElement("EntityContainer");
                xml.WriteAttributeString("Name", "dbo");

                GenerateSSDL_EntityContainer_ObjectClasses(xml, objClassList);
                GenerateSSDL_EntityContainer_ListProperties(xml, listProperties);
                GenerateSSDL_AssociationSet_ObjectClasses(xml, objClassList);
                GenerateSSDL_AssociationSet_ObjectReferenceProperties(xml, objectReferenceProperties);
                GenerateSSDL_AssociationSet_ListProperties(xml, listProperties);

                xml.WriteEndElement(); // </EntityContainer>

                GenerateSSDL_EntityTypes_ObjectClasses(xml, objClassList);
                GenerateSSDL_EntityTypes_ListProperties(xml, listProperties);
                GenerateSSDL_Associations_ObjectClasses(xml, objClassList);
                GenerateSSDL_Associations_ObjectReferences(xml, objectReferenceProperties);
                GenerateSSDL_Associations_ListProperties(xml, listProperties);

                xml.WriteEndElement(); // </Schema>
            }
        }

        #region GenerateSSDL_Associations_ListProperties
        private void GenerateSSDL_Associations_ListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                xml.WriteStartElement("Association");
                TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker(); // new TypeMoniker(prop.ObjectClass.Module.Namespace, prop.ObjectClass.ClassName);
                TypeMoniker childType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "fk_Parent"));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("Type", "Model.Store." + parentType.Classname);
                xml.WriteAttributeString("Multiplicity", "0..1");
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("Type", "Model.Store." + childType.Classname);
                xml.WriteAttributeString("Multiplicity", "*");
                xml.WriteEndElement(); // </End>

                xml.WriteStartElement("ReferentialConstraint");

                // Parent
                xml.WriteStartElement("Principal");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Principal>

                // Child
                xml.WriteStartElement("Dependent");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "fk_" + prop.ObjectClass.ClassName);
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Dependent>

                xml.WriteEndElement(); // </ReferentialConstraint>

                xml.WriteEndElement(); // </Association>
            }
        }
        #endregion

        #region GenerateSSDL_Associations_ObjectReferences
        private void GenerateSSDL_Associations_ObjectReferences(System.Xml.XmlTextWriter xml, IQueryable<ObjectReferenceProperty> objectReferenceProperties)
        {
            foreach (ObjectReferenceProperty prop in objectReferenceProperties)
            {
                xml.WriteStartElement("Association");
                TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
                TypeMoniker childType = Generator.GetAssociationChildType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, prop.PropertyName));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("Type", "Model.Store." + parentType.Classname);
                xml.WriteAttributeString("Multiplicity", "0..1");
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("Type", "Model.Store." + childType.Classname);
                xml.WriteAttributeString("Multiplicity", "*");
                xml.WriteEndElement(); // </End>

                xml.WriteStartElement("ReferentialConstraint");

                // Parent
                xml.WriteStartElement("Principal");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Principal>

                // Child
                xml.WriteStartElement("Dependent");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "fk_" + prop.PropertyName);
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Dependent>

                xml.WriteEndElement(); // </ReferentialConstraint>

                xml.WriteEndElement(); // </Association>
            }
        }
        #endregion
        
        #region GenerateSSDL_Associations_ObjectClasses
        private void GenerateSSDL_Associations_ObjectClasses(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList)
        {
            foreach (ObjectClass objClass in objClassList)
            {
                if (objClass.BaseObjectClass == null) continue;
                
                xml.WriteStartElement("Association");
                TypeMoniker parentType = objClass.BaseObjectClass.GetTypeMoniker();
                TypeMoniker childType = objClass.GetTypeMoniker();

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "ID"));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("Type", "Model.Store." + parentType.Classname);
                xml.WriteAttributeString("Multiplicity", "1");
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("Type", "Model.Store." + childType.Classname);
                xml.WriteAttributeString("Multiplicity", "0..1");
                xml.WriteEndElement(); // </End>

                xml.WriteStartElement("ReferentialConstraint");

                // Parent
                xml.WriteStartElement("Principal");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Principal>

                // Child
                xml.WriteStartElement("Dependent");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Dependent>

                xml.WriteEndElement(); // </ReferentialConstraint>

                xml.WriteEndElement(); // </Association>
            }
        }
        #endregion        

        #region GenerateSSDL_EntityTypes_ListProperties
        private void GenerateSSDL_EntityTypes_ListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                TypeMoniker otherType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteStartElement("EntityType");
                xml.WriteAttributeString("Name", otherType.Classname);

                xml.WriteStartElement("Key");
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Key>

                xml.WriteStartElement("Property");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("Type", "int");
                xml.WriteAttributeString("Nullable", "false");
                xml.WriteAttributeString("StoreGeneratedPattern", "Identity");
                xml.WriteEndElement(); // </Property>

                xml.WriteStartElement("Property");
                xml.WriteAttributeString("Name", "fk_" + prop.ObjectClass.ClassName);
                xml.WriteAttributeString("Type", "int");
                xml.WriteAttributeString("Nullable", "true");
                xml.WriteEndElement(); // </Property>

                xml.WriteStartElement("Property");
                if (prop is ObjectReferenceProperty)
                {
                    xml.WriteAttributeString("Name", "fk_" + prop.PropertyName);
                }
                else
                {
                    xml.WriteAttributeString("Name", prop.PropertyName);
                }
                xml.WriteAttributeString("Type", SQLServerHelper.GetDBType(prop));
                if (prop is StringProperty)
                {
                    xml.WriteAttributeString("MaxLength", ((StringProperty)prop).Length.ToString());
                }
                xml.WriteAttributeString("Nullable", prop.IsNullable.ToString().ToLowerInvariant());
                xml.WriteEndElement(); // </Property>

                xml.WriteEndElement(); // </EntityType>
            }
        }
        #endregion

        #region GenerateSSDL_EntityTypes_ObjectClasses
        private void GenerateSSDL_EntityTypes_ObjectClasses(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList)
        {
            foreach (ObjectClass obj in objClassList)
            {
                xml.WriteStartElement("EntityType");
                xml.WriteAttributeString("Name", obj.ClassName);

                xml.WriteStartElement("Key");
                xml.WriteStartElement("PropertyRef");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteEndElement(); // </PropertyRef>
                xml.WriteEndElement(); // </Key>

                xml.WriteStartElement("Property");
                xml.WriteAttributeString("Name", "ID");
                xml.WriteAttributeString("Type", "int");
                xml.WriteAttributeString("Nullable", "false");
                if (obj.BaseObjectClass == null)
                {
                    xml.WriteAttributeString("StoreGeneratedPattern", "Identity");
                }
                xml.WriteEndElement(); // </Property>

                foreach (Property p in obj.Properties.OfType<Property>().Where(p => p.IsList == false))
                {
                    xml.WriteStartElement("Property");
                    if (p is ObjectReferenceProperty)
                    {
                        xml.WriteAttributeString("Name", "fk_" + p.PropertyName);
                    }
                    else
                    {
                        xml.WriteAttributeString("Name", p.PropertyName);
                    }
                    xml.WriteAttributeString("Type", SQLServerHelper.GetDBType(p));
                    if (p is StringProperty)
                    {
                        xml.WriteAttributeString("MaxLength", ((StringProperty)p).Length.ToString());
                    }
                    if (p is ValueTypeProperty && !((ValueTypeProperty)p).IsList)
                    {
                        xml.WriteAttributeString("Nullable", p.IsNullable.ToString().ToLowerInvariant());
                    }
                    xml.WriteEndElement(); // </Property>
                }

                xml.WriteEndElement(); // </EntityType>
            }
        }
        #endregion

        #region GenerateSSDL_AssociationSet_ListProperties
        private void GenerateSSDL_AssociationSet_ListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                TypeMoniker parentType = prop.ObjectClass.GetTypeMoniker(); // new TypeMoniker(prop.ObjectClass.Module.Namespace, prop.ObjectClass.ClassName);
                TypeMoniker childType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteStartElement("AssociationSet");

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "fk_Parent"));
                xml.WriteAttributeString("Association", "Model.Store." + Generator.GetAssociationName(parentType, childType, "fk_Parent"));

                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("EntitySet", parentType.Classname);
                xml.WriteEndElement(); // </End>

                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("EntitySet", childType.Classname);
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateSSDL_AssociationSet_ObjectReferenceProperties
        private void GenerateSSDL_AssociationSet_ObjectReferenceProperties(System.Xml.XmlTextWriter xml, IQueryable<ObjectReferenceProperty> objectReferenceProperties)
        {
            foreach (ObjectReferenceProperty prop in objectReferenceProperties)
            {
                xml.WriteStartElement("AssociationSet");
                TypeMoniker parentType = new TypeMoniker(prop.GetPropertyTypeString());
                TypeMoniker childType = Generator.GetAssociationChildType(prop);

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, prop.PropertyName));
                xml.WriteAttributeString("Association", "Model.Store." + Generator.GetAssociationName(parentType, childType, prop.PropertyName));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("EntitySet", parentType.Classname);
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("EntitySet", childType.Classname); // No baseclass.
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateSSDL_AssociationSet_ObjectClasses
        private void GenerateSSDL_AssociationSet_ObjectClasses(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList)
        {
            foreach (ObjectClass objClass in objClassList)
            {
                if (objClass.BaseObjectClass == null) continue;

                xml.WriteStartElement("AssociationSet");
                TypeMoniker parentType = objClass.BaseObjectClass.GetTypeMoniker();
                TypeMoniker childType = objClass.GetTypeMoniker();

                xml.WriteAttributeString("Name", Generator.GetAssociationName(parentType, childType, "ID"));
                xml.WriteAttributeString("Association", "Model.Store." + Generator.GetAssociationName(parentType, childType, "ID"));

                // Parent
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationParentRoleName(parentType));
                xml.WriteAttributeString("EntitySet", parentType.Classname);
                xml.WriteEndElement(); // </End>

                // Child
                xml.WriteStartElement("End");
                xml.WriteAttributeString("Role", Generator.GetAssociationChildRoleName(childType));
                xml.WriteAttributeString("EntitySet", childType.Classname); // No baseclass.
                xml.WriteEndElement(); // </End>

                xml.WriteEndElement(); // </AssociationSet>
            }
        }
        #endregion

        #region GenerateSSDL_EntityContainer_ListProperties
        private void GenerateSSDL_EntityContainer_ListProperties(System.Xml.XmlTextWriter xml, IQueryable<Property> listProperties)
        {
            foreach (Property prop in listProperties)
            {
                TypeMoniker otherType = Generator.GetPropertyCollectionObjectType(prop);

                xml.WriteStartElement("EntitySet");
                xml.WriteAttributeString("Name", otherType.Classname);
                xml.WriteAttributeString("EntityType", "Model.Store." + otherType.Classname);
                xml.WriteAttributeString("Table", Generator.GetDatabaseTableName(prop));
                xml.WriteEndElement(); // </EntitySet>
            }
        }
        #endregion

        #region GenerateSSDL_EntityContainer_ObjectClasses
        private void GenerateSSDL_EntityContainer_ObjectClasses(System.Xml.XmlTextWriter xml, IQueryable<ObjectClass> objClassList)
        {
            foreach (ObjectClass obj in objClassList)
            {
                xml.WriteStartElement("EntitySet");
                xml.WriteAttributeString("Name", obj.ClassName);
                xml.WriteAttributeString("EntityType", "Model.Store." + obj.ClassName);
                xml.WriteAttributeString("Table", Generator.GetDatabaseTableName(obj));
                xml.WriteEndElement(); // </EntitySet>
            }
        }
        #endregion

        #endregion
    }
}
