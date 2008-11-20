//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    using Kistl.API.Client;
    
    
    public class Mitarbeiter__Implementation__ : BaseClientDataObject, Mitarbeiter
    {
        
        private BackReferenceCollection<Kistl.App.Projekte.Projekt> _Projekte;
        
        private string _Name;
        
        private System.DateTime? _Geburtstag;
        
        private string _SVNr;
        
        private string _TelefonNummer;
        
        public Mitarbeiter__Implementation__()
        {
        }
        
        [XmlIgnore()]
        public ICollection<Kistl.App.Projekte.Projekt> Projekte
        {
            get
            {
                if (_Projekte == null)
                {
                    List<Kistl.App.Projekte.Projekt> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Projekte.Projekt>(this, "Projekte");
                    else
                        serverList = new List<Kistl.App.Projekte.Projekt>();

                    _Projekte = new BackReferenceCollection<Kistl.App.Projekte.Projekt>(
                         "Mitarbeiter", this, serverList);
                }
                return _Projekte;
            }
        }
        
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Name != value)
                {
                    NotifyPropertyChanging("Name"); 
                    _Name = value;
                    NotifyPropertyChanged("Name");;
                }
            }
        }
        
        public System.DateTime? Geburtstag
        {
            get
            {
                return _Geburtstag;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (Geburtstag != value)
                {
                    NotifyPropertyChanging("Geburtstag"); 
                    _Geburtstag = value;
                    NotifyPropertyChanged("Geburtstag");;
                }
            }
        }
        
        public string SVNr
        {
            get
            {
                return _SVNr;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (SVNr != value)
                {
                    NotifyPropertyChanging("SVNr"); 
                    _SVNr = value;
                    NotifyPropertyChanged("SVNr");;
                }
            }
        }
        
        public string TelefonNummer
        {
            get
            {
                return _TelefonNummer;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (TelefonNummer != value)
                {
                    NotifyPropertyChanging("TelefonNummer"); 
                    _TelefonNummer = value;
                    NotifyPropertyChanged("TelefonNummer");;
                }
            }
        }
        
        public event ToStringHandler<Mitarbeiter> OnToString_Mitarbeiter;
        
        public event ObjectEventHandler<Mitarbeiter> OnPreSave_Mitarbeiter;
        
        public event ObjectEventHandler<Mitarbeiter> OnPostSave_Mitarbeiter;
        
        public event TestMethodForParameter_Handler<Mitarbeiter> OnTestMethodForParameter_Mitarbeiter;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Mitarbeiter != null)
            {
                OnToString_Mitarbeiter(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Mitarbeiter != null) OnPreSave_Mitarbeiter(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Mitarbeiter != null) OnPostSave_Mitarbeiter(this);
        }
        
        public override void ApplyChanges(Kistl.API.IDataObject obj)
        {
            base.ApplyChanges(obj);
            if(this._Projekte != null) this._Projekte.ApplyChanges(((Mitarbeiter__Implementation__)obj)._Projekte); else ((Mitarbeiter__Implementation__)obj)._Projekte = null; ((Mitarbeiter__Implementation__)obj).NotifyPropertyChanged("Projekte");
            ((Mitarbeiter__Implementation__)obj).Name = this.Name;
            ((Mitarbeiter__Implementation__)obj).Geburtstag = this.Geburtstag;
            ((Mitarbeiter__Implementation__)obj).SVNr = this.SVNr;
            ((Mitarbeiter__Implementation__)obj).TelefonNummer = this.TelefonNummer;
        }
        
        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            if(_Projekte != null) _Projekte.AttachToContext(ctx);
        }
        
        protected override string GetPropertyError(string prop)
        {
            switch(prop)
            {
                case "Projekte":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(21).Constraints
                            .Where(c => !c.IsValid(this, this.Projekte))
                            .Select(c => c.GetErrorText(this, this.Projekte))
                            .ToArray());
                case "Name":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(20).Constraints
                            .Where(c => !c.IsValid(this, this.Name))
                            .Select(c => c.GetErrorText(this, this.Name))
                            .ToArray());
                case "Geburtstag":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(38).Constraints
                            .Where(c => !c.IsValid(this, this.Geburtstag))
                            .Select(c => c.GetErrorText(this, this.Geburtstag))
                            .ToArray());
                case "SVNr":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(39).Constraints
                            .Where(c => !c.IsValid(this, this.SVNr))
                            .Select(c => c.GetErrorText(this, this.SVNr))
                            .ToArray());
                case "TelefonNummer":
                    return string.Join("\n", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>(40).Constraints
                            .Where(c => !c.IsValid(this, this.TelefonNummer))
                            .Select(c => c.GetErrorText(this, this.TelefonNummer))
                            .ToArray());
            }
            return base.GetPropertyError(prop);
        }
        
        public virtual System.DateTime TestMethodForParameter(System.Guid TestCLRObjectParameter, Kistl.App.Projekte.Auftrag TestObjectParameter, System.DateTime TestDateTime, bool TestBool, double TestDouble, int TestInt, string TestString)
        {
            MethodReturnEventArgs<System.DateTime> e = new MethodReturnEventArgs<System.DateTime>();
            if (OnTestMethodForParameter_Mitarbeiter != null)
            {
                OnTestMethodForParameter_Mitarbeiter(this, e, TestCLRObjectParameter, TestObjectParameter, TestDateTime, TestBool, TestDouble, TestInt, TestString);
            };
            return e.Result;
        }
        
        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(this._Name, sw);
            BinarySerializer.ToBinary(this._Geburtstag, sw);
            BinarySerializer.ToBinary(this._SVNr, sw);
            BinarySerializer.ToBinary(this._TelefonNummer, sw);
        }
        
        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            BinarySerializer.FromBinary(out this._Name, sr);
            BinarySerializer.FromBinary(out this._Geburtstag, sr);
            BinarySerializer.FromBinary(out this._SVNr, sr);
            BinarySerializer.FromBinary(out this._TelefonNummer, sr);
        }
        
        public delegate void TestMethodForParameter_Handler<T>(T obj, MethodReturnEventArgs<System.DateTime> e, System.Guid TestCLRObjectParameter, Kistl.App.Projekte.Auftrag TestObjectParameter, System.DateTime TestDateTime, bool TestBool, double TestDouble, int TestInt, string TestString);
    }
}
