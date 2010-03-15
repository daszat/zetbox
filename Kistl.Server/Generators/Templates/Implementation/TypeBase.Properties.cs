using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.App.Extensions;
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
                if (((EnumerationProperty)p).IsList)
                {
                    ApplyEnumerationListTemplate((EnumerationProperty)p);
                }
                else
                {
                    ApplyEnumerationPropertyTemplate((EnumerationProperty)p);
                    ApplyPropertyInvocationsTemplate(p, false);
                }
            }
            else if (p is ObjectReferenceProperty)
            {
                if (((ObjectReferenceProperty)p).IsList())
                {
                    ApplyObjectReferenceListTemplate((ObjectReferenceProperty)p);
                }
                else
                {
                    ApplyObjectReferencePropertyTemplate((ObjectReferenceProperty)p);
                    ApplyPropertyInvocationsTemplate(p, false);
                }
            }
            else if (p is CalculatedObjectReferenceProperty)
            {
                ApplyCalculatedObjectReferencePropertyTemplate((CalculatedObjectReferenceProperty)p);
                ApplyPropertyInvocationsTemplate(p, true);
            }
            else if (p is CompoundObjectProperty)
            {
                if (((CompoundObjectProperty)p).IsList)
                {
                    ApplyCompoundObjectListTemplate((CompoundObjectProperty)p);
                }
                else
                {
                    ApplyCompoundObjectPropertyTemplate((CompoundObjectProperty)p);
                    // no PropertyInvocationsTemplate for CompoundObject
                }
            }
            else if (p is ValueTypeProperty)
            {
                if (((ValueTypeProperty)p).IsList)
                {
                    ApplyValueTypeListTemplate((ValueTypeProperty)p);
                }
                else
                {
                    ApplyValueTypePropertyTemplate((ValueTypeProperty)p);
                    ApplyPropertyInvocationsTemplate(p, false);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("p", String.Format("unknown property type '{0}'", p.GetType().ToString()));
            }
        }

        protected virtual void ApplyPropertyInvocationsTemplate(Property p, bool isReadOnly)
        {
            Templates.Implementation.ObjectClasses.PropertyInvocationsTemplate.Call(Host, ctx, p, isReadOnly);
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
            this.MembersToSerialize.Add("Implementation.ObjectClasses.EnumBinarySerialization", SerializerType.All, prop.Module.Namespace, prop.Name, prop);
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

        protected virtual void ApplyCalculatedObjectReferencePropertyTemplate(CalculatedObjectReferenceProperty prop)
        {
            this.WriteLine("        // calculated object reference property");
            ApplyCalculatedProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject list property");
            ApplyListProperty(prop, this.MembersToSerialize);
        }

        protected virtual void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject property");
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
            Templates.Implementation.ObjectClasses.NotifyingDataProperty.Call(Host, ctx,
                serList, prop);
        }

        protected virtual void ApplyListProperty(Property prop, SerializationMembersList serList)
        {
            Implementation.ObjectClasses.ListProperty.Call(Host, ctx,
                serList,
                this.DataType,
                prop.Name,
                prop);
        }

        protected virtual void ApplyCalculatedProperty(CalculatedObjectReferenceProperty prop, SerializationMembersList serList)
        {
            Templates.Implementation.ObjectClasses.CalculatedProperty.Call(Host, ctx,
                serList, prop);
        }
    }
}
