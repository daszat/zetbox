
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
    
		public Constraint__Implementation__Frozen()
		{
        }


        /// <summary>
        /// The property to be constrained
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Property ConstrainedProperty
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
					var __oldValue = _ConstrainedProperty;
                    NotifyPropertyChanging("ConstrainedProperty", __oldValue, value);
                    _ConstrainedProperty = value;
                    NotifyPropertyChanged("ConstrainedProperty", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Property _ConstrainedProperty;

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
					var __oldValue = _Reason;
                    NotifyPropertyChanging("Reason", __oldValue, value);
                    _Reason = value;
                    NotifyPropertyChanged("Reason", __oldValue, value);
                }
            }
        }
        private string _Reason;

        /// <summary>
        /// 
        /// </summary>

		public virtual string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_Constraint != null)
            {
                OnGetErrorText_Constraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.GetErrorText");
            }
            return e.Result;
        }
		public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, System.Object constrainedObject, System.Object constrainedValue);
		public event GetErrorText_Handler<Constraint> OnGetErrorText_Constraint;



        /// <summary>
        /// 
        /// </summary>

		public virtual bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_Constraint != null)
            {
                OnIsValid_Constraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.IsValid");
            }
            return e.Result;
        }
		public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, System.Object constrainedObject, System.Object constrainedValue);
		public event IsValid_Handler<Constraint> OnIsValid_Constraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Constraint));
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ConstrainedProperty":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(171).Constraints
						.Where(c => !c.IsValid(this, this.ConstrainedProperty))
						.Select(c => c.GetErrorText(this, this.ConstrainedProperty))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Reason":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(167).Constraints
						.Where(c => !c.IsValid(this, this.Reason))
						.Select(c => c.GetErrorText(this, this.Reason))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
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
        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}