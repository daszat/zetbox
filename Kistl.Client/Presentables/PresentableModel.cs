using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    public enum ModelState
    {
        Loading,
        Active,
        Invalid,
    }

    /// <summary>
    /// A base class for implementing the PresentableModel pattern. This class proxies the actual
    /// data model into a non-blocking, view-state holding entity. Unless noted differently, members
    /// are not thread-safe and may only be called from the UI thread.
    /// </summary>
    /// See http://blogs.msdn.com/dancre/archive/2006/10/11/datamodel-view-viewmodel-pattern-series.aspx
    public abstract class PresentableModel : INotifyPropertyChanged
    {
        /// <summary>
        /// This application's global context
        /// </summary>
        protected IGuiApplicationContext AppContext { get; private set; }

        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        protected IThreadManager UI { get { return AppContext.UiThread; } }
        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        protected IThreadManager Async { get { return AppContext.AsyncThread; } }

        /// <summary>
        /// A read-only <see cref="IKistlContext"/> to access meta data
        /// </summary>
        protected IKistlContext GuiContext { get { return AppContext.FrozenContext; } }

        /// <summary>
        /// The factory from where new models should be created
        /// </summary>
        public ModelFactory Factory { get { return AppContext.Factory; } }

        /// <summary>
        /// A <see cref="IKistlContext"/> to access the current user's data
        /// </summary>
        protected IKistlContext DataContext { get; private set; }

        /// <param name="dataCtx">The <see cref="IKistlContext"/> to use to access the current user's data session</param>
        public PresentableModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
        {
            IsInDesignMode = false;

            AppContext = appCtx;

            UI.Verify();

            DataContext = dataCtx;
        }

        #region Public interface

        private ModelState _State = ModelState.Loading;
        public ModelState State
        {
            get
            {
                UI.Verify();
                return _State;
            }
            protected set
            {
                UI.Verify();
                if (value != _State)
                {
                    _State = value;
                    OnPropertyChanged("State");
                }
            }
        }


        #endregion

        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler _PropertyChangedEvent;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                UI.Verify();
                _PropertyChangedEvent += value;
            }
            remove
            {
                UI.Verify();
                _PropertyChangedEvent -= value;
            }
        }

        /// <summary>
        /// Delegate a property change notification to the UI thread.
        /// </summary>
        /// <param name="propertyName">the changed property</param>
        protected void AsyncOnPropertyChanged(string propertyName)
        {
            Async.Verify();
            UI.Queue(UI, () => OnPropertyChanged(propertyName));
        }

        /// <summary>
        /// Notifies all listeners of PropertyChanged about a change in a property
        /// </summary>
        /// <param name="propertyName">the changed property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            UI.Verify();
            if (_PropertyChangedEvent != null)
                _PropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Design Mode 
        
        /// <summary>
        /// Creates a PresentableModel in "design" mode
        /// </summary>
        /// <param name="designMode">always must be true</param>
        /// <seealso cref="IsInDesignMode"/>
        protected PresentableModel(bool designMode)
        {
            if (!designMode)
            {
                throw new ArgumentOutOfRangeException("designMode", "always has to be true");
            }

            IsInDesignMode = true;
            AppContext = new DesignApplicationContext();
            State = ModelState.Active;
        }

        /// <summary>
        /// Signifies that a model is in "design" mode or really accessing the data store.
        /// </summary>
        /// In design mode, no data store is used and only mock data is shown. 
        /// No <see cref="IKistlContext"/>s or <see cref="IThreadManager"/>s are available.
        public bool IsInDesignMode { get; private set; }

        #endregion

    }

    internal class DesignApplicationContext : IGuiApplicationContext
    {
        #region IGuiApplicationContext Members

        public IKistlContext FrozenContext
        {
            get { throw new InvalidOperationException("No data access allowed in Design mode"); }
        }

        public IKistlContext GuiDataContext
        {
            get { throw new InvalidOperationException("No data access operations allowed in Design mode"); }
        }

        public ModelFactory Factory
        {
            get { throw new NotImplementedException(); }
        }

        private IThreadManager _thread = new SynchronousThreadManager();
        public IThreadManager UiThread
        {
            get { return _thread; }
        }

        public IThreadManager AsyncThread
        {
            get { throw new InvalidOperationException("No asynchronous operations allowed in Design mode"); }
        }

        public Kistl.API.Configuration.KistlConfig Configuration
        {
            get { throw new InvalidOperationException("No asynchronous operations allowed in Design mode"); }
        }

        #endregion
    }

}
