using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public class ObjectClassModel : DataTypeModel
    {
        public ObjectClassModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            ObjectClass cls)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, cls)
        {
            _class = cls;
            Async.Queue(DataContext, AsyncQueryHasInstances);
        }

        #region Async handlers and UI callbacks

        protected override void AsyncQueryHasInstances()
        {
            Async.Verify();
            var obj = DataContext.GetQuery(_class.GetDataType()).FirstOrDefault();
            UI.Queue(UI, () => { HasInstances = (obj != null); State = ModelState.Active; });
        }

        protected override void AsyncLoadInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => State = ModelState.Loading);
            var objs = DataContext.GetQuery(_class.GetDataType()).ToList().OrderBy(obj => obj.ToString()).ToList();

            UI.Queue(UI, () =>
            {
                foreach (var obj in objs)
                {
                    // TODO: search for existing DOModel
                    Instances.Add(Factory.CreateModel<DataObjectModel>(obj));
                }
                State = ModelState.Active;
            });
        }

        #endregion

        private ObjectClass _class;
    }
}
