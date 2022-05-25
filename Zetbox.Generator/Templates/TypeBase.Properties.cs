// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class TypeBase
    {
        /// <summary>
        /// The list of members to serialize
        /// </summary>
        protected Serialization.SerializationMembersList MembersToSerialize { get { return _MembersToSerialize; } }
        private Serialization.SerializationMembersList _MembersToSerialize = new Serialization.SerializationMembersList();

        protected virtual void ApplyPropertyTemplate(Property p)
        {
            if (p is EnumerationProperty)
            {
                if (((EnumerationProperty)p).IsList)
                {
                    ApplyEnumerationListTemplate((EnumerationProperty)p);
                    ApplyListChangedEvent(p);
                }
                else
                {
                    ApplyEnumerationPropertyTemplate((EnumerationProperty)p);
                    ApplyPropertyEvents(p, false);
                }
            }
            else if (p is ObjectReferenceProperty)
            {
                var orp = (ObjectReferenceProperty)p;
                if (orp.IsList().Result)
                {
                    ApplyObjectReferenceListTemplate(orp);
                    ApplyListChangedEvent(p);
                }
                else
                {
                    ApplyObjectReferencePropertyTemplate(orp);
                    ApplyPropertyEvents(p, false);
                }
            }
            else if (p is CalculatedObjectReferenceProperty)
            {
                ApplyCalculatedPropertyTemplate((CalculatedObjectReferenceProperty)p);
                ApplyPropertyEvents(p, true);
            }
            else if (p is CompoundObjectProperty)
            {
                if (((CompoundObjectProperty)p).IsList)
                {
                    ApplyCompoundObjectListTemplate((CompoundObjectProperty)p);
                    ApplyListChangedEvent(p);
                }
                else
                {
                    ApplyCompoundObjectPropertyTemplate((CompoundObjectProperty)p);
                    // no PropertyInvocationsTemplate for CompoundObject
                }
            }
            else if (p is ValueTypeProperty)
            {
                var vtp = (ValueTypeProperty)p;
                if (vtp.IsList)
                {
                    ApplyValueTypeListTemplate(vtp);
                    ApplyListChangedEvent(p);
                }
                else
                {
                    ApplyValueTypePropertyTemplate(vtp);
                    ApplyPropertyEvents(p, false);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("p", String.Format("unknown property type '{0}'", p.GetType().ToString()));
            }

            ApplyPropertyIsValidEvent(p);
        }

        protected virtual void ApplyPropertyIsValidEvent(Property p)
        {
            if (!p.IsCalculated())
            {
                this.WriteLine();
                this.WriteObjects("        public static event PropertyIsValidHandler<", p.ObjectClass.GetDataTypeString().Result, "> On", p.Name, "_IsValid;");
                this.WriteLine();
            }
        }

        protected virtual void ApplyPropertyEvents(Property p, bool isReadOnly)
        {
            Properties.PropertyEvents.Call(Host, ctx, p, isReadOnly || p.IsCalculated());
        }

        protected virtual void ApplyListChangedEvent(Property p)
        {
            Properties.PropertyListChangedEvent.Call(Host, ctx, p);
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
            var backingStoreName = String.Format("this._{0}", prop.Name);
            if (prop.DefaultValue != null)
            {
                var isSetFlagName = String.Format("_is{0}Set", prop.Name);
                Serialization.EnumWithDefaultBinarySerialization.AddToSerializers(MembersToSerialize, prop, backingStoreName, isSetFlagName);
            }
            else
            {
                Serialization.EnumBinarySerialization.AddToSerializers(MembersToSerialize, prop, backingStoreName);
            }
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

        // override ApplyCalculatedProperty instead
        protected void ApplyCalculatedPropertyTemplate(CalculatedObjectReferenceProperty prop)
        {
            this.WriteLine("        // calculated  property");
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

        protected virtual void ApplyNotifyingValueProperty(Property prop, Serialization.SerializationMembersList serList)
        {
            Properties.NotifyingDataProperty.Call(Host, ctx,
                serList, prop);
        }

        protected virtual void ApplyListProperty(Property prop, Serialization.SerializationMembersList serList)
        {
            Properties.ListProperty.Call(Host, ctx,
                serList,
                this.DataType,
                prop.Name,
                prop);
        }

        protected virtual void ApplyCalculatedProperty(CalculatedObjectReferenceProperty prop, Serialization.SerializationMembersList serList)
        {
            Properties.CalculatedProperty.Call(Host, ctx,
                serList, prop);
        }
    }
}
