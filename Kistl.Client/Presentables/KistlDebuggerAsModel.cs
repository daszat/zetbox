using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    /////////////////////////////////////////////////////////////////
    //   hack the heck out of the model concept to re-use          //
    //   their structures for a sufficiently asynchronous          //
    //   kistl debugger                                            //
    /////////////////////////////////////////////////////////////////

    public class KistlDebuggerAsModel : PresentableModel, IKistlContextDebugger
    {

        public KistlDebuggerAsModel(IGuiApplicationContext appCtx, IDebuggingKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            KistlContextDebugger.SetDebugger(this);
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

        #region IKistlContextDebugger Members (all async)

        private KistlContextModel GetModel(IKistlContext ctx)
        {
            return Factory.CreateSpecificModel<KistlContextModel>(ctx);
        }

        void IKistlContextDebugger.Created(IKistlContext ctx)
        {
            UI.Queue(UI, () => _activeCtxCache.Add(GetModel(ctx)));
        }

        void IKistlContextDebugger.Disposed(IKistlContext ctx)
        {
            UI.Queue(UI, () => { var mdl = GetModel(ctx); _activeCtxCache.Remove(mdl); _disposedCtxCache.Add(mdl); });
        }

        void IKistlContextDebugger.Changed(IKistlContext ctx)
        {
            UI.Queue(UI, () =>
            {
                var ctxMdl = GetModel(ctx);
                Async.Queue(ctx, ctxMdl.AsyncOnContextChanged);
            });
        }

        #endregion

        #region IDisposable Members

        // as suggested on http://msdn.microsoft.com/en-us/system.idisposable.aspx
        // adapted for easier usage when inheriting, by naming the functions appropriately
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    // must not be done when running from the finalizer
                    DisposeManagedResources();
                }
                // free native resources
                DisposeNativeResources();

                this.disposed = true;
            }
        }

        /// <summary>
        /// Override this to be called when Managed Resources should be disposed
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
            if (_activeCtxCache != null)
                _activeCtxCache.Clear();
        }
        /// <summary>
        /// Override this to be called when Native Resources should be disposed
        /// </summary>
        protected virtual void DisposeNativeResources() { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~KistlDebuggerAsModel()
        {
            Dispose(false);
        }

        #endregion

    }

    public class KistlContextModel
        : PresentableModel
    {

        protected IDebuggingKistlContext DebuggingContext { get; private set; }

        public KistlContextModel(IGuiApplicationContext appCtx, IDebuggingKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            DebuggingContext = dataCtx;
        }

        #region Public Interface

        private ObservableCollection<DataObjectModel> _objCache = new ObservableCollection<DataObjectModel>();
        private ReadOnlyObservableCollection<DataObjectModel> _objView;
        public ReadOnlyObservableCollection<DataObjectModel> AttachedObjects
        {
            get
            {
                UI.Verify();
                if (_objView == null)
                {
                    _objView = new ReadOnlyObservableCollection<DataObjectModel>(_objCache);
                }
                return _objView;
            }
        }

        private int _countCache = 0;
        public int Count
        {
            get
            {
                UI.Verify();
                return _countCache;
            }
            private set
            {
                UI.Verify();
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
                UI.Verify();
                return String.Join("", DebuggingContext.CreatedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
            }
        }

        public string DisposedAt
        {
            get
            {
                UI.Verify();
                if (DebuggingContext.DisposedAt == null)
                {
                    return "still active";
                }
                else
                {

                    return String.Join("", DebuggingContext.DisposedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
                }
            }
        }

        #endregion

        #region Async Handlers and UI callbacks

        /// <summary>
        /// Update the model's state when the context is changed
        /// </summary>
        /// is only called by the <see cref="KistlDebuggerAsModel"/>
        internal void AsyncOnContextChanged()
        {
            Async.Verify();
            var objs = DataContext.AttachedObjects.OfType<IDataObject>().ToArray();
            UI.Queue(UI, () => SyncObjects(objs));
        }

        private void SyncObjects(IDataObject[] objs)
        {
            _objCache.Clear();
            objs.ForEach(o => _objCache.Add((DataObjectModel)Factory.CreateDefaultModel(DataContext, o)));
            Count = objs.Length;
        }

        #endregion

    }
}
