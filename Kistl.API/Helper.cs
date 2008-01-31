using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Reflection;
using TraceClient;

namespace Kistl.API
{
    /// <summary>
    /// Globale Helpermethoden
    /// </summary>
    public class Helper
    {
        public const int INVALIDID = -1;

        public static string GetLegalPathName(string path)
        {
            System.IO.Path.GetInvalidPathChars().ToList().ForEach(c => path = path.Replace(c, '_'));

            return path;
        }

        private static string _WorkingFolder;
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
        #region Binary Serializer
        public static void ToBinary(this bool val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(this DateTime val, System.IO.BinaryWriter sw)
        {
            sw.Write(val.ToBinary());
        }

        public static void ToBinary(this int val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(this float val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(this double val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(this string val, System.IO.BinaryWriter sw)
        {
            if (val != null) { sw.Write(true); sw.Write(val); } else sw.Write(false);
        }

        public static void ToBinary(this ObjectType val, System.IO.BinaryWriter sw)
        {
            val.Namespace.ToBinary(sw);
            val.Classname.ToBinary(sw);            
        }

        public static void ToBinary(this bool? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(this DateTime? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value.ToBinary()); } else sw.Write(false);
        }

        public static void ToBinary(this int? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(this float? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(this double? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static bool FromBinary(this bool val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean();
        }

        public static DateTime FromBinary(this DateTime val, System.IO.BinaryReader sr)
        {
            return DateTime.FromBinary(sr.ReadInt64());
        }

        public static int FromBinary(this int val, System.IO.BinaryReader sr)
        {
            return sr.ReadInt32();
        }

        public static float FromBinary(this float val, System.IO.BinaryReader sr)
        {
            return sr.ReadSingle();
        }

        public static double FromBinary(this double val, System.IO.BinaryReader sr)
        {
            return sr.ReadDouble();
        }

        public static string FromBinary(this string val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean() ? sr.ReadString() : null;
        }

        public static ObjectType FromBinary(this ObjectType val, System.IO.BinaryReader sr)
        {
            val.Namespace = val.Namespace.FromBinary(sr);
            val.Classname = val.Classname.FromBinary(sr);

            return val;
        }

        public static bool? FromBinary(this bool? val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean() ? (bool?)sr.ReadBoolean() : null;
        }

        public static DateTime? FromBinary(this DateTime? val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean() ? (DateTime?)DateTime.FromBinary(sr.ReadInt64()) : null;
        }

        public static int? FromBinary(this int? val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean() ? (int?)sr.ReadInt32() : null;
        }

        public static float? FromBinary(this float? val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean() ? (float?)sr.ReadSingle() : null;
        }

        public static double? FromBinary(this double? val, System.IO.BinaryReader sr)
        {
            return sr.ReadBoolean() ? (double?)sr.ReadDouble() : null;
        }
        #endregion

        /// <summary>
        /// Objekt in einen XML String umwandeln
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// XML String in Objekt umwandeln
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static T FromXmlString<T>(this string xmlStr) where T : new()
        {
            using (TraceHelper.TraceMethodCall("Size = {0}", xmlStr.Length))
            {
                System.IO.StringReader sr = new System.IO.StringReader(xmlStr);
                XmlSerializer xml = new XmlSerializer(typeof(T));
                return (T)xml.Deserialize(sr);
            }
        }

        public static bool In(this Enum e, params object[] p)
        {
            foreach (object v in p)
            {
                if (e.Equals(v)) return true;
            }
            return false;
        }

        public static T GetPropertyValue<T>(this object obj, string propName)
        {
            if (obj == null) return default(T);
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)pi.GetValue(obj, null);
        }

        public static void ForEach<T>(this IEnumerable lst, Action<T> action)
        {
            foreach(T obj in lst)
            {
                action(obj);
            }
        }
    }
}
