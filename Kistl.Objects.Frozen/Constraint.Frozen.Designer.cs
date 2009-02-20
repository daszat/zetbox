
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
    [System.Diagnostics.DebuggerDisplay("Constraint")]
    public class Constraint__Implementation__Frozen : BaseFrozenDataObject, Constraint
    {


        /// <summary>
        /// The property to be constrained
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.BaseProperty ConstrainedProperty
        {
            get
            {
                return _ConstrainedProperty;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ConstrainedProperty != value)
                {
                    NotifyPropertyChanging("ConstrainedProperty");
                    _ConstrainedProperty = value;
                    NotifyPropertyChanged("ConstrainedProperty");
                }
            }
        }
        private Kistl.App.Base.BaseProperty _ConstrainedProperty;

        /// <summary>
        /// The reason of this constraint
        /// </summary>
        // value type property
        public virtual string Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Reason != value)
                {
                    NotifyPropertyChanging("Reason");
                    _Reason = value;
                    NotifyPropertyChanged("Reason");
                }
            }
        }
        private string _Reason;

        /// <summary>
        /// 
        /// </summary>

		public virtual string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_Constraint != null)
            {
                OnGetErrorText_Constraint(this, e, constrainedValue, constrainedObject);
            };
            return e.Result;
        }
		public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, System.Object constrainedValue, System.Object constrainedObject);
		public event GetErrorText_Handler<Constraint> OnGetErrorText_Constraint;



        /// <summary>
        /// 
        /// </summary>

		public virtual bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_Constraint != null)
            {
                OnIsValid_Constraint(this, e, constrainedValue, constrainedObj);
            };
            return e.Result;
        }
		public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, System.Object constrainedValue, System.Object constrainedObj);
		public event IsValid_Handler<Constraint> OnIsValid_Constraint;



		public override Type GetInterfaceType()
		{
			return typeof(Constraint);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Constraint != null)
            {
                OnToString_Constraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Constraint> OnToString_Constraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Constraint != null) OnPreSave_Constraint(this);
        }
        public event ObjectEventHandler<Constraint> OnPreSave_Constraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Constraint != null) OnPostSave_Constraint(this);
        }
        public event ObjectEventHandler<Constraint> OnPostSave_Constraint;


        internal Constraint__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Constraint__Implementation__Frozen> DataStore = new Dictionary<int, Constraint__Implementation__Frozen>(0);
		internal static void CreateInstances()
		{
		}

		internal static void FillDataStore() {
	
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