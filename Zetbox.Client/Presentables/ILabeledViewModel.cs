
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.GUI;

    public interface ILabeledViewModel
        : INotifyPropertyChanged
    {
        string Label { get; }
        string ToolTip { get; }
        ControlKind RequestedKind { get; }
        ViewModel Model { get; }
        bool Required { get; }
    }

    // No Viewdescriptor, code only
    public class LabeledViewContainerViewModel
        : ViewModel, ILabeledViewModel
    {
        public new delegate LabeledViewContainerViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string label, string toolTip, ViewModel mdl);

        public LabeledViewContainerViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string label, string toolTip, ViewModel mdl)
            : base(dependencies, dataCtx, parent)
        {
            this._Label = label;
            this._ToolTip = toolTip;
            this.Model = mdl;
        }

        #region ILabeledViewModel Members

        private string _Label;
        public string Label
        {
            get { return _Label; }
            set { _Label = value; OnPropertyChanged("Label"); }
        }

        private string _ToolTip;
        public string ToolTip
        {
            get { return _ToolTip; }
            set { _ToolTip = value; OnPropertyChanged("ToolTip"); }
        }

        private bool _Required;
        public bool Required
        {
            get { return _Required; }
            set { _Required = value; OnPropertyChanged("Required"); }
        }

        public ViewModel Model
        {
            get;
            private set;
        }

        #endregion

        public override string Name
        {
            get { return _Label; }
        }
    }
}
