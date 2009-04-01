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
        }

        #region Public interface

        public RelationEndModel A { get; private set; }

        public RelationEndModel B { get; private set; }

        public PropertyModel<string> Description { get; private set; }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }

}
