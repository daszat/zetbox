using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Build.BuildEngine;
using Microsoft.Build.Framework;

using Arebis.CodeGeneration;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.GeneratorsOld;

namespace Kistl.Server.Generators
{
    public static class Generator
    {
        public static void GenerateCode()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Generating Code");
                using (IKistlContext ctx = KistlContext.InitSession())
                {
                    var generators = new[]{
                        new { Caption = "Interface Source Files", Generator = DataObjectGeneratorFactory.GetInterfaceGenerator() },
                        new { Caption = "Server Source Files", Generator = DataObjectGeneratorFactory.GetServerGenerator() },
                        new { Caption = "Client Source Files", Generator = DataObjectGeneratorFactory.GetClientGenerator() },
                        new { Caption = "Generating Frozen Source Files", Generator = DataObjectGeneratorFactory.GetFreezingGenerator() },
                    };

                    Directory.SetCurrentDirectory(Helper.CodeGenPath);
                    
                    // doesn't stop growing
                    if (File.Exists("TemplateCodegenLog.txt"))
                        File.Delete("TemplateCodegenLog.txt");

                    string binPath = Path.Combine(Helper.CodeGenPath, @"bin\Debug");
                    Directory.CreateDirectory(binPath);

                    var engine = new Engine(ToolsetDefinitionLocations.Registry);

                    engine.RegisterLogger(new ConsoleLogger(LoggerVerbosity.Minimal));

                    var logger = new FileLogger();
                    logger.Parameters = String.Format(@"logfile={0}", Path.Combine(Helper.CodeGenPath, "compile.log"));
                    engine.RegisterLogger(logger);

                    try
                    {

                        foreach (var gen in generators)
                        {
                            Trace.TraceInformation(String.Format("Generating: {0}", gen.Caption));
                            string projectFileName = gen.Generator.Generate(ctx, Helper.CodeGenPath);

                            var proj = new Project(engine);
                            proj.Load(projectFileName);
                            var defaultPropertyGroup = proj.AddNewPropertyGroup(false);
                            defaultPropertyGroup.AddNewProperty("SourcePath", @"P:\Kistl", true);
                            defaultPropertyGroup.AddNewProperty("OutputPath", binPath, true);

                            if (!engine.BuildProject(proj))
                            {
                                // TODO: fix dll name here
                                //File.Delete(Path.Combine(binPath, "Kistl.Objects.dll"));
                                //File.Delete(Path.Combine(binPath, "Kistl.Objects.pdb"));
                                throw new ApplicationException(String.Format("Failed to compile {0}", gen.Caption));
                            }
                        }

                    }
                    finally
                    {
                        // close all logfiles
                        engine.UnregisterAllLoggers();
                    }
                }
            }
        }

        public static void GenerateDatabase()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
            }
        }

        public static void GenerateAll()
        {
            GenerateCode();
            GenerateDatabase();
        }

        internal static TemplateGenerator GetTemplateGenerator(string providerTemplatePath,
            string template, string output, string targetdir, params object[] templateParameter)
        {
            var gen = new TemplateGenerator();

            gen.Settings.Add("basetemplatepath", "Kistl.Server.Generators.Templates");
            gen.Settings.Add("providertemplatepath", providerTemplatePath);

            gen.Settings.Add("template", template);

            gen.Settings.Add("targetdir", targetdir);
            gen.Settings.Add("output", output);
            gen.Settings.Add("logfile", "TemplateCodegenLog.txt");

            gen.TemplateParameters = templateParameter;

            return gen;
        }

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
    }

    public class KistlCodeTemplate : Arebis.CodeGeneration.CodeTemplate
    {
        public KistlCodeTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }

        protected string ResolveResourceUrl(string template)
        {
            return "res://kistl.server/Kistl.Server.Generators.Templates." + template;
        }
    }
}
