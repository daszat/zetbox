using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API.Utils;

namespace Kistl.API
{
    /// <summary>
    /// Global Helpermethods
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Constant for a invalid ID. Value is 0.
        /// </summary>
        public const int INVALIDID = 0;

        /// <summary>
        /// = int.MaxValue
        /// </summary>
        public const int LASTINDEXPOSITION = int.MaxValue;

        /// <summary>
        /// Suffix for Interface implementations
        /// </summary>
        public const string ImplementationSuffix = "__Implementation__";

        /// <summary>
        /// Suffix for Position Properties in Lists
        /// </summary>
        public const string PositionSuffix = "_pos";

        /// <summary>
        /// Newly created objects are not yet saved to the server and therefore handle some data only locally.
        /// This method can distinguish them from "older" objects that already have a representation on the server.
        /// </summary>
        /// <returns>true when the object has a valid context and already exists on server</returns>
        public static bool IsPersistedObject(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return obj.Context != null && obj.ID > INVALIDID;
        }

        /// <summary>
        /// Newly created objects are not yet saved to the server and therefore handle some data only locally.
        /// This method can distinguish them from "older" objects that already have a representation on the server.
        /// </summary>
        /// <returns>true when the object is detached and/or doesn't exist on the server yet</returns>
        public static bool IsFloatingObject(IPersistenceObject obj)
        {
            return !IsPersistedObject(obj);
        }

        /// <summary>
        /// Constant for Max List Count. Value is 500.
        /// </summary>
        public const int MAXLISTCOUNT = 500;

        /// <summary>
        /// Replaces all illigal Path Characters with an '_' char.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLegalPathName(string path)
        {
            System.IO.Path.GetInvalidPathChars().ToList().ForEach(c => path = path.Replace(c, '_'));

            return path;
        }
    }

    /// <summary>
    /// C# Extensions
    /// </summary>
    public static class ExtensionHelpers
    {
        /// <summary>
        /// Converts a object to XML.
        /// </summary>
        /// <param name="obj">Any XML Serializalable Object.</param>
        /// <returns>XML string</returns>
        public static string ToXmlString(this object obj)
        {
            using (Logging.Log.TraceMethodCall())
            {
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                StringBuilder sb = new StringBuilder();
                xml.Serialize(new System.IO.StringWriter(sb), obj);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Converts a XML String to a Objekt.
        /// </summary>
        /// <typeparam name="T">Type of the Object.</typeparam>
        /// <param name="xmlStr">XML string</param>
        /// <returns>Returns a Object or throws an XML-Exception (see MSDN, XmlSerializer)</returns>
        public static T FromXmlString<T>(this string xmlStr) where T : new()
        {
            using (Logging.Log.TraceMethodCall("Size = {0}", xmlStr.Length))
            {
                System.IO.StringReader sr = new System.IO.StringReader(xmlStr);
                XmlSerializer xml = new XmlSerializer(typeof(T));
                return (T)xml.Deserialize(sr);
            }
        }

        /// <summary>
        /// Checks if a enumeration is one of the given values
        /// </summary>
        /// <param name="e">Enum to check.</param>
        /// <param name="p">Values</param>
        /// <returns>true, if the Enum is one of the given Values.</returns>
        public static bool In(this Enum e, params object[] p)
        {
            foreach (object v in p)
            {
                if (e.Equals(v)) return true;
            }
            return false;
        }        

        /// <summary>
        /// Returns a _private_ Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivatePropertyValue<T>(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            PropertyInfo pi = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)pi.GetValue(obj, null);
        }

        /// <summary>
        /// Returns a private Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivateFieldValue<T>(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)fi.GetValue(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static MemberInfo FindFirstOrDefaultMember(this Type t, string memberName)
        {
            if (t == null) throw new ArgumentNullException("t");
            MemberInfo mi = t.GetMember(memberName).FirstOrDefault();
            if (mi != null) return mi;
            if (t.BaseType != null)
            {
                mi = FindFirstOrDefaultMember(t.BaseType, memberName);
                if (mi != null) return mi;
            }
            foreach (var iface in t.GetInterfaces())
            {
                mi = FindFirstOrDefaultMember(iface, memberName);
                if (mi != null) return mi;
            }
            return null;
        }

        /// <summary>
        /// Sets a Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is set</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">Value to set.</param>
        /// <returns>PropertyValue</returns>
        public static void SetPropertyValue<T>(this object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            var propertylist = propName.Split('.');
            object result = obj;
            foreach (string p in propertylist.Take(propertylist.Count() - 1))
            {
                PropertyInfo pi = result.GetType().GetProperty(p);
                if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
                result = pi.GetValue(result, null);
                if (result == null) throw new InvalidOperationException(string.Format("Unable to set Property {0}. The Path contains a NULL Object.", propName));
            }

            PropertyInfo set_pi = result.GetType().GetProperty(propertylist.Last());
            if (set_pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            set_pi.SetValue(result, val, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static bool HasProperty(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            object loopObj = obj;
            foreach (string it_p in propName.Split('.'))
            {
                string dictKey = string.Empty;
                string p = it_p;
                ExtractDictKey(ref dictKey, ref p);

                PropertyInfo pi = loopObj.GetType().GetProperty(p);
                if (pi == null) return false;

                loopObj = pi.GetValue(loopObj, null);
                if (!string.IsNullOrEmpty(dictKey) && loopObj != null)
                {
                    IDictionary dict = loopObj as IDictionary;
                    if (dict != null)
                    {
                        loopObj = dict[dictKey];
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPropertyValue<T>(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            object result = obj;
            foreach (string it_p in propName.Split('.'))
            {
                string dictKey = string.Empty;
                string p = it_p;
                ExtractDictKey(ref dictKey, ref p);

                PropertyInfo pi = result.GetType().GetProperty(p);
                if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
                result = pi.GetValue(result, null);
                if (result == null) return default(T);

                if (!string.IsNullOrEmpty(dictKey))
                {
                    IDictionary dict = result as IDictionary;
                    if (dict != null)
                    {
                        result = dict[dictKey];
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} is not a Dictionary, it's a {1}", p, obj.GetType().FullName));
                    }
                }
            }
            return (T)result;
        }

        private static void ExtractDictKey(ref string dictKey, ref string p)
        {
            if (p.Contains("[") && p.EndsWith("]"))
            {
                int idx = p.LastIndexOf("[");
                dictKey = p.Substring(idx + 1, p.Length - idx - 2);
                p = p.Substring(0, idx);
            }
        }

        public static void AddToCollectionQuick(this IDataObject obj, string propName, object val)
        {
            MagicCollectionFactory.WrapAsCollection<object>(obj.GetPropertyValue<object>(propName)).Add(val);
        }

        public static void RemoveFromCollectionQuick(this IDataObject obj, string propName, object val)
        {
            MagicCollectionFactory.WrapAsCollection<object>(obj.GetPropertyValue<object>(propName)).Remove(val);
        }

        public static void AddToCollection<T>(this object obj, string propName, T val)
        {
            AddToCollection<T>(obj, propName, val, false);
        }

        public static void AddToCollection<T>(this object obj, string propName, T val, bool unique)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            Type collectionType = obj.GetPropertyType(propName);
            Type collectionItemType = collectionType.GetGenericArguments()[0];
            object collection = pi.GetValue(obj, null);
            if (collection == null) throw new ArgumentException("Collection cannot be null");

            if (unique)
            {
                MethodInfo contains = collectionType.FindMethod("Contains", new Type[] { collectionItemType });
                if (contains == null) throw new ArgumentException("Cound not find \"Contains\" method of the given Collection");
                bool result = (bool)contains.Invoke(collection, new object[] { val });
                if (result) return;
            }

            MethodInfo add = collectionType.FindMethod("Add", new Type[] { collectionItemType });
            if (add == null) throw new ArgumentException("Cound not find \"Add\" method of the given Collection");
            add.Invoke(collection, new object[] { val });
        }

        public static void RemoveFromCollection<T>(this object obj, string propName, T val)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            Type collectionType = obj.GetPropertyType(propName);
            Type collectionItemType = collectionType.GetGenericArguments()[0];
            object collection = pi.GetValue(obj, null);
            if (collection == null) throw new ArgumentException("Collection cannot be null");

            MethodInfo add = collectionType.FindMethod("Remove", new Type[] { collectionItemType });
            if (add == null) throw new ArgumentException("Cound not find \"Remove\" method of the given Collection");
            add.Invoke(collection, new object[] { val });
        }

        /// <summary>
        /// returns the Type of the named property's values
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static Type GetPropertyType(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type result = null;
            object loopObj = obj;
            foreach (string p in propName.Split('.'))
            {
                PropertyInfo pi = loopObj.GetType().GetProperty(p);
                if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
                result = pi.PropertyType;
                loopObj = pi.GetValue(loopObj, null);
            }
            return result;
        }

        /// <summary>
        /// Sets a _private_ Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is set</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">Value to set.</param>
        /// <returns>PropertyValue</returns>
        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            Type t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }

        /// <summary>
        /// Set a private Property Value on a given Object. Uses Reflection.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <param name="val">the value to set</param>
        /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
        public static void SetPrivateFieldValue<T>(this object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            FieldInfo fi = null;
            while (fi == null && t != null)
            {
                fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
            fi.SetValue(obj, val);
        }

        /// <summary>
        /// Calls a public method on the given object. Uses Reflection.
        /// </summary>
        /// <typeparam name="TReturn">expected return type</typeparam>
        /// <param name="obj">the object on which to call the method</param>
        /// <param name="methodName">which method to call</param>
        /// <returns>the return value of the method</returns>
        /// <exception cref="ArgumentOutOfRangeException">if the method is not found</exception>
        public static TReturn CallMethod<TReturn>(this object obj, string methodName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            MethodInfo mi = null;
            while (mi == null && t != null)
            {
                mi = t.GetMethod(methodName, new Type[] { });
                t = t.BaseType;
            }
            if (mi == null) throw new ArgumentOutOfRangeException("methodName", string.Format("Method {0} was not found in Type {1}", methodName, obj.GetType().FullName));
            return (TReturn)mi.Invoke(obj, new object[] { });
        }


        public static MethodInfo FindMethod(this Type type, string methodName, Type[] parameterTypes)
        {
            if (parameterTypes == null)
            {
                MethodInfo mi = type.GetMethod(methodName);
                if (mi != null) return mi;
            }
            else
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo mi in methods)
                {
                    if (mi.Name == methodName)
                    {
                        ParameterInfo[] parameters = mi.GetParameters();
                        if (parameters.Length == parameterTypes.Length)
                        {
                            bool paramSame = true;
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                if (parameters[i].ParameterType != parameterTypes[i])
                                {
                                    paramSame = false;
                                    break;
                                }
                            }

                            if (paramSame) return mi;
                        }
                    }
                }
            }

            // Look in Basetypes
            if (type.BaseType != null)
            {
                MethodInfo mi = type.BaseType.FindMethod(methodName, parameterTypes);
                if (mi != null) return mi;
            }

            // Look in Interfaces
            foreach (Type i in type.GetInterfaces())
            {
                MethodInfo mi = i.FindMethod(methodName, parameterTypes);
                if (mi != null) return mi;
            }

            return null;
        }

        public static MethodInfo FindGenericMethod(this Type type, string methodName, Type[] typeArguments, Type[] parameterTypes)
        {
            if (parameterTypes == null)
            {
                MethodInfo mi = type.GetMethod(methodName);
                if (mi != null)
                {
                    return mi.MakeGenericMethod(typeArguments);
                }
            }
            else
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == methodName && method.GetGenericArguments().Length == typeArguments.Length)
                    {
                        MethodInfo mi = method.MakeGenericMethod(typeArguments);
                        ParameterInfo[] parameters = mi.GetParameters();

                        if (parameters.Length == parameterTypes.Length)
                        {
                            bool paramSame = true;
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                if (parameters[i].ParameterType != parameterTypes[i])
                                {
                                    paramSame = false;
                                    break;
                                }
                            }

                            if (paramSame) return mi;
                        }
                    }
                }
            }

            // Look in Basetypes
            if (type.BaseType != null)
            {
                MethodInfo mi = type.BaseType.FindGenericMethod(methodName, typeArguments, parameterTypes);
                if (mi != null) return mi;
            }

            // Look in Interfaces
            foreach (Type i in type.GetInterfaces())
            {
                MethodInfo mi = i.FindGenericMethod(methodName, typeArguments, parameterTypes);
                if (mi != null) return mi;
            }

            return null;
        }

        ///// <summary>
        ///// Finds the return type of the first implemented IEnumerable
        ///// </summary>
        ///// <param name="seqType"></param>
        ///// <returns></returns>
        //[Obsolete("Use the array variant FindElementTypes instead")]
        //public static Type GetCollectionElementType(this Type seqType)
        //{
        //    Type ienum = FindIEnumerable(seqType);
        //    if (ienum == null) return seqType;
        //    return ienum.GetGenericArguments()[0];
        //}

        ///// <summary>
        ///// Finds the first implemented by seqType from this List:
        ///// <list type="*">
        ///// <item>IEnumerable&lt;X&gt;,</item>
        ///// <item>IEnumerable,</item>
        ///// <item>null</item>
        ///// </list>
        ///// </summary>
        ///// <param name="seqType"></param>
        ///// <returns></returns>
        //[Obsolete("Use the array variant FindIEnumerables instead")]
        //public static Type FindIEnumerable(this Type seqType)
        //{
        //    if (seqType == null || seqType == typeof(string))
        //        return null;

        //    if (seqType.IsArray)
        //        return typeof(IEnumerable);

        //    if (seqType == typeof(IEnumerable))
        //        return seqType;

        //    // quick shortcut: guess that generic arguments often end up in
        //    // an IEnumerable<>, also handles coincidentally IEnumerable<>s
        //    if (seqType.IsGenericType)
        //    {
        //        foreach (Type arg in seqType.GetGenericArguments())
        //        {
        //            Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
        //            if (ienum.IsAssignableFrom(seqType))
        //            {
        //                return ienum;
        //            }
        //        }
        //    }

        //    Type[] ifaces = seqType.GetInterfaces();
        //    if (ifaces != null && ifaces.Length > 0)
        //    {
        //        foreach (Type iface in ifaces)
        //        {
        //            Type ienum = FindIEnumerable(iface);
        //            if (ienum != null) return ienum;
        //        }
        //    }

        //    if (seqType.BaseType != null && seqType.BaseType != typeof(object))
        //    {
        //        return FindIEnumerable(seqType.BaseType);
        //    }

        //    return null;
        //}

        /// <summary>
        /// Finds all implemented IEnumerables of the given Type
        /// </summary>
        public static IQueryable<Type> FindIEnumerables(this Type seqType)
        {
            if (seqType == null || seqType == typeof(object) || seqType == typeof(string))
                return new Type[] { }.AsQueryable();

            if (seqType.IsArray || seqType == typeof(IEnumerable))
                return new Type[] { typeof(IEnumerable) }.AsQueryable();

            if (seqType.IsGenericType && seqType.GetGenericArguments().Length == 1 && seqType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return new Type[] { seqType, typeof(IEnumerable) }.AsQueryable();
            }

            var result = new List<Type>();

            foreach (var iface in (seqType.GetInterfaces() ?? new Type[] { }))
            {
                result.AddRange(FindIEnumerables(iface));
            }

            return FindIEnumerables(seqType.BaseType).Union(result);
        }

        /// <summary>
        /// Finds all element types provided by a specified sequence type.
        /// "Element types" are T for IEnumerable&lt;T&gt; and object for IEnumerable.
        /// </summary>
        public static IQueryable<Type> FindElementTypes(this Type seqType)
        {
            return seqType.FindIEnumerables().Select(t => t.IsGenericType ? t.GetGenericArguments().Single() : typeof(object));
        }

        /// <summary>
        /// Foreach Extension Method for IEnumerable. This Extension does not check if the Enumeration Entry is NULL!
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the Enumeration.</typeparam>
        /// <param name="lst">Enumeration</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this IEnumerable lst, Action<T> action)
        {
            foreach (T obj in lst)
            {
                action(obj);
            }
        }

        /// <summary>
        /// Foreach Extension Method for IEnumerable. This Extension does not check if the Enumeration Entry is NULL!
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the Enumeration.</typeparam>
        /// <param name="lst">Enumeration</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this IEnumerable<T> lst, Action<T> action)
        {
            foreach (T obj in lst)
            {
                action(obj);
            }
        }

        /// <summary>
        /// Foreach Extension Method for IList&lt;>. This Extension does not check if the Enumeration Entry is NULL!
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the Enumeration.</typeparam>
        /// <param name="lst">Enumeration</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this IList<T> lst, Action<T> action)
        {
            foreach (T obj in lst)
            {
                action(obj);
            }
        }
        /// <summary>
        /// Foreach Extension Method for IQueryable&lt;>. This Extension does not check if the query results contain NULLs!
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the IQueryable.</typeparam>
        /// <param name="lst">IQueryable</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this IQueryable<T> lst, Action<T> action)
        {
            foreach (T i in lst)
            {
                action(i);
            }
        }

        public static Guid ParseGuidValue(this string str)
        {
            if (string.IsNullOrEmpty(str)) return Guid.Empty;
            try
            {
                return new Guid(str);
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }

    /// <summary>
    /// Provides a generic way to pass Data around in the event of an event.
    /// </summary>
    /// <typeparam name="T">The type of data to pass</typeparam>
    public class GenericEventArgs<T> : EventArgs
    {
        public T Data { get; set; }
    }

    public delegate void GenericEventHandler<TEventArgs>(object sender, GenericEventArgs<TEventArgs> e);
}
