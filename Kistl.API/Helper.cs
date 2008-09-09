using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Reflection;
using TraceClient;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

        /// <summary>
        /// Private Field for the Working Folder
        /// </summary>
        private static string _WorkingFolder;
        /// <summary>
        /// Gets the Working Folder. Path is [LocalApplicationData]\dasz\Kistl\[Current Configuration Name]\[AppDomain.FriendlyName]
        /// eg.: C:\Users\Arthur\AppData\Local\dasz\Kistl\Arthur's Configuration\Kistl.Client.exe
        /// </summary>
        public static string WorkingFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_WorkingFolder))
                {
                    _WorkingFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    _WorkingFolder += _WorkingFolder.EndsWith(@"\") ? "" : @"\";

                    _WorkingFolder += @"dasz\Kistl\"
                        + Helper.GetLegalPathName(Configuration.KistlConfig.Current.ConfigName)
                        + @"\"
                        + Helper.GetLegalPathName(AppDomain.CurrentDomain.FriendlyName)
                        + @"\";

                    System.IO.Directory.CreateDirectory(_WorkingFolder);
                }
                return _WorkingFolder;
            }
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
            using (TraceHelper.TraceMethodCall())
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
            using (TraceHelper.TraceMethodCall("Size = {0}", xmlStr.Length))
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
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)pi.GetValue(obj, null);
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
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            pi.SetValue(obj, val, null);
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
            PropertyInfo pi = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            pi.SetValue(obj, val, null);
        }

        /// <summary>
        /// Returns a private Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
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
        /// TODO: Das passt mir nicht!
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Type GetInterfaceType(this IPersistenceObject obj)
        {
            return ToInterfaceType(obj.GetType());
        }

        public static Type ToInterfaceType(this Type type)
        {
            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                List<Type> genericArguments = new List<Type>();
                genericArguments.AddRange(type.GetGenericArguments().Select(t => t.ToInterfaceType()));

                return genericType.MakeGenericType(genericArguments.ToArray());
            }
            else
            {
                if (
                    (typeof(IDataObject).IsAssignableFrom(type) || typeof(IStruct).IsAssignableFrom(type)) 
                    && type.Name.EndsWith("Impl")
                    )
                {
                    type = Type.GetType(type.FullName.Substring(0, type.FullName.Length - 4) + ", Kistl.Objects", true);
                }
                return type;
            }
        }

        public static Type ToImplementationType(this Type type)
        {
            if (type.IsGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                List<Type> genericArguments = new List<Type>();
                genericArguments.AddRange(type.GetGenericArguments().Select(t => t.ToImplementationType()));

                return genericType.MakeGenericType(genericArguments.ToArray());
            }
            else
            {
                if (type == typeof(IDataObject))
                {
                    return APIInit.BaseDataObjectType;
                }
                else if (type == typeof(IPersistenceObject))
                {
                    return APIInit.BasePersistenceObjectType;
                }
                else if (type == typeof(IStruct))
                {
                    return APIInit.BaseStructObjectType;
                }
                else if (type == typeof(ICollectionEntry))
                {
                    return APIInit.BaseCollectionEntryType;
                }
                else if (typeof(IDataObject).IsAssignableFrom(type) && !APIInit.BaseDataObjectType.IsAssignableFrom(type))
                {
                    // add "Impl"
                    string newType = type.FullName + "Impl, " + APIInit.ImplementationAssembly;
                    return Type.GetType(newType, true);
                }
                else
                {
                    return type;
                }
            }
        }

        public static MethodInfo FindGenericMethod(this Type type, string methodName, Type[] typeArguments, Type[] parameterTypes)
        {
            if (parameterTypes == null)
            {
                MethodInfo mi = type.GetMethod(methodName);
                if (mi == null) return null;
                return mi.MakeGenericMethod(typeArguments);
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

            return null;
        }

        public static Type GetCollectionElementType(this Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }

        public static Type FindIEnumerable(this Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;

            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetCollectionElementType());

            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }

            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }

            return null;
        }

        /// <summary>
        /// Foreach Extension Method for IEnumerable. This Extension does not check if the Enumeration Entry is NULL!
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the Enumeration.</typeparam>
        /// <param name="lst">Enumeration</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this IEnumerable lst, Action<T> action)
        {
            foreach(T obj in lst)
            {
                action(obj);
            }
        }

        /// <summary>
        /// Foreach Extension Method for IList<>. This Extension does not check if the Enumeration Entry is NULL!
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
        /// Foreach Extension Method for IQueryable<>. This Extension does not check if the query results contain NULLs!
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
