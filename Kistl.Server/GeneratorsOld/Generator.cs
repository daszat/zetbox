using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld.Helper;

namespace Kistl.Server.GeneratorsOld
{

    public static class Generator
    {
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
    }

}
