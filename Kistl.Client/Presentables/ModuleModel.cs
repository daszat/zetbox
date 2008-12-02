using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public class ModuleModel : DataObjectModel
    {
        public ModuleModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            Module mdl)
            : base(appCtx, dataCtx, mdl)
        {
            _module = mdl;
            _module.PropertyChanged += AsyncModulePropertyChanged;
        }

        #region public interface

        private ObservableCollection<DataObjectModel> _objectClassesCache = null;
        public ObservableCollection<DataObjectModel> ObjectClasses
        {
            get
            {
                UI.Verify();
                if (_objectClassesCache == null)
                {
                    _objectClassesCache = new ObservableCollection<DataObjectModel>();
                    State = ModelState.Loading;
                    Async.Queue(DataContext, () =>
                    {
                        AsyncLoadObjectClasses();
                        UI.Queue(UI, () => this.State = ModelState.Active);
                    });
                }
                return _objectClassesCache;
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        private void AsyncLoadObjectClasses()
        {
            Async.Verify();
            UI.Queue(UI, () => State = ModelState.Loading);
            var datatypes = DataContext.GetQuery<DataType>()
                .Where(dt => dt.Module.ID == _module.ID)
                .OrderBy(dt => dt.ClassName)
                .ToList();
            UI.Queue(UI, () =>
            {
                foreach (var dt in datatypes)
                {
                    if (dt is ObjectClass)
                    {
                        ObjectClasses.Add(Factory.CreateSpecificModel<ObjectClassModel>(DataContext, (ObjectClass)dt));
                    }
                    else
                    {
                        ObjectClasses.Add(Factory.CreateSpecificModel<DataTypeModel>(DataContext, dt));
                    }
                }
                State = ModelState.Active;
            });
        }

        #endregion

        #region PropertyChanged event handlers

        private void AsyncModulePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Async.Verify();
            switch (args.PropertyName)
            {
                case "ModuleName": AsyncOnPropertyChanged("Name"); break;
            }
        }

        #endregion

        private Module _module;
    }
}
