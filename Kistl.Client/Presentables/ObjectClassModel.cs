using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Configuration;

namespace Kistl.Client.Presentables
{
    public class ObjectClassModel : DataTypeModel
    {
        public new delegate ObjectClassModel Factory(IKistlContext dataCtx, ObjectClass cls);

        public ObjectClassModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            ObjectClass cls,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, config, dataCtx, cls, ctxFactory)
        {
            _class = cls;
        }

        public InterfaceType GetDescribedInterfaceType()
        {
            return _class.GetDescribedInterfaceType();
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
                Instances.Add(ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj));
            }
        }

        #endregion

        private ObjectClass _class;
    }
}
