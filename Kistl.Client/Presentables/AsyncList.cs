using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Kistl.API;

namespace Kistl.Client.Presentables
{
    public class AsyncListFactory
    {

        public static ImmutableAsyncList<TAsync, TUi> UiCreateImmutable<TAsync, TUi>(
            IGuiApplicationContext appCtx,
            IKistlContext ctx,
            PresentableModel parentModel,
            Func<IEnumerable<TAsync>> asyncOriginalList,
            Func<TAsync, TUi> uiTransform)
        {
            return new ImmutableAsyncList<TAsync, TUi>(appCtx, ctx, parentModel, asyncOriginalList, uiTransform);
        }

        public static AsyncList<TAsync, TUi> UiCreateMutable<TAsync, TUi>(
            IGuiApplicationContext appCtx,
            IKistlContext ctx,
            PresentableModel parentModel,
            Func<INotifyCollectionChanged> asyncNotifier,
            Func<IList<TAsync>> asyncOriginalList,
            Func<TAsync, TUi> uiTransform,
            Func<TUi, TAsync> uiInverseTransform)
        {
            return new AsyncList<TAsync, TUi>(appCtx, ctx, parentModel, asyncNotifier, asyncOriginalList, uiTransform, uiInverseTransform);
        }

    }

    /// <summary>
    /// This List can lift asynchronous items onto the UI Thread.
    /// </summary>
    public class ImmutableAsyncList<TAsync, TUi>
    {

        private IGuiApplicationContext _appCtx;
        private PresentableModel _parentModel;
        private Func<IEnumerable<TAsync>> _asyncOriginalList;
        private Func<TAsync, TUi> _uiTransform;

        private List<TUi> _uiCache;
        private ReadOnlyCollection<TUi> _uiView;

        protected IThreadManager UI { get { return _appCtx.UiThread; } }
        protected IThreadManager Async { get { return _appCtx.AsyncThread; } }
        protected IKistlContext DataContext { get; private set; }

        internal ImmutableAsyncList(
            IGuiApplicationContext appCtx,
            IKistlContext ctx,
            PresentableModel parentModel,
            Func<IEnumerable<TAsync>> asyncOriginalList,
            Func<TAsync, TUi> uiTransform)
        {
            _appCtx = appCtx;

            UI.Verify();

            DataContext = ctx;
            _parentModel = parentModel;
            _asyncOriginalList = asyncOriginalList;
            _uiTransform = uiTransform;

            Async.Queue(DataContext, AsyncHandleReset);

        }

        #region Public Interface

        public ReadOnlyCollection<TUi> GetUiView()
        {
            if (_uiView == null)
            {
                _uiView = new ReadOnlyCollection<TUi>(_uiCache);
            }
            return _uiView;
        }

        #endregion

        #region Async Handlers

        private void AsyncHandleReset()
        {
            Async.Verify();
            UI.Queue(UI, () => _parentModel.State = ModelState.Loading);

            var newItems = _asyncOriginalList().ToList();
            UI.Queue(UI, () =>
            {
                if (_uiCache == null)
                    _uiCache = new List<TUi>();
                else
                    _uiCache.Clear();

                foreach (var i in newItems)
                {
                    _uiCache.Add(_uiTransform(i));
                }
                _parentModel.State = ModelState.Active;
            });
        }

        #endregion

    }

    /// <summary>
    /// This List can lift asynchronous items onto the UI Thread.
    /// </summary>
    public class AsyncList<TAsync, TUi>
    {

        private IGuiApplicationContext _appCtx;
        private PresentableModel _parentModel;
        private Func<IList<TAsync>> _asyncOriginalList;
        private Func<INotifyCollectionChanged> _asyncNotifier;
        private Func<TAsync, TUi> _uiTransform;
        private Func<TUi, TAsync> _uiInverseTransform;

        private ObservableCollection<TUi> _uiCache;
        private ReadOnlyObservableCollection<TUi> _uiView;

        protected IThreadManager UI { get { return _appCtx.UiThread; } }
        protected IThreadManager Async { get { return _appCtx.AsyncThread; } }
        protected IKistlContext DataContext { get; private set; }

        internal AsyncList(
            IGuiApplicationContext appCtx,
            IKistlContext ctx,
            PresentableModel parentModel,
            Func<INotifyCollectionChanged> asyncNotifier,
            Func<IList<TAsync>> asyncOriginalList,
            Func<TAsync, TUi> uiTransform,
            Func<TUi, TAsync> uiInverseTransform)
        {
            _appCtx = appCtx;

            UI.Verify();

            DataContext = ctx;
            _parentModel = parentModel;
            _parentModel.State = ModelState.Loading;

            _asyncOriginalList = asyncOriginalList;
            _asyncNotifier = asyncNotifier;
            _uiTransform = uiTransform;
            _uiInverseTransform = uiInverseTransform;

            _uiCache = new ObservableCollection<TUi>();

            Async.Queue(DataContext, () =>
            {
                _asyncNotifier().CollectionChanged += AsyncCollectionChangedHandler;
                AsyncHandleReset();
                UI.Queue(UI, () => _parentModel.State = ModelState.Active);
            });

        }

        #region Public Interface

        public ReadOnlyObservableCollection<TUi> GetUiView()
        {
            if (_uiView == null)
            {
                _uiView = new ReadOnlyObservableCollection<TUi>(_uiCache);
            }
            return _uiView;
        }

        public void AddItem(TUi item)
        {
            UI.Verify();
            var asyncItem = _uiInverseTransform(item);
            Async.Queue(DataContext, () =>
            {
                _asyncOriginalList().Add(asyncItem);
            });
        }

        public void RemoveItem(TUi item)
        {
            UI.Verify();
            var asyncItem = _uiInverseTransform(item);
            Async.Queue(DataContext, () =>
            {
                _asyncOriginalList().Remove(asyncItem);
            });
        }

        #endregion

        #region Async Handlers

        private void AsyncCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            Async.Verify();
            UI.Queue(UI, () => _parentModel.State = ModelState.Loading);
            switch (e.Action)
            {
                // TODO: Optimize this!
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    AsyncHandleReset();
                    break;
                default:
                    break;
            }
            UI.Queue(UI, () => _parentModel.State = ModelState.Active);
        }

        private void AsyncHandleReset()
        {
            Async.Verify();
            var newItems = _asyncOriginalList().ToList();
            UI.Queue(UI, () =>
            {
                _uiCache.Clear();
                foreach (var i in newItems)
                {
                    _uiCache.Add(_uiTransform(i));
                }
            });
        }

        #endregion

    }

}
