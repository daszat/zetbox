
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
		internal new static void CreateInstances()
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
			DataStore[11].ParameterName = @"TestCLRObjectParameter";
			DataStore[11].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[83];
			DataStore[11].IsList = false;
			DataStore[11].IsReturnParameter = false;
			DataStore[11].Description = null;
			DataStore[11].Assembly = null;
			DataStore[11].FullTypeName = @"System.Guid";
			DataStore[11].Seal();
			DataStore[16].ParameterName = @"obj";
			DataStore[16].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[97];
			DataStore[16].IsList = false;
			DataStore[16].IsReturnParameter = false;
			DataStore[16].Description = null;
			DataStore[16].Assembly = null;
			DataStore[16].FullTypeName = @"Kistl.API.IDataObject";
			DataStore[16].Seal();
			DataStore[18].ParameterName = @"ctx";
			DataStore[18].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[98];
			DataStore[18].IsList = false;
			DataStore[18].IsReturnParameter = false;
			DataStore[18].Description = null;
			DataStore[18].Assembly = null;
			DataStore[18].FullTypeName = @"Kistl.API.IKistlContext";
			DataStore[18].Seal();
			DataStore[19].ParameterName = @"objectType";
			DataStore[19].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[98];
			DataStore[19].IsList = false;
			DataStore[19].IsReturnParameter = false;
			DataStore[19].Description = null;
			DataStore[19].Assembly = null;
			DataStore[19].FullTypeName = @"System.Type";
			DataStore[19].Seal();
			DataStore[20].ParameterName = @"result";
			DataStore[20].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[98];
			DataStore[20].IsList = false;
			DataStore[20].IsReturnParameter = true;
			DataStore[20].Description = null;
			DataStore[20].Assembly = null;
			DataStore[20].FullTypeName = @"Kistl.API.IDataObject";
			DataStore[20].Seal();
			DataStore[21].ParameterName = @"ReturnParameter";
			DataStore[21].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[118];
			DataStore[21].IsList = false;
			DataStore[21].IsReturnParameter = true;
			DataStore[21].Description = null;
			DataStore[21].Assembly = null;
			DataStore[21].FullTypeName = @"System.Type";
			DataStore[21].Seal();
			DataStore[24].ParameterName = @"ReturnParameter";
			DataStore[24].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[121];
			DataStore[24].IsList = false;
			DataStore[24].IsReturnParameter = true;
			DataStore[24].Description = null;
			DataStore[24].Assembly = null;
			DataStore[24].FullTypeName = @"System.Type";
			DataStore[24].Seal();
			DataStore[25].ParameterName = @"ReturnParameter";
			DataStore[25].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[123];
			DataStore[25].IsList = false;
			DataStore[25].IsReturnParameter = true;
			DataStore[25].Description = null;
			DataStore[25].Assembly = null;
			DataStore[25].FullTypeName = @"System.Type";
			DataStore[25].Seal();
			DataStore[28].ParameterName = @"constrainedValue";
			DataStore[28].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[28].IsList = false;
			DataStore[28].IsReturnParameter = false;
			DataStore[28].Description = null;
			DataStore[28].Assembly = null;
			DataStore[28].FullTypeName = @"System.Object";
			DataStore[28].Seal();
			DataStore[31].ParameterName = @"constrainedValue";
			DataStore[31].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[31].IsList = false;
			DataStore[31].IsReturnParameter = false;
			DataStore[31].Description = null;
			DataStore[31].Assembly = null;
			DataStore[31].FullTypeName = @"System.Object";
			DataStore[31].Seal();
			DataStore[34].ParameterName = @"constrainedObj";
			DataStore[34].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[135];
			DataStore[34].IsList = false;
			DataStore[34].IsReturnParameter = false;
			DataStore[34].Description = null;
			DataStore[34].Assembly = null;
			DataStore[34].FullTypeName = @"System.Object";
			DataStore[34].Seal();
			DataStore[35].ParameterName = @"constrainedObject";
			DataStore[35].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[139];
			DataStore[35].IsList = false;
			DataStore[35].IsReturnParameter = false;
			DataStore[35].Description = null;
			DataStore[35].Assembly = null;
			DataStore[35].FullTypeName = @"System.Object";
			DataStore[35].Seal();
			DataStore[37].ParameterName = @"return";
			DataStore[37].Method = Kistl.App.Base.Method__Implementation__Frozen.DataStore[151];
			DataStore[37].IsList = false;
			DataStore[37].IsReturnParameter = true;
			DataStore[37].Description = @"the referenced Type";
			DataStore[37].Assembly = null;
			DataStore[37].FullTypeName = @"System.Type";
			DataStore[37].Seal();
	
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