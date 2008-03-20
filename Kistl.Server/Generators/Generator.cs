using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Kistl.API;
using System.CodeDom;
using Kistl.App.Base;

namespace Kistl.Server.Generators
{
    public enum ClientServerEnum
    {
        Client,
        Server,
    }

    public sealed class Generator
    {
        public static void GenerateCode()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                BaseDataObjectGenerator gDataObjects = DataObjectGeneratorFactory.GetGenerator();
                IMappingGenerator gMapping = MappingGeneratorFactory.GetGenerator();
                using (Kistl.API.Server.KistlDataContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
                {
                    gDataObjects.Generate(ctx, Helper.CodeGenPath);
                    gMapping.Generate(ctx, Helper.CodeGenPath);

                    // Compile Code
                    Compile(ClientServerEnum.Server);
                    Compile(ClientServerEnum.Client);
                }
            }
        }

        private static void Compile(ClientServerEnum type)
        {
            System.IO.Directory.CreateDirectory(Helper.CodeGenPath + @"\bin\");

            Microsoft.CSharp.CSharpCodeProvider p = new Microsoft.CSharp.CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            CompilerParameters options = new CompilerParameters();

            options.OutputAssembly = Helper.CodeGenPath + @"\bin\Kistl.Objects." + type + ".dll";
            options.IncludeDebugInformation = true;
            options.GenerateExecutable = false;
            options.TreatWarningsAsErrors = false; // true in Production!!!
            options.ReferencedAssemblies.AddRange(new string[] {
                    Helper.CodeGenPath + @"\bin\Kistl.API.dll",
                    Helper.CodeGenPath + @"\bin\Kistl.API." + type + ".dll",
                    "System.dll",
                    "System.Core.dll",
                    "System.Data.dll",
                    "System.Data.DataSetExtensions.dll",
                    "System.Data.Entity.dll",
                    "System.Data.Linq.dll",
                    "System.Xml.dll",
                    "System.Xml.Linq.dll",
                    "WindowsBase.dll",
                });
            
            if (type == ClientServerEnum.Server)
            {
                options.EmbeddedResources.Add(Helper.CodeGenPath + @"\Kistl.Objects.Server\Model.csdl");
                options.EmbeddedResources.Add(Helper.CodeGenPath + @"\Kistl.Objects.Server\Model.msl");
                options.EmbeddedResources.Add(Helper.CodeGenPath + @"\Kistl.Objects.Server\Model.ssdl");
            }

            CompilerResults result = p.CompileAssemblyFromFile(options,
                System.IO.Directory.GetFiles(Helper.CodeGenPath + @"\Kistl.Objects." + type + @"\", "*.cs"));

            if (result.Errors.HasErrors)
            {
                throw new CompilerException(result);
            }
        }

        public static void GenerateDatabase()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Generators.IDatabaseGenerator gDatabase = Generators.DatabaseGeneratorFactory.GetGenerator();
                using (Kistl.API.Server.KistlDataContext ctx = Kistl.API.Server.KistlDataContext.InitSession())
                {
                    gDatabase.Generate(ctx);
                }
            }
        }

        public static void GenerateAll()
        {
            GenerateCode();
            GenerateDatabase();
        }

        /// <summary>
        /// prevent this class from being instatiated
        /// </summary>
        private Generator() { }

        #region GetAssociationName
        private static string GetAssociationName(string parentClass, string childClass)
        {
            return "FK_" + childClass + "_" + parentClass;
        }

        public static string GetAssociationName(ObjectType parentClass, ObjectType childClass)
        {
            return GetAssociationName(parentClass.Classname, childClass.Classname);
        }

        /*public static string GetAssociationName(ObjectType parentClass, ObjectClass childClass)
        {
            return GetAssociationName(parentClass.Classname, childClass.ClassName);
        }

        public static string GetAssociationName(ObjectClass parentClass, ObjectType childClass)
        {
            return GetAssociationName(parentClass.ClassName, childClass.Classname);
        }*/

        public static string GetAssociationName(ObjectClass parentClass, ObjectClass childClass)
        {
            return GetAssociationName(parentClass.ClassName, childClass.ClassName);
        }

        public static string GetAssociationName(CodeNamespace parentNamespace, CodeTypeDeclaration parentClass, CodeNamespace childNamespace, CodeTypeDeclaration childClass)
        {
            return GetAssociationName(parentClass.Name, childClass.Name);
        }

        public static string GetAssociationName(ObjectClass parentClass, CodeNamespace childNamespace, CodeTypeDeclaration childClass)
        {
            return GetAssociationName(parentClass.ClassName, childClass.Name);
        }
        #endregion 

        #region GetAssociationParentRoleName
        private static string GetAssociationParentRoleName(string obj)
        {
            return "A_" + obj;
        }

        public static string GetAssociationParentRoleName(ObjectType obj)
        {
            return GetAssociationParentRoleName(obj.Classname);
        }

        public static string GetAssociationParentRoleName(DataType obj)
        {
            return GetAssociationParentRoleName(obj.ClassName);
        }

        public static string GetAssociationParentRoleName(CodeNamespace ns, CodeTypeDeclaration c)
        {
            return GetAssociationParentRoleName(c.Name);
        }
        #endregion

        #region GetAssociationChildRoleName
        private static string GetAssociationChildRoleName(string obj)
        {
            return "B_" + obj;
        }

        public static string GetAssociationChildRoleName(ObjectType obj)
        {
            return GetAssociationChildRoleName(obj.Classname);
        }

        public static string GetAssociationChildRoleName(ObjectClass obj)
        {
            return GetAssociationChildRoleName(obj.ClassName);
        }

        public static string GetAssociationChildRoleName(CodeNamespace ns, CodeTypeDeclaration c)
        {
            return GetAssociationChildRoleName(c.Name);
        }
        #endregion

        #region GetPropertyCollectionObjectType
        public static ObjectType GetPropertyCollectionObjectType(Property prop)
        {
            return new ObjectType(prop.ObjectClass.Module.Namespace,
                        prop.ObjectClass.ClassName + "_" + prop.PropertyName + "CollectionEntry");
        }
        #endregion

        #region GetDatabaseTableName
        public static string GetDatabaseTableName(ObjectClass objClass)
        {
            return objClass.TableName;
        }

        public static string GetDatabaseTableName(Property prop)
        {
            if (!prop.IsList) throw new ArgumentException("Property must be a List Property", "prop");
            return ((ObjectClass)prop.ObjectClass).TableName + "_" + prop.PropertyName + "Collection";
        }
        #endregion

        #region GetLists
        public static IQueryable<ObjectClass> GetObjectClassList(Kistl.API.Server.KistlDataContext ctx)
        {
            return from c in ctx.GetTable<ObjectClass>()
                   select c;
        }

        public static IQueryable<Property> GetCollectionProperties(Kistl.API.Server.KistlDataContext ctx)
        {
            // I'll have to extract that Query, because otherwise Linq to EF will throw an Exception
            // It's a Beta Version - so what!
            var objClasses = GetObjectClassList(ctx);
            return from p in ctx.GetTable<Property>()
                   from o in objClasses
                   where p.ObjectClass.ID == o.ID && p.IsList && p is Property
                   select p;
        }

        public static IQueryable<ObjectReferenceProperty> GetObjectReferenceProperties(Kistl.API.Server.KistlDataContext ctx)
        {
            // I'll have to extract that Query, because otherwise Linq to EF will throw an Exception
            // It's a Beta Version - so what!
            var objClasses = GetObjectClassList(ctx);
            return from p in ctx.GetTable<ObjectReferenceProperty>()
                   from o in objClasses
                   where p.ObjectClass.ID == o.ID && p is ObjectReferenceProperty
                   select p;
        }
        #endregion

        #region GetAssociationChildType
        public static ObjectType GetAssociationChildType(Property prop)
        {
            if (!prop.IsList)
            {
                return prop.ObjectClass.GetObjectType(); //new ObjectType(prop.ObjectClass.Module.Namespace, prop.ObjectClass.ClassName);
            }
            else
            {
                return Generator.GetPropertyCollectionObjectType(prop);
            }
        }

        public static ObjectType GetAssociationChildType(BackReferenceProperty prop)
        {
            if (!prop.ReferenceProperty.IsList)
            {
                return new ObjectType(prop.GetDataType());
            }
            else
            {
                return Generator.GetPropertyCollectionObjectType(prop.ReferenceProperty);
            }
        }
        #endregion
    }

}
