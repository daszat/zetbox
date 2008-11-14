using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class WorkspaceModel : PresentableModel
    {
        public WorkspaceModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory)
        {
            Modules = new ObservableCollection<ModuleModel>();
            OpenObjects = new ObservableCollection<DataObjectModel>();
            Async.Queue(DataContext, () => { AsyncLoadModules(); UI.Queue(UI, () => this.State = ModelState.Active); });
        }

        #region public interface

        /// <summary>
        /// A list of "active" <see cref="IDataObjects"/>
        /// </summary>
        public ObservableCollection<DataObjectModel> OpenObjects { get; private set; }

        /// <summary>
        /// A collection of all <see cref="Module"/>s, to display as entry 
        /// point into the object hierarchy
        /// </summary>
        public ObservableCollection<ModuleModel> Modules { get; private set; }

        #endregion

        #region Async handlers and UI callbacks

        private void AsyncLoadModules()
        {
            Async.Verify();
            var modules = DataContext.GetQuery<Module>().ToList();
            UI.Queue(UI, () =>
            {
                foreach (var m in modules)
                {
                    Modules.Add(Factory.CreateModel<ModuleModel>(m));
                }
                State = ModelState.Active;
            });
        }

        #endregion

    }
}
