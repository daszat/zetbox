using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.API.Configuration;

namespace Kistl.Client.Presentables
{
    public class DataTypeModel : DataObjectModel
    {
        public new delegate DataTypeModel Factory(IKistlContext dataCtx, DataType dt);

        public DataTypeModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            DataType dt)
            : base(appCtx, config, dataCtx, dt)
        {
            _dataType = dt;
        }
        private DataType _dataType;
    }
}
