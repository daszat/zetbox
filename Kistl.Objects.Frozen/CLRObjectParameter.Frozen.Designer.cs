
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
    /// Metadefinition Object for CLR Object Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CLRObjectParameter")]
    public class CLRObjectParameter__Implementation__Frozen : Kistl.App.Base.BaseParameter__Implementation__Frozen, CLRObjectParameter
    {


        /// <summary>
        /// Assembly des CLR Objektes, NULL für Default Assemblies
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return _Assembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Assembly != value)
                {
                    NotifyPropertyChanging("Assembly");
                    _Assembly = value;
                    NotifyPropertyChanged("Assembly");;
                }
            }
        }
        private Kistl.App.Base.Assembly _Assembly;

        /// <summary>
        /// Name des CLR Datentypen
        /// </summary>
        // value type property
        public virtual string FullTypeName
        {
            get
            {
                return _FullTypeName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_FullTypeName != value)
                {
                    NotifyPropertyChanging("FullTypeName");
                    _FullTypeName = value;
                    NotifyPropertyChanged("FullTypeName");;
                }
            }
        }
        private string _FullTypeName;

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_CLRObjectParameter != null)
            {
                OnGetParameterTypeString_CLRObjectParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterTypeString_Handler<CLRObjectParameter> OnGetParameterTypeString_CLRObjectParameter;



        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_CLRObjectParameter != null)
            {
                OnGetParameterType_CLRObjectParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterType_Handler<CLRObjectParameter> OnGetParameterType_CLRObjectParameter;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CLRObjectParameter != null)
            {
                OnToString_CLRObjectParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<CLRObjectParameter> OnToString_CLRObjectParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CLRObjectParameter != null) OnPreSave_CLRObjectParameter(this);
        }
        public event ObjectEventHandler<CLRObjectParameter> OnPreSave_CLRObjectParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CLRObjectParameter != null) OnPostSave_CLRObjectParameter(this);
        }
        public event ObjectEventHandler<CLRObjectParameter> OnPostSave_CLRObjectParameter;


        internal CLRObjectParameter__Implementation__Frozen(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


		internal new static Dictionary<int, CLRObjectParameter__Implementation__Frozen> DataStore = new Dictionary<int, CLRObjectParameter__Implementation__Frozen>(13);
		static CLRObjectParameter__Implementation__Frozen()
		{
			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[11] = 
			DataStore[11] = new CLRObjectParameter__Implementation__Frozen(null, 11);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[16] = 
			DataStore[16] = new CLRObjectParameter__Implementation__Frozen(null, 16);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[18] = 
			DataStore[18] = new CLRObjectParameter__Implementation__Frozen(null, 18);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[19] = 
			DataStore[19] = new CLRObjectParameter__Implementation__Frozen(null, 19);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[20] = 
			DataStore[20] = new CLRObjectParameter__Implementation__Frozen(null, 20);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[21] = 
			DataStore[21] = new CLRObjectParameter__Implementation__Frozen(null, 21);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[24] = 
			DataStore[24] = new CLRObjectParameter__Implementation__Frozen(null, 24);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[25] = 
			DataStore[25] = new CLRObjectParameter__Implementation__Frozen(null, 25);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[28] = 
			DataStore[28] = new CLRObjectParameter__Implementation__Frozen(null, 28);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[31] = 
			DataStore[31] = new CLRObjectParameter__Implementation__Frozen(null, 31);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[34] = 
			DataStore[34] = new CLRObjectParameter__Implementation__Frozen(null, 34);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[35] = 
			DataStore[35] = new CLRObjectParameter__Implementation__Frozen(null, 35);

			Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[37] = 
			DataStore[37] = new CLRObjectParameter__Implementation__Frozen(null, 37);

		}

		internal new static void FillDataStore() {
			DataStore[11].Assembly = null;
			DataStore[11].FullTypeName = @"System.Guid";
			DataStore[16].Assembly = null;
			DataStore[16].FullTypeName = @"Kistl.API.IDataObject";
			DataStore[18].Assembly = null;
			DataStore[18].FullTypeName = @"Kistl.API.IKistlContext";
			DataStore[19].Assembly = null;
			DataStore[19].FullTypeName = @"System.Type";
			DataStore[20].Assembly = null;
			DataStore[20].FullTypeName = @"Kistl.API.IDataObject";
			DataStore[21].Assembly = null;
			DataStore[21].FullTypeName = @"System.Type";
			DataStore[24].Assembly = null;
			DataStore[24].FullTypeName = @"System.Type";
			DataStore[25].Assembly = null;
			DataStore[25].FullTypeName = @"System.Type";
			DataStore[28].Assembly = null;
			DataStore[28].FullTypeName = @"System.Object";
			DataStore[31].Assembly = null;
			DataStore[31].FullTypeName = @"System.Object";
			DataStore[34].Assembly = null;
			DataStore[34].FullTypeName = @"System.Object";
			DataStore[35].Assembly = null;
			DataStore[35].FullTypeName = @"System.Object";
			DataStore[37].Assembly = null;
			DataStore[37].FullTypeName = @"System.Type";
	
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