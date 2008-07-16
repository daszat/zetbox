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
    public sealed class Helper
    {
        /// <summary>
        /// Constant for a invalid ID. Value is -1.
        /// </summary>
        public const int INVALIDID = -1;

        /// <summary>
        /// Newly created objects are not yet saved to the server and therefore handle some data only locally.
        /// This method can distinguish them from "older" objects that already have a representation on the server.
        /// </summary>
        /// <returns>true when the object has a valid context and already exists on server</returns>
        public static bool IsPersistedObject(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return obj.Context != null && obj.ID > 0;
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
        /// <summary>
        /// prevent this class from being instantiated.
        /// </summary>
        private Helper() { }
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
            FieldInfo fi = obj.GetType().GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
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
        /// Foreach Extension Method for ObservableCollection. This Extension does not check if the ObservableCollection Entry is NULL!
        /// </summary>
        /// <typeparam name="T">Type of the Objects in the ObservableCollection.</typeparam>
        /// <param name="lst">ObservableCollection</param>
        /// <param name="action">Action to perform on each element.</param>
        public static void ForEach<T>(this ObservableCollection<T> lst, Action<T> action)
        {
            foreach (T i in lst)
            {
                action(i);
            }
        }
    }
}
