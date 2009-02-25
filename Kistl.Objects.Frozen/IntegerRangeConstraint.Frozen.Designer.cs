
namespace Kistl.App.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("IntegerRangeConstraint")]
    public class IntegerRangeConstraint__Implementation__Frozen : Kistl.App.Base.Constraint__Implementation__Frozen, IntegerRangeConstraint
    {


        /// <summary>
        /// The biggest value accepted by this constraint
        /// </summary>
        // value type property
        public virtual int Max
        {
            get
            {
                return _Max;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Max != value)
                {
                    NotifyPropertyChanging("Max");
                    _Max = value;
                    NotifyPropertyChanged("Max");
                }
            }
        }
        private int _Max;

        /// <summary>
        /// The smallest value accepted by this constraint
        /// </summary>
        // value type property
        public virtual int Min
        {
            get
            {
                return _Min;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Min != value)
                {
                    NotifyPropertyChanging("Min");
                    _Min = value;
                    NotifyPropertyChanged("Min");
                }
            }
        }
        private int _Min;

        /// <summary>
        /// 
        /// </summary>

		public override string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_IntegerRangeConstraint != null)
            {
                OnGetErrorText_IntegerRangeConstraint(this, e, constrainedValue, constrainedObject);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedValue, constrainedObject);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<IntegerRangeConstraint> OnGetErrorText_IntegerRangeConstraint;



        /// <summary>
        /// 
        /// </summary>

		public override bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_IntegerRangeConstraint != null)
            {
                OnIsValid_IntegerRangeConstraint(this, e, constrainedValue, constrainedObj);
            }
            else
            {
                e.Result = base.IsValid(constrainedValue, constrainedObj);
            }
            return e.Result;
        }
		public event IsValid_Handler<IntegerRangeConstraint> OnIsValid_IntegerRangeConstraint;



		public override Type GetInterfaceType()
		{
			return typeof(IntegerRangeConstraint);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_IntegerRangeConstraint != null)
            {
                OnToString_IntegerRangeConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<IntegerRangeConstraint> OnToString_IntegerRangeConstraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_IntegerRangeConstraint != null) OnPreSave_IntegerRangeConstraint(this);
        }
        public event ObjectEventHandler<IntegerRangeConstraint> OnPreSave_IntegerRangeConstraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_IntegerRangeConstraint != null) OnPostSave_IntegerRangeConstraint(this);
        }
        public event ObjectEventHandler<IntegerRangeConstraint> OnPostSave_IntegerRangeConstraint;


        internal IntegerRangeConstraint__Implementation__Frozen(int id)
            : base(id)
        { }


		internal new static Dictionary<int, IntegerRangeConstraint__Implementation__Frozen> DataStore = new Dictionary<int, IntegerRangeConstraint__Implementation__Frozen>(3);
		internal new static void CreateInstances()
		{
			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[147] = 
			DataStore[147] = new IntegerRangeConstraint__Implementation__Frozen(147);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[148] = 
			DataStore[148] = new IntegerRangeConstraint__Implementation__Frozen(148);

			Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[149] = 
			DataStore[149] = new IntegerRangeConstraint__Implementation__Frozen(149);

		}

		internal new static void FillDataStore() {
			DataStore[147].Reason = @"Strings have to have at least one character.";
			DataStore[147].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[28];
			DataStore[147].Max = 4000;
			DataStore[147].Min = 1;
			DataStore[147].Seal();
			DataStore[148].Reason = @"strings in the database should not be longer than 4k";
			DataStore[148].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[172];
			DataStore[148].Max = 4000;
			DataStore[148].Min = 0;
			DataStore[148].Seal();
			DataStore[149].Reason = @"strings in the database should not be longer than 4k";
			DataStore[149].ConstrainedProperty = Kistl.App.Base.BaseProperty__Implementation__Frozen.DataStore[173];
			DataStore[149].Max = 4000;
			DataStore[149].Min = 0;
			DataStore[149].Seal();
	
		}

#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}