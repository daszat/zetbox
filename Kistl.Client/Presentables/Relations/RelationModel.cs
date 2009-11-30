using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables.Relations
{

    public class RelationModel
        : DataObjectModel
    {
        private Relation _relation;

        public RelationModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            Relation rel)
            : base(appCtx, dataCtx, rel)
        {
            _relation = rel;
            _relation.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        #region Public interface

        public RelationEndModel A
        {
            get
            {
                return (RelationEndModel)Factory.CreateDefaultModel(DataContext, _relation.A);
            }
        }

        public RelationEndModel B
        {
            get
            {
                return (RelationEndModel)Factory.CreateDefaultModel(DataContext, _relation.B);
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
                return FormatNavigatorDescription(_relation.A.Navigator, _relation.B.Type);
            }
        }

        public string NavigatorBDescription
        {
            get
            {
                if (!IsFullyDefined) return string.Empty;
                return FormatNavigatorDescription(_relation.B.Navigator, _relation.A.Type);
            }
        }

        private static string FormatNavigatorDescription(ObjectReferenceProperty prop, ObjectClass refType)
        {
            if (prop == null) return "Navigator is not defined";
            if (prop.IsList()) return string.Format("Navigator {1}.{2} is a ICollection<{0}>", refType.ClassName, prop.ObjectClass.ClassName, prop.PropertyName);
            if (prop.IsNullable()) return string.Format("Navigator {1}.{2} is a nullable reference to {0}", refType.ClassName, prop.ObjectClass.ClassName, prop.PropertyName);
            return string.Format("Navigator {1}.{2} is a not nullable reference to {0}", refType.ClassName, prop.ObjectClass.ClassName, prop.PropertyName);
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
