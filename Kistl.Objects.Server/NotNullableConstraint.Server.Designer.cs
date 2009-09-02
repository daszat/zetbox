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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="NotNullableConstraint")]
    [System.Diagnostics.DebuggerDisplay("NotNullableConstraint")]
    public class NotNullableConstraint__Implementation__ : Kistl.App.Base.Constraint__Implementation__, NotNullableConstraint
    {
    
		public NotNullableConstraint__Implementation__()
		{
        }


        /// <summary>
        /// 
        /// </summary>
		[EventBasedMethod("OnGetErrorText_NotNullableConstraint")]
		public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_NotNullableConstraint != null)
            {
                OnGetErrorText_NotNullableConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
		public event GetErrorText_Handler<NotNullableConstraint> OnGetErrorText_NotNullableConstraint;



        /// <summary>
        /// 
        /// </summary>
		[EventBasedMethod("OnIsValid_NotNullableConstraint")]
		public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_NotNullableConstraint != null)
            {
                OnIsValid_NotNullableConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
		public event IsValid_Handler<NotNullableConstraint> OnIsValid_NotNullableConstraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(NotNullableConstraint));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (NotNullableConstraint)obj;
			var otherImpl = (NotNullableConstraint__Implementation__)obj;
			var me = (NotNullableConstraint)this;

		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_NotNullableConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_NotNullableConstraint != null)
            {
                OnToString_NotNullableConstraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<NotNullableConstraint> OnToString_NotNullableConstraint;

        [EventBasedMethod("OnPreSave_NotNullableConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_NotNullableConstraint != null) OnPreSave_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnPreSave_NotNullableConstraint;

        [EventBasedMethod("OnPostSave_NotNullableConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_NotNullableConstraint != null) OnPostSave_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnPostSave_NotNullableConstraint;

        [EventBasedMethod("OnCreated_NotNullableConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_NotNullableConstraint != null) OnCreated_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnCreated_NotNullableConstraint;

        [EventBasedMethod("OnDeleting_NotNullableConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_NotNullableConstraint != null) OnDeleting_NotNullableConstraint(this);
        }
        public event ObjectEventHandler<NotNullableConstraint> OnDeleting_NotNullableConstraint;



		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			base.ReloadReferences();
			
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
            
            base.ToStream(binStream, auxObjects);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            base.Export(xml, modules);
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            
            base.MergeImport(xml);
        }

#endregion

    }


}