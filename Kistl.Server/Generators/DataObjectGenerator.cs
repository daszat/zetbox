using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld.Helper;

namespace Kistl.Server.Generators
{
    public class BaseDataObjectGenerator
    {
        private string codeBasePath = "";

        #region Generate

        public virtual void Generate(Kistl.API.IKistlContext ctx, string codeBasePath)
        {
            this.codeBasePath = codeBasePath + (codeBasePath.EndsWith("\\") ? "" : "\\");
            Directory.CreateDirectory(codeBasePath);

            Directory.GetFiles(this.codeBasePath, "*.cs", SearchOption.AllDirectories).
                ToList().ForEach(f => File.Delete(f));

            var generatedInterfaceFileNames = new List<string>();
            var generatedClientFileNames = new List<string>();
            var generatedServerFileNames = new List<string>();

            var objClassList = Generator.GetObjectClassList(ctx).OrderBy(x => x.ClassName);

            Console.Write("  Object Classes");
            foreach (ObjectClass objClass in objClassList)
            {
                Console.Write(".");
                generatedInterfaceFileNames.Add(Generate_Interface(ctx, objClass));
                generatedClientFileNames.Add(Generate_ObjectClass(ctx, objClass, TaskEnum.Client));
                generatedServerFileNames.Add(Generate_ObjectClass(ctx, objClass, TaskEnum.Server));
            }
            Console.WriteLine();

            
            Console.Write("  Collection Entries");
            generatedClientFileNames.Add(Generate_CollectionEntries(ctx, TaskEnum.Client));
            generatedServerFileNames.Add(Generate_CollectionEntries(ctx, TaskEnum.Server));
            Console.WriteLine(".");

            Console.WriteLine("  Serializer");


            Console.Write("  Interfaces");
            var interfaceList = Generator.GetInterfaceList(ctx).OrderBy(x => x.ClassName);
            foreach (Interface i in interfaceList)
            {
                Console.Write(".");
                generatedInterfaceFileNames.Add(Generate_Interface_Interfaces(ctx, i));
            }
            Console.WriteLine();

            Console.Write("  Enums");
            var enumList = Generator.GetEnumList(ctx).OrderBy(x => x.ClassName);
            foreach (Enumeration e in enumList)
            {
                Console.Write(".");
                generatedInterfaceFileNames.Add(Generate_Interface_Enumerations(ctx, e));
            }
            Console.WriteLine();

            Console.Write("  Structs");
            var structList = Generator.GetStructList(ctx).OrderBy(x => x.ClassName);
            foreach (Struct s in structList)
            {
                Console.Write(".");
                generatedInterfaceFileNames.Add(Generate_Interface_Structs(ctx, s));
                generatedClientFileNames.Add(Generate_Implementation_Structs(ctx, TaskEnum.Client, s));
                generatedServerFileNames.Add(Generate_Implementation_Structs(ctx, TaskEnum.Server, s));
            }
            Console.WriteLine();

            Console.Write("  FrozenContext");

            Console.WriteLine();

            Console.WriteLine("  Assemblyinfo");
            generatedInterfaceFileNames.Add(Generate_AssemblyInfo(ctx, TaskEnum.Interface));
            generatedClientFileNames.Add(Generate_AssemblyInfo(ctx, TaskEnum.Client));
            generatedServerFileNames.Add(Generate_AssemblyInfo(ctx, TaskEnum.Server));

            Console.WriteLine("  Project File");
            Generate_ProjectFile(ctx, TaskEnum.Interface, "{0C9E6E69-309F-46F7-A936-D5762229DEB9}", generatedInterfaceFileNames);
            Generate_ProjectFile(ctx, TaskEnum.Client, "{80F37FB5-66C6-45F2-9E2A-F787B141D66C}", generatedClientFileNames);
            Generate_ProjectFile(ctx, TaskEnum.Server, "{62B9344A-87D1-4715-9ABB-EAE0ACC4F523}", generatedServerFileNames);

        }

        #endregion

        private static string RunTemplate(IKistlContext ctx, TaskEnum task, string templateName, string baseFilename, string extension, params object[] args)
        {
            string providerPath;
            switch (task)
            {
                case TaskEnum.Interface:
                    providerPath = "Kistl.Server.Generators.Templates";
                    break;
                case TaskEnum.Server:
                    providerPath = "Kistl.Server.Generators.EntityFramework";
                    break;
                case TaskEnum.Client:
                    providerPath = "Kistl.Server.Generators.ClientObjects";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("task");
            }
            string filename = String.Join(".", new string[] { baseFilename, task.ToString(), extension });
            var gen = Generator.GetTemplateGenerator(
                providerPath,
                templateName,
                filename,
                task.ToNameSpace(),
                new object[] { ctx }.Concat(args).ToArray());
            gen.ExecuteTemplate();
            return filename;
        }

        private static void Generate_ProjectFile(IKistlContext ctx, TaskEnum task, string projectGuid, List<string> generatedFileNames)
        {
            string templateName;
            if (task == TaskEnum.Interface)
            {
                templateName = "Interface.ProjectFile";
            }
            else
            {
                templateName = "Implementation.ProjectFile";
            }
            RunTemplate(ctx, task, templateName, "Kistl.Objects", "csproj", projectGuid, generatedFileNames);
        }

        private static string Generate_AssemblyInfo(IKistlContext ctx, TaskEnum task)
        {
            string templateName;
            if (task == TaskEnum.Interface)
            {
                templateName = "Interface.AssemblyInfoTemplate";
            }
            else
            {
                templateName = "Implementation.AssemblyInfoTemplate";
            }
            return RunTemplate(ctx, task, templateName, "AssemblyInfo", "cs");
        }

        private static string Generate_Interface(IKistlContext ctx, ObjectClass objClass)
        {
            return RunTemplate(ctx, TaskEnum.Interface, "Interface.DataTypes.Template", objClass.ClassName, "Designer.cs", objClass);
        }

        private static string Generate_ObjectClass(IKistlContext ctx, ObjectClass objClass, TaskEnum task)
        {
            return RunTemplate(ctx, task, "Implementation.ObjectClasses.Template", objClass.ClassName, "Designer.cs", objClass);
        }

        private static string Generate_CollectionEntries(IKistlContext ctx, TaskEnum task)
        {
            return RunTemplate(ctx, task, "Implementation.ObjectClasses.CollectionEntries", "CollectionEntries", "Designer.cs");
        }

        private static string Generate_Interface_Enumerations(IKistlContext ctx, Enumeration e)
        {
            return RunTemplate(ctx, TaskEnum.Interface, "Interface.Enumerations.Template", e.ClassName, "Designer.cs", e);
        }

        private static string Generate_Interface_Structs(IKistlContext ctx, Struct s)
        {
            return RunTemplate(ctx, TaskEnum.Interface, "Interface.DataTypes.Template", s.ClassName, "Designer.cs", s);
        }

        private static string Generate_Implementation_Structs(IKistlContext ctx, TaskEnum task, Struct s)
        {
            return RunTemplate(ctx, task, "Implementation.Structs.Template", s.ClassName , "Designer.cs", s);
        }

        private static string Generate_Interface_Interfaces(IKistlContext ctx, Interface i)
        {
            return RunTemplate(ctx, TaskEnum.Interface, "Interface.DataTypes.Template", i.ClassName, "Designer.cs", i);
        }

    }


    #region DataObjectGeneratorFactory
    public static class DataObjectGeneratorFactory
    {
        public static BaseDataObjectGenerator GetGenerator()
        {
            return new BaseDataObjectGenerator();
        }
    }
    #endregion
}
