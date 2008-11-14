using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class BoolModel
        : ValuePropertyModel<Boolean>
    {
        public BoolModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, BoolProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableBoolModel
        : NullableValuePropertyModel<Boolean>
    {
        public NullableBoolModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, BoolProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class DateTimeModel
        : ValuePropertyModel<DateTime>
    {
        public DateTimeModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, DateTimeProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableDateTimeModel
        : NullableValuePropertyModel<DateTime>
    {
        public NullableDateTimeModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, DateTimeProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class DoubleModel
              : ValuePropertyModel<Double>
    {
        public DoubleModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, DoubleProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableDoubleModel
        : NullableValuePropertyModel<Double>
    {
        public NullableDoubleModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, DoubleProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class IntModel
        : ValuePropertyModel<int>
    {
        public IntModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, IntProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(!prop.IsNullable, "cannot handle nullable properties");
        }
    }

    public class NullableIntModel
        : NullableValuePropertyModel<int>
    {
        public NullableIntModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, IntProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
            Debug.Assert(prop.IsNullable, "can only handle nullable properties");
        }
    }



    public class StringModel
        : ReferencePropertyModel<String>
    {
        public StringModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IDataObject obj, StringProperty prop)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, obj, prop)
        {
        }
    }





}
