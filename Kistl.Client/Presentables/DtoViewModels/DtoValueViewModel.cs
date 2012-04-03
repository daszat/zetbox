
namespace Kistl.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;

    public class DtoValueViewModel : DtoBaseViewModel
    {
        public DtoValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IFileOpener fileOpener, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, debugInfo)
        {
        }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        private ContentAlignment _valueAlignment;
        public ContentAlignment ValueAlignment
        {
            get
            {
                return _valueAlignment;
            }
            set
            {
                if (_valueAlignment != value)
                {
                    _valueAlignment = value;
                    OnPropertyChanged("ValueAlignment");
                }
            }
        }

        private string _alternateRepresentation;
        public string AlternateRepresentation
        {
            get
            {
                return _alternateRepresentation;
            }
            set
            {
                if (_alternateRepresentation != value)
                {
                    _alternateRepresentation = value;
                    OnPropertyChanged("AlternateRepresentation");
                }
            }
        }


        private ContentAlignment _alternateRepresentationAlignment;
        public ContentAlignment AlternateRepresentationAlignment
        {
            get
            {
                return _alternateRepresentationAlignment;
            }
            set
            {
                if (_alternateRepresentationAlignment != value)
                {
                    _alternateRepresentationAlignment = value;
                    OnPropertyChanged("AlternateRepresentationAlignment");
                }
            }
        }

        private string _toolTip;
        public string ToolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                if (_toolTip != value)
                {
                    _toolTip = value;
                    OnPropertyChanged("ToolTip");
                }
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(AlternateRepresentation))
            {
                return Value;
            }
            else
            {
                return string.Format("{0} ({1})", Value, AlternateRepresentation);
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoValueViewModel;

            this.AlternateRepresentation = other.AlternateRepresentation;
            this.AlternateRepresentationAlignment = other.AlternateRepresentationAlignment;
            this.ToolTip = other.ToolTip;
            this.Value = other.Value;
            this.ValueAlignment = other.ValueAlignment;
        }
    }
}
