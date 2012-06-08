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

namespace Zetbox.Client.Presentables.Relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ValueViewModels;

    public class RelationViewModel
        : DataObjectViewModel
    {
        public new delegate RelationViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Relation rel);

        private Relation _relation;

        public RelationViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Relation rel)
            : base(appCtx, dataCtx, parent, rel)
        {
            _relation = rel;
            _relation.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region Public interface

        public RelationEndViewModel A
        {
            get
            {
                return (RelationEndViewModel)((IValueViewModel<DataObjectViewModel>)PropertyModelsByName["A"]).Value;
            }
        }

        public RelationEndViewModel B
        {
            get
            {
                return (RelationEndViewModel)((IValueViewModel<DataObjectViewModel>)PropertyModelsByName["B"]).Value;
            }
        }

        public ValueViewModel<string, string> Description
        {
            get
            {
                return (ValueViewModel<string, string>)this.PropertyModelsByName["Description"];
            }
        }

        public string NavigatorADescription
        {
            get
            {
                if (!IsFullyDefined) return string.Empty;
                return FormatNavigatorDescription(_relation.A, _relation.B.Type);
            }
        }

        public string NavigatorBDescription
        {
            get
            {
                if (!IsFullyDefined) return string.Empty;
                return FormatNavigatorDescription(_relation.B, _relation.A.Type);
            }
        }

        private static string FormatNavigatorDescription(RelationEnd relEnd, ObjectClass refType)
        {
            var rel = relEnd.Parent;
            var otherEnd = rel.GetOtherEnd(relEnd);
            var prop = relEnd.Navigator;

            if (prop == null) return string.Format("No Navigator for {0} defined", string.IsNullOrEmpty(relEnd.RoleName) ? (object)relEnd.GetRole() : relEnd.RoleName);
            if (prop.GetIsList())
            {
                return string.Format("Navigator {1}.{2} is a {3}<{0}>", refType.Name, prop.ObjectClass.Name, prop.Name, rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList" : "ICollection");
            }
            if (prop.IsNullable()) return string.Format("Navigator {1}.{2} is a nullable reference to {0}", refType.Name, prop.ObjectClass.Name, prop.Name);
            return string.Format("Navigator {1}.{2} is a required reference to {0}", refType.Name, prop.ObjectClass.Name, prop.Name);
        }

        public bool IsFullyDefined
        {
            get
            {
                return _relation.A != null && _relation.B != null && _relation.A.Type != null && _relation.B.Type != null;
            }
        }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion
    }
}
