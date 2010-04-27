using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.API.Utils;

namespace Kistl.Client.Presentables
{
    public class CacheDebuggerViewModel : ViewModel
    {
        public new delegate CacheDebuggerViewModel Factory(IKistlContext dataCtx);

        public CacheDebuggerViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            Cache.CachesCollectionChanged += new EventHandler(Cache_CachesCollectionChanged);
        }

        void Cache_CachesCollectionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Caches");
        }

        public override string Name
        {
            get { return "Cache Debugger"; }
        }

        public static ReadOnlyCollection<Cache> Caches
        {
            get
            {
                return Cache.Caches;
            }
        }

        private ClearCommand _clearCommand = null;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = ModelFactory.CreateViewModel<ClearCommand.Factory>().Invoke(DataContext);
                }
                return _clearCommand;
            }
        }
    }

    internal class ClearCommand : CommandModel
    {
        public new delegate ClearCommand Factory(IKistlContext dataCtx);

        public ClearCommand(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx, "Clear", "Clears all caches")
        {
        }

        public override bool CanExecute(object data)
        {
            return true;
        }

        protected override void DoExecute(object data)
        {
            Cache.ClearAll();
        }
    }
}
