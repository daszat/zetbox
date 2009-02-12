using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation
{
    public partial class TypeBase
    {
        /// <summary>
        /// The list of members to serialize
        /// </summary>
        protected SerializationMembersList MembersToSerialize { get { return _MembersToSerialize; } }
        private SerializationMembersList _MembersToSerialize = new SerializationMembersList();

        protected virtual void ApplyPropertyTemplate(Property p)
        {
            if (p is EnumerationProperty)
            {
                if (p.IsList)
                {
                    ApplyEnumerationListTemplate((EnumerationProperty)p);
                }
                else
                {
                    ApplyEnumerationPropertyTemplate((EnumerationProperty)p);
                }
            }
            else if (p is ObjectReferenceProperty)
            {
                if (p.IsList)
                {
                    ApplyObjectReferenceListTemplate((ObjectReferenceProperty)p);
                }
                else
                {
                    ApplyObjectReferencePropertyTemplate((ObjectReferenceProperty)p);
                }
            }
            else if (p is StructProperty)
            {
                if (p.IsList)
                {
                    ApplyStructListTemplate((StructProperty)p);
                }
                else
                {
                    ApplyStructPropertyTemplate((StructProperty)p);
                }
            }
            else if (p is ValueTypeProperty)
            {
                if (p.IsList)
                {
                    ApplyValueTypeListTemplate((ValueTypeProperty)p);
                }
                else
                {
                    ApplyValueTypePropertyTemplate((ValueTypeProperty)p);
                }
            }
            else
            {
                if (p.IsList)
                {
                    ApplyOtherListTemplate(p);
                }
                else
                {
                    ApplyOtherPropertyTemplate(p);
                }
            }
        }

        protected virtual void ApplyEnumerationListTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration list property");
            ApplyListProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration property");
            this.ApplyNotifyingValueProperty(prop, null);
            this.MembersToSerialize.Add("Implementation.ObjectClasses.EnumBinarySerialization", prop);
        }


        protected virtual void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            this.WriteLine("        // object reference list property");
            ApplyListProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            this.WriteLine("        // object reference property");
            ApplyNotifyingValueProperty(prop, this.MembersToSerialize);
        }


        protected virtual void ApplyStructListTemplate(StructProperty prop)
        {
            this.WriteLine("        // struct list property");
            ApplyListProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyStructPropertyTemplate(StructProperty prop)
        {
            this.WriteLine("        // struct property");
            ApplyNotifyingValueProperty(prop, this.MembersToSerialize);
        }


        protected virtual void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            ApplyListProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyValueTypePropertyTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value type property");
            ApplyNotifyingValueProperty(prop, this.MembersToSerialize);
        }


        protected virtual void ApplyOtherListTemplate(Property prop)
        {
            this.WriteLine("        // other list property");
            ApplyListProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyOtherPropertyTemplate(Property prop)
        {
            this.WriteLine("        // other property");
            ApplyNotifyingValueProperty(prop, this.MembersToSerialize);
        }


        protected virtual void ApplyNotifyingValueProperty(Property prop, SerializationMembersList serList)
        {
            this.Host.CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
                serList,
                prop.ReferencedTypeAsCSharp(),
                prop.PropertyName);
        }

        protected virtual void ApplyListProperty(Property prop, SerializationMembersList serList)
        {
            this.Host.CallTemplate("Implementation.ObjectClasses.ListProperty", ctx,
                serList,
                this.DataType,
                prop.GetPropertyType(),
                prop.PropertyName,
                prop);
        }

    }
}
