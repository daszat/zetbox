
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Models an Assembly.
    /// </summary>
    public class AssemblyReferenceViewModel
        : ObjectReferenceViewModel
    {
        public AssemblyReferenceViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IValueModel mdl)
            : base(appCtx, dataCtx,mdl)
        {
            //// TODO: use a static reference here
            //if (prop.GetReferencedObjectClass() != DataContext.GetQuery<ObjectClass>().Where(oc => oc.Name == "Assembly"))
            //{
            //    throw new ArgumentOutOfRangeException("prop", "Can only handle Assembly References");
            //}
        }

        public void LoadValueFromFile()
        {
            string assemblyFileName = ViewModelFactory.GetSourceFileNameFromUser("Assembly files|*.dll;*.exe", "All files|*.*");
            var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
            var assemblyDescriptor = DataContext.GetQuery<Assembly>().SingleOrDefault(a => a.Name == assembly.FullName);
            if (assemblyDescriptor == null)
            {
                assemblyDescriptor = DataContext.Create<Assembly>();
                assemblyDescriptor.Name = assembly.FullName;
            }

            this.Value = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, assemblyDescriptor);
        }
    }
}
