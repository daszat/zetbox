using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public class BoolResultModel
        : StructResultModel<Boolean>
    {
        public BoolResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.Parameter.Single().IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableBoolResultModel
        : NullableResultModel<Boolean>
    {
        public NullableBoolResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }

    public class DateTimeResultModel
        : StructResultModel<DateTime>
    {
        public DateTimeResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableDateTimeResultModel
        : NullableResultModel<DateTime>
    {
        public NullableDateTimeResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }

    public class DoubleResultModel
        : StructResultModel<Double>
    {
        public DoubleResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableDoubleResultModel
        : NullableResultModel<Double>
    {
        public NullableDoubleResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }

    public class IntResultModel
        : StructResultModel<int>
    {
        public IntResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            // Debug.Assert(!m.IsNullable, "cannot handle nullable parameter");
        }
    }

    public class NullableIntResultModel
        : NullableResultModel<int>
    {
        public NullableIntResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
            //  Debug.Assert(m.IsNullable, "can only handle nullable parameter");
        }
    }

    public class StringResultModel
        : ObjectResultModel<String>
    {
        public StringResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
        }
    }

    public class DataObjectResultModel
        : ObjectResultModel<IDataObject>
    {
        public DataObjectResultModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, obj, m)
        {
        }
    }
}
