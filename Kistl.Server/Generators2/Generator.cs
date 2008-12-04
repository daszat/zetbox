using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Kistl.API;
using Kistl.Server.Generators;
using Kistl.App.Base;
using Kistl.API.Server;
using Arebis.CodeGeneration;

namespace Kistl.Server.Generators2
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
                    // gMapping.Generate(ctx, Helper.CodeGenPath);

                    try
                    {
                        // Compile Code
                        Trace.TraceInformation("Compiling Interfaces");
                        Generators.Generator.Compile(TaskEnum.Interface);
                        Trace.TraceInformation("Compiling Server Assembly");
                        Generators.Generator.Compile(TaskEnum.Server);
                        Trace.TraceInformation("Compiling Client Assembly");
                        Generators.Generator.Compile(TaskEnum.Client);
                    }
                    catch
                    {
                        // Delete files
                        Generators.Generator.Delete(TaskEnum.Interface);
                        Generators.Generator.Delete(TaskEnum.Server);
                        Generators.Generator.Delete(TaskEnum.Client);
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

        internal static Arebis.CodeGenerator.TemplateGenerator GetTemplateGenerator(
            string template, string output, string targetdir, params object[] templateParameter)
        {
            Arebis.CodeGenerator.TemplateGenerator gen = new Arebis.CodeGenerator.TemplateGenerator();
            gen.Settings.Add("referencepath", Helper.CodeGenPath + @"\bin");
            ServerApiContext.Current.Configuration.SourceFileLocation.ForEach(l => gen.Settings.Add("referencepath", l));
            gen.Settings.Add("referencepath", System.IO.Path.GetDirectoryName(typeof(Generator).Assembly.Location));

            gen.Settings.Add("referenceassembly", "System.dll");
            gen.Settings.Add("referenceassembly", "System.Core.dll");
            gen.Settings.Add("referenceassembly", "System.Data.dll");
            gen.Settings.Add("referenceassembly", "System.Data.Linq.dll");
            gen.Settings.Add("referenceassembly", "WindowsBase.dll");
            gen.Settings.Add("referenceassembly", "Kistl.API.dll");
            gen.Settings.Add("referenceassembly", "Kistl.API.Server.dll");
            gen.Settings.Add("referenceassembly", "Kistl.Objects.dll");
            gen.Settings.Add("referenceassembly", "Kistl.Server.exe");
            gen.Settings.Add("template", "res://kistl.server/Kistl.Server.Generators2.Templates." + template);
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
    }
}
