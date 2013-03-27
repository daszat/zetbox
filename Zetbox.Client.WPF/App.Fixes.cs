// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
// <copyright file="App.Fixes.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

namespace Zetbox.Client.WPF
{
    using System;
    using System.Linq;
    using System.Windows;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;

    /// <content>Contains various and temporary fixes needed to clean the database</content>
    public partial class App
    {
        //private static void PrintEagerLoadingGraphViz()
        //{
        //    using (Logging.Log.DebugTraceMethodCall("PrintEagerLoadingGraphViz"))
        //    {
        //        // Create output suitable for graphviz
        //        Console.WriteLine("---------------------------------------------------------------");
        //        Console.WriteLine();
        //        Console.WriteLine("graph A{");
        //        using (IZetboxContext ctx = GetContext())
        //        {
        //            var relations = ctx.GetQuery<Relation>();

        //            foreach (var rel in relations.Where(r => r.A.Navigator != null && r.A.Navigator.EagerLoading))
        //            {
        //                Console.WriteLine("{0} -> {1};", rel.A.Type.Name, rel.B.Type.Name);
        //            }

        //            relations = ctx.GetQuery<Relation>();
        //            foreach (var rel in relations.Where(r => r.B.Navigator != null && r.B.Navigator.EagerLoading))
        //            {
        //                Console.WriteLine("{0} -> {1};", rel.B.Type.Name, rel.A.Type.Name);
        //            }
        //        }
        //        Console.WriteLine("}");
        //        Console.WriteLine(String.Empty);
        //        Console.WriteLine("---------------------------------------------------------------");
        //    }
        //}

        //private static void CreateTestFragebögen()
        //{
        //    using (Logging.Log.DebugTraceMethodCall("FixupTypeRefParents"))
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        ctx.GetQuery<Zetbox.App.Test.Antwort>().ForEach(a => ctx.Delete(a));
        //        ctx.GetQuery<Zetbox.App.Test.Fragebogen>().ForEach(a => ctx.Delete(a));

        //        var fb1 = ctx.Create<Zetbox.App.Test.Fragebogen>();
        //        fb1.BogenNummer = 1;
        //        fb1.Antworten.Add(ctx.Create<Zetbox.App.Test.Antwort>());
        //        fb1.Antworten[0].Frage = "Erste Frage";
        //        fb1.Antworten[0].FragenNummer = 1;
        //        fb1.Antworten[0].GegebeneAntwort = 2;
        //        fb1.Antworten.Add(ctx.Create<Zetbox.App.Test.Antwort>());
        //        fb1.Antworten[1].Frage = "Zweite Frage";
        //        fb1.Antworten[1].FragenNummer = 2;
        //        fb1.Antworten[1].GegebeneAntwort = 4;

        //        ctx.SubmitChanges();
        //    }
        //}

        //private static void PrintControlKindTypes()
        //{
        //    using (IZetboxContext ctx = GetContext())
        //    {
        //        foreach (var ck in ctx.GetQuery<ControlKind>())
        //        {
        //            Logging.Log.InfoFormat("ID = {0}, Type = {1}", ck.ID, ck.GetType().Name);
        //        }
        //    }
        //}
        /// <summary>
        /// Calls currently needed Database fixes
        /// </summary>
        internal static void FixupDatabase(Func<IZetboxContext> ctxFactory)
        {
            //ImportIcons(ctxFactory());
            //FixIcons(ctxFactory());
            // PrintEagerLoadingGraphViz();
            // CreateTestFragebögen();
            //PrintControlKindTypes();
            //FixupCallImplementInterfaces(ctx);
            //CreateTestClasses(ctxFactory);

            //MigrateTypeRefs(ctxFactory());
        }

        //private static string GetSimpleName(TypeRef tr)
        //{
        //    var type = tr.AsType(false);
        //    if (type == null)
        //        return "ERROR";
        //    else
        //        return TypeExtensions.GetSimpleName(type);
        //}

        //private static void MigrateTypeRefs(IZetboxContext ctx)
        //{
        //    // ViewModelDescriptor
        //    foreach (var vmd in ctx.GetQuery<ViewModelDescriptor>())
        //    {
        //        vmd.ViewModelTypeRef = GetSimpleName(vmd.ViewModelRef);
        //    }

        //    // ViewDescriptor
        //    foreach (var vd in ctx.GetQuery<ViewDescriptor>())
        //    {
        //        vd.ControlTypeRef = GetSimpleName(vd.ControlRef);
        //        vd.SupportedViewModelRefs.Clear();
        //        foreach (var supported in vd.SupportedViewModels)
        //        {
        //            vd.SupportedViewModelRefs.Add(GetSimpleName(supported));
        //        }
        //    }

        //    // CLRObjectParameter
        //    foreach (var clrop in ctx.GetQuery<CLRObjectParameter>())
        //    {
        //        clrop.TypeRef = GetSimpleName(clrop.Type);
        //    }

        //    ctx.SubmitChanges();
        //}

        //private static void CreateTestClasses(Func<IZetboxContext> ctxFactory)
        //{
        //    using (var ctx = ctxFactory())
        //    {
        //        var baseCls = ctx.Create<ObjectClass>();
        //        baseCls.Name = "PropertyTestBase";
        //        baseCls.TableName = "PropertyTests";
        //        baseCls.TableMapping = TableMapping.TPH;
        //        baseCls.Module = ctx.FindPersistenceObject<Zetbox.App.Base.Module>(new Guid("81e8ce31-65eb-46fe-ba86-de7452692d5b")); // TestModule
        //        baseCls.IsSimpleObject = true;
        //        baseCls.IsAbstract = true;

        //        foreach (var data in new[]{
        //                new { Type = typeof(IntProperty), Name="Int",
        //                    DefaultValue = typeof(IntDefaultValue), AssignDefaultValue = new Action<DefaultPropertyValue>( v => ((IntDefaultValue)v).IntValue = 5 ) },
        //                new { Type = typeof(DoubleProperty), Name="Double",
        //                    DefaultValue = typeof(DoubleDefaultValue), AssignDefaultValue  = new Action<DefaultPropertyValue>( v => ((DoubleDefaultValue)v).DoubleValue = 5.5 )  },
        //                new { Type = typeof(StringProperty), Name="String",
        //                    DefaultValue = typeof(StringDefaultValue), AssignDefaultValue  = new Action<DefaultPropertyValue>( v => ((StringDefaultValue)v).DefaultValue = "five point five" ) },
        //                new { Type = typeof(DecimalProperty), Name="Decimal",
        //                    DefaultValue = typeof(DecimalDefaultValue), AssignDefaultValue  = new Action<DefaultPropertyValue>( v => ((DecimalDefaultValue)v).DecimalValue = 5.5m ) },
        //                new { Type = typeof(EnumerationProperty), Name="Enum",
        //                    DefaultValue = typeof(EnumDefaultValue), AssignDefaultValue  = new Action<DefaultPropertyValue>( v => ((EnumDefaultValue)v).EnumValue = ctx.FindPersistenceObject<EnumerationEntry>(new Guid("93bac9f3-c4c6-4ed2-9c93-0f4575200333"))) }, // TestEnum.Third
        //                new { Type = typeof(BoolProperty), Name = "Bool",
        //                    DefaultValue = typeof(BoolDefaultValue), AssignDefaultValue  = new Action<DefaultPropertyValue>( v => ((BoolDefaultValue)v).BoolValue = true) },
        //                new { Type = typeof(DateTimeProperty), Name = "DateTime",
        //                    DefaultValue = typeof(CurrentDateTimeDefaultValue), AssignDefaultValue =  new Action<DefaultPropertyValue>(v => {}) },
        //                new { Type = typeof(GuidProperty), Name = "Guid",
        //                    DefaultValue = typeof(NewGuidDefaultValue), AssignDefaultValue =  new Action<DefaultPropertyValue>(v => {}) },
        //            })
        //        {
        //            var cls = ctx.Create<ObjectClass>();
        //            cls.Name = "Property" + data.Name + "Test";
        //            cls.TableName = "Property" + data.Name + "Tests";
        //            cls.IsSimpleObject = true;

        //            cls.BaseObjectClass = baseCls;
        //            cls.Module = baseCls.Module;

        //            var standard = (Property)ctx.Create(ctx.GetInterfaceType(data.Type));
        //            var nullable = (Property)ctx.Create(ctx.GetInterfaceType(data.Type));
        //            var standardWithDefault = (Property)ctx.Create(ctx.GetInterfaceType(data.Type));
        //            var nullableWithDefault = (Property)ctx.Create(ctx.GetInterfaceType(data.Type));

        //            standard.Name = "Standard";
        //            standard.ObjectClass = cls;
        //            standard.Module = cls.Module;
        //            standard.Constraints.Add(ctx.Create<NotNullableConstraint>());

        //            nullable.Name = "Nullable";
        //            nullable.ObjectClass = cls;
        //            nullable.Module = cls.Module;

        //            standardWithDefault.Name = "StandardWithDefault";
        //            standardWithDefault.ObjectClass = cls;
        //            standardWithDefault.Module = cls.Module;
        //            standardWithDefault.Constraints.Add(ctx.Create<NotNullableConstraint>());
        //            standardWithDefault.DefaultValue = (DefaultPropertyValue)ctx.Create(ctx.GetInterfaceType(data.DefaultValue));
        //            data.AssignDefaultValue(standardWithDefault.DefaultValue);

        //            nullableWithDefault.Name = "NullableWithDefault";
        //            nullableWithDefault.ObjectClass = cls;
        //            nullableWithDefault.Module = cls.Module;
        //            nullableWithDefault.DefaultValue = (DefaultPropertyValue)ctx.Create(ctx.GetInterfaceType(data.DefaultValue));
        //            data.AssignDefaultValue(nullableWithDefault.DefaultValue);

        //            if (data.Type == typeof(EnumerationProperty))
        //            {
        //                ((EnumerationProperty)standard).Enumeration = ctx.FindPersistenceObject<Enumeration>(new Guid("67b48828-e7d2-4432-a942-88e96d74b40a"));
        //                ((EnumerationProperty)nullable).Enumeration = ctx.FindPersistenceObject<Enumeration>(new Guid("67b48828-e7d2-4432-a942-88e96d74b40a"));
        //                ((EnumerationProperty)standardWithDefault).Enumeration = ctx.FindPersistenceObject<Enumeration>(new Guid("67b48828-e7d2-4432-a942-88e96d74b40a"));
        //                ((EnumerationProperty)nullableWithDefault).Enumeration = ctx.FindPersistenceObject<Enumeration>(new Guid("67b48828-e7d2-4432-a942-88e96d74b40a"));
        //            }
        //        }
        //        ctx.SubmitChanges();
        //    }
        //}

        //private static void ImportIcons(IZetboxContext ctx)
        //{
        //    if (ctx == null) throw new ArgumentNullException("ctx");

        //    var zetboxBase = ctx.FindPersistenceObject<Module>(NamedObjects.Module_ZetboxBase);

        //    foreach (var f in System.IO.Directory.GetFiles("C:\\temp\\Icons"))
        //    {
        //        var fi = new System.IO.FileInfo(f);
        //        var icon = ctx.Create<Icon>();
        //        icon.IconFile = System.IO.Path.GetFileName(f);
        //        icon.Module = zetboxBase;
        //        icon.Blob = ctx.Find<Blob>(ctx.CreateBlob(fi, fi.GetMimeType()));
        //    }
        //    ctx.SubmitChanges();
        //}

        //private static string GetIconPath(string name)
        //{
        //    string result = @"P:\Zetbox\DocumentStore\Client"
        //        + @"\GUI.Icons\"
        //        + name;
        //    result = System.IO.Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
        //    return result;
        //}

        //private static void FixIcons(IZetboxContext ctx)
        //{
        //    foreach(var i in ctx.GetQuery<Icon>().Where(i => i.Blob == null))
        //    {
        //        var fi = new System.IO.FileInfo(GetIconPath(i.IconFile));
        //        i.Blob = ctx.Find<Blob>(ctx.CreateBlob(fi, fi.GetMimeType()));
        //    }
        //    ctx.SubmitChanges();
        //}

        //private static void FixupCallImplementInterfaces(Func<IZetboxContext> ctxFactory)
        //{
        //    foreach (var objClass in ctx.GetQuery<ObjectClass>())
        //    {
        //        objClass.ImplementInterfaces();
        //    }
        //    ctx.SubmitChanges();
        //}
    }
}