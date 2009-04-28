
namespace Kistl.App.GUI
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
    [System.Diagnostics.DebuggerDisplay("PresentableModelDescriptor")]
    public class PresentableModelDescriptor__Implementation__Frozen : BaseFrozenDataObject, PresentableModelDescriptor
    {
    
		public PresentableModelDescriptor__Implementation__Frozen()
		{
        }


        /// <summary>
        /// The default visual type used for this PresentableModel
        /// </summary>
        // enumeration property
        public virtual Kistl.App.GUI.VisualType DefaultVisualType
        {
            get
            {
                return _DefaultVisualType;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultVisualType != value)
                {
					var __oldValue = _DefaultVisualType;
                    NotifyPropertyChanging("DefaultVisualType", __oldValue, value);
                    _DefaultVisualType = value;
                    NotifyPropertyChanged("DefaultVisualType", __oldValue, value);
                }
            }
        }
        private Kistl.App.GUI.VisualType _DefaultVisualType;

        /// <summary>
        /// describe this PresentableModel
        /// </summary>
        // value type property
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// The described CLR class' reference
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef PresentableModelRef
        {
            get
            {
                return _PresentableModelRef;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PresentableModelRef != value)
                {
					var __oldValue = _PresentableModelRef;
                    NotifyPropertyChanging("PresentableModelRef", __oldValue, value);
                    _PresentableModelRef = value;
                    NotifyPropertyChanged("PresentableModelRef", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.TypeRef _PresentableModelRef;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(PresentableModelDescriptor));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_PresentableModelDescriptor != null)
            {
                OnToString_PresentableModelDescriptor(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<PresentableModelDescriptor> OnToString_PresentableModelDescriptor;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_PresentableModelDescriptor != null) OnPreSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPreSave_PresentableModelDescriptor;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_PresentableModelDescriptor != null) OnPostSave_PresentableModelDescriptor(this);
        }
        public event ObjectEventHandler<PresentableModelDescriptor> OnPostSave_PresentableModelDescriptor;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "DefaultVisualType":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(233).Constraints
						.Where(c => !c.IsValid(this, this.DefaultVisualType))
						.Select(c => c.GetErrorText(this, this.DefaultVisualType))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Description":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(232).Constraints
						.Where(c => !c.IsValid(this, this.Description))
						.Select(c => c.GetErrorText(this, this.Description))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "PresentableModelRef":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(231).Constraints
						.Where(c => !c.IsValid(this, this.PresentableModelRef))
						.Select(c => c.GetErrorText(this, this.PresentableModelRef))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
        internal PresentableModelDescriptor__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, PresentableModelDescriptor__Implementation__Frozen> DataStore = new Dictionary<int, PresentableModelDescriptor__Implementation__Frozen>(32);
		internal static void CreateInstances()
		{
			DataStore[1] = new PresentableModelDescriptor__Implementation__Frozen(1);

			DataStore[2] = new PresentableModelDescriptor__Implementation__Frozen(2);

			DataStore[3] = new PresentableModelDescriptor__Implementation__Frozen(3);

			DataStore[4] = new PresentableModelDescriptor__Implementation__Frozen(4);

			DataStore[5] = new PresentableModelDescriptor__Implementation__Frozen(5);

			DataStore[6] = new PresentableModelDescriptor__Implementation__Frozen(6);

			DataStore[7] = new PresentableModelDescriptor__Implementation__Frozen(7);

			DataStore[8] = new PresentableModelDescriptor__Implementation__Frozen(8);

			DataStore[9] = new PresentableModelDescriptor__Implementation__Frozen(9);

			DataStore[10] = new PresentableModelDescriptor__Implementation__Frozen(10);

			DataStore[11] = new PresentableModelDescriptor__Implementation__Frozen(11);

			DataStore[12] = new PresentableModelDescriptor__Implementation__Frozen(12);

			DataStore[13] = new PresentableModelDescriptor__Implementation__Frozen(13);

			DataStore[14] = new PresentableModelDescriptor__Implementation__Frozen(14);

			DataStore[15] = new PresentableModelDescriptor__Implementation__Frozen(15);

			DataStore[16] = new PresentableModelDescriptor__Implementation__Frozen(16);

			DataStore[17] = new PresentableModelDescriptor__Implementation__Frozen(17);

			DataStore[18] = new PresentableModelDescriptor__Implementation__Frozen(18);

			DataStore[19] = new PresentableModelDescriptor__Implementation__Frozen(19);

			DataStore[20] = new PresentableModelDescriptor__Implementation__Frozen(20);

			DataStore[21] = new PresentableModelDescriptor__Implementation__Frozen(21);

			DataStore[22] = new PresentableModelDescriptor__Implementation__Frozen(22);

			DataStore[23] = new PresentableModelDescriptor__Implementation__Frozen(23);

			DataStore[24] = new PresentableModelDescriptor__Implementation__Frozen(24);

			DataStore[25] = new PresentableModelDescriptor__Implementation__Frozen(25);

			DataStore[30] = new PresentableModelDescriptor__Implementation__Frozen(30);

			DataStore[31] = new PresentableModelDescriptor__Implementation__Frozen(31);

			DataStore[32] = new PresentableModelDescriptor__Implementation__Frozen(32);

			DataStore[33] = new PresentableModelDescriptor__Implementation__Frozen(33);

			DataStore[37] = new PresentableModelDescriptor__Implementation__Frozen(37);

			DataStore[38] = new PresentableModelDescriptor__Implementation__Frozen(38);

			DataStore[39] = new PresentableModelDescriptor__Implementation__Frozen(39);

		}

		internal static void FillDataStore() {
			DataStore[1].DefaultVisualType = (VisualType)22;
			DataStore[1].Description = @"A debugger window showing the used IKistlContexts and their AttachedObjects";
			DataStore[1].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[95];
			DataStore[1].Seal();
			DataStore[2].DefaultVisualType = (VisualType)24;
			DataStore[2].Description = @"A top-level window containing a Workspace, a visual representation for IKistlContext";
			DataStore[2].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[13];
			DataStore[2].Seal();
			DataStore[3].DefaultVisualType = (VisualType)23;
			DataStore[3].Description = @"A task for the user: select an IDataObject from a list";
			DataStore[3].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[18];
			DataStore[3].Seal();
			DataStore[4].DefaultVisualType = (VisualType)17;
			DataStore[4].Description = @"An action which can be triggered by the user";
			DataStore[4].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[16];
			DataStore[4].Seal();
			DataStore[5].DefaultVisualType = (VisualType)4;
			DataStore[5].Description = @"A reference to an IDataObject";
			DataStore[5].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[34];
			DataStore[5].Seal();
			DataStore[6].DefaultVisualType = (VisualType)3;
			DataStore[6].Description = @"A list of IDataObjects";
			DataStore[6].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[17];
			DataStore[6].Seal();
			DataStore[7].DefaultVisualType = (VisualType)1;
			DataStore[7].Description = @"A complete IDataObject";
			DataStore[7].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[83];
			DataStore[7].Seal();
			DataStore[8].DefaultVisualType = (VisualType)26;
			DataStore[8].Description = @"Select a string value from a set of values";
			DataStore[8].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[394];
			DataStore[8].Seal();
			DataStore[9].DefaultVisualType = (VisualType)13;
			DataStore[9].Description = @"A string attribute";
			DataStore[9].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[384];
			DataStore[9].Seal();
			DataStore[10].DefaultVisualType = (VisualType)13;
			DataStore[10].Description = @"An integer attribute";
			DataStore[10].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[399];
			DataStore[10].Seal();
			DataStore[11].DefaultVisualType = (VisualType)13;
			DataStore[11].Description = @"A floating point attribute";
			DataStore[11].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[404];
			DataStore[11].Seal();
			DataStore[12].DefaultVisualType = (VisualType)13;
			DataStore[12].Description = @"A date and time attribute";
			DataStore[12].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[409];
			DataStore[12].Seal();
			DataStore[13].DefaultVisualType = (VisualType)5;
			DataStore[13].Description = @"A simple true/false attribute";
			DataStore[13].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[414];
			DataStore[13].Seal();
			DataStore[14].DefaultVisualType = (VisualType)4;
			DataStore[14].Description = @"A method returning an IDataObject reference";
			DataStore[14].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[448];
			DataStore[14].Seal();
			DataStore[15].DefaultVisualType = (VisualType)2;
			DataStore[15].Description = @"A group of properties";
			DataStore[15].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[283];
			DataStore[15].Seal();
			DataStore[16].DefaultVisualType = (VisualType)13;
			DataStore[16].Description = @"A method returning a string";
			DataStore[16].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[450];
			DataStore[16].Seal();
			DataStore[17].DefaultVisualType = (VisualType)13;
			DataStore[17].Description = @"A method returning an integer value";
			DataStore[17].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[454];
			DataStore[17].Seal();
			DataStore[18].DefaultVisualType = (VisualType)13;
			DataStore[18].Description = @"A method returning a floating point value";
			DataStore[18].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[458];
			DataStore[18].Seal();
			DataStore[19].DefaultVisualType = (VisualType)13;
			DataStore[19].Description = @"A method returning a date and time value";
			DataStore[19].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[462];
			DataStore[19].Seal();
			DataStore[20].DefaultVisualType = (VisualType)5;
			DataStore[20].Description = @"A method returning true or false";
			DataStore[20].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[466];
			DataStore[20].Seal();
			DataStore[21].DefaultVisualType = (VisualType)15;
			DataStore[21].Description = @"An enumeration value for Multiplicity";
			DataStore[21].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[273];
			DataStore[21].Seal();
			DataStore[22].DefaultVisualType = (VisualType)15;
			DataStore[22].Description = @"An enumeration value for StorageType";
			DataStore[22].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[274];
			DataStore[22].Seal();
			DataStore[23].DefaultVisualType = (VisualType)15;
			DataStore[23].Description = @"An enumeration value for VisualType";
			DataStore[23].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[275];
			DataStore[23].Seal();
			DataStore[24].DefaultVisualType = (VisualType)15;
			DataStore[24].Description = @"An enumeration value for Toolkit";
			DataStore[24].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[276];
			DataStore[24].Seal();
			DataStore[25].DefaultVisualType = (VisualType)15;
			DataStore[25].Description = @"An enumeration value for TestEnum";
			DataStore[25].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[277];
			DataStore[25].Seal();
			DataStore[30].DefaultVisualType = (VisualType)1;
			DataStore[30].Description = @"DataObjectModel with specific extensions for DataTypes";
			DataStore[30].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[11];
			DataStore[30].Seal();
			DataStore[31].DefaultVisualType = (VisualType)1;
			DataStore[31].Description = @"DataObjectModel with specific extensions for MethodInvocations";
			DataStore[31].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[41];
			DataStore[31].Seal();
			DataStore[32].DefaultVisualType = (VisualType)1;
			DataStore[32].Description = @"DataObjectModel with specific extensions for Modules";
			DataStore[32].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[9];
			DataStore[32].Seal();
			DataStore[33].DefaultVisualType = (VisualType)1;
			DataStore[33].Description = @"DataObjectModel with specific extensions for ObjectClasses";
			DataStore[33].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[10];
			DataStore[33].Seal();
			DataStore[37].DefaultVisualType = (VisualType)1;
			DataStore[37].Description = @"A model for a single work effort";
			DataStore[37].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[563];
			DataStore[37].Seal();
			DataStore[38].DefaultVisualType = (VisualType)24;
			DataStore[38].Description = @"A workspace for recording work efforts";
			DataStore[38].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[564];
			DataStore[38].Seal();
			DataStore[39].DefaultVisualType = (VisualType)27;
			DataStore[39].Description = @"A GUID attribute";
			DataStore[39].PresentableModelRef = Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[570];
			DataStore[39].Seal();
	
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