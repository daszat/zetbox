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
            ObjectClass cls)
            : base(appCtx, config, dataCtx, cls)
        {
            _class = cls;
        }

        public InterfaceType GetDescribedInterfaceType()
        {
            return _class.GetDescribedInterfaceType();
        }

        private ObjectClass _class;

        public override Kistl.App.GUI.Icon Icon
        {
            get { return _class.DefaultIcon; }
        }
    }
}
