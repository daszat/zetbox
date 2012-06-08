
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.Client.Models;

    public class ObjectClassViewModel : DataTypeViewModel
    {
        public new delegate ObjectClassViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass cls);

        public ObjectClassViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ObjectClass cls)
            : base(appCtx, dataCtx, parent, cls)
        {
            _class = cls;
        }

        public InterfaceType GetDescribedInterfaceType()
        {
            return _class.GetDescribedInterfaceType();
        }

        private ObjectClass _class;

        public override Zetbox.App.GUI.Icon Icon
        {
            get { return _class.DefaultIcon; }
        }

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            var relListMdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, this, () => DataContext, typeof(Relation).GetObjectClass(FrozenContext), () => DataContext.GetQuery<Relation>());
            relListMdl.EnableAutoFilter = false;
            relListMdl.AddFilter(new ConstantValueFilterModel("A.Type = @0 || B.Type = @0", this.Object));
            relListMdl.Commands.Add(ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "New Relation", "Creates a new Relation", CreateRelation, null, null));

            var lblMdl = ViewModelFactory.CreateViewModel<LabeledViewContainerViewModel.Factory>().Invoke(DataContext, this, "Relations", "", relListMdl);
            var propGrpMdl = ViewModelFactory.CreateViewModel<SinglePropertyGroupViewModel.Factory>().Invoke(DataContext, this, "Relations", new ViewModel[] { lblMdl });
            result.Add(propGrpMdl);
            return result;
        }

        public void CreateRelation()
        {
            var rel = _class.CreateRelation();
            ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, rel), true);
        }
    }
}
