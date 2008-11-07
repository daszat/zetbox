using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kistl.Client.PresenterModel
{
    public class ModuleModel : PresentableModel
    {
        public ModuleModel(IThreadManager uiManager, IThreadManager asyncManager, Module mdl)
            : base(uiManager, asyncManager)
        {
            ObjectClasses = new ObservableCollection<DataObjectModel>();
            _module = mdl;
            _module.PropertyChanged += ModulePropertyChanged;
            Async.Queue(() => { LoadObjectClasses(); UI.Queue(() => this.State = ModelState.Active); });
        }

        #region public interface

        public ObservableCollection<DataObjectModel> ObjectClasses { get; private set; }

        // TODO: proxying implementations might block on simple property accesses too.
        public string Name { get { UI.Verify(); return _module.ModuleName; } }

        #endregion

        #region Async handlers and UI callbacks

        private void LoadObjectClasses()
        {
            Async.Verify();
            lock (_module.Context)
            {
                var classes = _module.Context.GetQuery<DataType>().Where(oc => oc.Module.ID == _module.ID).ToList();
                UI.Queue(() =>
                {
                    foreach (var oc in classes)
                    {
                        ObjectClasses.Add(new DataObjectModel(UI, Async, oc));
                    }
                    State = ModelState.Active;
                });
            }
        }

        #endregion

        #region PropertyChanged event handlers

        private void ModulePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Async.Verify();
            switch (args.PropertyName)
            {
                case "ModuleName": InvokePropertyChanged("Name"); break;
            }
        }

        #endregion

        private Module _module;
    }
}
