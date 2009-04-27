
namespace Kistl.App.Projekte
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

    using Kistl.API.Client;
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Projekt")]
    public class Projekt__Implementation__ : BaseClientDataObject_ClientObjects, Projekt
    {
    
		public Projekt__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Aufträge
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Auftrag> Auftraege
        {
            get
            {
                if (_AuftraegeWrapper == null)
                {
                    List<Kistl.App.Projekte.Auftrag> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Auftrag>(this, "Auftraege");
                    else
                        serverList = new List<Kistl.App.Projekte.Auftrag>();
                        
                    _AuftraegeWrapper = new OneNRelationCollection<Kistl.App.Projekte.Auftrag>(
                        "Projekt",
                        this,
                        serverList);
                }
                return _AuftraegeWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Projekte.Auftrag> _AuftraegeWrapper;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual double? AufwandGes
        {
            get
            {
                return _AufwandGes;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AufwandGes != value)
                {
					var __oldValue = _AufwandGes;
                    NotifyPropertyChanging("AufwandGes", __oldValue, value);
                    _AufwandGes = value;
                    NotifyPropertyChanged("AufwandGes", __oldValue, value);
                }
            }
        }
        private double? _AufwandGes;

        /// <summary>
        /// Bitte geben Sie den Kundennamen ein
        /// </summary>
        // value type property
        public virtual string Kundenname
        {
            get
            {
                return _Kundenname;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Kundenname != value)
                {
					var __oldValue = _Kundenname;
                    NotifyPropertyChanging("Kundenname", __oldValue, value);
                    _Kundenname = value;
                    NotifyPropertyChanged("Kundenname", __oldValue, value);
                }
            }
        }
        private string _Kundenname;

        /// <summary>
        /// 
        /// </summary>
        // collection reference property

		public IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
		{
			get
			{
				if (_Mitarbeiter == null)
				{
					Context.FetchRelation<Projekt_Mitarbeiter23CollectionEntry__Implementation__>(23, RelationEndRole.A, this);
					_Mitarbeiter 
						= new ClientRelationBSideListWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__>(
							this, 
							new RelationshipFilterASideCollection<Projekt_Mitarbeiter23CollectionEntry__Implementation__>(this.Context, this));
				}
				return _Mitarbeiter;
			}
		}

		private ClientRelationBSideListWrapper<Kistl.App.Projekte.Projekt, Kistl.App.Projekte.Mitarbeiter, Projekt_Mitarbeiter23CollectionEntry__Implementation__> _Mitarbeiter;

        /// <summary>
        /// Projektname
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
        /// 
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Projekte.Task> Tasks
        {
            get
            {
                if (_TasksWrapper == null)
                {
                    List<Kistl.App.Projekte.Task> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Task>(this, "Tasks");
                    else
                        serverList = new List<Kistl.App.Projekte.Task>();
                        
                    _TasksWrapper = new OneNRelationCollection<Kistl.App.Projekte.Task>(
                        "Projekt",
                        this,
                        serverList);
                }
                return _TasksWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Projekte.Task> _TasksWrapper;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Projekt));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Projekt)obj;
			var otherImpl = (Projekt__Implementation__)obj;
			var me = (Projekt)this;

			me.AufwandGes = other.AufwandGes;
			me.Kundenname = other.Kundenname;
			me.Name = other.Name;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Projekt != null)
            {
                OnToString_Projekt(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Projekt> OnToString_Projekt;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Projekt != null) OnPreSave_Projekt(this);
        }
        public event ObjectEventHandler<Projekt> OnPreSave_Projekt;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Projekt != null) OnPostSave_Projekt(this);
        }
        public event ObjectEventHandler<Projekt> OnPostSave_Projekt;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Auftraege":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(67).Constraints
						.Where(c => !c.IsValid(this, this.Auftraege))
						.Select(c => c.GetErrorText(this, this.Auftraege))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "AufwandGes":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(23).Constraints
						.Where(c => !c.IsValid(this, this.AufwandGes))
						.Select(c => c.GetErrorText(this, this.AufwandGes))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Kundenname":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(48).Constraints
						.Where(c => !c.IsValid(this, this.Kundenname))
						.Select(c => c.GetErrorText(this, this.Kundenname))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(22).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Name":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(13).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Tasks":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(14).Constraints
						.Where(c => !c.IsValid(this, this.Tasks))
						.Select(c => c.GetErrorText(this, this.Tasks))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._AufwandGes, binStream);
            BinarySerializer.ToStream(this._Kundenname, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AufwandGes, binStream);
            BinarySerializer.FromStream(out this._Kundenname, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._AufwandGes, xml, "AufwandGes", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Kundenname, xml, "Kundenname", "Kistl.App.Projekte");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Projekte");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._AufwandGes, xml, "AufwandGes", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Kundenname, xml, "Kundenname", "Kistl.App.Projekte");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Projekte");
        }

#endregion

    }


}