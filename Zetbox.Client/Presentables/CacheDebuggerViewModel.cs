
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;

    public class CacheDebuggerViewModel
        : ViewModel
    {
        public new delegate CacheDebuggerViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public CacheDebuggerViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
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
                        null,
                        CacheDebuggerViewModelResources.ClearCommand_Name,
                        CacheDebuggerViewModelResources.ClearCommand_Tooltip, 
                        Cache.ClearAll,
                        null, null);

                }
                return _clearCommand;
            }
        }
    }
}
