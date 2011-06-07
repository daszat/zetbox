namespace Kistl.Client.Presentables.Parties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using ZBox.Basic.Parties;
    using Kistl.App.Extensions;
    using System.Collections.ObjectModel;
    using Kistl.API.Utils;

    [ViewModelDescriptor]
    public class PartyRolesViewModel : ViewModel
    {
        public new delegate PartyRolesViewModel Factory(IKistlContext dataCtx, ViewModel parent, Party obj, InterfaceType roleType);

        public PartyRolesViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Party obj, InterfaceType roleType)
            : base(appCtx, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (roleType == null) throw new ArgumentNullException("roleType");

            this.Party = obj;
            this.RoleType = roleType;
        }

        public Party Party { get; private set; }
        public InterfaceType RoleType { get; private set; }

        public override string Name
        {
            get { return RoleType.Type.Name; }
        }

        private ObservableCollection<IDataObject> _partyRolesList;
        public ObservableCollection<IDataObject> PartyRolesList
        {
            get
            {
                if (_partyRolesList == null)
                {
                    _partyRolesList = new ObservableCollection<IDataObject>(
                        Party.PartyRole.ToList().Where(i => DataContext.GetInterfaceType(i) == RoleType).Cast<IDataObject>().ToList());
                }
                return _partyRolesList;
            }
        }

        private Models.ObjectCollectionValueModel<ICollection<IDataObject>> _RolesModel;
        public Models.ObjectCollectionValueModel<ICollection<IDataObject>> RolesModel
        {
            get
            {
                if (_RolesModel == null)
                {
                    _RolesModel = new Models.ObjectCollectionValueModel<ICollection<IDataObject>>("Roles", "", false, false, RoleType.GetObjectClass(FrozenContext), PartyRolesList);
                }
                return _RolesModel;
            }
        }


        private ValueViewModels.ObjectCollectionViewModel _Roles;
        public ValueViewModels.ObjectCollectionViewModel Roles
        {
            get
            {
                if(_Roles == null)
                {
                    _Roles = ViewModelFactory.CreateViewModel<ValueViewModels.ObjectCollectionViewModel.Factory>().Invoke(DataContext, this, RolesModel);
                    _Roles.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_Roles_PropertyChanged);
                    _Roles.SelectedItem = _Roles.Value.FirstOrDefault(i => ((PartyRole)i.Object).Thru == null);
                }
                return _Roles;
            }
        }

        void _Roles_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem") OnPropertyChanged("SelectedRole");
        }

        public ViewModel SelectedRole
        {
            get
            {
                return Roles.SelectedItem;
            }
        }
    }
}
