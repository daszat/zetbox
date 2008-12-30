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

            var objClassList = Generator.GetObjectClassList(ctx);

            Console.Write("  Object Classes");
            foreach (ObjectClass objClass in objClassList)
            {
                Console.Write(".");
                Generate_Interface_ObjectClasses(ctx, objClass);
                //Generate_Client_ObjectClasses(ctx, objClass);
                Generate_Server_ObjectClasses(ctx, objClass);
            }
            Console.WriteLine();

            Console.WriteLine("  Serializer");


            Console.Write("  Interfaces");
            var interfaceList = Generator.GetInterfaceList(ctx);
            foreach (Interface i in interfaceList)
            {
                Console.Write(".");
                Generate_Interface_Interfaces(ctx, i);
            }
            Console.WriteLine();

            Console.Write("  Enums");
            var enumList = Generator.GetEnumList(ctx);
            foreach (Enumeration e in enumList)
            {
                Console.Write(".");
                Generate_Interface_Enumerations(ctx, e);
            }
            Console.WriteLine();

            Console.Write("  Structs");
            var structList = Generator.GetStructList(ctx);
            foreach (Struct s in structList)
            {
                Console.Write(".");
                Generate_Interface_Structs(ctx, s);
            }
            Console.WriteLine();

            Console.Write("  FrozenContext");

            Console.WriteLine();

            Console.WriteLine("  Assemblyinfo");

        }

        #endregion

        private static void Generate_Interface_ObjectClasses(IKistlContext ctx, ObjectClass objClass)
        {

            var gen = Generator.GetTemplateGenerator(
                @"Interface.DataTypes.Template",
                objClass.ClassName + ".Designer.cs",
                Kistl.Server.GeneratorsOld.TaskEnum.Interface.ToNameSpace(),
                objClass);
            gen.ExecuteTemplate();
        }

        private void Generate_Server_ObjectClasses(IKistlContext ctx, ObjectClass objClass)
        {
            var gen = Generator.GetTemplateGenerator(
                @"Server.ObjectClasses.Template",
                objClass.ClassName + ".Server.Designer.cs",
                Kistl.Server.GeneratorsOld.TaskEnum.Server.ToNameSpace(),
                objClass);
            gen.ExecuteTemplate();
        }

        private void Generate_Interface_Enumerations(IKistlContext ctx, Enumeration e)
        {
            var gen = Generator.GetTemplateGenerator(
                            @"Interface.Enumerations.Template",
                            e.ClassName + ".Designer.cs",
                            Kistl.Server.GeneratorsOld.TaskEnum.Interface.ToNameSpace(),
                            e);
            gen.ExecuteTemplate();
        }

        private void Generate_Interface_Structs(IKistlContext ctx, Struct s)
        {
            var gen = Generator.GetTemplateGenerator(
                            @"Interface.DataTypes.Template",
                            s.ClassName + ".Designer.cs",
                            Kistl.Server.GeneratorsOld.TaskEnum.Interface.ToNameSpace(),
                            s);
            gen.ExecuteTemplate();
        }

        private void Generate_Interface_Interfaces(IKistlContext ctx, Interface i)
        {
            var gen = Generator.GetTemplateGenerator(
                            @"Interface.DataTypes.Template",
                            i.ClassName + ".Designer.cs",
                            Kistl.Server.GeneratorsOld.TaskEnum.Interface.ToNameSpace(),
                            i);
            gen.ExecuteTemplate();
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
