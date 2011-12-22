using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public static readonly int INVALIDID = 0;

        /// <summary>
        /// = int.MaxValue
        /// </summary>
        public static readonly int LASTINDEXPOSITION = int.MaxValue;

        /// <summary>
        /// Suffix for Interface implementations
        /// </summary>
        public static readonly string ImplementationSuffix = "Impl";

        /// <summary>
        /// Suffix for Position Properties in Lists
        /// </summary>
        public static readonly string PositionSuffix = "_pos";

        /// <summary>
        /// Interface Assembly
        /// </summary>
        public static readonly string InterfaceAssembly = "Kistl.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b69192d05046fdf";

        /// <summary>
        /// Default length if StringRangeConstraint is missing
        /// </summary>
        public static readonly int DefaultStringPropertyLength = 1000;

        /// <summary>
        /// Keyname for the Kistl connection string
        /// </summary>
        public static readonly string KistlConnectionStringKey = "Kistl";

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
            return !IsPersistedObject(obj) && obj.ObjectState != DataObjectState.Unmodified;
        }


        /// <summary>
        /// Constant for Max List Count. Value is 1000.
        /// </summary>
        public static readonly int MAXLISTCOUNT = 1000;

        /// <summary>
        /// Replaces all illigal Path Characters with an '_' char.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLegalPathName(string path)
        {
            Path.GetInvalidPathChars().ToList().ForEach(c => path = path.Replace(c, '_'));

            return path;
        }

        public static string Indent(int count)
        {
            StringBuilder sb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                sb.Append(' ');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Assure that all indices are strictly monotonous rising (i.e. n.Index &lt; (n+1).Index) according to their order in <paramref name="orderedItems"/>
        /// </summary>
        public static void FixIndices<TItem>(List<TItem> orderedItems, Func<TItem, int?> getIndex, Action<TItem, int> setIndex)
        {
            if (orderedItems == null) { throw new ArgumentNullException("orderedItems"); }
            if (getIndex == null) { throw new ArgumentNullException("getIndex"); }
            if (setIndex == null) { throw new ArgumentNullException("setIndex"); }

            int maxIdx = -1;
            for (int i = 0; i < orderedItems.Count; i++)
            {
                var item = orderedItems[i];
                int? idx = getIndex(item);
                if (!idx.HasValue || idx <= maxIdx || idx == Kistl.API.Helper.LASTINDEXPOSITION)
                {
                    // TODO: try to space out items better
                    idx = maxIdx + 1;
                    setIndex(item, idx.Value);
                }
                maxIdx = idx.Value;
            }
        }

        public static void CopyTo(this System.IO.Stream src, System.IO.Stream dest)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (dest == null) throw new ArgumentNullException("dest");

            if (src.CanSeek) src.Seek(0, SeekOrigin.Begin);

            var buffer = new byte[4096];
            int cnt;
            while ((cnt = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, cnt);
            }
        }

        public static string PathCombine(params string[] components)
        {
            if (components == null) throw new ArgumentNullException("components");
            if (components.Length == 0) throw new ArgumentOutOfRangeException("components", "need at least one element for PathCombine()");

            var result = components[0];
            for (int i = 1; i < components.Length; i++)
            {
                result = Path.Combine(result, components[i]);
            }
            return result;
        }
    }

    public static class TypeExtensions
    {
        public static bool IsStatic(this Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }
            return type.IsAbstract && type.IsSealed;
        }

        public static bool IsIList(this Type type)
        {
            return typeof(IList).IsAssignableFrom(type);
        }

        public static bool IsIEnumerable(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public static bool IsIStreamable(this Type type)
        {
            return typeof(IStreamable).IsAssignableFrom(type);
        }
        public static bool IsIDataObject(this Type type)
        {
            return typeof(IDataObject).IsAssignableFrom(type);
        }
        public static bool IsIRelationEntry(this Type type)
        {
            return typeof(IRelationEntry).IsAssignableFrom(type);
        }
        public static bool IsIPersistenceObject(this Type type)
        {
            return typeof(IPersistenceObject).IsAssignableFrom(type);
        }
        public static bool IsICompoundObject(this Type type)
        {
            return typeof(ICompoundObject).IsAssignableFrom(type);
        }
        public static bool IsIExportableInternal(this Type type)
        {
            return typeof(IExportableInternal).IsAssignableFrom(type);
        }
        public static bool IsNullableEnum(this Type type)
        {
            if (type == null) return false;
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments().Single().IsEnum;
        }

        #region HasGenericDefinition
        // From: http://stackoverflow.com/questions/457676/c-sharp-reflection-check-if-a-class-is-derived-from-a-generic-class/897388#897388

        /// <summary>
        /// Checks whether this type has the specified definition in its ancestry.
        /// </summary>
        public static bool HasGenericDefinition(this Type type, Type definition)
        {
            return GetTypeWithGenericDefinition(type, definition) != null;
        }

        /// <summary>
        /// Returns the actual type implementing the specified definition from the ancestry of the type, if available. Else, null.
        /// </summary>
        public static Type GetTypeWithGenericDefinition(this Type type, Type definition)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (definition == null)
                throw new ArgumentNullException("definition");
            if (!definition.IsGenericTypeDefinition)
                throw new ArgumentException(
                    "The definition needs to be a GenericTypeDefinition", "definition");

            if (definition.IsInterface)
                foreach (var interfaceType in type.GetInterfaces())
                    if (interfaceType.IsGenericType
                        && interfaceType.GetGenericTypeDefinition() == definition)
                        return interfaceType;

            for (Type t = type; t != null; t = t.BaseType)
                if (t.IsGenericType
                    && t.GetGenericTypeDefinition() == definition)
                    return t;

            return null;
        }

        #endregion
    }

    /// <summary>
    /// Extensions for accessing properties and fields generic
    /// </summary>
    public static class GetSetHasValueExtensions
    {
        #region Private
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
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("GetPrivatePropertyValue of {0}.{1}", obj.GetType().FullName, propName);
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
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("GetPrivateFieldValue of {0}.{1}", obj.GetType().FullName, propName);
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
            if (obj == null) throw new ArgumentNullException("obj");
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("SetPrivatePropertyValue of {0}.{1}", obj.GetType().FullName, propName);

            Type t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
        }

        #endregion

        #region Public
        /// <summary>
        /// Returns the Type of the named property's values
        /// Uses Reflection.
        /// Supports extended access syntax Propertyname.NeestedProperty[DictKey1].Propertyname2
        /// </summary>
        /// <param name="obj">Object where the type of a property should be checked</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns></returns>
        public static Type GetPropertyType(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (propName == null) throw new ArgumentNullException("propName");
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("GetPropertyType of {0}.{1}", obj.GetType().FullName, propName);

            Type result = null;
            object loopObj = obj;
            foreach (string it_p in propName.Split('.'))
            {
                string dictKey = string.Empty;
                string p = it_p;
                ExtractDictKey(ref dictKey, ref p);

                PropertyInfo pi = loopObj.GetType().GetProperty(p);
                if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
                result = pi.PropertyType;
                loopObj = pi.GetValue(loopObj, null);

                if (!string.IsNullOrEmpty(dictKey))
                {
                    IDictionary dict = loopObj as IDictionary;
                    if (dict != null)
                    {
                        loopObj = dict[dictKey];
                        result = loopObj.GetType();
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} is not a Dictionary, it's a {1}", p, obj.GetType().FullName));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if a Property exists.
        /// Does not throw exceptions.
        /// Uses Reflection.
        /// Supports extended access syntax Name.NeestedProperty[DictKey1].PropertyName2
        /// </summary>
        /// <param name="obj">Object where the existens of a property should be checked</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>true if the property exists or false if any property does not exists</returns>
        public static bool HasProperty(this object obj, string propName)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (propName == null) throw new ArgumentNullException("propName");
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("HasProperty of {0}.{1}", obj.GetType().FullName, propName);

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
        /// Sets a Property Value from a given Object. 
        /// Uses Reflection.
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
            if (propName == null) throw new ArgumentNullException("propName");
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("SetPropertyValue of {0}.{1}", obj.GetType().FullName, propName);

            var propertylist = propName.Split('.');
            object result = obj;
            foreach (string p in propertylist.Take(propertylist.Count() - 1))
            {
                //PropertyInfo pi = result.GetType().GetProperty(p);
                //if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
                //result = pi.GetValue(result, null);
                result = GetSinglePropertyValue(result, p);
                if (result == null) throw new InvalidOperationException(string.Format("Unable to set Property {0}. The Path contains a NULL Object.", propName));
            }

            var ctd = result as ICustomTypeDescriptor;
            if (ctd != null)
            {
                var pd = ctd.GetProperties()[propName];
                if (pd != null)
                {
                    pd.SetValue(ctd.GetPropertyOwner(pd), val);
                    return;
                }
            }
            PropertyInfo set_pi = result.GetType().GetProperty(propertylist.Last());
            if (set_pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            set_pi.SetValue(result, val, null);
        }

        /// <summary>
        /// Returns a Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// Supports extended access syntax Name.NeestedProperty[DictKey1].PropertyName2
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPropertyValue<T>(this object obj, string propName)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (propName == null) { throw new ArgumentNullException("propName"); }
            if (Logging.Reflection.IsDebugEnabled) Logging.Reflection.DebugFormat("GetPropertyValue of {0}.{1}", obj.GetType().FullName, propName);

            object result = obj;
            foreach (string it_p in propName.Split('.'))
            {
                string dictKey = string.Empty;
                string p = it_p;
                ExtractDictKey(ref dictKey, ref p);

                result = GetSinglePropertyValue(result, p);
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

        private static bool _haveWarnedAboutReflection = false;
        private static object GetSinglePropertyValue(object obj, string propName)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (propName == null) { throw new ArgumentNullException("propName"); }

            var ctd = obj as ICustomTypeDescriptor;
            if (ctd != null)
            {
                var pd = ctd.GetProperties()[propName];
                if (pd != null)
                {
                    return pd.GetValue(ctd.GetPropertyOwner(pd));
                }
            }

            // fallback to reflection
            if (!_haveWarnedAboutReflection && Logging.Reflection.IsWarnEnabled)
            {
                _haveWarnedAboutReflection = true;
                Logging.Reflection.WarnFormat("Fallback to Reflection (first time for {0}.{1})", obj.GetType().FullName, propName);
            }
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return pi.GetValue(obj, null);
        }

        private static void ExtractDictKey(ref string dictKey, ref string p)
        {
            if (p.Contains("[") && p.EndsWith("]"))
            {
                int idx = p.LastIndexOf('[');
                dictKey = p.Substring(idx + 1, p.Length - idx - 2);
                p = p.Substring(0, idx);
            }
        }
        #endregion
    }

    /// <summary>
    /// C# Extensions
    /// </summary>
    public static class ExtensionHelpers
    {
        /// <summary>
        /// Converts a object to XML.
        /// </summary>
        /// <param name="obj">Any XML Serializable Object.</param>
        /// <returns>XML string</returns>
        public static string ToXmlString(this object obj)
        {
            using (Logging.Log.DebugTraceMethodCall("ToXmlString"))
            {
                if (obj == null) { throw new ArgumentNullException("obj"); }

                XmlSerializer xml = new XmlSerializer(obj.GetType());
                StringBuilder sb = new StringBuilder();
                xml.Serialize(new StringWriter(sb), obj);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Converts a object to XML.
        /// </summary>
        /// <param name="obj">Any XML Serializable Object.</param>
        /// <returns>XML in byte[]</returns>
        public static byte[] ToXmlByteArray(this object obj)
        {
            using (Logging.Log.DebugTraceMethodCall("ToXmlString"))
            {
                if (obj == null) { throw new ArgumentNullException("obj"); }

                var xml = new XmlSerializer(obj.GetType());
                using (var stream = new MemoryStream())
                using (var tw = new StreamWriter(stream))
                {
                    xml.Serialize(tw, obj);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Converts a object to XML.
        /// </summary>
        /// <param name="obj">Any XML Serializable Object.</param>
        /// <param name="s">Output stream</param>
        public static void ToXmlStream(this object obj, Stream s)
        {
            using (Logging.Log.DebugTraceMethodCall("ToXmlString"))
            {
                if (obj == null) { throw new ArgumentNullException("obj"); }

                XmlSerializer xml = new XmlSerializer(obj.GetType());
                xml.Serialize(new StreamWriter(s), obj);
            }
        }

        /// <summary>
        /// Converts a XML String to a Objekt.
        /// </summary>
        /// <typeparam name="T">Type of the Object.</typeparam>
        /// <param name="xmlStr">XML string. May not be null.</param>
        /// <returns>Returns a Object or throws an XML-Exception (see MSDN, XmlSerializer)</returns>
        public static T FromXmlString<T>(this string xmlStr)
            where T : new()
        {
            if (xmlStr == null) throw new ArgumentNullException("xmlStr");

            using (Logging.Log.DebugTraceMethodCallFormat("FromXmlString<T>", "Size = [{0}], T = {1}", xmlStr.Length, typeof(T).FullName))
            {
                using (var sr = new StringReader(xmlStr))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T));
                    return (T)xml.Deserialize(sr);
                }
            }
        }

        /// <summary>
        /// Converts a XML byte array to a Objekt.
        /// </summary>
        /// <typeparam name="T">Type of the Object.</typeparam>
        /// <param name="xmlByteArray">XML byte array. May not be null.</param>
        /// <returns>Returns a Object or throws an XML-Exception (see MSDN, XmlSerializer)</returns>
        public static T FromXmlByteArray<T>(this byte[] xmlByteArray)
            where T : new()
        {
            if (xmlByteArray == null) throw new ArgumentNullException("xmlByteArray");

            using (Logging.Log.DebugTraceMethodCallFormat("FromXmlByteArray<T>", "Size = [{0}], T = {1}", xmlByteArray.Length, typeof(T).FullName))
            {
                using (var ms = new MemoryStream(xmlByteArray))
                using (var sr = new StreamReader(ms))
                {
                    var xml = new XmlSerializer(typeof(T));
                    return (T)xml.Deserialize(sr);
                }
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
            if (e == null) { throw new ArgumentNullException("e"); }

            foreach (object v in p)
            {
                if (e.Equals(v)) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a string is one of the given values
        /// </summary>
        /// <param name="str">string to check.</param>
        /// <param name="p">Values</param>
        /// <returns>true, if the Enum is one of the given Values.</returns>
        public static bool In(this string str, params string[] p)
        {
            if (str == null) { throw new ArgumentNullException("str"); }

            foreach (string v in p)
            {
                if (str == v) return true;
            }
            return false;
        }

        /// <summary>
        /// Finds the first member of the given type or null if not found.
        /// </summary>
        /// <param name="t">Type to search</param>
        /// <param name="memberName">Membername to search</param>
        /// <returns>MemberInfo or null if not found</returns>
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
        /// Add a value to a collection.
        /// </summary>
        /// <param name="obj">Object holding the collection</param>
        /// <param name="propName">Propertyname of the collection</param>
        /// <param name="val">value to add</param>
        public static void AddToCollectionQuick(this IDataObject obj, string propName, object val)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            MagicCollectionFactory.WrapAsCollection<object>(obj.GetPropertyValue<object>(propName)).Add(val);
        }

        /// <summary>
        /// Removes a value from a collection.
        /// </summary>
        /// <param name="obj">Object holding the collection</param>
        /// <param name="propName">Propertyname of the collection</param>
        /// <param name="val">value to remove</param>
        public static void RemoveFromCollectionQuick(this IDataObject obj, string propName, object val)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            MagicCollectionFactory.WrapAsCollection<object>(obj.GetPropertyValue<object>(propName)).Remove(val);
        }

        /// <summary>
        /// Add a value to a collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Object holding the collection</param>
        /// <param name="propName">Propertyname of the collection</param>
        /// <param name="val">value to add</param>
        public static void AddToCollection<T>(this object obj, string propName, T val)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            AddToCollection<T>(obj, propName, val, false);
        }

        /// <summary>
        /// Add a value to a collection.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="obj">Object holding the collection</param>
        /// <param name="propName">Propertyname of the collection</param>
        /// <param name="val">value to add</param>
        /// <param name="unique">if the value already exists nothing will be added.</param>
        public static void AddToCollection<T>(this object obj, string propName, T val, bool unique)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            Type collectionType = obj.GetPropertyType(propName);
            Type collectionItemType = collectionType.FindElementTypes().Single(t => t != typeof(object));
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

        public static void Add(this ICollection col, object val, bool unique)
        {
            if (col == null) throw new ArgumentNullException("col");

            Type collectionType = col.GetType();
            Type collectionItemType = collectionType.FindElementTypes().Single(t => t != typeof(object));

            if (unique)
            {
                MethodInfo contains = collectionType.FindMethod("Contains", new Type[] { collectionItemType });
                if (contains == null) throw new ArgumentException("Cound not find \"Contains\" method of the given Collection");
                bool result = (bool)contains.Invoke(col, new object[] { val });
                if (result) return;
            }

            MethodInfo add = collectionType.FindMethod("Add", new Type[] { collectionItemType });
            if (add == null) throw new ArgumentException("Cound not find \"Add\" method of the given Collection");
            add.Invoke(col, new object[] { val });
        }

        /// <summary>
        /// Removes a value from a collection.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="obj">Object holding the collection</param>
        /// <param name="propName">Propertyname of the collection</param>
        /// <param name="val">value to remove</param>
        public static void RemoveFromCollection<T>(this object obj, string propName, T val)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            Type collectionType = obj.GetPropertyType(propName);
            Type collectionItemType = collectionType.FindElementTypes().Single(t => t != typeof(object));
            object collection = pi.GetValue(obj, null);
            if (collection == null) throw new ArgumentException("Collection cannot be null");

            MethodInfo add = collectionType.FindMethod("Remove", new Type[] { collectionItemType });
            if (add == null) throw new ArgumentException("Cound not find \"Remove\" method of the given Collection");
            add.Invoke(collection, new object[] { val });
        }

        public static void Remove(this ICollection col, object val)
        {
            if (col == null) throw new ArgumentNullException("col");

            Type collectionType = col.GetType();
            Type collectionItemType = collectionType.FindElementTypes().Single(t => t != typeof(object));

            MethodInfo remove = collectionType.FindMethod("Remove", new Type[] { collectionItemType });
            if (remove == null) throw new ArgumentException("Cound not find \"Remove\" method of the given Collection");
            remove.Invoke(col, new object[] { val });
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

        /// <summary>
        /// Finds a Method with the given method parameter.
        /// </summary>
        /// <param name="type">Type to search in</param>
        /// <param name="methodName">Methodname to search for</param>
        /// <param name="parameterTypes">parameter types to match</param>
        /// <returns>MethodInfo or null if the method was not found</returns>
        public static MethodInfo FindMethod(this Type type, string methodName, Type[] parameterTypes)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

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

        /// <summary>
        /// Finds a Method with the given method parameter.
        /// </summary>
        /// <param name="type">Type to search in</param>
        /// <param name="methodName">Methodname to search for</param>
        /// <param name="typeArguments">type arguments to match</param>
        /// <param name="parameterTypes">parameter types to match</param>
        /// <returns>MethodInfo or null if the method was not found</returns>
        public static MethodInfo FindGenericMethod(this Type type, string methodName, Type[] typeArguments, Type[] parameterTypes)
        {
            return type.FindGenericMethod(false, methodName, typeArguments, parameterTypes);
        }

        /// <summary>
        /// Finds a Method with the given method parameter.
        /// </summary>
        /// <param name="type">Type to search in</param>
        /// <param name="isPrivate">whether or not the method is private</param>
        /// <param name="methodName">Methodname to search for</param>
        /// <param name="typeArguments">type arguments to match</param>
        /// <param name="parameterTypes">parameter types to match</param>
        /// <returns>MethodInfo or null if the method was not found</returns>
        public static MethodInfo FindGenericMethod(this Type type, bool isPrivate, string methodName, Type[] typeArguments, Type[] parameterTypes)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (parameterTypes == null)
            {
                MethodInfo mi = isPrivate
                    ? type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic)
                    : type.GetMethod(methodName);

                if (mi != null)
                {
                    return mi.MakeGenericMethod(typeArguments);
                }
            }
            else
            {
                MethodInfo[] methods = isPrivate
                    ? type.GetMethods(BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic)
                    : type.GetMethods();

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
                MethodInfo mi = type.BaseType.FindGenericMethod(isPrivate, methodName, typeArguments, parameterTypes);
                if (mi != null) return mi;
            }

            // Look in Interfaces
            foreach (Type i in type.GetInterfaces())
            {
                MethodInfo mi = i.FindGenericMethod(isPrivate, methodName, typeArguments, parameterTypes);
                if (mi != null) return mi;
            }

            return null;
        }

        /// <summary>
        /// Searches a type hierarchie for a event
        /// </summary>
        /// <param name="t">type to search</param>
        /// <param name="name">name of the event to search for</param>
        /// <returns>EventInfo or null if not found</returns>
        public static EventInfo FindEvent(this Type t, string name)
        {
            if (t == null) throw new ArgumentNullException("t");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            while (t != null)
            {
                var result = t.GetEvent(name);
                if (result != null) return result;
                t = t.BaseType;
            }

            return null;
        }

        /// <summary>
        /// Searches a type hierarchie for a property
        /// </summary>
        /// <param name="t">type to search</param>
        /// <param name="name">name of the property to search for</param>
        /// <returns>MemberInfo or null if not found</returns>
        public static MemberInfo[] FindProperty(this Type t, string name)
        {
            if (t == null) throw new ArgumentNullException("t");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            var result = t.GetMember(name, MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            if (result == null || result.Length == 0)
            {
                foreach (var iface in t.GetInterfaces())
                {
                    result = iface.GetMember(name, MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                    if (result != null && result.Length > 0)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Finds all implemented IEnumerables, IQueryables and IOrderedQueryables of the given Type
        /// </summary>
        public static IQueryable<Type> FindSequences(this Type seqType)
        {
            if (seqType == null || seqType == typeof(object) || seqType == typeof(string))
                return new Type[] { }.AsQueryable();

            if (seqType.IsArray || seqType == typeof(IEnumerable))
                return new Type[] { typeof(IEnumerable) }.AsQueryable();

            if (seqType.IsArray || seqType == typeof(IQueryable))
                return new Type[] { typeof(IQueryable) }.AsQueryable();

            if (seqType.IsArray || seqType == typeof(IOrderedQueryable))
                return new Type[] { typeof(IOrderedQueryable) }.AsQueryable();

            if (seqType.IsGenericType && seqType.GetGenericArguments().Length == 1 && seqType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return new Type[] { seqType, typeof(IEnumerable) }.AsQueryable();
            }

            if (seqType.IsGenericType && seqType.GetGenericArguments().Length == 1 && seqType.GetGenericTypeDefinition() == typeof(IQueryable<>))
            {
                return new Type[] { seqType, typeof(IQueryable) }.AsQueryable();
            }

            if (seqType.IsGenericType && seqType.GetGenericArguments().Length == 1 && seqType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>))
            {
                return new Type[] { seqType, typeof(IOrderedQueryable) }.AsQueryable();
            }

            var result = new List<Type>();

            foreach (var iface in (seqType.GetInterfaces() ?? new Type[] { }))
            {
                result.AddRange(FindSequences(iface));
            }

            return FindSequences(seqType.BaseType).Union(result);
        }

        /// <summary>
        /// Finds all element types provided by a specified sequence type.
        /// "Element types" are T for IEnumerable&lt;T&gt; and object for IEnumerable.
        /// </summary>
        public static IQueryable<Type> FindElementTypes(this Type seqType)
        {
            return seqType.FindSequences().Select(t => t.IsGenericType ? t.GetGenericArguments().Single() : typeof(object));
        }

        /// <summary>
        /// Foreach Extension Method for IEnumerable.
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the Enumeration.</typeparam>
        /// <param name="lst">Enumeration</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this IEnumerable lst, Action<T> action)
        {
            if (lst == null) { throw new ArgumentNullException("lst"); }
            if (action == null) { throw new ArgumentNullException("action"); }

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
            if (lst == null) { throw new ArgumentNullException("lst"); }
            if (action == null) { throw new ArgumentNullException("action"); }

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
            if (lst == null) { throw new ArgumentNullException("lst"); }
            if (action == null) { throw new ArgumentNullException("action"); }

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
            if (lst == null) { throw new ArgumentNullException("lst"); }
            if (action == null) { throw new ArgumentNullException("action"); }

            foreach (T i in lst)
            {
                action(i);
            }
        }

        /// <summary>
        /// Parse a GUID value.
        /// Returns Guid.Empty if the str is null or empty or str couldn't be parsed
        /// </summary>
        /// <param name="str">String to parse</param>
        /// <returns>Guid or Guid.Empty</returns>
        public static Guid TryParseGuidValue(this string str)
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

        public static string GetInnerMessage(this Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex.Message;
        }

        public static Exception GetInnerException(this Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex;
        }

        public static List<TITEM> AndParents<T, TITEM>(this T obj, Func<T, IEnumerable<TITEM>> lstSelector, Func<T, T> parentSelector)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (lstSelector == null) throw new ArgumentNullException("lstSelector");
            if (parentSelector == null) throw new ArgumentNullException("parentSelector");

            var result = new List<TITEM>();
            while (obj != null)
            {
                result.AddRange(lstSelector(obj));
                obj = parentSelector(obj);
            }
            return result;
        }

        public static List<T> AndParents<T>(this T obj, Func<T, T> parentSelector)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (parentSelector == null) throw new ArgumentNullException("parentSelector");

            var result = new List<T>();
            while (obj != null)
            {
                result.Add(obj);
                obj = parentSelector(obj);
            }
            return result;
        }
    }

    public static class FileExtensions
    {
        public static void ShellExecute(this System.IO.FileInfo file)
        {
            ShellExecute(file, "");
        }

        public static void ShellExecute(this System.IO.FileInfo file, string verb)
        {
            if (file == null) throw new ArgumentNullException("file");

            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
            si.UseShellExecute = true;
            si.FileName = file.FullName;
            si.Verb = verb;
            System.Diagnostics.Process.Start(si);
        }

        public static string[] GetFileVerbs(this System.IO.FileInfo file)
        {
            if (file == null) throw new ArgumentNullException("file");

            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
            si.UseShellExecute = true;
            si.FileName = file.FullName;
            return si.Verbs;
        }

        public static string GetMimeType(this System.IO.FileInfo file)
        {
            if (file == null) throw new ArgumentNullException("file");

            var mimeType = "application/unknown";
            var ext = System.IO.Path.GetExtension(file.FullName).ToLower();
            using (Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext))
            {
                if (regKey != null)
                {
                    mimeType = regKey.GetValue("Content Type", mimeType).ToString();
                }
            }
            return mimeType;
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

    public static class DateTimeExtensions
    {
        public static DateTime FirstWeekDay(this DateTime dt)
        {
            return dt.Date.AddDays(-(((int)dt.DayOfWeek - 1) % 7));
        }
        public static DateTime FirstMonthDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime FirstYearDay(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }
        public static DateTime FirstQuaterDay(this DateTime dt)
        {
            var q = dt.GetQuater();
            return new DateTime(dt.Year, ((q - 1) * 3) + 1, 1);
        }
        public static DateTime LastMonthDay(this DateTime dt)
        {
            return dt.FirstMonthDay().AddMonths(1).AddDays(-1);
        }
        public static DateTime LastYearDay(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, 31);
        }
        public static DateTime LastQuaterDay(this DateTime dt)
        {
            return FirstQuaterDay(dt).AddMonths(3).AddDays(-1);
        }

        public static int GetQuater(this DateTime dt)
        {
            return ((dt.Month - 1) / 3) + 1;
        }

        public static int GetAge(this DateTime birthday)
        {
            return GetAge(birthday, DateTime.Today);
        }
        public static int GetAge(this DateTime birthday, DateTime refDate)
        {
            int age = (refDate.Year - birthday.Year);
            if (birthday > refDate.AddYears(-age)) age--;
            return age;
        }
    }
}
