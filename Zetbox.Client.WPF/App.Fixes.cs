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

    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using System.Windows;
    using Zetbox.App.GUI;

    /// <content>Contains various and temporary fixes needed to clean the database</content>
    public partial class App
    {
        ///// <summary>
        ///// Fix broken TypeRefs.
        ///// </summary>
        //private static void FixupTypeRefParents(IZetboxContext ctx)
        //{
        //    if (ctx == null) throw new ArgumentNullException("ctx");
        //    using (Logging.Log.DebugTraceMethodCall("FixupTypeRefParents"))
        //    {
        //        var typeRefs = ctx.GetQuery<TypeRef>();
        //        foreach (var tr in typeRefs)
        //        {
        //            if (tr.Parent != null)
        //            {
        //                continue;
        //            }

        //            UpdateParent(ctx, tr);
        //            ctx.SubmitChanges();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Creates the parent chain for a given TypeRef.
        ///// </summary>
        ///// <param name="ctx">the context to use</param>
        ///// <param name="tr">the <see cref="TypeRef"/> to fix</param>
        //private static void UpdateParent(IZetboxContext ctx, TypeRef tr)
        //{
        //    var type = tr.AsType(false);
        //    if (type != null
        //        && type != typeof(object)
        //        && !type.IsGenericTypeDefinition
        //        && type.BaseType != null)
        //    {
        //        tr.Parent = type.BaseType.ToRef(ctx);
        //        UpdateParent(ctx, tr.Parent);
        //    }
        //}

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
            //FixupTypeRefParents(ctxFactory());
            // PrintEagerLoadingGraphViz();
            // CreateTestFragebögen();
            //PrintControlKindTypes();
            //FixupCallImplementInterfaces(ctx);
            //RegenerateTypeRefs(ctxFactory());
        }

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

        //private static void RegenerateTypeRefs(IZetboxContext ctx)
        //{
        //    foreach (var a in ctx.GetQuery<Assembly>())
        //    {
        //        a.RegenerateTypeRefs();
        //        ctx.SubmitChanges();
        //    }
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