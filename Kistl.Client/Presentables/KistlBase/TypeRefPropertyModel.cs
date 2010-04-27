
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public class TypeRefPropertyModel
           : ObjectReferenceModel
    {
        public TypeRefPropertyModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            // TODO: use a static reference here
            if (prop.GetReferencedObjectClass().Name != "TypeRef")
            {
                throw new ArgumentOutOfRangeException("prop", "Can only handle TypeRef References");
            }
        }

        private LoadTypeRefFromAssemblyFileCommand _loadTypeRefFromAssemblyFileCommand = null;
        public ICommand LoadTypeRefFromAssemblyFileCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyFileCommand == null)
                {
                    _loadTypeRefFromAssemblyFileCommand = ModelFactory.CreateViewModel<LoadTypeRefFromAssemblyFileCommand.Factory>().Invoke(DataContext, this);
                }
                return _loadTypeRefFromAssemblyFileCommand;
            }
        }

        private LoadTypeRefFromAssemblyRefCommand _loadTypeRefFromAssemblyRefCommand = null;
        public ICommand LoadTypeRefFromAssemblyRefCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyRefCommand == null)
                {
                    _loadTypeRefFromAssemblyRefCommand = ModelFactory.CreateViewModel<LoadTypeRefFromAssemblyRefCommand.Factory>().Invoke(DataContext, this);
                }
                return _loadTypeRefFromAssemblyRefCommand;
            }
        }
    }
}