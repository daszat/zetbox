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
using Kistl.Server;
using System.Globalization;
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
                Generate_Interface_ObjectClass(ctx, objClass);
                //Generate_Client_ObjectClass(ctx, objClass);
                //Generate_Server_ObjectClass(ctx, objClass);
            }
            Console.WriteLine();

            Console.WriteLine("  Serializer");


            Console.Write("  Interfaces");
            var interfaceList = Generator.GetInterfaceList(ctx);
            foreach (Interface i in interfaceList)
            {
                Console.Write(".");
            }
            Console.WriteLine();

            Console.Write("  Enums");
            var enumList = Generator.GetEnumList(ctx);
            foreach (Enumeration e in enumList)
            {
                Console.Write(".");
            }
            Console.WriteLine();

            Console.Write("  Structs");
            var structList = Generator.GetStructList(ctx);
            foreach (Struct s in structList)
            {
                Console.Write(".");
            }
            Console.WriteLine();

            Console.Write("  FrozenContext");

            Console.WriteLine();

            Console.WriteLine("  Assemblyinfo");

        }
        #endregion

        private static void Generate_Interface_ObjectClass(IKistlContext ctx, ObjectClass objClass)
        {
            Arebis.CodeGenerator.TemplateGenerator gen = Generator.GetTemplateGenerator(
                @"Interface.ObjectClassTemplate",
                objClass.ClassName + ".Designer.cs",
                Kistl.Server.GeneratorsOld.TaskEnum.Interface.GetKistObjectsName(),
                objClass);
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
