// <autogenerated/>


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
    public class Assembly__Implementation__Frozen : BaseFrozenDataObject, Assembly, Kistl.API.IExportableInternal
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
        /// Export Guid
        /// </summary>
        // value type property
        public virtual Guid ExportGuid
        {
            get
            {
                return _ExportGuid;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ExportGuid != value)
                {
					var __oldValue = _ExportGuid;
                    NotifyPropertyChanging("ExportGuid", __oldValue, value);
                    _ExportGuid = value;
                    NotifyPropertyChanged("ExportGuid", __oldValue, value);
                }
            }
        }
        private Guid _ExportGuid;

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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "AssemblyName":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(71).Constraints
						.Where(c => !c.IsValid(this, this.AssemblyName))
						.Select(c => c.GetErrorText(this, this.AssemblyName))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "ExportGuid":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(255).Constraints
						.Where(c => !c.IsValid(this, this.ExportGuid))
						.Select(c => c.GetErrorText(this, this.ExportGuid))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "IsClientAssembly":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(83).Constraints
						.Where(c => !c.IsValid(this, this.IsClientAssembly))
						.Select(c => c.GetErrorText(this, this.IsClientAssembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Module":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(70).Constraints
						.Where(c => !c.IsValid(this, this.Module))
						.Select(c => c.GetErrorText(this, this.Module))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}
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
			DataStore[1].AssemblyName = @"Kistl.App.Projekte.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[1].ExportGuid = new Guid("8937dfc1-288e-49ca-a6c6-701e9c230b07");
			DataStore[1].IsClientAssembly = true;
			DataStore[1].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[1].Seal();
			DataStore[2].AssemblyName = @"Kistl.App.Projekte.Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[2].ExportGuid = new Guid("9acf0b2c-a4a1-4418-99b8-3ec69157c3f0");
			DataStore[2].IsClientAssembly = false;
			DataStore[2].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[1];
			DataStore[2].Seal();
			DataStore[3].AssemblyName = @"Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[3].ExportGuid = new Guid("b4a7503f-38ba-4b60-af41-d702d041c09a");
			DataStore[3].IsClientAssembly = false;
			DataStore[3].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[3].Seal();
			DataStore[4].AssemblyName = @"Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[4].ExportGuid = new Guid("2d670f6d-8208-4748-a197-a6ad4cffc392");
			DataStore[4].IsClientAssembly = false;
			DataStore[4].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[4].Seal();
			DataStore[13].AssemblyName = @"Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[13].ExportGuid = new Guid("242a1ea6-e8e1-420c-824d-0d2707ece25d");
			DataStore[13].IsClientAssembly = false;
			DataStore[13].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[13].Seal();
			DataStore[14].AssemblyName = @"Kistl.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[14].ExportGuid = new Guid("ccb4bfc9-d76a-43be-963e-bc3712e6b405");
			DataStore[14].IsClientAssembly = false;
			DataStore[14].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[14].Seal();
			DataStore[15].AssemblyName = @"Kistl.API, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[15].ExportGuid = new Guid("ba51f88e-57df-49cb-a907-b25e1091487b");
			DataStore[15].IsClientAssembly = false;
			DataStore[15].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[15].Seal();
			DataStore[16].AssemblyName = @"Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[16].ExportGuid = new Guid("eb5c0841-a9bb-4262-bb9a-5aed5a7086c7");
			DataStore[16].IsClientAssembly = false;
			DataStore[16].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[16].Seal();
			DataStore[17].AssemblyName = @"Kistl.Client.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[17].ExportGuid = new Guid("62afd077-5c95-46ce-8daa-ffda715ad3f5");
			DataStore[17].IsClientAssembly = false;
			DataStore[17].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[17].Seal();
			DataStore[18].AssemblyName = @"Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[18].ExportGuid = new Guid("157c1754-6eed-49f7-883b-ae2543bafd7e");
			DataStore[18].IsClientAssembly = false;
			DataStore[18].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[18].Seal();
			DataStore[29].AssemblyName = @"mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			DataStore[29].ExportGuid = new Guid("59dbd4ab-f5d2-455f-8181-acffe0fcec0b");
			DataStore[29].IsClientAssembly = false;
			DataStore[29].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[29].Seal();
			DataStore[30].AssemblyName = @"Kistl.API.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[30].ExportGuid = new Guid("15b6b4c4-6a16-485a-aa55-017b0c76fc56");
			DataStore[30].IsClientAssembly = false;
			DataStore[30].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[30].Seal();
			DataStore[31].AssemblyName = @"WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
			DataStore[31].ExportGuid = new Guid("331a8ad3-8ca2-411c-9823-5f6ab2acfdcb");
			DataStore[31].IsClientAssembly = false;
			DataStore[31].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[31].Seal();
			DataStore[32].AssemblyName = @"PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
			DataStore[32].ExportGuid = new Guid("c218dca0-cb1a-4d74-aaf0-0180604c4f4c");
			DataStore[32].IsClientAssembly = false;
			DataStore[32].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[32].Seal();
			DataStore[33].AssemblyName = @"PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
			DataStore[33].ExportGuid = new Guid("8ab0933e-9dda-4ae5-9cd8-d87aefe9b555");
			DataStore[33].IsClientAssembly = false;
			DataStore[33].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[33].Seal();
			DataStore[34].AssemblyName = @"Kistl.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[34].ExportGuid = new Guid("f3a264d3-ea6e-4e03-8ab6-52aa41f7aeac");
			DataStore[34].IsClientAssembly = false;
			DataStore[34].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[34].Seal();
			DataStore[35].AssemblyName = @"Kistl.Client.ASPNET.Toolkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[35].ExportGuid = new Guid("2780c2f0-cf4a-406f-8b11-9555662de568");
			DataStore[35].IsClientAssembly = false;
			DataStore[35].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[35].Seal();
			DataStore[36].AssemblyName = @"System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			DataStore[36].ExportGuid = new Guid("1f064a7c-8424-4cea-b640-6a8ad0c31324");
			DataStore[36].IsClientAssembly = false;
			DataStore[36].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[36].Seal();
			DataStore[37].AssemblyName = @"System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
			DataStore[37].ExportGuid = new Guid("2c8b4516-93cd-4a11-92ec-6c4d90cad8ac");
			DataStore[37].IsClientAssembly = false;
			DataStore[37].Module = Kistl.App.Base.Module__Implementation__Frozen.DataStore[4];
			DataStore[37].Seal();
			DataStore[38].AssemblyName = @"Kistl.Client.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			DataStore[38].ExportGuid = new Guid("1356f091-8beb-4b07-aa2f-010be2c14b76");
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
        public override void ToStream(System.Xml.XmlWriter xml)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }
        public virtual void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            throw new NotImplementedException();
        }
        public virtual void MergeImport(System.Xml.XmlReader xml)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}