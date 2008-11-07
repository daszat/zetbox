using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class DesktopModel : PresentableModel
    {
        public DesktopModel(IThreadManager uiManager, IThreadManager asyncManager, IKistlContext ctx)
            : base(uiManager, asyncManager)
        {
            _ctx = ctx;
            Modules = new ObservableCollection<ModuleModel>();
            Async.Queue(() => { LoadModules(); UI.Queue(() => this.State = ModelState.Active); });
        }

        #region public interface

        public ObservableCollection<ModuleModel> Modules { get; private set; }

        #endregion

        #region Async handlers and UI callbacks

        private void LoadModules()
        {
            Async.Verify();
            lock (_ctx)
            {
                var modules = _ctx.GetQuery<Module>().ToList();
                UI.Queue(() =>
                {
                    foreach (var m in modules)
                    {
                        Modules.Add(new ModuleModel(UI, Async, m));
                    }
                    State = ModelState.Active;
                });
            }
        }

        #endregion

        private IKistlContext _ctx;
    }
}
