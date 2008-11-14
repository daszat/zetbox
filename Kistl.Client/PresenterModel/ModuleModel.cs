using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class ModuleModel : DataObjectModel
    {
        public ModuleModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            Module mdl)
            : base(uiManager, asyncManager, guiCtx, dataCtx, mdl)
        {
            ObjectClasses = new ObservableCollection<DataObjectModel>();
            _module = mdl;
            _module.PropertyChanged += AsyncModulePropertyChanged;
            Async.Queue(DataContext, () => { AsyncLoadObjectClasses(); UI.Queue(UI, () => this.State = ModelState.Active); });
        }

        #region public interface

        public ObservableCollection<DataObjectModel> ObjectClasses { get; private set; }

        #endregion

        #region Async handlers and UI callbacks

        private void AsyncLoadObjectClasses()
        {
            Async.Verify();
            UI.Queue(UI, () => State = ModelState.Loading);
            var datatypes = DataContext.GetQuery<DataType>().Where(dt => dt.Module.ID == _module.ID).OrderBy(dt => dt.ClassName).ToList();
            UI.Queue(UI, () =>
            {
                foreach (var dt in datatypes)
                {
                    if (dt is ObjectClass)
                    {
                        ObjectClasses.Add(new ObjectClassModel(UI, Async, GuiContext, DataContext, (ObjectClass)dt));
                    }
                    else
                    {
                        ObjectClasses.Add(new DataTypeModel(UI, Async, GuiContext, DataContext, dt));
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
