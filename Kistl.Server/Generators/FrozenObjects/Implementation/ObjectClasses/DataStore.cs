using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.ObjectClasses
{
    public partial class DataStore
    {
        public static string GetPropertyValueAsCSharp(IDataObject obj, Property prop)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (prop == null)
                throw new ArgumentNullException("prop");

            string propName = prop.PropertyName;

            PropertyInfo pi = obj.GetType().GetProperty(propName);

            if (pi == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            object value = pi.GetValue(obj, null);

            if (value == null)
            {
                return "null";
            }
            else
            {
                if (prop is EnumerationProperty)
                {
                    var enumProp = (EnumerationProperty)prop;
                    return String.Format("{0}.{1}", enumProp.Enumeration.ClassName, value);
                }
                else if (prop is ValueTypeProperty)
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        return String.Format(@"@""{0}""", value.ToString().Replace("\"", "\"\""));
                    }
                    else if (pi.PropertyType == typeof(bool))
                    {
                        return String.Format("{0}", value).ToLowerInvariant();
                    }
                    else
                    {
                        return String.Format("{0}", value);
                    }
                }
                else if (prop is ObjectReferenceProperty)
                {
                    var orp = (ObjectReferenceProperty)prop;
                    if (orp.ReferenceObjectClass.IsFrozenObject)
                    {
                        string referencedType = String.Format("{0}.{1}", orp.ReferenceObjectClass.Module.Namespace, Template.GetClassName(orp.ReferenceObjectClass));
                        string referencedInterface = String.Format("{0}.{1}", orp.ReferenceObjectClass.Module.Namespace, orp.ReferenceObjectClass.ClassName);
                        if (orp.IsList)
                        {
                            var items = ((ICollection)value).Cast<IDataObject>();
                            StringBuilder sb = new StringBuilder();
                            sb.Append("new System.Collections.ObjectModel.ReadOnlyCollection<");
                            sb.Append(referencedInterface);
                            sb.Append(">(new List<");
                            sb.Append(referencedInterface);
                            sb.Append(">(");
                            sb.Append(items.Count());
                            sb.AppendLine(") {");
                            foreach (var item in items)
                            {
                                sb.AppendFormat("{0}.DataStore[{1}],\n", referencedType, item.ID);
                            }
                            sb.Append("})");
                            return sb.ToString();
                        }
                        else
                        {
                            return String.Format("{0}.DataStore[{1}]", referencedType, ((IDataObject)value).ID);
                        }
                    }
                    else
                    {
                        // TODO: property accessor should throw notimplementedexception
                        // and this method shouldn't be called
                        return null;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

        }
    }
}
