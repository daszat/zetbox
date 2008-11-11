using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using System.Collections.ObjectModel;

namespace Kistl.Client.PresenterModel
{
    public class ObjectClassModel : DataTypeModel
    {
        public ObjectClassModel(IThreadManager uiManager, IThreadManager asyncManager, ObjectClass cls)
            : base(uiManager, asyncManager, cls)
        {
            _class = cls;
            Async.Queue(AsyncContext, AsyncQueryHasInstances);
        }

        #region Async handlers and UI callbacks

        protected override void AsyncQueryHasInstances()
        {
            Async.Verify();
            var obj = AsyncContext.GetQuery(_class.GetDataType()).FirstOrDefault();
            UI.Queue(UI, () => { HasInstances = (obj != null); State = ModelState.Active; });
        }

        protected override void AsyncLoadInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => State = ModelState.Loading);
            var objs = AsyncContext.GetQuery(_class.GetDataType()).ToList().OrderBy(obj => obj.ToString()).ToList();

            UI.Queue(UI, () =>
            {
                foreach (var obj in objs)
                {
                    Instances.Add(new DataObjectModel(UI, Async, obj));
                }
                State = ModelState.Active;
            });
        }

        #endregion

        private ObjectClass _class;
    }
}
