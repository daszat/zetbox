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

        public IDataObject GetDataObject(IKistlContext ctx)
        {
            return ctx.Find(Type.GetSerializedType(), ID);
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

        public static string ToJSON(this System.Type type)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(SerializableType));
            MemoryStream ms = new MemoryStream();
            s.WriteObject(ms, new SerializableType(type));
            return GetEncoder().GetString(ms.ToArray());
        }

        public static string ToJSONArray(this IEnumerable<IDataObject> list)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            MemoryStream ms = new MemoryStream();
            s.WriteObject(ms, list.Select(i => new JavaScriptObjectMoniker(i)));
            return GetEncoder().GetString(ms.ToArray());
        }

        public static IEnumerable<IDataObject> FromJSONArray(this string jsonArray, IKistlContext ctx)
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
                return result.Select(i => i.GetDataObject(ctx));
            }
        }

        public static T FromJSON<T>(this string json, IKistlContext ctx) where T : IDataObject
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(JavaScriptObjectMoniker));
            MemoryStream ms = new MemoryStream(GetEncoder().GetBytes(json));
            var result = (JavaScriptObjectMoniker)s.ReadObject(ms);
            if (result == null)
            {
                return default(T);
            }
            else
            {
                return (T)result.GetDataObject(ctx);
            }
        }
    }
}
