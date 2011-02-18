using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Configuration;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Client.Presentables.KistlBase;

namespace Kistl.Client.Presentables.ObjectBrowser
{
    public class ModuleViewModel : DataObjectViewModel
    {
        public new delegate ModuleViewModel Factory(IKistlContext dataCtx, Module mdl);

        private Func<IKistlContext> _ctxFactory;

        public ModuleViewModel(
            IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory, IKistlContext dataCtx,
            Module mdl)
            : base(appCtx, dataCtx, mdl)
        {
            _ctxFactory = ctxFactory;
            _module = mdl;
            _module.PropertyChanged += ModulePropertyChanged;
        }

        #region public interface

        private ObservableCollection<InstanceListViewModel> _objectClassesCache = null;
        public ObservableCollection<InstanceListViewModel> ObjectClasses
        {
            get
            {
                if (_objectClassesCache == null)
                {
                    _objectClassesCache = new ObservableCollection<InstanceListViewModel>();
                    LoadObjectClasses();
                }
                return _objectClassesCache;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        private void LoadObjectClasses()
        {
            var datatypes = FrozenContext.GetQuery<ObjectClass>()
                .Where(dt => dt.Module.ExportGuid == _module.ExportGuid && !dt.IsSimpleObject)
                .OrderBy(dt => dt.Name)
                .ToList();
            foreach (var cls in datatypes)
            {
                var mdl = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(DataContext, _ctxFactory, cls, null);
                mdl.AllowAddNew = true;
                mdl.AllowDelete = true;
                mdl.Commands.Add(ViewModelFactory.CreateViewModel<EditDataObjectClassCommand.Factory>().Invoke(DataContext, cls));
                ObjectClasses.Add(mdl);
            }
        }

        #endregion

        #region PropertyChanged event handlers

        private void ModulePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Name": OnPropertyChanged("Name"); break;
            }
        }

        #endregion

        private Module _module;
    }
}
