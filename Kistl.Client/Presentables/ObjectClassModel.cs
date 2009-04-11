using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Client.GUI.DB;

namespace Kistl.Client.Presentables
{
    public class ObjectClassModel : DataTypeModel
    {
        public ObjectClassModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            ObjectClass cls)
            : base(appCtx, dataCtx, cls)
        {
            _class = cls;
        }

        #region Utilities and UI callbacks

        protected override void QueryHasInstances()
        {
            var obj = DataContext.GetQuery(_class.GetDescribedInterfaceType()).FirstOrDefault();
            HasInstances = (obj != null);
        }

        protected override void LoadInstances()
        {
            foreach (var obj in DataContext.GetQuery(_class.GetDescribedInterfaceType()).ToList().OrderBy(obj => obj.ToString()))
            {
                Instances.Add((DataObjectModel)Factory.CreateDefaultModel(DataContext, obj));
            }
        }

        #endregion

        private ObjectClass _class;
    }
}
