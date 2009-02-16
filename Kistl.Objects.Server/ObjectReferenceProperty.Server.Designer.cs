
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for ObjectReference Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ObjectReferenceProperty")]
    [System.Diagnostics.DebuggerDisplay("ObjectReferenceProperty")]
    public class ObjectReferenceProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, ObjectReferenceProperty
    {


        /// <summary>
        /// This Property is the left Part of the selected Relation.
        /// </summary>
    /*
    NewRelation: FK_Relation_ObjectReferenceProperty_LeftOf_43 
    A: ZeroOrOne Relation as LeftOf (site: A, from relation ID = 17)
    B: One ObjectReferenceProperty as LeftPart (site: B, from relation ID = 17)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation LeftOf
        {
            get
            {
                return LeftOf__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                LeftOf__Implementation__ = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_LeftOf
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && LeftOf != null)
                {
                    _fk_LeftOf = LeftOf.ID;
                }
                return _fk_LeftOf;
            }
            set
            {
                _fk_LeftOf = value;
            }
        }
        private int _fk_LeftOf;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_ObjectReferenceProperty_LeftOf_43", "LeftOf")]
        public Kistl.App.Base.Relation__Implementation__ LeftOf__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_LeftOf_43",
                        "LeftOf");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_LeftOf_43",
                        "LeftOf");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Pointer zur Objektklasse
        /// </summary>
    /*
    NewRelation: FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_7 
    A: ZeroOrMore ObjectReferenceProperty as ObjectReferenceProperty (site: A, no Relation, prop ID=46)
    B: ZeroOrOne ObjectClass as ReferenceObjectClass (site: B, no Relation, prop ID=46)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass ReferenceObjectClass
        {
            get
            {
                return ReferenceObjectClass__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                ReferenceObjectClass__Implementation__ = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_ReferenceObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && ReferenceObjectClass != null)
                {
                    _fk_ReferenceObjectClass = ReferenceObjectClass.ID;
                }
                return _fk_ReferenceObjectClass;
            }
            set
            {
                _fk_ReferenceObjectClass = value;
            }
        }
        private int _fk_ReferenceObjectClass;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_7", "ReferenceObjectClass")]
        public Kistl.App.Base.ObjectClass__Implementation__ ReferenceObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_7",
                        "ReferenceObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.ObjectClass__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.ObjectClass__Implementation__>(
                        "Model.FK_ObjectReferenceProperty_ObjectClass_ObjectReferenceProperty_7",
                        "ReferenceObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.ObjectClass__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// This Property is the right Part of the selected Relation.
        /// </summary>
    /*
    NewRelation: FK_Relation_ObjectReferenceProperty_RightOf_44 
    A: ZeroOrOne Relation as RightOf (site: A, from relation ID = 18)
    B: One ObjectReferenceProperty as RightPart (site: B, from relation ID = 18)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation RightOf
        {
            get
            {
                return RightOf__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                RightOf__Implementation__ = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_RightOf
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && RightOf != null)
                {
                    _fk_RightOf = RightOf.ID;
                }
                return _fk_RightOf;
            }
            set
            {
                _fk_RightOf = value;
            }
        }
        private int _fk_RightOf;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Relation_ObjectReferenceProperty_RightOf_44", "RightOf")]
        public Kistl.App.Base.Relation__Implementation__ RightOf__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_RightOf_44",
                        "RightOf");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Relation__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Relation__Implementation__>(
                        "Model.FK_Relation_ObjectReferenceProperty_RightOf_44",
                        "RightOf");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Relation__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_ObjectReferenceProperty != null)
            {
                OnGetGUIRepresentation_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<ObjectReferenceProperty> OnGetGUIRepresentation_ObjectReferenceProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_ObjectReferenceProperty != null)
            {
                OnGetPropertyType_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<ObjectReferenceProperty> OnGetPropertyType_ObjectReferenceProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ObjectReferenceProperty != null)
            {
                OnGetPropertyTypeString_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<ObjectReferenceProperty> OnGetPropertyTypeString_ObjectReferenceProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectReferenceProperty != null)
            {
                OnToString_ObjectReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectReferenceProperty> OnToString_ObjectReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectReferenceProperty != null) OnPreSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPreSave_ObjectReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectReferenceProperty != null) OnPostSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPostSave_ObjectReferenceProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_LeftOf, binStream);
            BinarySerializer.ToStream(this._fk_ReferenceObjectClass, binStream);
            BinarySerializer.ToStream(this._fk_RightOf, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_LeftOf, binStream);
            BinarySerializer.FromStream(out this._fk_ReferenceObjectClass, binStream);
            BinarySerializer.FromStream(out this._fk_RightOf, binStream);
        }

#endregion

    }


}