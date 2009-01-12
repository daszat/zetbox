using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API;
using Kistl.Server.GeneratorsOld;
using Kistl.App.Base;
using Kistl.API.Server;
using Arebis.CodeGeneration;

namespace Kistl.Server.Generators
{
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
                    gDataObjects.Generate(ctx, Helper.CodeGenPath);

                    Trace.TraceInformation("Generating Mapping");
                    gMapping.Generate(ctx, Helper.CodeGenPath);

                    try
                    {
                        // Compile Code
                        Trace.TraceInformation("Compiling Interfaces");
                        GeneratorsOld.Generator.Compile(TaskEnum.Interface);
                        Trace.TraceInformation("Compiling Server Assembly");
                        GeneratorsOld.Generator.Compile(TaskEnum.Server);
                        Trace.TraceInformation("Compiling Client Assembly");
                        GeneratorsOld.Generator.Compile(TaskEnum.Client);
                    }
                    catch
                    {
                        // Delete files
                        GeneratorsOld.Generator.Delete(TaskEnum.Interface);
                        GeneratorsOld.Generator.Delete(TaskEnum.Server);
                        GeneratorsOld.Generator.Delete(TaskEnum.Client);
                        throw;
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

            gen.Settings.Add("targetdir", Helper.CodeGenPath + @"\" + targetdir);
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
