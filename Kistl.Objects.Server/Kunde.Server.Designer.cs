//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Kunde_EMailsCollectionEntry_Kunde_fk_Parent", "A_Kunde", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Kunde__Implementation__), "B_Kunde_EMailsCollectionEntry", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__))]

namespace Kistl.App.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Kistl.DALProvider.EF;
    using Kistl.API.Server;
    
    
    [System.Diagnostics.DebuggerDisplay("Kistl.App.Projekte.Kunde")]
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Kunde")]
    public class Kunde__Implementation__ : BaseServerDataObject_EntityFramework, Kunde
    {
        
        private int _ID;
        
        private string _Kundenname;
        
        private string _Adresse;
        
        private string _PLZ;
        
        private string _Ort;
        
        private string _Land;
        
        private EntityCollectionEntryValueWrapper<Kistl.App.Projekte.Kunde, string, Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__> EMailsWrapper;
        
        public Kunde__Implementation__()
        {
        }
        
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Kundenname
        {
            get
            {
                return _Kundenname;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Kundenname != value)
                {
                    NotifyPropertyChanging("Kundenname"); 
                    _Kundenname = value;
                    NotifyPropertyChanged("Kundenname");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Adresse
        {
            get
            {
                return _Adresse;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Adresse != value)
                {
                    NotifyPropertyChanging("Adresse"); 
                    _Adresse = value;
                    NotifyPropertyChanged("Adresse");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string PLZ
        {
            get
            {
                return _PLZ;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (PLZ != value)
                {
                    NotifyPropertyChanging("PLZ"); 
                    _PLZ = value;
                    NotifyPropertyChanged("PLZ");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Ort
        {
            get
            {
                return _Ort;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Ort != value)
                {
                    NotifyPropertyChanging("Ort"); 
                    _Ort = value;
                    NotifyPropertyChanged("Ort");;
                }
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Land
        {
            get
            {
                return _Land;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Land != value)
                {
                    NotifyPropertyChanging("Land"); 
                    _Land = value;
                    NotifyPropertyChanged("Land");;
                }
            }
        }
        
        public ICollection<System.String> EMails
        {
            get
            {
                if (EMailsWrapper == null) EMailsWrapper = new EntityCollectionEntryValueWrapper<Kistl.App.Projekte.Kunde, System.String, Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__>(this, EMails__Implementation__);
                return EMailsWrapper;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Kunde_EMailsCollectionEntry_Kunde_fk_Parent", "B_Kunde_EMailsCollectionEntry")]
        public EntityCollection<Kistl.App.Projekte.Kunde_EMailsCollectionEntry__Implementation__> EMails__Implementation__
        {
            get
            {
                EntityCollection<Kunde_EMailsCollectionEntry__Implementation__> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kunde_EMailsCollectionEntry__Implementation__>("Model.FK_Kunde_EMailsCollectionEntry_Kunde_fk_Parent", "B_Kunde_EMailsCollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        public event ToStringHandler<Kunde> OnToString_Kunde;
        
        public event ObjectEventHandler<Kunde> OnPreSave_Kunde;
        
        public event ObjectEventHandler<Kunde> OnPostSave_Kunde;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Kunde != null)
            {
                OnToString_Kunde(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Kunde != null) OnPreSave_Kunde(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Kunde != null) OnPostSave_Kunde(this);
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            /// Use ToList before using foreach - the collection will change in the KistContext.Attach() Method because EntityFramework will need a Trick to attach CollectionEntries correctly
            EMails__Implementation__.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i));
        }
        
        protected override string GetPropertyError(string prop)
        {
            switch(prop)
            {
                case "Kundenname":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(59).Constraints
                            .Where(c => !c.IsValid(this, this.Kundenname))
                            .Select(c => c.GetErrorText(this, this.Kundenname))
                            .ToArray());
                case "Adresse":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(60).Constraints
                            .Where(c => !c.IsValid(this, this.Adresse))
                            .Select(c => c.GetErrorText(this, this.Adresse))
                            .ToArray());
                case "PLZ":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(61).Constraints
                            .Where(c => !c.IsValid(this, this.PLZ))
                            .Select(c => c.GetErrorText(this, this.PLZ))
                            .ToArray());
                case "Ort":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(62).Constraints
                            .Where(c => !c.IsValid(this, this.Ort))
                            .Select(c => c.GetErrorText(this, this.Ort))
                            .ToArray());
                case "Land":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(63).Constraints
                            .Where(c => !c.IsValid(this, this.Land))
                            .Select(c => c.GetErrorText(this, this.Land))
                            .ToArray());
                case "EMails":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(85).Constraints
                            .Where(c => !c.IsValid(this, this.EMails))
                            .Select(c => c.GetErrorText(this, this.EMails))
                            .ToArray());
            }
            return base.GetPropertyError(prop);
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Kundenname, sw);
            BinarySerializer.ToBinary(this._Adresse, sw);
            BinarySerializer.ToBinary(this._PLZ, sw);
            BinarySerializer.ToBinary(this._Ort, sw);
            BinarySerializer.ToBinary(this._Land, sw);
            BinarySerializer.ToBinary(this.EMails__Implementation__, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Kundenname, sr);
            BinarySerializer.FromBinary(out this._Adresse, sr);
            BinarySerializer.FromBinary(out this._PLZ, sr);
            BinarySerializer.FromBinary(out this._Ort, sr);
            BinarySerializer.FromBinary(out this._Land, sr);
            BinarySerializer.FromBinaryCollectionEntries(this.EMails__Implementation__, sr);
        }
    }
    
    [System.Diagnostics.DebuggerDisplay("Kistl.App.Projekte.Kunde_EMailsCollectionEntry")]
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Kunde_EMailsCollectionEntry")]
    public class Kunde_EMailsCollectionEntry__Implementation__ : BaseServerCollectionEntry_EntityFramework, ICollectionEntry<System.String, Kistl.App.Projekte.Kunde>
    {
        
        private int _ID;
        
        private string _Value;
        
        private int _fk_Parent;
        
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Value != value)
                {
                    NotifyPropertyChanging("Value"); 
                    _Value = value;
                    NotifyPropertyChanged("Value");;
                }
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Kunde Parent
        {
            get
            {
                return ParentImpl;
            }
            set
            {
                ParentImpl = (Kistl.App.Projekte.Kunde__Implementation__)value;
            }
        }
        
        public int fk_Parent
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Parent != null)
                {
                    _fk_Parent = Parent.ID;
                }
                return _fk_Parent;
            }
            set
            {
                _fk_Parent = value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Kunde_EMailsCollectionEntry_Kunde_fk_Parent", "A_Kunde")]
        public Kistl.App.Projekte.Kunde__Implementation__ ParentImpl
        {
            get
            {
                EntityReference<Kunde__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kunde__Implementation__>("Model.FK_Kunde_EMailsCollectionEntry_Kunde_fk_Parent", "A_Kunde");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kunde__Implementation__> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kunde__Implementation__>("Model.FK_Kunde_EMailsCollectionEntry_Kunde_fk_Parent", "A_Kunde");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = (Kunde__Implementation__)value;
            }
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this.Value, sw);
            BinarySerializer.ToBinary(this.fk_Parent, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Value, sr);
            BinarySerializer.FromBinary(out this._fk_Parent, sr);
        }
    }
}
