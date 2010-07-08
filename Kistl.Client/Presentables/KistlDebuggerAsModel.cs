using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using System.ComponentModel;

namespace Kistl.Client.Presentables
{

    public class KistlDebuggerAsModel
        : ViewModel, IKistlContextDebugger
    {
        public new delegate KistlDebuggerAsModel Factory(IKistlContext dataCtx);

        public KistlDebuggerAsModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            KistlContextDebuggerSingleton.SetDebugger(this);
        }

        public KistlDebuggerAsModel(bool designMode)
            : base(designMode)
        {
        }

        public override string Name
        {
            get { return this.GetType().Name; }
        }

        #region Public Interface

        private ObservableCollection<KistlContextModel> _activeCtxCache = new ObservableCollection<KistlContextModel>();
        private ObservableCollection<KistlContextModel> _disposedCtxCache = new ObservableCollection<KistlContextModel>();
        private ReadOnlyObservableCollection<KistlContextModel> _activeCtxView;
        private ReadOnlyObservableCollection<KistlContextModel> _disposedCtxView;

        public ReadOnlyObservableCollection<KistlContextModel> ActiveContexts
        {
            get
            {
                if (_activeCtxView == null)
                {
                    _activeCtxView = new ReadOnlyObservableCollection<KistlContextModel>(_activeCtxCache);
                }
                return _activeCtxView;
            }
        }

        public ReadOnlyObservableCollection<KistlContextModel> DisposedContexts
        {
            get
            {
                if (_disposedCtxView == null)
                {
                    _disposedCtxView = new ReadOnlyObservableCollection<KistlContextModel>(_disposedCtxCache);
                }
                return _disposedCtxView;
            }
        }
        #endregion

        #region IKistlContextDebugger Members

        private KistlContextModel GetModel(IKistlContext ctx)
        {
            return new KistlContextModel(ctx);
        }

        void IKistlContextDebugger.Created(IKistlContext ctx)
        {
            _activeCtxCache.Add(GetModel(ctx));
            ctx.Disposing += Disposing;
            ctx.Changed += Changed;
        }

        void Disposing(object sender, GenericEventArgs<IReadOnlyKistlContext> e)
        {
            var mdl = GetModel((IKistlContext)e.Data);
            _activeCtxCache.Remove(mdl);
            _disposedCtxCache.Add(mdl);
        }

        void Changed(object sender, GenericEventArgs<IKistlContext> e)
        {
            var mdl = GetModel(e.Data);
            mdl.OnContextChanged();
        }
        #endregion
    }

    /// <summary>
    /// Cant be a regular view model -> recursion when creating
    /// </summary>
    public class KistlContextModel : INotifyPropertyChanged
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

        protected IKistlContext DebuggingContext { get; private set; }

        public KistlContextModel(IKistlContext dataCtx)
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
                if (DebuggingContext is IDebuggingKistlContext)
                {
                    return String.Join(String.Empty, ((IDebuggingKistlContext)DebuggingContext).CreatedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
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
                IDebuggingKistlContext dbgCtx = DebuggingContext as IDebuggingKistlContext;
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
        /// is only called by the <see cref="KistlDebuggerAsModel"/>
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
            get { return "KistlContext"; }
        }
    }
}
