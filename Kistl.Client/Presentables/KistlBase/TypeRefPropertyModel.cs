
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    public class TypeRefPropertyModel
           : ObjectReferenceModel
    {
        public TypeRefPropertyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(appCtx, dataCtx, referenceHolder, prop)
        {
            // TODO: use a static reference here
            if (prop.ReferenceObjectClass.ClassName != "TypeRef")
            {
                throw new ArgumentOutOfRangeException("prop", "Can only handle TypeRef References");
            }
        }

        private LoadTypeRefFromAssemblyFileCommand _loadTypeRefFromAssemblyFileCommand = null;
        public LoadTypeRefFromAssemblyFileCommand LoadTypeRefFromAssemblyFileCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyFileCommand == null)
                {
                    _loadTypeRefFromAssemblyFileCommand = new LoadTypeRefFromAssemblyFileCommand(AppContext, DataContext, this);
                }
                return _loadTypeRefFromAssemblyFileCommand;
            }
        }

        private LoadTypeRefFromAssemblyRefCommand _loadTypeRefFromAssemblyRefCommand = null;
        public LoadTypeRefFromAssemblyRefCommand LoadTypeRefFromAssemblyRefCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyRefCommand == null)
                {
                    _loadTypeRefFromAssemblyRefCommand = new LoadTypeRefFromAssemblyRefCommand(AppContext, DataContext, this);
                }
                return _loadTypeRefFromAssemblyRefCommand;
            }
        }
    }
}