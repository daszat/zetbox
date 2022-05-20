// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables.Debugger
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class ZetboxContextDebuggerViewModel
        : ViewModel, IZetboxContextDebugger
    {
        public new delegate ZetboxContextDebuggerViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ZetboxContextDebuggerViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            ZetboxContextDebuggerSingleton.SetDebugger(this);
        }

        public ZetboxContextDebuggerViewModel(bool designMode)
            : base(designMode)
        {
        }

        public override string Name
        {
            get { return ZetboxContextDebuggerViewModelResources.Name; }
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
            return _activeCtxCache.FirstOrDefault(m => m.DebuggingContext == ctx) ?? new ZetboxContextModel(ctx);
        }

        void IZetboxContextDebugger.Created(IZetboxContext ctx)
        {
            _activeCtxCache.Add(GetModel(ctx));
            ctx.Disposed += Disposed;
            ctx.Changed += Changed;
        }

        Task Disposed(object sender, GenericEventArgs<IReadOnlyZetboxContext> e)
        {
            var mdl = GetModel((IZetboxContext)e.Data);
            _activeCtxCache.Remove(mdl);
            _disposedCtxCache.Add(mdl);
            mdl.Disposed();

            return Task.CompletedTask;
        }

        Task Changed(object sender, GenericEventArgs<IZetboxContext> e)
        {
            var mdl = GetModel(e.Data);
            mdl.OnContextChanged();

            return Task.CompletedTask;
        }
        #endregion
    }

    /// <summary>
    /// Cant be a regular view model -> recursion when creating
    /// </summary>
    public class ZetboxContextModel : INotifyPropertyChanged
    {
        public ZetboxContextModel(IZetboxContext dataCtx)
        {
            DebuggingContext = dataCtx;
            CreatedOn = DateTime.Now;
            DisposedAt = "still active";
            if (dataCtx is IDebuggingZetboxContext)
            {
                CreatedAt = String.Join(String.Empty, ((IDebuggingZetboxContext)dataCtx).CreatedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
            }
        }

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

        #region Public Interface
        public IZetboxContext DebuggingContext { get; private set; }

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
                    OnPropertyChanged("Name");
                }
            }
        }

        public string CreatedAt { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string DisposedAt { get; private set; }

        internal void Disposed()
        {
            IDebuggingZetboxContext dbgCtx = DebuggingContext as IDebuggingZetboxContext;
            if (dbgCtx != null && dbgCtx.DisposedAt != null)
            {
                DisposedAt = String.Join(String.Empty, dbgCtx.DisposedAt.GetFrames().Take(10).Select(sf => sf.ToString()).ToArray());
                OnPropertyChanged("DisposedAt");
            }

            // Release to GC
            DebuggingContext = null;
        }


        #endregion

        #region Utilities and UI callbacks

        /// <summary>
        /// Update the model's state when the context is changed
        /// </summary>
        /// is only called by the <see cref="ZetboxContextDebuggerViewModel"/>
        internal void OnContextChanged()
        {
            if (DebuggingContext == null) return;

            var objs = DebuggingContext.AttachedObjects.OfType<IDataObject>().ToArray();

            _objCache.Clear();
            objs.ForEach(o => _objCache.Add(string.Format("({0}) {1}", o.ID, DebuggingContext.GetInterfaceType(o).Type.FullName)));

            Count = objs.Length;
        }
        #endregion

        public string Name
        {
            get { return string.Format("{0}, Items: {1}", CreatedOn, Count); }
        }
    }
}
