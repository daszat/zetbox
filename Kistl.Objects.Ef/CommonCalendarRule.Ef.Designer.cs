// <autogenerated/>

namespace Kistl.App.Calendar
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
    /// This rule applies every day
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="CommonCalendarRule")]
    [System.Diagnostics.DebuggerDisplay("CommonCalendarRule")]
    public class CommonCalendarRuleEfImpl : Kistl.App.Calendar.CalendarRuleEfImpl, CommonCalendarRule
    {
        [Obsolete]
        public CommonCalendarRuleEfImpl()
            : base(null)
        {
        }

        public CommonCalendarRuleEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Checks if the Rule applies to the given date
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnAppliesTo_CommonCalendarRule")]
        public override bool AppliesTo(System.DateTime date)
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnAppliesTo_CommonCalendarRule != null)
            {
                OnAppliesTo_CommonCalendarRule(this, e, date);
            }
            else
            {
                e.Result = base.AppliesTo(date);
            }
            return e.Result;
        }
        public static event AppliesTo_Handler<CommonCalendarRule> OnAppliesTo_CommonCalendarRule;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(CommonCalendarRule);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (CommonCalendarRule)obj;
            var otherImpl = (CommonCalendarRuleEfImpl)obj;
            var me = (CommonCalendarRule)this;

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
        [EventBasedMethod("OnToString_CommonCalendarRule")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CommonCalendarRule != null)
            {
                OnToString_CommonCalendarRule(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<CommonCalendarRule> OnToString_CommonCalendarRule;

        [EventBasedMethod("OnPreSave_CommonCalendarRule")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CommonCalendarRule != null) OnPreSave_CommonCalendarRule(this);
        }
        public static event ObjectEventHandler<CommonCalendarRule> OnPreSave_CommonCalendarRule;

        [EventBasedMethod("OnPostSave_CommonCalendarRule")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CommonCalendarRule != null) OnPostSave_CommonCalendarRule(this);
        }
        public static event ObjectEventHandler<CommonCalendarRule> OnPostSave_CommonCalendarRule;

        [EventBasedMethod("OnCreated_CommonCalendarRule")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_CommonCalendarRule != null) OnCreated_CommonCalendarRule(this);
        }
        public static event ObjectEventHandler<CommonCalendarRule> OnCreated_CommonCalendarRule;

        [EventBasedMethod("OnDeleting_CommonCalendarRule")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_CommonCalendarRule != null) OnDeleting_CommonCalendarRule(this);
        }
        public static event ObjectEventHandler<CommonCalendarRule> OnDeleting_CommonCalendarRule;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (!CurrentAccessRights.HasReadRights()) return;
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
            if (!CurrentAccessRights.HasReadRights()) return;
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
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}