using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Configuration;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.Presentables.Relations
{

    public class RelationModel
        : DataObjectModel
    {
        public new delegate RelationModel Factory(IKistlContext dataCtx, Relation rel);

        private Relation _relation;

        public RelationModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            Relation rel)
            : base(appCtx, config, dataCtx, rel)
        {
            _relation = rel;
            _relation.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region Public interface

        public RelationEndModel A
        {
            get
            {
                return (RelationEndModel)((IValueViewModel<DataObjectModel>)PropertyModelsByName["A"]).Value;
            }
        }

        public RelationEndModel B
        {
            get
            {
                return (RelationEndModel)((IValueViewModel<DataObjectModel>)PropertyModelsByName["B"]).Value;
            }
        }

        public PropertyModel<string> Description
        {
            get
            {
                return (PropertyModel<string>)this.PropertyModelsByName["Description"];
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
