using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Kistl.Client.PresenterModel
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
        private ModelState _State = ModelState.Loading;
        /// <summary>
        /// A <see cref="IThreadManager"/> for the UI Thread
        /// </summary>
        protected IThreadManager UI { get; private set; }
        /// <summary>
        /// A <see cref="IThreadManager"/> for asynchronous Tasks
        /// </summary>
        protected IThreadManager Async { get; private set; }

        public PresentableModel(IThreadManager uiManager, IThreadManager asyncManager)
        {
            Async = asyncManager;
            UI = uiManager;
            UI.Verify();
        }

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

    }
}
