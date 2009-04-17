
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
    /// Metadefinition Object for an Enumeration Entry.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EnumerationEntry")]
    public class EnumerationEntry__Implementation__Frozen : BaseFrozenDataObject, EnumerationEntry
    {
    
		public EnumerationEntry__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Description of this Enumeration Entry
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
        /// Übergeordnete Enumeration
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                return _Enumeration;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Enumeration != value)
                {
					var __oldValue = _Enumeration;
                    NotifyPropertyChanging("Enumeration", __oldValue, value);
                    _Enumeration = value;
                    NotifyPropertyChanged("Enumeration", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Enumeration _Enumeration;

        /// <summary>
        /// CLR name of this entry
        /// </summary>
        // value type property
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
					var __oldValue = _Name;
                    NotifyPropertyChanging("Name", __oldValue, value);
                    _Name = value;
                    NotifyPropertyChanged("Name", __oldValue, value);
                }
            }
        }
        private string _Name;

        /// <summary>
        /// The CLR value of this entry
        /// </summary>
        // value type property
        public virtual int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Value != value)
                {
					var __oldValue = _Value;
                    NotifyPropertyChanging("Value", __oldValue, value);
                    _Value = value;
                    NotifyPropertyChanged("Value", __oldValue, value);
                }
            }
        }
        private int _Value;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(EnumerationEntry));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumerationEntry != null)
            {
                OnToString_EnumerationEntry(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<EnumerationEntry> OnToString_EnumerationEntry;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationEntry != null) OnPreSave_EnumerationEntry(this);
        }
        public event ObjectEventHandler<EnumerationEntry> OnPreSave_EnumerationEntry;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationEntry != null) OnPostSave_EnumerationEntry(this);
        }
        public event ObjectEventHandler<EnumerationEntry> OnPostSave_EnumerationEntry;


        internal EnumerationEntry__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, EnumerationEntry__Implementation__Frozen> DataStore = new Dictionary<int, EnumerationEntry__Implementation__Frozen>(41);
		internal static void CreateInstances()
		{
			DataStore[2] = new EnumerationEntry__Implementation__Frozen(2);

			DataStore[3] = new EnumerationEntry__Implementation__Frozen(3);

			DataStore[5] = new EnumerationEntry__Implementation__Frozen(5);

			DataStore[6] = new EnumerationEntry__Implementation__Frozen(6);

			DataStore[7] = new EnumerationEntry__Implementation__Frozen(7);

			DataStore[40] = new EnumerationEntry__Implementation__Frozen(40);

			DataStore[41] = new EnumerationEntry__Implementation__Frozen(41);

			DataStore[42] = new EnumerationEntry__Implementation__Frozen(42);

			DataStore[43] = new EnumerationEntry__Implementation__Frozen(43);

			DataStore[44] = new EnumerationEntry__Implementation__Frozen(44);

			DataStore[45] = new EnumerationEntry__Implementation__Frozen(45);

			DataStore[46] = new EnumerationEntry__Implementation__Frozen(46);

			DataStore[47] = new EnumerationEntry__Implementation__Frozen(47);

			DataStore[48] = new EnumerationEntry__Implementation__Frozen(48);

			DataStore[49] = new EnumerationEntry__Implementation__Frozen(49);

			DataStore[50] = new EnumerationEntry__Implementation__Frozen(50);

			DataStore[51] = new EnumerationEntry__Implementation__Frozen(51);

			DataStore[52] = new EnumerationEntry__Implementation__Frozen(52);

			DataStore[53] = new EnumerationEntry__Implementation__Frozen(53);

			DataStore[54] = new EnumerationEntry__Implementation__Frozen(54);

			DataStore[55] = new EnumerationEntry__Implementation__Frozen(55);

			DataStore[56] = new EnumerationEntry__Implementation__Frozen(56);

			DataStore[57] = new EnumerationEntry__Implementation__Frozen(57);

			DataStore[58] = new EnumerationEntry__Implementation__Frozen(58);

			DataStore[59] = new EnumerationEntry__Implementation__Frozen(59);

			DataStore[60] = new EnumerationEntry__Implementation__Frozen(60);

			DataStore[61] = new EnumerationEntry__Implementation__Frozen(61);

			DataStore[62] = new EnumerationEntry__Implementation__Frozen(62);

			DataStore[63] = new EnumerationEntry__Implementation__Frozen(63);

			DataStore[64] = new EnumerationEntry__Implementation__Frozen(64);

			DataStore[65] = new EnumerationEntry__Implementation__Frozen(65);

			DataStore[66] = new EnumerationEntry__Implementation__Frozen(66);

			DataStore[67] = new EnumerationEntry__Implementation__Frozen(67);

			DataStore[68] = new EnumerationEntry__Implementation__Frozen(68);

			DataStore[69] = new EnumerationEntry__Implementation__Frozen(69);

			DataStore[70] = new EnumerationEntry__Implementation__Frozen(70);

			DataStore[71] = new EnumerationEntry__Implementation__Frozen(71);

			DataStore[74] = new EnumerationEntry__Implementation__Frozen(74);

			DataStore[75] = new EnumerationEntry__Implementation__Frozen(75);

			DataStore[76] = new EnumerationEntry__Implementation__Frozen(76);

			DataStore[77] = new EnumerationEntry__Implementation__Frozen(77);

		}

		internal static void FillDataStore() {
			DataStore[2].Description = @"First Test Entry";
			DataStore[2].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[50];
			DataStore[2].Name = @"First";
			DataStore[2].Value = 0;
			DataStore[2].Seal();
			DataStore[3].Description = @"Second Test Entry";
			DataStore[3].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[50];
			DataStore[3].Name = @"Second";
			DataStore[3].Value = 1;
			DataStore[3].Seal();
			DataStore[5].Description = @"WPF Toolkit";
			DataStore[5].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[53];
			DataStore[5].Name = @"WPF";
			DataStore[5].Value = 0;
			DataStore[5].Seal();
			DataStore[6].Description = @"ASPNET Toolkit";
			DataStore[6].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[53];
			DataStore[6].Name = @"ASPNET";
			DataStore[6].Value = 1;
			DataStore[6].Seal();
			DataStore[7].Description = @"TEST Toolkit";
			DataStore[7].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[53];
			DataStore[7].Name = @"TEST";
			DataStore[7].Value = 2;
			DataStore[7].Seal();
			DataStore[40].Description = null;
			DataStore[40].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[40].Name = @"SimpleObjectList";
			DataStore[40].Value = 16;
			DataStore[40].Seal();
			DataStore[41].Description = @"display a value from an Enumeration";
			DataStore[41].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[41].Name = @"Enumeration";
			DataStore[41].Value = 15;
			DataStore[41].Seal();
			DataStore[42].Description = null;
			DataStore[42].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[42].Name = @"StringList";
			DataStore[42].Value = 14;
			DataStore[42].Seal();
			DataStore[43].Description = null;
			DataStore[43].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[43].Name = @"String";
			DataStore[43].Value = 13;
			DataStore[43].Seal();
			DataStore[44].Description = null;
			DataStore[44].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[44].Name = @"IntegerList";
			DataStore[44].Value = 12;
			DataStore[44].Seal();
			DataStore[45].Description = null;
			DataStore[45].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[45].Name = @"Integer";
			DataStore[45].Value = 11;
			DataStore[45].Seal();
			DataStore[46].Description = null;
			DataStore[46].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[46].Name = @"DoubleList";
			DataStore[46].Value = 10;
			DataStore[46].Seal();
			DataStore[47].Description = null;
			DataStore[47].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[47].Name = @"Double";
			DataStore[47].Value = 9;
			DataStore[47].Seal();
			DataStore[48].Description = @"a list of date/time values";
			DataStore[48].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[48].Name = @"DateTimeList";
			DataStore[48].Value = 8;
			DataStore[48].Seal();
			DataStore[49].Description = @"a date/time value";
			DataStore[49].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[49].Name = @"DateTime";
			DataStore[49].Value = 7;
			DataStore[49].Seal();
			DataStore[50].Description = @"a list of booleans";
			DataStore[50].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[50].Name = @"BooleanList";
			DataStore[50].Value = 6;
			DataStore[50].Seal();
			DataStore[51].Description = @"a boolean";
			DataStore[51].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[51].Name = @"Boolean";
			DataStore[51].Value = 5;
			DataStore[51].Seal();
			DataStore[52].Description = @"A reference to an object";
			DataStore[52].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[52].Name = @"ObjectReference";
			DataStore[52].Value = 4;
			DataStore[52].Seal();
			DataStore[53].Description = @"A list of objects";
			DataStore[53].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[53].Name = @"ObjectList";
			DataStore[53].Value = 3;
			DataStore[53].Seal();
			DataStore[54].Description = @"A group of properties";
			DataStore[54].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[54].Name = @"PropertyGroup";
			DataStore[54].Value = 2;
			DataStore[54].Seal();
			DataStore[55].Description = @"A full view of the object";
			DataStore[55].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[55].Name = @"Object";
			DataStore[55].Value = 1;
			DataStore[55].Seal();
			DataStore[56].Description = @"The renderer class is no actual ""View"", but neverthe less needs to be found";
			DataStore[56].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[56].Name = @"Renderer";
			DataStore[56].Value = 0;
			DataStore[56].Seal();
			DataStore[57].Description = null;
			DataStore[57].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[57].Name = @"MenuGroup";
			DataStore[57].Value = 18;
			DataStore[57].Seal();
			DataStore[58].Description = null;
			DataStore[58].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[58].Name = @"MenuItem";
			DataStore[58].Value = 17;
			DataStore[58].Seal();
			DataStore[59].Description = null;
			DataStore[59].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[59].Name = @"TemplateEditor";
			DataStore[59].Value = 19;
			DataStore[59].Seal();
			DataStore[60].Description = @"The relation information is stored on both sides of the Relation";
			DataStore[60].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[78];
			DataStore[60].Name = @"Replicate";
			DataStore[60].Value = 3;
			DataStore[60].Seal();
			DataStore[61].Description = @"Deprecated alias for MergeIntoB";
			DataStore[61].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[78];
			DataStore[61].Name = @"Right";
			DataStore[61].Value = 2;
			DataStore[61].Seal();
			DataStore[62].Description = @"Deprecated alias for MergeIntoA";
			DataStore[62].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[78];
			DataStore[62].Name = @"Left";
			DataStore[62].Value = 1;
			DataStore[62].Seal();
			DataStore[63].Description = @"Required Element (exactly one)";
			DataStore[63].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[81];
			DataStore[63].Name = @"One";
			DataStore[63].Value = 2;
			DataStore[63].Seal();
			DataStore[64].Description = @"Optional Element (zero or one)";
			DataStore[64].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[81];
			DataStore[64].Name = @"ZeroOrOne";
			DataStore[64].Value = 1;
			DataStore[64].Seal();
			DataStore[65].Description = @"Optional List Element (zero or more)";
			DataStore[65].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[81];
			DataStore[65].Name = @"ZeroOrMore";
			DataStore[65].Value = 3;
			DataStore[65].Seal();
			DataStore[66].Description = @"The relation information is stored with the A-side ObjectClass";
			DataStore[66].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[78];
			DataStore[66].Name = @"MergeIntoA";
			DataStore[66].Value = 1;
			DataStore[66].Seal();
			DataStore[67].Description = @"The relation information is stored with the B-side ObjectClass";
			DataStore[67].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[78];
			DataStore[67].Name = @"MergeIntoB";
			DataStore[67].Value = 2;
			DataStore[67].Seal();
			DataStore[68].Description = @"The relation information is stored in a separate entity";
			DataStore[68].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[78];
			DataStore[68].Name = @"Separate";
			DataStore[68].Value = 4;
			DataStore[68].Seal();
			DataStore[69].Description = @"Displays an Integer with a slider instead of a text box";
			DataStore[69].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[69].Name = @"IntegerSlider";
			DataStore[69].Value = 20;
			DataStore[69].Seal();
			DataStore[70].Description = @"An object as entry of a list";
			DataStore[70].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[70].Name = @"ObjectListEntry";
			DataStore[70].Value = 21;
			DataStore[70].Seal();
			DataStore[71].Description = @"The debugger window for displaying the active contexts";
			DataStore[71].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[71].Name = @"KistlDebugger";
			DataStore[71].Value = 22;
			DataStore[71].Seal();
			DataStore[74].Description = @"A task for the user: select a value from a list";
			DataStore[74].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[74].Name = @"SelectionTaskDialog";
			DataStore[74].Value = 23;
			DataStore[74].Seal();
			DataStore[75].Description = @"A top-level window containing a Workspace, a visual representation for IKistlContext";
			DataStore[75].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[75].Name = @"WorkspaceWindow";
			DataStore[75].Value = 24;
			DataStore[75].Seal();
			DataStore[76].Description = @"Select a string from a aset of values";
			DataStore[76].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[55];
			DataStore[76].Name = @"StringSelection";
			DataStore[76].Value = 26;
			DataStore[76].Seal();
			DataStore[77].Description = @"Windows Forms Toolkit";
			DataStore[77].Enumeration = Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[53];
			DataStore[77].Name = @"WinForms";
			DataStore[77].Value = 3;
			DataStore[77].Seal();
	
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