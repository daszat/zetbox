
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;

    /// <summary>
    /// Models an Assembly.
    /// </summary>
    public class AssemblyReferenceModel
        : ObjectReferenceViewModel
    {
        public AssemblyReferenceModel(
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
            string assemblyFileName = ModelFactory.GetSourceFileNameFromUser("Assembly files|*.dll;*.exe", "All files|*.*");
            var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
            var assemblyDescriptor = DataContext.GetQuery<Assembly>().SingleOrDefault(a => a.Name == assembly.FullName);
            if (assemblyDescriptor == null)
            {
                assemblyDescriptor = DataContext.Create<Assembly>();
                assemblyDescriptor.Name = assembly.FullName;
            }

            this.Value = ModelFactory.CreateViewModel<DataObjectViewModel.Factory>(assemblyDescriptor).Invoke(DataContext, assemblyDescriptor);
        }
    }
}
