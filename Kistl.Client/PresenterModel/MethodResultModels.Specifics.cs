using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class BoolResultModel
        : StructResultModel<Boolean>
    {
        public BoolResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx, 
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.Parameter.Single().IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableBoolResultModel
        : NullableResultModel<Boolean>
    {
        public NullableBoolResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx, 
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }



    public class DateTimeResultModel
        : StructResultModel<DateTime>
    {
        public DateTimeResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx, 
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableDateTimeResultModel
        : NullableResultModel<DateTime>
    {
        public NullableDateTimeResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx, 
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }



    public class DoubleResultModel
        : StructResultModel<Double>
    {
        public DoubleResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableDoubleResultModel
        : NullableResultModel<Double>
    {
        public NullableDoubleResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }



    public class IntResultModel
        : StructResultModel<int>
    {
        public IntResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableIntResultModel
        : NullableResultModel<int>
    {
        public NullableIntResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
            //  Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }

    public class StringResultModel
        : ObjectResultModel<String>
    {
        public StringResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
        }
    }

    public class DataObjectResultModel
        : ObjectResultModel<IDataObject>
    {
        public DataObjectResultModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(uiManager, asyncManager, guiCtx, dataCtx, obj, m)
        {
        }
    }
}
