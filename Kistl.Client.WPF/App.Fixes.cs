// <copyright file="App.Fixes.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

namespace Kistl.Client.WPF
{
    using System;
    using System.Linq;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using System.Windows;
    using Kistl.App.GUI;

    /// <content>Contains various and temporary fixes needed to clean the database</content>
    public partial class App
    {
        /// <summary>
        /// Fix broken TypeRefs.
        /// </summary>
        private static void FixupTypeRefParents()
        {
            using (Logging.Log.DebugTraceMethodCall("FixupTypeRefParents"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var typeRefs = ctx.GetQuery<TypeRef>();
                    foreach (var tr in typeRefs)
                    {
                        if (tr.Parent != null)
                        {
                            continue;
                        }

                        UpdateParent(ctx, tr);
                        ctx.SubmitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Creates the parent chain for a given TypeRef.
        /// </summary>
        /// <param name="ctx">the context to use</param>
        /// <param name="tr">the <see cref="TypeRef"/> to fix</param>
        private static void UpdateParent(IKistlContext ctx, TypeRef tr)
        {
            var type = tr.AsType(false);
            if (type != null
                && type != typeof(object)
                && !type.IsGenericTypeDefinition
                && type.BaseType != null)
            {
                tr.Parent = type.BaseType.ToRef(ctx);
                UpdateParent(ctx, tr.Parent);
            }
        }

        private static void MoveClrObjectParameterFullTypeName()
        {
            using (Logging.Log.DebugTraceMethodCall("MoveClrObjectParameterFullTypeName"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var clrObjectParameters = ctx.GetQuery<CLRObjectParameter>();
                    foreach (var param in clrObjectParameters)
                    {
                        if (param.Type == null)
                        {
                            param.Type = param.GuessParameterType().ToRef(ctx);
                        }
                    }
                    ctx.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Prints a list of unreferenced WPF Controls (<see cref="UIElement"/>s).
        /// Those are potentially dead code and should be audited.
        /// </summary>
        private static void CheckUnusedWpfControls()
        {
            using (Logging.Log.DebugTraceMethodCall("CheckUnusedWpfControls"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var uiElementType = typeof(UIElement);
                    var assemblyName = typeof(App).Assembly.FullName;

                    var referencedControlKinds = ctx.GetQuery<PresentableModelDescriptor>()
                        .ToList()
                        .SelectMany(pmd => new[] { pmd.DefaultKind, pmd.DefaultGridCellKind }.Concat(pmd.SecondaryControlKinds))
                        .Where(ck => ck != null)
                        .ToLookup(ck => ck.GetType());

                    var referencedTypes = ctx.GetQuery<ViewDescriptor>()
                        .Where(vd => vd.Toolkit == Toolkit.WPF)
                        .Select(vd => vd.ControlRef)
                        .ToList()
                        .Select(tr => tr.AsType(false))
                        .Where(t => t != null)
                        .ToLookup(t => t);

                    var unreferencedTypes = typeof(App).Assembly
                        .GetTypes()
                        .Where(t => uiElementType.IsAssignableFrom(t) && !referencedTypes.Contains(t));

                    foreach (var t in unreferencedTypes.OrderBy(t => t.FullName))
                    {
                        Logging.Log.Warn(String.Format("Found UIElement {0}, which is not referenced from the DataStore", t));
                    }
                }
            }
        }

        private static void PrintEagerLoadingGraphViz()
        {
            using (Logging.Log.DebugTraceMethodCall("PrintEagerLoadingGraphViz"))
            {
                // Create output suitable for graphviz
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("graph A{");
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    var relations = ctx.GetQuery<Relation>();

                    foreach (var rel in relations.Where(r => r.A.Navigator != null && r.A.Navigator.EagerLoading))
                    {
                        Console.WriteLine("{0} -> {1};", rel.A.Type.ClassName, rel.B.Type.ClassName);
                    }

                    relations = ctx.GetQuery<Relation>();
                    foreach (var rel in relations.Where(r => r.B.Navigator != null && r.B.Navigator.EagerLoading))
                    {
                        Console.WriteLine("{0} -> {1};", rel.B.Type.ClassName, rel.A.Type.ClassName);
                    }
                }
                Console.WriteLine("}");
                Console.WriteLine("");
                Console.WriteLine("---------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Calls currently needed Database fixes
        /// </summary>
        internal static void FixupDatabase()
        {
            //MoveClrObjectParameterFullTypeName();
            //FixupTypeRefParents();
            //CheckUnusedWpfControls();
            //PrintEagerLoadingGraphViz();
        }
    }
}