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
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="MethodInvocationConstraint")]
    [System.Diagnostics.DebuggerDisplay("MethodInvocationConstraint")]
    public class MethodInvocationConstraintEfImpl : Kistl.App.Base.ConstraintEfImpl, MethodInvocationConstraint
    {
        [Obsolete]
        public MethodInvocationConstraintEfImpl()
            : base(null)
        {
        }

        public MethodInvocationConstraintEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetErrorText_MethodInvocationConstraint")]
        public override string GetErrorText(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_MethodInvocationConstraint != null)
            {
                OnGetErrorText_MethodInvocationConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.GetErrorText(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event GetErrorText_Handler<MethodInvocationConstraint> OnGetErrorText_MethodInvocationConstraint;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnIsValid_MethodInvocationConstraint")]
        public override bool IsValid(System.Object constrainedObject, System.Object constrainedValue)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_MethodInvocationConstraint != null)
            {
                OnIsValid_MethodInvocationConstraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                e.Result = base.IsValid(constrainedObject, constrainedValue);
            }
            return e.Result;
        }
        public static event IsValid_Handler<MethodInvocationConstraint> OnIsValid_MethodInvocationConstraint;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(MethodInvocationConstraint);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (MethodInvocationConstraint)obj;
            var otherImpl = (MethodInvocationConstraintEfImpl)obj;
            var me = (MethodInvocationConstraint)this;

        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }


        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_MethodInvocationConstraint")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocationConstraint != null)
            {
                OnToString_MethodInvocationConstraint(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<MethodInvocationConstraint> OnToString_MethodInvocationConstraint;

        [EventBasedMethod("OnPreSave_MethodInvocationConstraint")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocationConstraint != null) OnPreSave_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnPreSave_MethodInvocationConstraint;

        [EventBasedMethod("OnPostSave_MethodInvocationConstraint")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocationConstraint != null) OnPostSave_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnPostSave_MethodInvocationConstraint;

        [EventBasedMethod("OnCreated_MethodInvocationConstraint")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_MethodInvocationConstraint != null) OnCreated_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnCreated_MethodInvocationConstraint;

        [EventBasedMethod("OnDeleting_MethodInvocationConstraint")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_MethodInvocationConstraint != null) OnDeleting_MethodInvocationConstraint(this);
        }
        public static event ObjectEventHandler<MethodInvocationConstraint> OnDeleting_MethodInvocationConstraint;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
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
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (CurrentAccessRights == Kistl.API.AccessRights.None) return;
        }

        #endregion

    }
}