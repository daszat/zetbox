// <autogenerated/>

namespace at.dasz.DocumentManagement
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
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.DalProvider.Base;
    using Kistl.DalProvider.Memory;

    /// <summary>
    /// Document with revisions
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Document")]
    public class DocumentMemoryImpl : at.dasz.DocumentManagement.FileMemoryImpl, Document
    {
        private static readonly Guid _objectClassID = new Guid("3f496de2-bef4-4059-8c3e-c25db38bd3c2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DocumentMemoryImpl()
            : base(null)
        {
        }

        public DocumentMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Kistl.Generator.Templates.Properties.CollectionEntryListProperty
		public IList<Kistl.App.Base.Blob> Revisions
		{
			get
			{
				if (_Revisions == null)
				{
					Context.FetchRelation<at.dasz.DocumentManagement.Document_has_Blob_RelationEntryMemoryImpl>(new Guid("69d27812-e981-443b-a94b-dfe1a95f3aad"), RelationEndRole.A, this);
					_Revisions 
						= new ObservableBSideListWrapper<at.dasz.DocumentManagement.Document, Kistl.App.Base.Blob, at.dasz.DocumentManagement.Document_has_Blob_RelationEntryMemoryImpl, ICollection<at.dasz.DocumentManagement.Document_has_Blob_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterASideCollection<at.dasz.DocumentManagement.Document_has_Blob_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (IList<Kistl.App.Base.Blob>)_Revisions;
			}
		}

		private ObservableBSideListWrapper<at.dasz.DocumentManagement.Document, Kistl.App.Base.Blob, at.dasz.DocumentManagement.Document_has_Blob_RelationEntryMemoryImpl, ICollection<at.dasz.DocumentManagement.Document_has_Blob_RelationEntryMemoryImpl>> _Revisions;

        public static event PropertyIsValidHandler<at.dasz.DocumentManagement.Document> OnRevisions_IsValid;

        /// <summary>
        /// Handles the change of the current blob
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnHandleBlobChange_Document")]
        public override Kistl.App.Base.Blob HandleBlobChange(Kistl.App.Base.Blob oldBlob, Kistl.App.Base.Blob newBlob)
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.Blob>();
            if (OnHandleBlobChange_Document != null)
            {
                OnHandleBlobChange_Document(this, e, oldBlob, newBlob);
            }
            else
            {
                e.Result = base.HandleBlobChange(oldBlob, newBlob);
            }
            return e.Result;
        }
        public static event HandleBlobChange_Handler<Document> OnHandleBlobChange_Document;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Document> OnHandleBlobChange_Document_CanExec;

        [EventBasedMethod("OnHandleBlobChange_Document_CanExec")]
        public override bool HandleBlobChangeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnHandleBlobChange_Document_CanExec != null)
				{
					OnHandleBlobChange_Document_CanExec(this, e);
				}
				else
				{
					e.Result = base.HandleBlobChangeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Document> OnHandleBlobChange_Document_CanExecReason;

        [EventBasedMethod("OnHandleBlobChange_Document_CanExecReason")]
        public override string HandleBlobChangeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnHandleBlobChange_Document_CanExecReason != null)
				{
					OnHandleBlobChange_Document_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.HandleBlobChangeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Öffnet das Dokument schreibgeschützt
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnOpen_Document")]
        public override void Open()
        {
            // base.Open();
            if (OnOpen_Document != null)
            {
                OnOpen_Document(this);
            }
            else
            {
                base.Open();
            }
        }
        public static event Open_Handler<Document> OnOpen_Document;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Document> OnOpen_Document_CanExec;

        [EventBasedMethod("OnOpen_Document_CanExec")]
        public override bool OpenCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnOpen_Document_CanExec != null)
				{
					OnOpen_Document_CanExec(this, e);
				}
				else
				{
					e.Result = base.OpenCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Document> OnOpen_Document_CanExecReason;

        [EventBasedMethod("OnOpen_Document_CanExecReason")]
        public override string OpenCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnOpen_Document_CanExecReason != null)
				{
					OnOpen_Document_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.OpenCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Uploads a new Content
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnUpload_Document")]
        public override void Upload()
        {
            // base.Upload();
            if (OnUpload_Document != null)
            {
                OnUpload_Document(this);
            }
            else
            {
                base.Upload();
            }
        }
        public static event Upload_Handler<Document> OnUpload_Document;
        // BEGIN Kistl.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Document> OnUpload_Document_CanExec;

        [EventBasedMethod("OnUpload_Document_CanExec")]
        public override bool UploadCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnUpload_Document_CanExec != null)
				{
					OnUpload_Document_CanExec(this, e);
				}
				else
				{
					e.Result = base.UploadCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Document> OnUpload_Document_CanExecReason;

        [EventBasedMethod("OnUpload_Document_CanExecReason")]
        public override string UploadCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnUpload_Document_CanExecReason != null)
				{
					OnUpload_Document_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.UploadCanExecReason;
				}
				return e.Result;
			}
        }
        // END Kistl.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(Document);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Document)obj;
            var otherImpl = (DocumentMemoryImpl)obj;
            var me = (Document)this;

        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange


        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Document, IList<Kistl.App.Base.Blob>>(
                        lazyCtx,
                        new Guid("ec544fe0-8189-4bb2-a3d1-3cb61d815aa5"),
                        "Revisions",
                        null,
                        obj => obj.Revisions,
                        null, // lists are read-only properties
                        obj => OnRevisions_IsValid), 
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Document")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Document != null)
            {
                OnToString_Document(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Document> OnToString_Document;

		[System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Document")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
			var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
			e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Document != null)
            {
                OnObjectIsValid_Document(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Document> OnObjectIsValid_Document;

        [EventBasedMethod("OnNotifyPreSave_Document")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Document != null) OnNotifyPreSave_Document(this);
        }
        public static event ObjectEventHandler<Document> OnNotifyPreSave_Document;

        [EventBasedMethod("OnNotifyPostSave_Document")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Document != null) OnNotifyPostSave_Document(this);
        }
        public static event ObjectEventHandler<Document> OnNotifyPostSave_Document;

        [EventBasedMethod("OnNotifyCreated_Document")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_Document != null) OnNotifyCreated_Document(this);
        }
        public static event ObjectEventHandler<Document> OnNotifyCreated_Document;

        [EventBasedMethod("OnNotifyDeleting_Document")]
        public override void NotifyDeleting()
        {
            Revisions.Clear();
            base.NotifyDeleting();
            if (OnNotifyDeleting_Document != null) OnNotifyDeleting_Document(this);
        }
        public static event ObjectEventHandler<Document> OnNotifyDeleting_Document;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}