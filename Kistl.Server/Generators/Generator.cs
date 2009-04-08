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
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld;

namespace Kistl.Server.Generators
{
    public static class Generator
    {
        public static void GenerateCode()
        {
            //MoveNewRelationToDb();
            //TranslateRelations();
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Generating Code");
                using (IKistlContext ctx = KistlContext.GetContext())
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

        private static void TranslateRelations()
        {
            //using (TraceClient.TraceHelper.TraceMethodCall())
            //{
            //    using (IKistlContext ctx = KistlContext.GetContext())
            //    {
            //        foreach (Kistl.Server.Movables.NewRelation r in Kistl.Server.Movables.NewRelation.GetAll(ctx))
            //        {
            //            var rel = ctx.Create<Relation>();
            //            rel.Storage = Movables.StorageHintExtensions.ToStorageType(r.GetPreferredStorage());

            //            rel.A = ctx.Create<RelationEnd>();
            //            rel.A.Multiplicity = r.A.Multiplicity;
            //            rel.A.Navigator = ctx.Find<ObjectReferenceProperty>(r.A.Navigator.ID);
            //            rel.A.Role = (int)RelationEndRole.A;
            //            rel.A.RoleName = r.A.RoleName;
            //            rel.A.Type = r.A.Type.ToObjectClass(ctx);

            //            rel.B = ctx.Create<RelationEnd>();
            //            rel.B.Multiplicity = r.B.Multiplicity;
            //            if (r.B.Navigator != null)
            //                rel.B.Navigator = ctx.Find<ObjectReferenceProperty>(r.B.Navigator.ID);
            //            rel.B.Role = (int)RelationEndRole.B;
            //            rel.B.RoleName = r.B.RoleName;
            //            rel.B.Type = r.B.Type.ToObjectClass(ctx);

            //            ctx.SubmitChanges();
            //        }
            //    }
            //}
        }

        private static void MoveNewRelationToDb()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                Trace.TraceInformation("Generating Code");
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var multiplicityEnum = CreateMultiplicityEnum(ctx);
                    var relEndOC = CreateRelationEndClass(ctx, multiplicityEnum);
                    var relationOC = CreateRelationClass(ctx, relEndOC);
                    ctx.SubmitChanges();
                }
            }
        }

        private static ObjectClass CreateRelationClass(IKistlContext ctx, ObjectClass relEndOC)
        {
            var baseModule = ctx.Find<Module>(1);
            var objectClassOC = ctx.Find<ObjectClass>(2);
            var propertyOC = ctx.Find<ObjectClass>(7);

            var relationOC = ctx.Find<ObjectClass>(77);
            {
                var aProp = ctx.Create<ObjectReferenceProperty>();
                aProp.AltText = "The A-side of this Relation.";
                aProp.Description = aProp.AltText;
                aProp.IsList = false;
                aProp.IsNullable = true; // false;
                aProp.Module = baseModule;
                aProp.PropertyName = "A";
                aProp.ReferenceObjectClass = relEndOC;

                relationOC.Properties.Add(aProp);
            }
            {
                var bProp = ctx.Create<ObjectReferenceProperty>();
                bProp.AltText = "The B-side of this Relation.";
                bProp.Description = bProp.AltText;
                bProp.IsList = false;
                bProp.IsNullable = true; // false;
                bProp.Module = baseModule;
                bProp.PropertyName = "B";
                bProp.ReferenceObjectClass = relEndOC;

                relationOC.Properties.Add(bProp);
            }
            return relationOC;
        }

        private static Enumeration CreateMultiplicityEnum(IKistlContext ctx)
        {
            var baseModule = ctx.Find<Module>(1);
            var objectClassOC = ctx.Find<ObjectClass>(2);
            var propertyOC = ctx.Find<ObjectClass>(7);

            var multiplicityEnum = ctx.Create<Enumeration>();
            multiplicityEnum.ClassName = "Multiplicity";
            multiplicityEnum.Description = "Describes the multiplicities of objects on RelationEnds";
            multiplicityEnum.Module = baseModule;

            {
                var zeroOrOneEnumEntry = ctx.Create<EnumerationEntry>();
                zeroOrOneEnumEntry.Description = "Optional Element (zero or one)";
                zeroOrOneEnumEntry.Enumeration = multiplicityEnum;
                zeroOrOneEnumEntry.Name = "ZeroOrOne";
                zeroOrOneEnumEntry.Value = 1;
            }
            {
                var oneEnumEntry = ctx.Create<EnumerationEntry>();
                oneEnumEntry.Description = "Required Element (exactly one)";
                oneEnumEntry.Enumeration = multiplicityEnum;
                oneEnumEntry.Name = "One";
                oneEnumEntry.Value = 2;
            }
            {
                var zeroOrMoreEnumEntry = ctx.Create<EnumerationEntry>();
                zeroOrMoreEnumEntry.Description = "Optional List Element (zero or more)";
                zeroOrMoreEnumEntry.Enumeration = multiplicityEnum;
                zeroOrMoreEnumEntry.Name = "ZeroOrMore";
                zeroOrMoreEnumEntry.Value = 3;
            }

            return multiplicityEnum;
        }

        private static ObjectClass CreateRelationEndClass(IKistlContext ctx, Enumeration multiplicityEnum)
        {
            var baseModule = ctx.Find<Module>(1);
            var objectClassOC = ctx.Find<ObjectClass>(2);
            var propertyOC = ctx.Find<ObjectClass>(7);

            var relEndOC = ctx.Create<ObjectClass>();
            relEndOC.ClassName = "RelationEnd";
            relEndOC.Description = "Describes one end of a relation between two object classes";
            relEndOC.Module = baseModule;
            relEndOC.TableName = "RelationEnds";
            {
                var relEndRoleProp = ctx.Create<IntProperty>();
                relEndRoleProp.AltText = "Which RelationEndRole this End has";
                {
                    var relEndRolePropConstraint = ctx.Create<IntegerRangeConstraint>();
                    relEndRolePropConstraint.Min = 1;
                    relEndRolePropConstraint.Max = 2;
                    relEndRolePropConstraint.Reason = "RelationEndRole can only be 1 ('A') or 2 ('B')";
                    relEndRoleProp.Constraints.Add(relEndRolePropConstraint);
                }
                relEndRoleProp.Description = relEndRoleProp.AltText;
                relEndRoleProp.IsList = false;
                relEndRoleProp.IsNullable = false;
                relEndRoleProp.Module = baseModule;
                relEndRoleProp.PropertyName = "Role";

                relEndOC.Properties.Add(relEndRoleProp);
            }
            {
                var navigatorProp = ctx.Create<ObjectReferenceProperty>();
                navigatorProp.AltText = "The ORP to navigate FROM this end of the relation. MAY be null.";
                navigatorProp.Description = navigatorProp.AltText;
                navigatorProp.IsList = false;
                navigatorProp.IsNullable = true;
                navigatorProp.Module = baseModule;
                navigatorProp.PropertyName = "Navigator";
                navigatorProp.ReferenceObjectClass = propertyOC;

                relEndOC.Properties.Add(navigatorProp);
            }
            {
                var typeProp = ctx.Create<ObjectReferenceProperty>();
                typeProp.AltText = "Specifies which type this End of the relation has. MUST NOT be null.";
                typeProp.Description = typeProp.AltText;
                typeProp.IsList = false;
                typeProp.IsNullable = false;
                typeProp.Module = baseModule;
                typeProp.PropertyName = "Type";
                typeProp.ReferenceObjectClass = objectClassOC;

                relEndOC.Properties.Add(typeProp);
            }
            {
                var multiplicityProp = ctx.Create<EnumerationProperty>();
                multiplicityProp.AltText = "Specifies how many instances may occur on this end of the relation.";
                multiplicityProp.Description = multiplicityProp.AltText;
                multiplicityProp.Enumeration = multiplicityEnum;
                multiplicityProp.IsList = false;
                multiplicityProp.IsNullable = false;
                multiplicityProp.Module = baseModule;
                multiplicityProp.PropertyName = "Multiplicity";

                relEndOC.Properties.Add(multiplicityProp);
            }
            {
                var roleNameProp = ctx.Create<StringProperty>();
                roleNameProp.AltText = "This end's role name in the relation";
                roleNameProp.Description = roleNameProp.AltText;
                roleNameProp.IsList = false;
                roleNameProp.IsNullable = false;
                roleNameProp.Module = baseModule;
                roleNameProp.PropertyName = "RoleName";
                roleNameProp.Length = 200;

                relEndOC.Properties.Add(roleNameProp);
            }

            return relEndOC;
        }

        public static void GenerateDatabase()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                var dbGenerator = DatabaseGeneratorFactory.GetGenerator();

                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    dbGenerator.Generate(ctx);
                }
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
