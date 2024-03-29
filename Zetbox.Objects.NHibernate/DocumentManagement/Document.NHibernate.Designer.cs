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

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using Zetbox.API.Utils;
    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.NHibernate;

    /// <summary>
    /// Depricated
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Document")]
    public class DocumentNHibernateImpl : at.dasz.DocumentManagement.FileNHibernateImpl, Document
    {
        private static readonly Guid _objectClassID = new Guid("3f496de2-bef4-4059-8c3e-c25db38bd3c2");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public DocumentNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public DocumentNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new DocumentProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public DocumentNHibernateImpl(Func<IFrozenContext> lazyCtx, DocumentProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly DocumentProxy Proxy;

        /// <summary>
        /// Creates an excerpt from the current file
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnExtractText_Document")]
        public override async System.Threading.Tasks.Task ExtractText()
        {
            // base.ExtractText();
            if (OnExtractText_Document != null)
            {
                await OnExtractText_Document(this);
            }
            else
            {
                await base.ExtractText();
            }
        }
        public static event ExtractText_Handler<Document> OnExtractText_Document;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<Document> OnExtractText_Document_CanExec;

        [EventBasedMethod("OnExtractText_Document_CanExec")]
        public override bool ExtractTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnExtractText_Document_CanExec != null)
				{
					OnExtractText_Document_CanExec(this, e);
				}
				else
				{
					e.Result = base.ExtractTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<Document> OnExtractText_Document_CanExecReason;

        [EventBasedMethod("OnExtractText_Document_CanExecReason")]
        public override string ExtractTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnExtractText_Document_CanExecReason != null)
				{
					OnExtractText_Document_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.ExtractTextCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Handles the change of the current blob
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnHandleBlobChange_Document")]
        public override async System.Threading.Tasks.Task<Zetbox.App.Base.Blob> HandleBlobChange(Zetbox.App.Base.Blob oldBlob, Zetbox.App.Base.Blob newBlob)
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.Blob>();
            if (OnHandleBlobChange_Document != null)
            {
                await OnHandleBlobChange_Document(this, e, oldBlob, newBlob);
            }
            else
            {
                e.Result = await base.HandleBlobChange(oldBlob, newBlob);
            }
            return e.Result;
        }
        public static event HandleBlobChange_Handler<Document> OnHandleBlobChange_Document;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Öffnet das Dokument schreibgeschützt
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnOpen_Document")]
        public override async System.Threading.Tasks.Task Open()
        {
            // base.Open();
            if (OnOpen_Document != null)
            {
                await OnOpen_Document(this);
            }
            else
            {
                await base.Open();
            }
        }
        public static event Open_Handler<Document> OnOpen_Document;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Uploads a new Content
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnUpload_Document")]
        public override async System.Threading.Tasks.Task Upload()
        {
            // base.Upload();
            if (OnUpload_Document != null)
            {
                await OnUpload_Document(this);
            }
            else
            {
                await base.Upload();
            }
        }
        public static event Upload_Handler<Document> OnUpload_Document;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
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
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(Document);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Document)obj;
            var otherImpl = (DocumentNHibernateImpl)obj;
            var me = (Document)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override async System.Threading.Tasks.Task ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            await base.ReloadReferences();

            // fix direct object references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

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
            base.NotifyDeleting();
            if (OnNotifyDeleting_Document != null) OnNotifyDeleting_Document(this);


        }
        public static event ObjectEventHandler<Document> OnNotifyDeleting_Document;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class DocumentProxy
            : at.dasz.DocumentManagement.FileNHibernateImpl.FileProxy
        {
            public DocumentProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(DocumentNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(DocumentProxy); } }

        }

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
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