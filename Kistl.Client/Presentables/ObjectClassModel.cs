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

        #region Async handlers and UI callbacks

        protected override void AsyncQueryHasInstances()
        {
            Async.Verify();
            var obj = DataContext.GetQuery(_class.GetDescribedInterfaceType()).FirstOrDefault();
            UI.Queue(UI, () => { HasInstances = (obj != null); State = ModelState.Active; });
        }

        protected override void AsyncLoadInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => State = ModelState.Loading);
            var objs = DataContext.GetQuery(_class.GetDescribedInterfaceType()).ToList().OrderBy(obj => obj.ToString()).ToList();

            UI.Queue(UI, () =>
            {
                foreach (var obj in objs)
                {
                    var mdlDesc = DataMocks.LookupDefaultModelDescriptor(obj);
                    Instances.Add((DataObjectModel)Factory.CreateModel(
                        mdlDesc,
                        DataContext,
                       new object[] { obj }));
                }
                State = ModelState.Active;
            });
        }

        #endregion

        private ObjectClass _class;
    }
}
