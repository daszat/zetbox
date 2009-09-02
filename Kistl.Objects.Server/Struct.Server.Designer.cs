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
    /// Metadefinition Object for Structs.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Struct")]
    [System.Diagnostics.DebuggerDisplay("Struct")]
    public class Struct__Implementation__ : Kistl.App.Base.DataType__Implementation__, Struct
    {
    
		public Struct__Implementation__()
		{
        }


        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>
		[EventBasedMethod("OnGetDataType_Struct")]
		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Struct != null)
            {
                OnGetDataType_Struct(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
		public event GetDataType_Handler<Struct> OnGetDataType_Struct;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>
		[EventBasedMethod("OnGetDataTypeString_Struct")]
		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Struct != null)
            {
                OnGetDataTypeString_Struct(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<Struct> OnGetDataTypeString_Struct;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Struct));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Struct)obj;
			var otherImpl = (Struct__Implementation__)obj;
			var me = (Struct)this;

		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Struct")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Struct != null)
            {
                OnToString_Struct(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Struct> OnToString_Struct;

        [EventBasedMethod("OnPreSave_Struct")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Struct != null) OnPreSave_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnPreSave_Struct;

        [EventBasedMethod("OnPostSave_Struct")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Struct != null) OnPostSave_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnPostSave_Struct;

        [EventBasedMethod("OnCreated_Struct")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Struct != null) OnCreated_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnCreated_Struct;

        [EventBasedMethod("OnDeleting_Struct")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Struct != null) OnDeleting_Struct(this);
        }
        public event ObjectEventHandler<Struct> OnDeleting_Struct;



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