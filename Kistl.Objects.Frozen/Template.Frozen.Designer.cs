
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
    [System.Diagnostics.DebuggerDisplay("Template")]
    public class Template__Implementation__ : BaseFrozenDataObject, Template
    {


        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
        // value type property
        public virtual string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayName != value)
                {
                    NotifyPropertyChanging("DisplayName");
                    _DisplayName = value;
                    NotifyPropertyChanged("DisplayName");;
                }
            }
        }
        private string _DisplayName;

        /// <summary>
        /// The visual representation of this Template
        /// </summary>
        // object reference property
        public virtual Kistl.App.GUI.Visual VisualTree
        {
            get
            {
                return _VisualTree;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_VisualTree != value)
                {
                    NotifyPropertyChanging("VisualTree");
                    _VisualTree = value;
                    NotifyPropertyChanged("VisualTree");;
                }
            }
        }
        private Kistl.App.GUI.Visual _VisualTree;

        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
        // value type property
        public virtual string DisplayedTypeFullName
        {
            get
            {
                return _DisplayedTypeFullName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayedTypeFullName != value)
                {
                    NotifyPropertyChanging("DisplayedTypeFullName");
                    _DisplayedTypeFullName = value;
                    NotifyPropertyChanged("DisplayedTypeFullName");;
                }
            }
        }
        private string _DisplayedTypeFullName;

        /// <summary>
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Assembly DisplayedTypeAssembly
        {
            get
            {
                return _DisplayedTypeAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayedTypeAssembly != value)
                {
                    NotifyPropertyChanging("DisplayedTypeAssembly");
                    _DisplayedTypeAssembly = value;
                    NotifyPropertyChanged("DisplayedTypeAssembly");;
                }
            }
        }
        private Kistl.App.Base.Assembly _DisplayedTypeAssembly;

        /// <summary>
        /// The main menu for this Template
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.GUI.Visual> Menu
        {
            get
            {
                if (_Menu == null)
                    _Menu = new List<Kistl.App.GUI.Visual>();
                return _Menu;
            }
        }
        private ICollection<Kistl.App.GUI.Visual> _Menu;

        /// <summary>
        /// 
        /// </summary>

		public virtual void PrepareDefault(Kistl.App.Base.ObjectClass cls) 
		{
            // base.PrepareDefault();
            if (OnPrepareDefault_Template != null)
            {
				OnPrepareDefault_Template(this, cls);
			}
        }
		public delegate void PrepareDefault_Handler<T>(T obj, Kistl.App.Base.ObjectClass cls);
		public event PrepareDefault_Handler<Template> OnPrepareDefault_Template;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Template != null)
            {
                OnToString_Template(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Template> OnToString_Template;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Template != null) OnPreSave_Template(this);
        }
        public event ObjectEventHandler<Template> OnPreSave_Template;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Template != null) OnPostSave_Template(this);
        }
        public event ObjectEventHandler<Template> OnPostSave_Template;


        internal Template__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


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