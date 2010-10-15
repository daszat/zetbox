
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Presentables.KistlBase;
    using Kistl.Client.Models;
    
    public class ObjectClassViewModel : DataTypeViewModel
    {
        public new delegate ObjectClassViewModel Factory(IKistlContext dataCtx, ObjectClass cls);

        public ObjectClassViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            ObjectClass cls)
            : base(appCtx, config, dataCtx, cls)
        {
            _class = cls;
        }

        public InterfaceType GetDescribedInterfaceType()
        {
            return _class.GetDescribedInterfaceType();
        }

        private ObjectClass _class;

        public override Kistl.App.GUI.Icon Icon
        {
            get { return _class.DefaultIcon; }
        }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            var relListMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Relation>());
            relListMdl.Filter.Add(new ConstantValueFilterModel("A.Type = @0 || B.Type = @0", this.Object));

            var lblMdl = ViewModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, "Relations", "", relListMdl);
            var propGrpMdl = ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(DataContext, "Relations", new ViewModel[] { lblMdl });
            result.Add(propGrpMdl);
            return result;
        }
    }
}
