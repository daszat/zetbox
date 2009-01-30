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
                generatedInterfaceFileNames.Add(Generate_Interface_ObjectClasses(ctx, objClass));
                generatedClientFileNames.Add(Generate_Client_ObjectClasses(ctx, objClass));
                generatedServerFileNames.Add(Generate_Server_ObjectClasses(ctx, objClass));
            }
            Console.WriteLine();

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
                generatedClientFileNames.Add(Generate_Implementation_Structs(ctx, TaskEnum.Client, "Kistl.Server.Generators.ClientObjects", s));
                generatedServerFileNames.Add(Generate_Implementation_Structs(ctx, TaskEnum.Server, "Kistl.Server.Generators.EntityFramework", s));
            }
            Console.WriteLine();

            Console.Write("  FrozenContext");

            Console.WriteLine();

            Console.WriteLine("  Assemblyinfo");
            generatedInterfaceFileNames.Add(Generate_AssemblyInfo(ctx, "Kistl.Server.Generators.Templates", "Interface.AssemblyInfoTemplate", TaskEnum.Interface));
            generatedClientFileNames.Add(Generate_AssemblyInfo(ctx, "Kistl.Server.Generators.ClientObjects", "Implementation.AssemblyInfoTemplate", TaskEnum.Client));
            generatedServerFileNames.Add(Generate_AssemblyInfo(ctx, "Kistl.Server.Generators.EntityFramework", "Implementation.AssemblyInfoTemplate", TaskEnum.Server));

            Console.WriteLine("  Project File");
            Generate_ProjectFile(ctx, "Kistl.Server.Generators.Templates", "Interface.ProjectFile", TaskEnum.Interface, "{0C9E6E69-309F-46F7-A936-D5762229DEB9}", generatedInterfaceFileNames);
            Generate_ProjectFile(ctx, "Kistl.Server.Generators.ClientObjects", "Implementation.ProjectFile", TaskEnum.Client, "{80F37FB5-66C6-45F2-9E2A-F787B141D66C}", generatedClientFileNames);
            Generate_ProjectFile(ctx, "Kistl.Server.Generators.EntityFramework", "Implementation.ProjectFile", TaskEnum.Server, "{62B9344A-87D1-4715-9ABB-EAE0ACC4F523}", generatedServerFileNames);

        }

        #endregion

        private static void Generate_ProjectFile(IKistlContext ctx, string templateProviderPath, string templateName, TaskEnum task, string projectGuid, List<string> generatedFileNames)
        {
            var gen = Generator.GetTemplateGenerator(
                templateProviderPath,
                templateName,
                task.ToNameSpace() + ".csproj",
                task.ToNameSpace(),
                ctx,
                projectGuid,
                generatedFileNames);
            gen.ExecuteTemplate();
        }

        private static string Generate_AssemblyInfo(IKistlContext ctx, string templateProviderPath, string templateName, TaskEnum task)
        {
            string filename = "AssemblyInfo.cs";

            var gen = Generator.GetTemplateGenerator(
                templateProviderPath,
                templateName,
                filename,
                task.ToNameSpace(),
                ctx);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Interface_ObjectClasses(IKistlContext ctx, ObjectClass objClass)
        {
            string filename = objClass.ClassName + ".Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                "Kistl.Server.Generators.EntityFramework",
                @"Interface.DataTypes.Template",
                filename,
                TaskEnum.Interface.ToNameSpace(),
                ctx,
                objClass);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Client_ObjectClasses(IKistlContext ctx, ObjectClass objClass)
        {
            string filename = objClass.ClassName + ".Client.Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                "Kistl.Server.Generators.ClientObjects",
                @"Implementation.ObjectClasses.Template",
                filename,
                TaskEnum.Client.ToNameSpace(),
                ctx,
                objClass);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Server_ObjectClasses(IKistlContext ctx, ObjectClass objClass)
        {
            string filename = objClass.ClassName + ".Server.Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                 "Kistl.Server.Generators.EntityFramework",
                @"Implementation.ObjectClasses.Template",
                filename,
                TaskEnum.Server.ToNameSpace(),
                ctx,
                objClass);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Interface_Enumerations(IKistlContext ctx, Enumeration e)
        {
            string filename = e.ClassName + ".Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                 "Kistl.Server.Generators.EntityFramework",
                 @"Interface.Enumerations.Template",
                 filename,
                 TaskEnum.Interface.ToNameSpace(),
                 ctx,
                 e);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Interface_Structs(IKistlContext ctx, Struct s)
        {
            string filename = s.ClassName + ".Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                 "Kistl.Server.Generators.EntityFramework",
                 @"Interface.DataTypes.Template",
                 filename,
                 TaskEnum.Interface.ToNameSpace(),
                 ctx,
                 s);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Implementation_Structs(IKistlContext ctx, TaskEnum task, string providerNamespace, Struct s)
        {
            string filename = s.ClassName + "." + task + ".Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                 providerNamespace,
                 @"Implementation.Structs.Template",
                 filename,
                 task.ToNameSpace(),
                 ctx,
                 s);
            gen.ExecuteTemplate();

            return filename;
        }

        private static string Generate_Interface_Interfaces(IKistlContext ctx, Interface i)
        {
            string filename = i.ClassName + ".Designer.cs";

            var gen = Generator.GetTemplateGenerator(
                 "Kistl.Server.Generators.EntityFramework",
                 @"Interface.DataTypes.Template",
                 filename,
                 TaskEnum.Interface.ToNameSpace(),
                 ctx,
                 i);
            gen.ExecuteTemplate();

            return filename;
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
