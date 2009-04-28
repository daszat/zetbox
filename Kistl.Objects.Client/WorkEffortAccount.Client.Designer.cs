
namespace Kistl.App.TimeRecords
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
    /// An account of work efforts. May be used to limit the hours being expended.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("WorkEffortAccount")]
    public class WorkEffortAccount__Implementation__ : BaseClientDataObject_ClientObjects, WorkEffortAccount
    {
    
		public WorkEffortAccount__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Maximal erlaubte Stundenanzahl
        /// </summary>
        // value type property
        public virtual double? BudgetHours
        {
            get
            {
                return _BudgetHours;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_BudgetHours != value)
                {
					var __oldValue = _BudgetHours;
                    NotifyPropertyChanging("BudgetHours", __oldValue, value);
                    _BudgetHours = value;
                    NotifyPropertyChanged("BudgetHours", __oldValue, value);
                }
            }
        }
        private double? _BudgetHours;

        /// <summary>
        /// Zugeordnete Mitarbeiter
        /// </summary>
        // collection reference property

		public ICollection<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
		{
			get
			{
				if (_Mitarbeiter == null)
				{
					Context.FetchRelation<WorkEffortAccount_Mitarbeiter42CollectionEntry__Implementation__>(42, RelationEndRole.A, this);
					_Mitarbeiter 
						= new ClientRelationBSideCollectionWrapper<Kistl.App.TimeRecords.WorkEffortAccount, Kistl.App.Projekte.Mitarbeiter, WorkEffortAccount_Mitarbeiter42CollectionEntry__Implementation__>(
							this, 
							new RelationshipFilterASideCollection<WorkEffortAccount_Mitarbeiter42CollectionEntry__Implementation__>(this.Context, this));
				}
				return _Mitarbeiter;
			}
		}

		private ClientRelationBSideCollectionWrapper<Kistl.App.TimeRecords.WorkEffortAccount, Kistl.App.Projekte.Mitarbeiter, WorkEffortAccount_Mitarbeiter42CollectionEntry__Implementation__> _Mitarbeiter;

        /// <summary>
        /// Name des TimeRecordsskontos
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
        /// Space for notes
        /// </summary>
        // value type property
        public virtual string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Notes != value)
                {
					var __oldValue = _Notes;
                    NotifyPropertyChanging("Notes", __oldValue, value);
                    _Notes = value;
                    NotifyPropertyChanged("Notes", __oldValue, value);
                }
            }
        }
        private string _Notes;

        /// <summary>
        /// Aktuell gebuchte Stunden
        /// </summary>
        // value type property
        public virtual double? SpentHours
        {
            get
            {
                return _SpentHours;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_SpentHours != value)
                {
					var __oldValue = _SpentHours;
                    NotifyPropertyChanging("SpentHours", __oldValue, value);
                    _SpentHours = value;
                    NotifyPropertyChanged("SpentHours", __oldValue, value);
                }
            }
        }
        private double? _SpentHours;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(WorkEffortAccount));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (WorkEffortAccount)obj;
			var otherImpl = (WorkEffortAccount__Implementation__)obj;
			var me = (WorkEffortAccount)this;

			me.BudgetHours = other.BudgetHours;
			me.Name = other.Name;
			me.Notes = other.Notes;
			me.SpentHours = other.SpentHours;
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
            if (OnToString_WorkEffortAccount != null)
            {
                OnToString_WorkEffortAccount(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<WorkEffortAccount> OnToString_WorkEffortAccount;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_WorkEffortAccount != null) OnPreSave_WorkEffortAccount(this);
        }
        public event ObjectEventHandler<WorkEffortAccount> OnPreSave_WorkEffortAccount;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_WorkEffortAccount != null) OnPostSave_WorkEffortAccount(this);
        }
        public event ObjectEventHandler<WorkEffortAccount> OnPostSave_WorkEffortAccount;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "BudgetHours":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(89).Constraints
						.Where(c => !c.IsValid(this, this.BudgetHours))
						.Select(c => c.GetErrorText(this, this.BudgetHours))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Mitarbeiter":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(86).Constraints
						.Where(c => !c.IsValid(this, this.Mitarbeiter))
						.Select(c => c.GetErrorText(this, this.Mitarbeiter))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Name":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(52).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Notes":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(237).Constraints
						.Where(c => !c.IsValid(this, this.Notes))
						.Select(c => c.GetErrorText(this, this.Notes))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "SpentHours":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(90).Constraints
						.Where(c => !c.IsValid(this, this.SpentHours))
						.Select(c => c.GetErrorText(this, this.SpentHours))
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
            BinarySerializer.ToStream(this._BudgetHours, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._Notes, binStream);
            BinarySerializer.ToStream(this._SpentHours, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._BudgetHours, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._Notes, binStream);
            BinarySerializer.FromStream(out this._SpentHours, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._BudgetHours, xml, "BudgetHours", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._Notes, xml, "Notes", "Kistl.App.TimeRecords");
            XmlStreamer.ToStream(this._SpentHours, xml, "SpentHours", "Kistl.App.TimeRecords");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._BudgetHours, xml, "BudgetHours", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._Notes, xml, "Notes", "Kistl.App.TimeRecords");
            XmlStreamer.FromStream(ref this._SpentHours, xml, "SpentHours", "Kistl.App.TimeRecords");
        }

#endregion

    }


}