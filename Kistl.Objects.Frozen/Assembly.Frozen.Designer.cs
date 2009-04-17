
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
    [System.Diagnostics.DebuggerDisplay("Assembly")]
    public class Assembly__Implementation__Frozen : BaseFrozenDataObject, Assembly
    {
    
		public Assembly__Implementation__Frozen()
		{
        }


        /// <summary>
        /// Full Assemblyname eg. MyActions, Version=1.0.0.0
        /// </summary>
        // value type property
        public virtual string AssemblyName
        {
            get
            {
                return _AssemblyName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AssemblyName != value)
                {
					var __oldValue = _AssemblyName;
                    NotifyPropertyChanging("AssemblyName", __oldValue, value);
                    _AssemblyName = value;
                    NotifyPropertyChanged("AssemblyName", __oldValue, value);
                }
            }
        }
        private string _AssemblyName;

        /// <summary>
        /// Legt fest, ob es sich um ein Client-Assembly handelt.
        /// </summary>
        // value type property
        public virtual bool IsClientAssembly
        {
            get
            {
                return _IsClientAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsClientAssembly != value)
                {
					var __oldValue = _IsClientAssembly;
                    NotifyPropertyChanging("IsClientAssembly", __oldValue, value);
                    _IsClientAssembly = value;
                    NotifyPropertyChanged("IsClientAssembly", __oldValue, value);
                }
            }
        }
        private bool _IsClientAssembly;

        /// <summary>
        /// Module
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Module Module
        {
            get
            {
                return _Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Module != value)
                {
					var __oldValue = _Module;
                    NotifyPropertyChanging("Module", __oldValue, value);
                    _Module = value;
                    NotifyPropertyChanged("Module", __oldValue, value);
                }
            }
        }
        private Kistl.App.Base.Module _Module;

        /// <summary>
        /// Regenerates the stored list of TypeRefs from the loaded assembly
        /// </summary>

		public virtual void RegenerateTypeRefs() 
		{
            // base.RegenerateTypeRefs();
            if (OnRegenerateTypeRefs_Assembly != null)
            {
				OnRegenerateTypeRefs_Assembly(this);
			}
			else
			{
                throw new NotImplementedException("No handler registered on Assembly.RegenerateTypeRefs");
			}
        }
		public delegate void RegenerateTypeRefs_Handler<T>(T obj);
		public event RegenerateTypeRefs_Handler<Assembly> OnRegenerateTypeRefs_Assembly;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Assembly));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Assembly != null)
            {
                OnToString_Assembly(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Assembly> OnToString_Assembly;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Assembly != null) OnPreSave_Assembly(this);
        }
        public event ObjectEventHandler<Assembly> OnPreSave_Assembly;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Assembly != null) OnPostSave_Assembly(this);
        }
        public event ObjectEventHandler<Assembly> OnPostSave_Assembly;


        internal Assembly__Implementation__Frozen(int id)
            : base(id)
        { }


		internal static Dictionary<int, Assembly__Implementation__Frozen> DataStore = new Dictionary<int, Assembly__Implementation__Frozen>(20);
		internal static void CreateInstances()
		{
			DataStore[1] = new Assembly__Implementation__Frozen(1);

			DataStore[2] = new Assembly__Implementation__Frozen(2);

			DataStore[3] = new Assembly__Implementation__Frozen(3);

			DataStore[4] = new Assembly__Implementation__Frozen(4);

			DataStore[13] = new Assembly__Implementation__Frozen(13);

			DataStore[14] = new Assembly__Implementation__Frozen(14);

			DataStore[15] = new Assembly__Implementation__Frozen(15);

			DataStore[16] = new Assembly__Implementation__Frozen(16);

			DataStore[17] = new Assembly__Implementation__Frozen(17);

			DataStore[18] = new Assembly__Implementation__Frozen(18);

			DataStore[29] = new Assembly__Implementation__Frozen(29);

			DataStore[30] = new Assembly__Implementation__Frozen(30);

			DataStore[31] = new Assembly__Implementation__Frozen(31);

			DataStore[32] = new Assembly__Implementation__Frozen(32);

			DataStore[33] = new Assembly__Implementation__Frozen(33);

			DataStore[34] = new Assembly__Implementation__Frozen(34);

			DataStore[35] = new Assembly__Implementation__Frozen(35);

			DataStore[36] = new Assembly__Implementation__Frozen(36);

			DataStore[37] = new Assembly__Implementation__Frozen(37);

			DataStore[38] = new Assembly__Implementation__Frozen(38);

		}

		internal static void FillDataStore() {
			DataStore[1].AssemblyName = @"Kistl.App.Projekte.Client";
			DataStore[1].IsClientAssembly = true;
			DataStore[1].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[1].Seal();
			DataStore[2].AssemblyName = @"Kistl.App.Projekte.Server";
			DataStore[2].IsClientAssembly = false;
			DataStore[2].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[2].Seal();
			DataStore[3].AssemblyName = @"Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0";
			DataStore[3].IsClientAssembly = false;
			DataStore[3].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[3].Seal();
			DataStore[4].AssemblyName = @"Kistl.Client.WPF, Version=1.0.0.0";
			DataStore[4].IsClientAssembly = false;
			DataStore[4].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[4].Seal();
			DataStore[13].AssemblyName = @"Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[13].IsClientAssembly = false;
			DataStore[13].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[13].Seal();
			DataStore[14].AssemblyName = @"Kistl.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[14].IsClientAssembly = false;
			DataStore[14].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[14].Seal();
			DataStore[15].AssemblyName = @"Kistl.API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[15].IsClientAssembly = false;
			DataStore[15].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[15].Seal();
			DataStore[16].AssemblyName = @"Kistl.Client.ASPNET.Toolkit";
			DataStore[16].IsClientAssembly = false;
			DataStore[16].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[16].Seal();
			DataStore[17].AssemblyName = @"Kistl.Client.Forms";
			DataStore[17].IsClientAssembly = false;
			DataStore[17].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[17].Seal();
			DataStore[18].AssemblyName = @"Kistl.Client.WPF";
			DataStore[18].IsClientAssembly = false;
			DataStore[18].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[18].Seal();
			DataStore[29].AssemblyName = @"mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			DataStore[29].IsClientAssembly = false;
			DataStore[29].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[29].Seal();
			DataStore[30].AssemblyName = @"Kistl.API.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[30].IsClientAssembly = false;
			DataStore[30].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[30].Seal();
			DataStore[31].AssemblyName = @"WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
			DataStore[31].IsClientAssembly = false;
			DataStore[31].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[31].Seal();
			DataStore[32].AssemblyName = @"PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
			DataStore[32].IsClientAssembly = false;
			DataStore[32].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[32].Seal();
			DataStore[33].AssemblyName = @"PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
			DataStore[33].IsClientAssembly = false;
			DataStore[33].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[33].Seal();
			DataStore[34].AssemblyName = @"Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[34].IsClientAssembly = false;
			DataStore[34].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[34].Seal();
			DataStore[35].AssemblyName = @"Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[35].IsClientAssembly = false;
			DataStore[35].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[35].Seal();
			DataStore[36].AssemblyName = @"System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			DataStore[36].IsClientAssembly = false;
			DataStore[36].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[36].Seal();
			DataStore[37].AssemblyName = @"System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			DataStore[37].IsClientAssembly = false;
			DataStore[37].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[37].Seal();
			DataStore[38].AssemblyName = @"Kistl.Client.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[38].IsClientAssembly = false;
			DataStore[38].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[38].Seal();
	
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