
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    public class ZetboxDebuggerAsViewModel
        : ViewModel, IZetboxContextDebugger
    {
        public new delegate ZetboxDebuggerAsViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ZetboxDebuggerAsViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            ZetboxContextDebuggerSingleton.SetDebugger(this);
        }

        public ZetboxDebuggerAsViewModel(bool designMode)
            : base(designMode)
        {
        }

        public override string Name
        {
            get { return this.GetType().Name; }
        }

        #region Public Interface

        private ObservableCollection<ZetboxContextModel> _activeCtxCache = new ObservableCollection<ZetboxContextModel>();
        private ObservableCollection<ZetboxContextModel> _disposedCtxCache = new ObservableCollection<ZetboxContextModel>();
        private ReadOnlyObservableCollection<ZetboxContextModel> _activeCtxView;
        private ReadOnlyObservableCollection<ZetboxContextModel> _disposedCtxView;

        public ReadOnlyObservableCollection<ZetboxContextModel> ActiveContexts
        {
            get
            {
                if (_activeCtxView == null)
                {
                    _activeCtxView = new ReadOnlyObservableCollection<ZetboxContextModel>(_activeCtxCache);
                }
                return _activeCtxView;
            }
        }

        public ReadOnlyObservableCollection<ZetboxContextModel> DisposedContexts
        {
            get
            {
                if (_disposedCtxView == null)
                {
                    _disposedCtxView = new ReadOnlyObservableCollection<ZetboxContextModel>(_disposedCtxCache);
                }
                return _disposedCtxView;
            }
        }
        #endregion

        #region IZetboxContextDebugger Members

        private ZetboxContextModel GetModel(IZetboxContext ctx)
        {
            return new ZetboxContextModel(ctx);
        }

        void IZetboxContextDebugger.Created(IZetboxContext ctx)
        {
            _activeCtxCache.Add(GetModel(ctx));
            ctx.Disposing += Disposing;
            ctx.Changed += Changed;
        }

        void Disposing(object sender, GenericEventArgs<IReadOnlyZetboxContext> e)
        {
            var mdl = GetModel((IZetboxContext)e.Data);
            _activeCtxCache.Remove(mdl);
            _disposedCtxCache.Add(mdl);
        }

        void Changed(object sender, GenericEventArgs<IZetboxContext> e)
        {
            var mdl = GetModel(e.Data);
            mdl.OnContextChanged();
        }
        #endregion
    }

    /// <summary>
    /// Cant be a regular view model -> recursion when creating
    /// </summary>
    public class ZetboxContextModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler _PropertyChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _PropertyChangedEvent += value;
            }
            remove
            {
                _PropertyChangedEvent -= value;
            }
        }

        /// <summary>
        /// Notifies all listeners of PropertyChanged about a change in a property
        /// </summary>
        /// <param name="propertyName">the changed property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_PropertyChangedEvent != null)
                _PropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected IZetboxContext DebuggingContext { get; private set; }

        public ZetboxContextModel(IZetboxContext dataCtx)
        {
            DebuggingContext = dataCtx;
        }

        #region Public Interface

        private ObservableCollection<string> _objCache = new ObservableCollection<string>();
        public ObservableCollection<string> AttachedObjects
        {
            get
            {
                return _objCache;
            }
        }

        private int _countCache = 0;
        public int Count
        {
            get
            {
                return _countCache;
            }
            private set
            {
                if (_countCache != value)
                {
                    _countCache = value;
                    OnPropertyChanged("Count");
                }
            }
        }

        public string CreatedAt
        {
            get
            {
                if (DebuggingContext is IDebuggingZetboxContext)
                {
                    return String.Join(String.Empty, ((IDebuggingZetboxContext)DebuggingContext).CreatedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string DisposedAt
        {
            get
            {
                IDebuggingZetboxContext dbgCtx = DebuggingContext as IDebuggingZetboxContext;
                if (dbgCtx != null)
                {
                    if (dbgCtx.DisposedAt == null)
                    {
                        return "still active";
                    }
                    else
                    {
                        return String.Join(String.Empty, dbgCtx.DisposedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        #region Utilities and UI callbacks

        /// <summary>
        /// Update the model's state when the context is changed
        /// </summary>
        /// is only called by the <see cref="ZetboxDebuggerAsViewModel"/>
        internal void OnContextChanged()
        {
            var objs = DebuggingContext.AttachedObjects.OfType<IDataObject>().ToArray();
            SyncObjects(objs);
        }

        private void SyncObjects(IDataObject[] objs)
        {
            _objCache.Clear();
            objs.ForEach(o => _objCache.Add(string.Format("({0}) {1}", o.ID, DebuggingContext.GetInterfaceType(o).Type.FullName)));
            Count = objs.Length;
        }

        #endregion

        public string Name
        {
            get { return "ZetboxContext"; }
        }
    }
}
