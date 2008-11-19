using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables
{
    public class BoolModel
        : ValuePropertyModel<Boolean>
    {
        public BoolModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, BoolProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableBoolModel
        : NullableValuePropertyModel<Boolean>
    {
        public NullableBoolModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, BoolProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class DateTimeModel
        : ValuePropertyModel<DateTime>
    {
        public DateTimeModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DateTimeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableDateTimeModel
        : NullableValuePropertyModel<DateTime>
    {
        public NullableDateTimeModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DateTimeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class DoubleModel
              : ValuePropertyModel<Double>
    {
        public DoubleModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DoubleProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableDoubleModel
        : NullableValuePropertyModel<Double>
    {
        public NullableDoubleModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DoubleProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class IntModel
        : ValuePropertyModel<int>
    {
        public IntModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, IntProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableIntModel
        : NullableValuePropertyModel<int>
    {
        public NullableIntModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, IntProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class StringModel
        : ReferencePropertyModel<String>
    {
        public StringModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, StringProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
        }
    }





}
