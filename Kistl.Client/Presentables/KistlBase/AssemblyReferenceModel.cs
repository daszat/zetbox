
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// Models an Assembly.
    /// </summary>
    public class AssemblyReferenceModel
        : ObjectReferenceModel
    {
        public AssemblyReferenceModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            if (prop.ReferenceObjectClass != DataContext.GetQuery<ObjectClass>().Where(oc => oc.ClassName == "Assembly"))
            {
                throw new ArgumentOutOfRangeException("prop", "Can only handle Assembly References");
            }
        }

        public void LoadValueFromFile()
        {
            string assemblyFileName = Factory.GetSourceFileNameFromUser("Assembly files|*.dll;*.exe", "All files|*.*");
            var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
            var assemblyDescriptor = DataContext.GetQuery<Assembly>().SingleOrDefault(a => a.AssemblyName == assembly.FullName);
            if (assemblyDescriptor == null)
            {
                assemblyDescriptor = DataContext.Create<Assembly>();
                assemblyDescriptor.AssemblyName = assembly.FullName;
            }

            this.Value = (DataObjectModel)Factory.CreateDefaultModel(DataContext, assemblyDescriptor);
        }

    }
}
