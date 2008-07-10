using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Web;

namespace Kistl.Client.ASPNET.Toolkit
{
    [DataContract]
    [Serializable]
    public class JavaScriptObjectMoniker
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public SerializableType Type { get; set; }
        [DataMember]
        public string Text { get; set; }

        public JavaScriptObjectMoniker()
        {
        }

        public JavaScriptObjectMoniker(IDataObject obj)
        {
            ID = obj.ID;
            Type = new SerializableType(obj.GetType());
            Text = obj.ToString();
        }

        public ObjectMoniker GetObjectMoniker()
        {
            return new ObjectMoniker(ID, Type.GetSerializedType(), Text);
        }
    }

    public static class JavaScriptObjectMonikerExtensions
    {
        private static Encoding GetEncoder()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.ContentEncoding;
            }
            else
            {
                return Encoding.Default;
            }
        }

        public static string ToJSONArray(this IEnumerable<IDataObject> list)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            MemoryStream ms = new MemoryStream();
            s.WriteObject(ms, list.Select(i => new JavaScriptObjectMoniker(i)));
            return GetEncoder().GetString(ms.ToArray());
        }

        public static IEnumerable<IDataObject> FromJSONArray(this string jsonArray)
        {
            if (string.IsNullOrEmpty(jsonArray)) return new List<IDataObject>();
 
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            MemoryStream ms = new MemoryStream(GetEncoder().GetBytes(jsonArray));
            var result = (IEnumerable<JavaScriptObjectMoniker>)s.ReadObject(ms);
            if (result == null)
            {
                return new List<IDataObject>();
            }
            else
            {
                return result.Select(i => i.GetObjectMoniker()).Cast<IDataObject>();
            }
        }
    }
}
