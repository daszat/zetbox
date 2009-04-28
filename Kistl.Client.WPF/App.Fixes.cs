// <copyright file="App.Fixes.cs" company="dasz.at OG">
//     Copyright (C) 2009 dasz.at OG. All rights reserved.
// </copyright>

namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables.TimeRecords;
    using Kistl.Client.WPF.View.TimeRecords;

    /// <content>Contains various and temporary fixes needed to clean the database</content>
    public partial class App
    {
        /// <summary>
        /// Creates and deletes NotNullableConstraints according to the Property.IsNullable flag.
        /// </summary>
        private static void FixNotNullableConstraints()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Fixing NotNullableConstraints"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    // Apply a NotNullableConstraint as appropriate
                    foreach (var prop in ctx.GetQuery<Property>())
                    {
                        var currentNotNullableConstraint = prop.Constraints.OfType<NotNullableConstraint>().SingleOrDefault();
                        bool hasNullableConstraint = currentNotNullableConstraint != null;
                        if (prop.IsNullable && hasNullableConstraint)
                        {
                            prop.Constraints.Remove(currentNotNullableConstraint);
                            ctx.Delete(currentNotNullableConstraint);
                            System.Console.Out.WriteLine("Removed obsolete NotNullableConstraint");
                        }
                        else if (!prop.IsNullable && !hasNullableConstraint)
                        {
                            prop.Constraints.Add(ctx.Create<NotNullableConstraint>());
                            System.Console.Out.WriteLine("Added missing NotNullableConstraint");
                        }
                    }

                    // synchronize Stringproperty's Length
                    foreach (var prop in ctx.GetQuery<StringProperty>())
                    {
                        var currentStringRangeConstraint = prop.Constraints.OfType<StringRangeConstraint>().SingleOrDefault();

                        if (currentStringRangeConstraint == null)
                        {
                            currentStringRangeConstraint = ctx.Create<StringRangeConstraint>();
                            currentStringRangeConstraint.MinLength = 0;
                            prop.Constraints.Add(currentStringRangeConstraint);
                        }

                        currentStringRangeConstraint.MaxLength = prop.Length;
                    }

                    ctx.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Fix broken TypeRefs.
        /// </summary>
        private static void FixupTypeRefParents()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("FixupTypeRefParents"))
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

        /// <summary>
        /// Create various descriptors for work in progress.
        /// </summary>
        private static void CreateVariousDescriptors()
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Creating various descriptors"))
            {
                using (IKistlContext ctx = KistlContext.GetContext())
                {
                    // var lefmDesc = ctx.Create<PresentableModelDescriptor>();
                    // lefmDesc.DefaultVisualType = VisualType.WorkspaceWindow;
                    // lefmDesc.Description = "A workspace for Leistungserfassung/work effort recording";
                    // lefmDesc.PresentableModelRef = typeof(WorkEffortRecorderModel).ToRef(ctx);
                    // 
                    // var lemDesc = ctx.Create<PresentableModelDescriptor>();
                    // lemDesc.DefaultVisualType = VisualType.Object;
                    // lemDesc.Description = "A model for WorkEfforts";
                    // lemDesc.PresentableModelRef = typeof(WorkEffortModel).ToRef(ctx);
                    // 
                    // var lev = ctx.Create<ViewDescriptor>();
                    // lev.ControlRef = typeof(WorkEffortView).ToRef(ctx);
                    // lev.Toolkit = Toolkit.WPF;
                    // lev.VisualType = VisualType.Object;
                    // lev.PresentedModelDescriptor = ctx.GetQuery<PresentableModelDescriptor>()
                    //    .Single(o => o.PresentableModelRef.ID == typeof(WorkEffortModel).ToRef(ctx).ID);
                    // 
                    // var lv = ctx.Create<ViewDescriptor>();
                    // lv.ControlRef = typeof(WorkEffortRecorderView).ToRef(ctx);
                    // lv.Toolkit = Toolkit.WPF;
                    // lv.VisualType = VisualType.WorkspaceWindow;
                    // lv.PresentedModelDescriptor = ctx.GetQuery<PresentableModelDescriptor>()
                    //    .Single(o => o.PresentableModelRef.ID == typeof(WorkEffortRecorderModel).ToRef(ctx).ID);
                    ctx.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Calls currently needed Database fixes
        /// </summary>
        private void FixupDatabase()
        {
            FixNotNullableConstraints();
            CreateVariousDescriptors();
            FixupTypeRefParents();
        }
    }
}