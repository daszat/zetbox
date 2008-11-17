using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables
{
    public interface ICommand
    {
        bool CanExecute { get; }
        event EventHandler CanExecuteChanged;
        void Execute(object data);

        string Label { get; }
        string ToolTip { get; }
    }

    public abstract class CommandModel : PresentableModel, ICommand
    {
        public CommandModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory)
        {
        }

        #region ICommand Members

        private bool _canExecuteCache = false;
        public bool CanExecute
        {
            get { UI.Verify(); return _canExecuteCache; }
            protected set
            {
                UI.Verify();
                if (_canExecuteCache != value)
                {
                    _canExecuteCache = value;
                    OnCanExecuteChanged();
                    OnPropertyChanged("CanExecute");
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object data);

        private string _labelCache;
        public string Label
        {
            get { UI.Verify(); return _labelCache; }
            protected set
            {
                UI.Verify();
                if (_labelCache != value)
                {
                    _labelCache = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        private string _toolTipCache;
        public string ToolTip
        {
            get { UI.Verify(); return _toolTipCache; }
            protected set
            {
                UI.Verify();
                if (_toolTipCache != value)
                {
                    _toolTipCache = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        #endregion

        private void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

    }

}
