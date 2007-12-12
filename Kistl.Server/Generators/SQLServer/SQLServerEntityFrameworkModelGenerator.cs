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
        private Kistl.API.Server.KistlDataContext ctx = null;

        public void Generate(Kistl.API.Server.KistlDataContext ctx, string path)
        {
            Console.WriteLine("Generating EF-Model...");

            this.path = path + (path.EndsWith("\\") ? "" : "\\");
            this.ctx = ctx;

            IQueryable<ObjectClass> objClassList = from c in ctx.GetTable<ObjectClass>()
                                              select c;
            GenerateCSDL(objClassList);
            GenerateMSL(objClassList);
            GenerateSSDL(objClassList);

            Console.WriteLine("...finished!");
        }

        #region GenerateCSDL

        private ObjectClass GetRootClass(ObjectClass c)
        {
            while (c.BaseObjectClass != null)
            {
                c = c.BaseObjectClass;
            }

            return c;
        }

        private void GenerateCSDL(IQueryable<ObjectClass> objClassList)
        {
            using (System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(path + @"Kistl.Objects.Server\Model.csdl", Encoding.UTF8))
            {
                xml.Indentation = 2;
                xml.IndentChar = ' ';
                xml.Formatting = System.Xml.Formatting.Indented;

                // Maintag
                xml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                xml.WriteStartElement("Schema");
                xml.WriteAttributeString("Namespace", "Model");
                xml.WriteAttributeString("Alias", "Self");
                xml.WriteAttributeString("xmlns", "http://schemas.microsoft.com/ado/2006/04/edm");

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

                var assocProperties = from p in ctx.GetTable<BaseProperty>()
                                      from o in objClassList
                                      where p.ObjectClass.ID == o.ID && p is ObjectReferenceProperty
                                      select p;

                foreach (BaseProperty prop in assocProperties)
                {
                    xml.WriteStartElement("AssociationSet");
                    ObjectType otherType = new ObjectType(prop.GetDataType());
                    string assocName = "FK_" + prop.ObjectClass.ClassName + "_" + otherType.Classname;
                    xml.WriteAttributeString("Name", assocName);
                    xml.WriteAttributeString("Association", "Model." + assocName);

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "A_" + otherType.Classname);
                    xml.WriteAttributeString("EntitySet", GetRootClass(objClassList.First(c => otherType.Classname == c.ClassName)).ClassName);
                    xml.WriteEndElement(); // </End>

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "B_" + prop.ObjectClass.ClassName);
                    xml.WriteAttributeString("EntitySet", GetRootClass(prop.ObjectClass).ClassName);
                    xml.WriteEndElement(); // </End>

                    xml.WriteEndElement(); // </AssociationSet>
                }

                xml.WriteEndElement(); // </EntityContainer>

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
                        if (p is BackReferenceProperty)
                        {
                            xml.WriteStartElement("NavigationProperty");
                            xml.WriteAttributeString("Name", p.PropertyName);
                            ObjectType otherType = new ObjectType(p.GetDataType());
                            string assocName = "FK_" + otherType.Classname + "_" + p.ObjectClass.ClassName;
                            xml.WriteAttributeString("Relationship", "Model." + assocName);
                            xml.WriteAttributeString("FromRole", "A_" + obj.ClassName);
                            xml.WriteAttributeString("ToRole", "B_" + otherType.Classname);
                            xml.WriteEndElement(); // </NavigationProperty>
                        }
                        else if (p is ObjectReferenceProperty)
                        {
                            xml.WriteStartElement("NavigationProperty");
                            xml.WriteAttributeString("Name", p.PropertyName.Replace("fk_", ""));
                            ObjectType otherType = new ObjectType(p.GetDataType());
                            string assocName = "FK_" + p.ObjectClass.ClassName + "_" + otherType.Classname;
                            xml.WriteAttributeString("Relationship", "Model." + assocName);
                            xml.WriteAttributeString("FromRole", "B_" + obj.ClassName);
                            xml.WriteAttributeString("ToRole", "A_" + otherType.Classname);
                            xml.WriteEndElement(); // </NavigationProperty>
                        }
                        else if (p is ValueTypeProperty)
                        {
                            xml.WriteStartElement("Property");
                            xml.WriteAttributeString("Name", p.PropertyName);
                            xml.WriteAttributeString("Type", Type.GetType(p.GetDataType()).Name);
                            if (p is StringProperty)
                            {
                                xml.WriteAttributeString("MaxLength", ((StringProperty)p).Length.ToString());
                            }
                            xml.WriteAttributeString("Nullable", "true");
                            xml.WriteEndElement(); // </Property>
                        }
                    }

                    xml.WriteEndElement(); // </EntityType>
                }

                foreach (BaseProperty prop in assocProperties)
                {
                    xml.WriteStartElement("Association");
                    ObjectType otherType = new ObjectType(prop.GetDataType());
                    string assocName = "FK_" + prop.ObjectClass.ClassName + "_" + otherType.Classname;
                    xml.WriteAttributeString("Name", assocName);

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "A_" + otherType.Classname);
                    xml.WriteAttributeString("Type", "Model." + otherType.Classname);
                    xml.WriteAttributeString("Multiplicity", "0..1");
                    xml.WriteEndElement(); // </End>

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "B_" + prop.ObjectClass.ClassName);
                    xml.WriteAttributeString("Type", "Model." + prop.ObjectClass.ClassName);
                    xml.WriteAttributeString("Multiplicity", "*");
                    xml.WriteEndElement(); // </End>

                    xml.WriteEndElement(); // </AssociationSet>
                }

                xml.WriteEndElement(); // </Schema>
            }
        }
        #endregion

        #region GenerateMSL
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

            foreach (BaseProperty p in obj.Properties.OfType<ValueTypeProperty>())
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

        public void GenerateMSL(IQueryable<ObjectClass> objClassList)
        {
            using (System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(path + @"Kistl.Objects.Server\Model.msl", Encoding.UTF8))
            {
                xml.Indentation = 2;
                xml.IndentChar = ' ';
                xml.Formatting = System.Xml.Formatting.Indented;

                // Maintag
                xml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                xml.WriteStartElement("Mapping");
                xml.WriteAttributeString("Space", "C-S");
                xml.WriteAttributeString("xmlns", "urn:schemas-microsoft-com:windows:storage:mapping:CS");

                xml.WriteStartElement("EntityContainerMapping");
                xml.WriteAttributeString("StorageEntityContainer", "dbo");
                xml.WriteAttributeString("CdmEntityContainer", "Entities");

                foreach (ObjectClass obj in objClassList)
                {
                    // Nur für Basisobjekte
                    if (obj.BaseObjectClass != null) continue;

                    xml.WriteStartElement("EntitySetMapping");
                    xml.WriteAttributeString("Name", obj.ClassName);

                    AddEntityTypeMapping(xml, obj);

                    xml.WriteEndElement(); // </EntitySetMapping>
                }

                var assocProperties = from p in ctx.GetTable<BaseProperty>()
                                      from o in objClassList
                                      where p.ObjectClass.ID == o.ID && p is ObjectReferenceProperty
                                      select p;

                foreach (BaseProperty prop in assocProperties)
                {
                    xml.WriteStartElement("AssociationSetMapping");
                    ObjectType otherType = new ObjectType(prop.GetDataType());
                    string assocName = "FK_" + prop.ObjectClass.ClassName + "_" + otherType.Classname;
                    xml.WriteAttributeString("Name", assocName);
                    xml.WriteAttributeString("TypeName", "Model." + assocName);
                    xml.WriteAttributeString("StoreEntitySet", prop.ObjectClass.ClassName);

                    xml.WriteStartElement("EndProperty");
                    xml.WriteAttributeString("Name", "A_" + otherType.Classname);
                    xml.WriteStartElement("ScalarProperty");
                    xml.WriteAttributeString("Name", "ID");
                    xml.WriteAttributeString("ColumnName", prop.PropertyName);
                    xml.WriteEndElement(); // </ScalarProperty>
                    xml.WriteEndElement(); // </EndProperty>

                    xml.WriteStartElement("EndProperty");
                    xml.WriteAttributeString("Name", "B_" + prop.ObjectClass.ClassName);
                    xml.WriteStartElement("ScalarProperty");
                    xml.WriteAttributeString("Name", "ID");
                    xml.WriteAttributeString("ColumnName", "ID");
                    xml.WriteEndElement(); // </ScalarProperty>
                    xml.WriteEndElement(); // </EndProperty>

                    xml.WriteStartElement("Condition");
                    xml.WriteAttributeString("ColumnName", prop.PropertyName);
                    xml.WriteAttributeString("IsNull", "false");
                    xml.WriteEndElement(); // </Condition>


                    xml.WriteEndElement(); // </AssociationSet>
                }

                xml.WriteEndElement(); // </EntityContainerMapping>

                xml.WriteEndElement(); // </Mapping>
            }
        }
        #endregion

        #region GenerateSSDL
        public void GenerateSSDL(IQueryable<ObjectClass> objClassList)
        {
            using (System.Xml.XmlTextWriter xml = new System.Xml.XmlTextWriter(path + @"Kistl.Objects.Server\Model.ssdl", Encoding.UTF8))
            {
                xml.Indentation = 2;
                xml.IndentChar = ' ';
                xml.Formatting = System.Xml.Formatting.Indented;

                // Maintag
                xml.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                xml.WriteStartElement("Schema");
                xml.WriteAttributeString("Namespace", "Model.Store");
                xml.WriteAttributeString("Alias", "Self");
                xml.WriteAttributeString("xmlns", "http://schemas.microsoft.com/ado/2006/04/edm/ssdl");

                // Write EntityContainer
                xml.WriteStartElement("EntityContainer");
                xml.WriteAttributeString("Name", "dbo");

                foreach (ObjectClass obj in objClassList)
                {
                    xml.WriteStartElement("EntitySet");
                    xml.WriteAttributeString("Name", obj.ClassName);
                    xml.WriteAttributeString("EntityType", "Model.Store." + obj.ClassName);
                    xml.WriteAttributeString("Table", obj.TableName);
                    xml.WriteEndElement(); // </EntitySet>
                }

                var assocProperties = from p in ctx.GetTable<BaseProperty>()
                                      from o in objClassList
                                      where p.ObjectClass.ID == o.ID && p is ObjectReferenceProperty
                                      select p;

                foreach (BaseProperty prop in assocProperties)
                {
                    xml.WriteStartElement("AssociationSet");
                    ObjectType otherType = new ObjectType(prop.GetDataType());
                    string assocName = "FK_" + prop.ObjectClass.ClassName + "_" + otherType.Classname;
                    xml.WriteAttributeString("Name", assocName);
                    xml.WriteAttributeString("Association", "Model.Store." + assocName);

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "A_" + otherType.Classname);
                    xml.WriteAttributeString("EntitySet", otherType.Classname);
                    xml.WriteEndElement(); // </End>

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "B_" + prop.ObjectClass.ClassName);
                    xml.WriteAttributeString("EntitySet", prop.ObjectClass.ClassName);
                    xml.WriteEndElement(); // </End>

                    xml.WriteEndElement(); // </AssociationSet>
                }


                xml.WriteEndElement(); // </EntityContainer>

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

                    foreach (BaseProperty p in obj.Properties.OfType<Property>())
                    {
                        xml.WriteStartElement("Property");
                        xml.WriteAttributeString("Name", p.PropertyName);
                        xml.WriteAttributeString("Type", p is ObjectReferenceProperty ? "int" : SQLServerHelper.GetDBType(p.GetDataType()));
                        if (p is StringProperty)
                        {
                            xml.WriteAttributeString("MaxLength", ((StringProperty)p).Length.ToString());
                        }
                        xml.WriteAttributeString("Nullable", "true");
                        xml.WriteEndElement(); // </Property>
                    }

                    xml.WriteEndElement(); // </EntityType>
                }

                foreach (BaseProperty prop in assocProperties)
                {
                    xml.WriteStartElement("Association");
                    ObjectType otherType = new ObjectType(prop.GetDataType());
                    string assocName = "FK_" + prop.ObjectClass.ClassName + "_" + otherType.Classname;
                    xml.WriteAttributeString("Name", assocName);

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "A_" + otherType.Classname);
                    xml.WriteAttributeString("Type", "Model.Store." + otherType.Classname);
                    xml.WriteAttributeString("Multiplicity", "0..1");
                    xml.WriteEndElement(); // </End>

                    xml.WriteStartElement("End");
                    xml.WriteAttributeString("Role", "B_" + prop.ObjectClass.ClassName);
                    xml.WriteAttributeString("Type", "Model.Store." + prop.ObjectClass.ClassName);
                    xml.WriteAttributeString("Multiplicity", "*");
                    xml.WriteEndElement(); // </End>

                    xml.WriteStartElement("ReferentialConstraint");

                    xml.WriteStartElement("Principal");
                    xml.WriteAttributeString("Role", "A_" + otherType.Classname);
                    xml.WriteStartElement("PropertyRef");
                    xml.WriteAttributeString("Name", "ID");
                    xml.WriteEndElement(); // </PropertyRef>
                    xml.WriteEndElement(); // </Principal>

                    xml.WriteStartElement("Dependent");
                    xml.WriteAttributeString("Role", "B_" + prop.ObjectClass.ClassName);
                    xml.WriteStartElement("PropertyRef");
                    xml.WriteAttributeString("Name", prop.PropertyName);
                    xml.WriteEndElement(); // </PropertyRef>
                    xml.WriteEndElement(); // </Dependent>

                    xml.WriteEndElement(); // </ReferentialConstraint>

                    xml.WriteEndElement(); // </AssociationSet>
                }

                xml.WriteEndElement(); // </Schema>
            }
        }
        #endregion
    }
}
