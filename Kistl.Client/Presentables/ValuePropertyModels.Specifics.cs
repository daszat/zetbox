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
        : ValuePropertyModel<Boolean>, IValueModel<string>
    {
        public BoolModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, BoolProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion
        
        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = Boolean.Parse(value); }
        }

        #endregion

    }

    public class NullableBoolModel
        : NullableValuePropertyModel<Boolean>, IValueModel<string>
    {
        public NullableBoolModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, BoolProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = Boolean.Parse(value); }
        }

        #endregion

    }



    public class DateTimeModel
        : ValuePropertyModel<DateTime>, IValueModel<string>
    {
        public DateTimeModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DateTimeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = DateTime.Parse(value); }
        }

        #endregion

    }

    public class NullableDateTimeModel
        : NullableValuePropertyModel<DateTime>, IValueModel<string>
    {
        public NullableDateTimeModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DateTimeProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = DateTime.Parse(value); }
        }

        #endregion

    }



    public class DoubleModel
        : ValuePropertyModel<Double>, IValueModel<string>
    {
        public DoubleModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DoubleProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = Double.Parse(value); }
        }

        #endregion

    }

    public class NullableDoubleModel
        : NullableValuePropertyModel<Double>, IValueModel<string>
    {
        public NullableDoubleModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, DoubleProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = Double.Parse(value); }
        }

        #endregion

    }



    public class IntModel
        : ValuePropertyModel<int>, IValueModel<string>
    {
        public IntModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, IntProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = int.Parse(value); }
        }

        #endregion

    }

    public class NullableIntModel
        : NullableValuePropertyModel<int>, IValueModel<string>
    {
        public NullableIntModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, IntProperty prop)
            : base(appCtx, dataCtx, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }

        #region IReadOnlyValueModel<string> Members

        string IReadOnlyValueModel<string>.Value { get { return Value.ToString(); } }

        #endregion

        #region IValueModel<string> Members

        string IValueModel<string>.Value
        {
            get { return Value.ToString(); }
            set { Value = int.Parse(value); }
        }

        #endregion

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
