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
        protected IKistlContext MetaContext { get { return AppContext.MetaContext; } }

        /// <summary>
        /// The factory from where new models should be created
        /// </summary>
        public ModelFactory Factory { get { return AppContext.Factory; } }

        /// <summary>
        /// A <see cref="IKistlContext"/> to access the current user's data
        /// </summary>
        protected IKistlContext DataContext { get; private set; }

        /// <param name="appCtx">The <see cref="IGuiApplicationContext"/> to access the current application context</param>
        /// <param name="dataCtx">The <see cref="IKistlContext"/> to access the current user's data session</param>
        protected PresentableModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
        {
            IsInDesignMode = false;

            AppContext = appCtx;

            DataContext = dataCtx;
        }

        #region Public interface

        private ModelState _State = ModelState.Active;
        public ModelState State
        {
            get
            {
                return _State;
            }
            internal set
            {
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

        public IKistlContext GuiDataContext
        {
            get { throw new InvalidOperationException("No data access allowed in Design mode"); }
        }

        public IKistlContext MetaContext
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
