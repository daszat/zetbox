using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kistl.Client.PresenterModel
{
    public class ModuleModel : DataObjectModel
    {
        public ModuleModel(IThreadManager uiManager, IThreadManager asyncManager, Module mdl)
            : base(uiManager, asyncManager, mdl)
        {
            ObjectClasses = new ObservableCollection<DataObjectModel>();
            _module = mdl;
            _module.PropertyChanged += AsyncModulePropertyChanged;
            Async.Queue(_module.Context, () => { AsyncLoadObjectClasses(); UI.Queue(UI, () => this.State = ModelState.Active); });
        }

        #region public interface

        public ObservableCollection<DataObjectModel> ObjectClasses { get; private set; }

        #endregion

        #region Async handlers and UI callbacks

        private void AsyncLoadObjectClasses()
        {
            Async.Verify();
            UI.Queue(UI, () => State = ModelState.Loading);
            var datatypes = _module.Context.GetQuery<DataType>().Where(dt => dt.Module.ID == _module.ID).OrderBy(dt => dt.ClassName).ToList();
            UI.Queue(UI, () =>
            {
                foreach (var dt in datatypes)
                {
                    if (dt is ObjectClass)
                    {
                        ObjectClasses.Add(new ObjectClassModel(UI, Async, (ObjectClass)dt));
                    }
                    else
                    {
                        ObjectClasses.Add(new DataTypeModel(UI, Async, dt));
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
