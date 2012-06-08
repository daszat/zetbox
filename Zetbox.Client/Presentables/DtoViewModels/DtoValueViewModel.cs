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

namespace Zetbox.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;

    public class DtoValueViewModel : DtoBaseViewModel
    {
        public DtoValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
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
