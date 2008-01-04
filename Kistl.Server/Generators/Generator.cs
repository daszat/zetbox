using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;

namespace Kistl.Server.Generators
{
    public class Generator
    {
        public static void GenerateCode()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                IDataObjectGenerator gDataObjects = DataObjectGeneratorFactory.GetGenerator();
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
            options.TreatWarningsAsErrors = true;
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
    }
}
