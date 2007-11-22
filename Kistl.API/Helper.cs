using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace Kistl.API
{
    /// <summary>
    /// Globale Helpermethoden
    /// </summary>
    public class Helper
    {
        public const int INVALIDID = -1;
    }

    /// <summary>
    /// C# Extensions
    /// </summary>
    public static class ExtensionHelpers
    {
        /// <summary>
        /// Objekt in einen XML String umwandeln
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXmlString(this object obj)
        {
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            xml.Serialize(new System.IO.StringWriter(sb), obj);
            return sb.ToString();
        }

        /// <summary>
        /// XML String in Objekt umwandeln
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static T FromXmlString<T>(this string xmlStr) where T : new()
        {
            System.IO.StringReader sr = new System.IO.StringReader(xmlStr);
            XmlSerializer xml = new XmlSerializer(typeof(T));
            return (T)xml.Deserialize(sr);
        }

        public static bool In(this Enum e, params object[] p)
        {
            foreach (object v in p)
            {
                if (e.Equals(v)) return true;
            }
            return false;
        }


    }
}
