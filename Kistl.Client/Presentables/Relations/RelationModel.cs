using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

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

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }

}
