
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;

    [Implementor]
    public static class RelationEndActions
    {
        [Invocation]
        public static void ToString(RelationEnd obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("RelationEnd {0}({1})",
                obj.RoleName,
                obj.Type == null
                    ? "no type"
                    : obj.Type.Name);

            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }
        [Invocation]
        public static void get_Parent(RelationEnd relEnd, PropertyGetterEventArgs<Relation> e)
        {
            e.Result = relEnd.AParent ?? relEnd.BParent;
        }

        // TODO: Replace this when NamedInstances are introduced 
        public static readonly Guid ViewModelDescriptor_ObjectReferenceModel = new Guid("83aae6fd-0fae-4348-b313-737a6e751027");
        public static readonly Guid ViewModelDescriptor_ObjectListModel = new Guid("9fce01fe-fd6d-4e21-8b55-08d5e38aea36");
        public static readonly Guid ViewModelDescriptor_ObjectCollectionModel = new Guid("67A49C49-B890-4D35-A8DB-1F8E43BFC7DF");

        [Invocation]
        public static void CreateNavigator(RelationEnd obj, MethodReturnEventArgs<ObjectReferenceProperty> e)
        {
            Relation rel = obj.AParent ?? obj.BParent;
            RelationEnd other = rel != null ? rel.GetOtherEnd(obj) : null;

            var nav = obj.Context.Create<ObjectReferenceProperty>();
            nav.CategoryTags = String.Empty;
            nav.ObjectClass = obj.Type;
            nav.RelationEnd = obj;
            nav.Module = rel != null ? rel.Module : null;

            if (other != null)
            {
                if (nav.GetIsList())
                {
                    if (nav.RelationEnd.Parent.GetOtherEnd(nav.RelationEnd).HasPersistentOrder)
                    {
                        nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectListModel);
                    }
                    else
                    {
                        nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectCollectionModel);
                    }
                }
                else
                {
                    nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectReferenceModel);
                }

                nav.Name = other.RoleName;
            }

            e.Result = nav;
        }
    }
}
