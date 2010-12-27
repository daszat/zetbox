
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;

    public class CacheDebuggerViewModel
        : ViewModel
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
            get { return CacheDebuggerViewModelResources.Name; }
        }

        public static ReadOnlyCollection<Cache> Caches
        {
            get
            {
                return Cache.Caches;
            }
        }

        private SimpleCommandViewModel _clearCommand = null;
        public ICommandViewModel ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        CacheDebuggerViewModelResources.ClearCommand_Name,
                        CacheDebuggerViewModelResources.ClearCommand_Tooltip, 
                        Cache.ClearAll, 
                        null);

                }
                return _clearCommand;
            }
        }
    }
}
