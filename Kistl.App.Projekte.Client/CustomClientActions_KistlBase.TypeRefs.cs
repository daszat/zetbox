using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Extensions;
using System.Diagnostics;
using Kistl.API.Utils;
using Kistl.App.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.App.Base
{
    public static partial class CustomClientActions_KistlBase
    {
        // TODO: Move to common
        private const string transientTypeTypeRefCacheKey = "__TypeTypeRefCache__";
        public static void OnAsType_TypeRef(TypeRef obj, MethodReturnEventArgs<Type> e, bool throwOnError)
        {
            Dictionary<TypeRef, Type> cache;
            if(obj.Context.TransientState.ContainsKey(transientTypeTypeRefCacheKey))
            {
                cache = (Dictionary<TypeRef, Type>)obj.Context.TransientState[transientTypeTypeRefCacheKey];
            }
            else
            {
                cache = new Dictionary<TypeRef,Type>();
                obj.Context.TransientState[transientTypeTypeRefCacheKey] = cache;
            }

            if(cache.ContainsKey(obj))
            {
                e.Result = cache[obj];
                return;
            }

            e.Result = Type.GetType(String.Format("{0}, {1}", obj.FullName, obj.Assembly.Name), false);
            if (e.Result == null)
            {
                // Try ReflectionOnly
                System.Reflection.Assembly a = null;
                try
                {
                    a = System.Reflection.Assembly.ReflectionOnlyLoad(obj.Assembly.Name);
                }
                catch
                {
                    if (throwOnError) throw;
                    cache[obj] = e.Result;
                    return;
                }
                e.Result = a.GetType(obj.FullName, throwOnError);
            }

            if (e.Result == null)
            {
                cache[obj] = e.Result;
                return;
            }
            if (obj.GenericArguments.Count > 0)
            {
                var args = obj.GenericArguments.Select(tRef => tRef.AsType(throwOnError)).ToArray();
                if (args.Contains(null))
                {
                    e.Result = null;
                    if (throwOnError)
                    {
                        throw new InvalidOperationException("Cannot create Type: missing generic argument");
                    }
                    else
                    {
                        cache[obj] = e.Result;
                        return;
                    }
                }
                e.Result = e.Result.MakeGenericType(args);
            }
            cache[obj] = e.Result;
        }

        public static void OnRegenerateTypeRefs_Assembly(Assembly assembly)
        {
            using (Logging.Log.InfoTraceMethodCall(assembly.Name))
            {
                var ctx = assembly.Context;

                // pre-load context
                var oldTypes = ctx.GetQuery<TypeRef>()
                    .WithEagerLoading()
                    .Where(tr => tr.Assembly.ID == assembly.ID)
                    .ToLookup(tr => tr.FullName);

                try
                {
                    var newTypes = LoadAndCreateTypes(assembly, ctx);
                    MarkOldTypesAsDeleted(ctx, oldTypes, newTypes);
                    UpdateTypeParents(newTypes);
                    CreateViewModelDescriptors(ctx, newTypes);
                    CreateViewDescriptors(ctx, newTypes);
                }
                catch (FileNotFoundException ex)
                {
                    Logging.Log.Warn("Failed to RegenerateTypeRefs", ex);
                }
            }
        }

        private static void CreateViewModelDescriptors(IKistlContext ctx, Dictionary<int, TypeRef> newTypes)
        {
            using (Logging.Log.InfoTraceMethodCallFormat("Creating ViewModelDescriptors"))
            {
                foreach (var tr in newTypes.Values)
                {
                    var type = tr.AsType(false);
                    // Sorry, no support for that yet
                    // http://blogs.msdn.com/b/kaevans/archive/2005/10/24/484186.aspx
                    if (type.Assembly.ReflectionOnly) continue; 
                    if (type != null)
                    {
                        var attr = type.GetCustomAttributes(typeof(ViewModelDescriptorAttribute), false).FirstOrDefault() as ViewModelDescriptorAttribute;
                        if (attr != null)
                        {
                            var descr = ctx.GetQuery<ViewModelDescriptor>().FirstOrDefault(i => i.ViewModelRef == tr);
                            if (descr == null)
                            {
                                descr = ctx.Create<ViewModelDescriptor>();
                                descr.ViewModelRef = tr;
                                descr.Module = ctx.GetQuery<Module>().First(i => i.Name == attr.Module);
                                descr.Description = string.IsNullOrEmpty(attr.Description) ? "TODO: Add description" : attr.Description;
                                if (!string.IsNullOrEmpty(attr.DefaultKind))
                                {
                                    descr.DefaultKind = ctx.GetQuery<ControlKind>().FirstOrDefault(c => c.Name == attr.DefaultKind);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CreateViewDescriptors(IKistlContext ctx, Dictionary<int, TypeRef> newTypes)
        {
            using (Logging.Log.InfoTraceMethodCallFormat("Creating ViewDescriptors"))
            {
                foreach (var tr in newTypes.Values)
                {
                    var type = tr.AsType(false);
                    // Sorry, no support for that yet
                    // http://blogs.msdn.com/b/kaevans/archive/2005/10/24/484186.aspx
                    if (type.Assembly.ReflectionOnly) continue;
                    if (type != null)
                    {
                        var attr = type.GetCustomAttributes(typeof(ViewDescriptorAttribute), false).FirstOrDefault() as ViewDescriptorAttribute;
                        if (attr != null)
                        {
                            var descr = ctx.GetQuery<ViewDescriptor>().FirstOrDefault(i => i.ControlRef == tr);
                            if (descr == null)
                            {
                                descr = ctx.Create<ViewDescriptor>();
                                descr.ControlRef = tr;
                                descr.Module = ctx.GetQuery<Module>().First(i => i.Name == attr.Module);
                                descr.Toolkit = attr.Toolkit;
                                if (!string.IsNullOrEmpty(attr.Kind))
                                {
                                    descr.ControlKind = ctx.GetQuery<ControlKind>().FirstOrDefault(c => c.Name == attr.Kind);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void UpdateTypeParents(Dictionary<int, TypeRef> newTypes)
        {
            using (Logging.Log.InfoTraceMethodCallFormat("Updating parents"))
            {
                // update parent infos
                foreach (var tr in newTypes.Values)
                {
                    tr.UpdateParent();
                }
            }
        }

        private static void MarkOldTypesAsDeleted(IKistlContext ctx, ILookup<string, TypeRef> oldTypes, Dictionary<int, TypeRef> newTypes)
        {
            using (Logging.Log.InfoTraceMethodCallFormat("Updating refs"))
            {
                // Delete unused Refs
                foreach (var tr in oldTypes.SelectMany(g => g))
                {
                    var type = tr.AsType(false);
                    if (type == null)
                    {
                        Logging.Log.Warn("Should delete " + tr.FullName);
                        tr.Deleted = true;
                    }
                    else if (!type.IsGenericType)
                    {
                        if (!newTypes.ContainsKey(tr.ID))
                        {
                            Logging.Log.Warn("Should delete " + tr.FullName);
                            tr.Deleted = true;
                        }
                    }
                    else
                    {
                        tr.Deleted = null;
                    }
                }
            }
        }

        private static Dictionary<int, TypeRef> LoadAndCreateTypes(Assembly assembly, IKistlContext ctx)
        {
            using (Logging.Log.InfoTraceMethodCall("Loading new types"))
            {
                var newTypes = System.Reflection.Assembly
                    .ReflectionOnlyLoad(assembly.Name)
                    .GetExportedTypes()
                    .Where(t => !t.IsGenericTypeDefinition)
                    .Select(t => t.ToRef(ctx))
                    .ToDictionary(tr => tr.ID);
                return newTypes;
            }
        }

        public static void OnUpdateParent_TypeRef(TypeRef obj)
        {
            var baseType = obj.AsType(true).BaseType;
            if (baseType == null)
            {
                obj.Parent = null;
            }
            else
            {
                obj.Parent = baseType.ToRef(obj.Context);
            }
        }
    }
}
