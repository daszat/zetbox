using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld.Helper;
using Kistl.Server.Generators;

namespace Kistl.Server.GeneratorsOld
{

    public static class GeneratorExtensionHelper
    {
        public static TypeMoniker GetTypeMoniker(this DataType objClass)
        {
            return new TypeMoniker(objClass.Module.Namespace, objClass.ClassName);
        }

        public static TypeMoniker GetTypeMonikerImplementation(this DataType objClass)
        {
            return new TypeMoniker(objClass.Module.Namespace, objClass.ClassName + Kistl.API.Helper.ImplementationSuffix);
        }
    }

    public static class Generator
    {
        public static void GenerateCode()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Generating Code");
                BaseDataObjectGenerator gDataObjects = DataObjectGeneratorFactory.GetGenerator();
                IMappingGenerator gMapping = MappingGeneratorFactory.GetGenerator();
                using (IKistlContext ctx = Kistl.API.Server.KistlContext.InitSession())
                {
                    Trace.TraceInformation("Generating SouceFiles");
                    gDataObjects.Generate(ctx, Kistl.Server.Helper.CodeGenPath);

                    Trace.TraceInformation("Generating Mapping");
                    gMapping.Generate(ctx, Kistl.Server.Helper.CodeGenPath);

                    try
                    {
                        // Compile Code
                        Trace.TraceInformation("Compiling Interfaces");
                        Compile(TaskEnum.Interface);
                        Trace.TraceInformation("Compiling Server Assembly");
                        Compile(TaskEnum.Server);
                        Trace.TraceInformation("Compiling Client Assembly");
                        Compile(TaskEnum.Client);
                    }
                    catch
                    {
                        // Delete files
                        Delete(TaskEnum.Interface);
                        Delete(TaskEnum.Server);
                        Delete(TaskEnum.Client);
                        throw;
                    }
                }
            }
        }

        internal static void Delete(TaskEnum type)
        {
            System.IO.File.Delete(Kistl.Server.Helper.CodeGenPath + @"\bin\" + type.ToNameSpace() + ".dll");
            System.IO.File.Delete(Kistl.Server.Helper.CodeGenPath + @"\bin\" + type.ToNameSpace() + ".pdb");
        }

        internal static void Compile(TaskEnum type)
        {
            System.IO.Directory.CreateDirectory(Kistl.Server.Helper.CodeGenPath + @"\bin\");

            Microsoft.CSharp.CSharpCodeProvider p = new Microsoft.CSharp.CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            CompilerParameters options = new CompilerParameters();

            options.OutputAssembly = Kistl.Server.Helper.CodeGenPath + @"\bin\" + type.ToNameSpace() + ".dll";
            options.IncludeDebugInformation = true; // false in Production!!!
            options.GenerateExecutable = false;
            options.TreatWarningsAsErrors = false; // true in Production!!!
            options.ReferencedAssemblies.AddRange(new string[] {
                    Kistl.Server.Helper.CodeGenPath + @"\bin\Kistl.API.dll",
                    "System.dll",
                    "System.Core.dll",
                    "System.Data.dll",
                    "System.Data.DataSetExtensions.dll",
                    "System.Data.Linq.dll",
                    "System.Xml.dll",
                    "System.Xml.Linq.dll",
                    "WindowsBase.dll",
                });

            if (type != TaskEnum.Interface)
            {
                options.ReferencedAssemblies.Add(Kistl.Server.Helper.CodeGenPath + @"\bin\Kistl.API." + type + ".dll");
                options.ReferencedAssemblies.Add(Kistl.Server.Helper.CodeGenPath + @"\bin\Kistl.Objects.dll");
            }
            
            if (type == TaskEnum.Server)
            {
                options.EmbeddedResources.Add(Kistl.Server.Helper.CodeGenPath + @"\Kistl.Objects.Server\Model.csdl");
                options.EmbeddedResources.Add(Kistl.Server.Helper.CodeGenPath + @"\Kistl.Objects.Server\Model.msl");
                options.EmbeddedResources.Add(Kistl.Server.Helper.CodeGenPath + @"\Kistl.Objects.Server\Model.ssdl");

                options.ReferencedAssemblies.Add("System.Data.Entity.dll");
                options.ReferencedAssemblies.Add(Kistl.Server.Helper.CodeGenPath + @"\bin\Kistl.DALProvider.EF.dll");
            }

            CompilerResults result = p.CompileAssemblyFromFile(options,
                System.IO.Directory.GetFiles(Kistl.Server.Helper.CodeGenPath + @"\" + type.ToNameSpace() + @"\", "*.cs"));

            using (System.IO.StreamWriter file = System.IO.File.CreateText(Kistl.Server.Helper.CodeGenPath + @"\errors.txt"))
            {
                if (result.Errors.HasErrors)
                {
                    CompilerException ex = new CompilerException(result);
                    file.WriteLine(ex.Message);
                    throw ex;
                }
                else
                {
                    file.WriteLine("No errors");
                }
            }
        }

        public static void GenerateDatabase()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                GeneratorsOld.IDatabaseGenerator gDatabase = GeneratorsOld.DatabaseGeneratorFactory.GetGenerator();
                using (IKistlContext ctx = Kistl.API.Server.KistlContext.InitSession())
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

        #region GetAssociationName
        private static string GetAssociationName(string parentClass, string childClass, string propertyName)
        {
            return "FK_" + childClass + "_" + parentClass + "_" + propertyName;
        }

        public static string GetAssociationName(TypeMoniker parentClass, TypeMoniker childClass, string propertyName)
        {
            return GetAssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        }

        public static string GetAssociationName(ObjectClass parentClass, ObjectClass childClass, string propertyName)
        {
            return GetAssociationName(parentClass.ClassName, childClass.ClassName, propertyName);
        }

        public static string GetAssociationName(ObjectClass parentClass, Property listProperty)
        {
            return GetAssociationName(
                parentClass.ClassName, 
                Generator.GetPropertyCollectionObjectType(listProperty).ClassName, 
                listProperty.PropertyName);
        }
        public static string GetAssociationName(ObjectClass parentClass, Property listProperty, string propertyName)
        {
            return GetAssociationName(
                parentClass.ClassName,
                Generator.GetPropertyCollectionObjectType(listProperty).ClassName,
                propertyName);
        }

        //public static string GetAssociationName(CodeNamespace parentNamespace, CodeTypeDeclaration parentClass, CodeNamespace childNamespace, CodeTypeDeclaration childClass, string propertyName)
        //{
        //    return GetAssociationName(parentClass.Name, childClass.Name, propertyName);
        //}

        //public static string GetAssociationName(ObjectClass parentClass, CodeNamespace childNamespace, CodeTypeDeclaration childClass, string propertyName)
        //{
        //    return GetAssociationName(parentClass.ClassName, childClass.Name, propertyName);
        //}
        #endregion 

        #region GetAssociationParentRoleName
        private static string GetAssociationParentRoleName(string obj)
        {
            return "A_" + obj;
        }

        public static string GetAssociationParentRoleName(TypeMoniker obj)
        {
            return GetAssociationParentRoleName(obj.ClassName);
        }

        public static string GetAssociationParentRoleName(DataType obj)
        {
            return GetAssociationParentRoleName(obj.ClassName);
        }

        //public static string GetAssociationParentRoleName(CodeNamespace ns, CodeTypeDeclaration c)
        //{
        //    return GetAssociationParentRoleName(c.Name);
        //}
        #endregion

        #region GetAssociationChildRoleName
        private static string GetAssociationChildRoleName(string obj)
        {
            return "B_" + obj;
        }

        public static string GetAssociationChildRoleName(TypeMoniker obj)
        {
            return GetAssociationChildRoleName(obj.ClassName);
        }

        public static string GetAssociationChildRoleName(ObjectClass obj)
        {
            return GetAssociationChildRoleName(obj.ClassName);
        }

        //public static string GetAssociationChildRoleName(CodeNamespace ns, CodeTypeDeclaration c)
        //{
        //    return GetAssociationChildRoleName(c.Name);
        //}
        #endregion

        #region GetPropertyCollectionObjectType
        // ==>> Construct.PropertyCollectionEntryType
        public static TypeMoniker GetPropertyCollectionObjectType(Property prop)
        {
            return new TypeMoniker(prop.ObjectClass.Module.Namespace,
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
        public static IQueryable<ObjectClass> GetObjectClassList(Kistl.API.IKistlContext ctx)
        {
            return from c in ctx.GetQuery<ObjectClass>()
                   select c;
        }

        public static IQueryable<Interface> GetInterfaceList(Kistl.API.IKistlContext ctx)
        {
            return from i in ctx.GetQuery<Interface>()
                   select i;
        }

        public static IQueryable<Enumeration> GetEnumList(Kistl.API.IKistlContext ctx)
        {
            return from e in ctx.GetQuery<Enumeration>()
                   select e;
        }

        public static IQueryable<Struct> GetStructList(Kistl.API.IKistlContext ctx)
        {
            return from s in ctx.GetQuery<Struct>()
                   select s;
        }

        public static IEnumerable<Property> GetCollectionProperties(Kistl.API.IKistlContext ctx)
        {
            return (from p in ctx.GetQuery<Property>()
                   where p.ObjectClass is ObjectClass && p.IsList
                   select p).ToList().Where(p => p.HasStorage());
        }

        public static IEnumerable<ObjectReferenceProperty> GetObjectReferenceProperties(Kistl.API.IKistlContext ctx)
        {
            return (from p in ctx.GetQuery<ObjectReferenceProperty>()
                   where p.ObjectClass is ObjectClass
                    select p).ToList().Where(p => p.HasStorage());
        }
        #endregion

        #region GetAssociationChildType
        public static TypeMoniker GetAssociationChildType(Property prop)
        {
            if (prop.HasStorage())
            {
                if (!prop.IsList)
                {
                    return prop.ObjectClass.GetTypeMoniker();
                }
                else
                {
                    return Generator.GetPropertyCollectionObjectType(prop);
                }
            }
            else if(prop is ObjectReferenceProperty)
            {
                if (!((ObjectReferenceProperty)prop).GetOpposite().IsList)
                {
                    return new TypeMoniker(prop.GetPropertyTypeString());
                }
                else
                {
                    return Generator.GetPropertyCollectionObjectType(((ObjectReferenceProperty)prop).GetOpposite());
                }
            }

            throw new InvalidOperationException("Unable to find out AssociationChildType");
        }

        //public static TypeMoniker GetAssociationChildType(BackReferenceProperty prop)
        //{
        //    if (!prop.ReferenceProperty.IsList)
        //    {
        //        return new TypeMoniker(prop.GetPropertyTypeString());
        //    }
        //    else
        //    {
        //        return Generator.GetPropertyCollectionObjectType(prop.ReferenceProperty);
        //    }
        //}

        public static TypeMoniker GetAssociationChildTypeImplementation(Property prop)
        {
            if (!prop.IsList)
            {
                return prop.ObjectClass.GetTypeMonikerImplementation();
            }
            else
            {
                return Generator.GetPropertyCollectionObjectType(prop);
            }
        }

        //public static TypeMoniker GetAssociationChildTypeImplementation(BackReferenceProperty prop)
        //{
        //    if (!prop.ReferenceProperty.IsList)
        //    {
        //        return new TypeMoniker(prop.GetPropertyTypeString() + Kistl.API.Helper.ImplementationSuffix);
        //    }
        //    else
        //    {
        //        return Generator.GetPropertyCollectionObjectType(prop.ReferenceProperty);
        //    }
        //}
        #endregion
    }

}
