using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Configuration;
using Kistl.Client.Presentables.KistlBase;

namespace Kistl.Client.Presentables
{
    public class ObjectClassModel : DataTypeModel
    {
        public new delegate ObjectClassModel Factory(IKistlContext dataCtx, ObjectClass cls);

        public ObjectClassModel(
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

        protected override List<PropertyGroupModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            var relListMdl = ModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, null, DataContext.GetQuery<Relation>());
            relListMdl.Filter.Add(new ConstantFilterExpression("A.Type = @0 || B.Type = @0", this.Object));

            var lblMdl = ModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, "Relations", "", relListMdl);
            var propGrpMdl = ModelFactory.CreateViewModel<SinglePropertyGroupModel.Factory>().Invoke(DataContext, "Relations", new ViewModel[] { lblMdl });
            result.Add(propGrpMdl);
            return result;
        }
    }
}
