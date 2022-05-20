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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using SR = System.Reflection;
    using System.Reflection.Metadata;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class AssemblyActions
    {
        private static IViewModelFactory _mdlFactory;

        public AssemblyActions(IViewModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;
        }

        [Invocation]
        public static System.Threading.Tasks.Task RegenerateTypeRefs(Assembly assembly, MethodReturnEventArgs<System.Boolean> e)
        {
            using (Logging.Log.InfoTraceMethodCall(assembly.Name))
            {
                var newScope = _mdlFactory.CreateNewScope();
                var ctx = newScope.ViewModelFactory.CreateNewContext();

                try
                {
                    SR.Assembly srAssembly = ReflectAssembly(assembly);

                    CreateViewModelDescriptors(ctx, srAssembly);
                    CreateViewDescriptors(ctx, srAssembly);

                    var newDescriptors = new List<IDataObject>();
                    newDescriptors.AddRange(ctx.AttachedObjects.OfType<ViewModelDescriptor>().Where(d => d.ObjectState == DataObjectState.New).Cast<IDataObject>().ToList());
                    newDescriptors.AddRange(ctx.AttachedObjects.OfType<ViewDescriptor>().Where(d => d.ObjectState == DataObjectState.New).Cast<IDataObject>().ToList());

                    if (newDescriptors.Count > 0)
                    {
                        var workSpace = Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, ctx);
                        foreach (IDataObject i in newDescriptors)
                        {
                            workSpace.AddItem(DataObjectViewModel.Fetch(newScope.ViewModelFactory, ctx, workSpace, i));
                        }

                        newScope.ViewModelFactory.ShowModel(workSpace, true);
                        e.Result = true;
                    }
                    else
                    {
                        newScope.ViewModelFactory.ShowMessage("Regenerating TypeRefs finished successfully, no new Descriptors or Assemblies found", "Success");
                        newScope.Dispose();
                        e.Result = false;
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Logging.Log.Warn("Failed to RegenerateTypeRefs", ex);
                    e.Result = false;
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }
        }

        private static void CreateViewModelDescriptors(IZetboxContext ctx, SR.Assembly srAssembly)
        {
            using (Logging.Log.InfoTraceMethodCallFormat("CreateViewModelDescriptors", "Creating ViewModelDescriptors"))
            {
                var liveDescriptors = new HashSet<ViewModelDescriptor>();

                foreach (var type in GetTypes(srAssembly))
                {
                    object attr;
                    // http://blogs.msdn.com/b/kaevans/archive/2005/10/24/484186.aspx
                    if (type.Assembly.ReflectionOnly)
                    {
                        attr = SR.CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(i => i.Constructor.DeclaringType.FullName == typeof(ViewModelDescriptorAttribute).FullName);
                    }
                    else
                    {
                        attr = type.GetCustomAttributes(typeof(ViewModelDescriptorAttribute), false).FirstOrDefault() as ViewModelDescriptorAttribute;
                    }
                    if (attr != null)
                    {
                        var typeName = type.GetSimpleName();
                        var descr = ctx.GetQuery<ViewModelDescriptor>().FirstOrDefault(i => i.ViewModelTypeRef == typeName);
                        if (descr == null)
                        {
                            descr = ctx.Create<ViewModelDescriptor>();
                            descr.ViewModelTypeRef = typeName;
                            descr.Description = "TODO: Add description";
                        }
                        else
                        {
                            descr.Deleted = false;
                        }

                        liveDescriptors.Add(descr);
                    }
                }

                var simpleAssemblyName = srAssembly.GetSimpleName();
                var deadDescriptors = ctx
                    .GetQuery<ViewModelDescriptor>()
                    .ToList()
                    .Where(vmd => TypeSpec.Parse(vmd.ViewModelTypeRef).AssemblyName.IfNullOrWhiteSpace(string.Empty).Split(',').FirstOrDefault().IfNullOrWhiteSpace(string.Empty).Trim() == simpleAssemblyName)
                    .Except(liveDescriptors);

                foreach (var d in deadDescriptors)
                {
                    d.Deleted = true;
                }
            }
        }

        private static void CreateViewDescriptors(IZetboxContext ctx, SR.Assembly srAssembly)
        {
            using (Logging.Log.InfoTraceMethodCallFormat("CreateViewDescriptors", "Creating ViewDescriptors"))
            {
                var liveDescriptors = new HashSet<ViewDescriptor>();

                foreach (var type in GetTypes(srAssembly))
                {
                    object attr;
                    Toolkit? tk = null;
                    // http://blogs.msdn.com/b/kaevans/archive/2005/10/24/484186.aspx
                    if (type.Assembly.ReflectionOnly)
                    {
                        attr = SR.CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(i => i.Constructor.DeclaringType.FullName == typeof(ViewDescriptorAttribute).FullName);
                        if (attr != null) tk = (Toolkit)((SR.CustomAttributeData)attr).ConstructorArguments.Single().Value;
                    }
                    else
                    {
                        attr = type.GetCustomAttributes(typeof(ViewDescriptorAttribute), false).FirstOrDefault() as ViewDescriptorAttribute;
                        if (attr != null) tk = ((ViewDescriptorAttribute)attr).Toolkit;
                    }
                    if (attr != null)
                    {
                        var typeName = type.GetSimpleName();

                        // if a view can be used under multiple ControlKinds, it needs to have multiple ViewDescriptors
                        var descrList = ctx.GetQuery<ViewDescriptor>().Where(i => i.ControlTypeRef == typeName).ToList();
                        if (descrList.Count == 0)
                        {
                            var descr = ctx.Create<ViewDescriptor>();
                            descr.ControlTypeRef = typeName;
                            if (tk != null) descr.Toolkit = tk.Value;
                            liveDescriptors.Add(descr);
                        }
                        else
                        {
                            foreach (var descr in descrList)
                            {
                                descr.Deleted = false;
                                liveDescriptors.Add(descr);
                            }
                        }
                    }
                }

                var simpleAssemblyName = srAssembly.GetSimpleName();
                var deadDescriptors = ctx
                    .GetQuery<ViewDescriptor>()
                    .ToList()
                    .Where(vmd => TypeSpec.Parse(vmd.ControlTypeRef).AssemblyName.IfNullOrWhiteSpace(string.Empty).Split(',').FirstOrDefault().IfNullOrWhiteSpace(string.Empty).Trim() == simpleAssemblyName)
                    .Except(liveDescriptors);
                foreach (var d in deadDescriptors)
                {
                    d.Deleted = true;
                }
            }
        }

        /// <summary>
        /// Loads the referenced Assembly into the ReflectionOnly Context. May ask the user to locate the file.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static SR.Assembly ReflectAssembly(Assembly assembly)
        {
            string[] runtimeAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");
            string[] baseAssemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            string[] commonAssemblies = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common"), "*.dll");
            string[] clientAssemblies = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Client"), "*.dll");
            var paths = new List<string>(runtimeAssemblies.Concat(baseAssemblies).Concat(commonAssemblies).Concat(clientAssemblies));

            SR.Assembly a = null;
            try
            {
                var resolver = new PathAssemblyResolver(paths);
                var mlc = new MetadataLoadContext(resolver);
                a = mlc.LoadFromAssemblyName(assembly.Name);
            }
            catch (FileNotFoundException)
            {
            }
            if (a == null)
            {
                // Ask user
                var f = _mdlFactory.GetSourceFileNameFromUser("Assembly files|*.dll;*.exe", "All files|*.*");
                if (!string.IsNullOrEmpty(f))
                {
                    paths.Add(Path.GetDirectoryName(f));
                    var resolver = new PathAssemblyResolver(paths);
                    var mlc = new MetadataLoadContext(resolver);
                    a = mlc.LoadFromAssemblyPath(f);
                }
            }
            if (a == null)
            {
                // Give it up
                throw new InvalidOperationException("Unable to load assembly: " + assembly.Name);
            }
            return a;
        }

        private static Type[] GetTypes(SR.Assembly assembly)
        {
            IEnumerable<Type> types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types;
            }
            return types.Where(t => t != null).ToArray();
        }
    }
}
