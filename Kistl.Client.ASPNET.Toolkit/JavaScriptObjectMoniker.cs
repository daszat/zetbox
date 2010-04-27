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
using Kistl.Client.Presentables;

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
        
        public JavaScriptObjectMoniker(DataObjectModel m)
        {
            Init(m);
        }
        
        public JavaScriptObjectMoniker(IKistlContext ctx, IDataObject obj)
        {
            DataObjectModel m = KistlContextManagerModule.ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(ctx, obj);
            Init(m);
        }

        private void Init(DataObjectModel m)
        {
            ID = m.ID;
            Type = new SerializableType(m.GetInterfaceType());
            Text = m.Name;
        }

        public DataObjectModel GetDataObject(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            IDataObject obj;
            
            if (ID <= Helper.INVALIDID)
            {
                obj = ctx.Create(Type.GetInterfaceType());
                ID = obj.ID;
            }
            else
            {
                obj = (IDataObject)ctx.Find(Type.GetInterfaceType(), ID);
            }

            return KistlContextManagerModule.ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(ctx, obj);
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

        public static string ToJSON(this InterfaceType ifType)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(SerializableType));
            using (var ms = new MemoryStream())
            {
                s.WriteObject(ms, new SerializableType(ifType));
                return GetEncoder().GetString(ms.ToArray());
            }
        }

        public static string ToJSON(this DataObjectModel obj)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(JavaScriptObjectMoniker));
            using (var ms = new MemoryStream())
            {
                s.WriteObject(ms, new JavaScriptObjectMoniker(obj));
                return GetEncoder().GetString(ms.ToArray());
            }
        }

        public static string ToJSONArray(this IEnumerable<DataObjectModel> list)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            using (var ms = new MemoryStream())
            {
                s.WriteObject(ms, list.Select(i => new JavaScriptObjectMoniker(i)));
                return GetEncoder().GetString(ms.ToArray());
            }
        }

        public static IEnumerable<DataObjectModel> FromJSONArray(this string jsonArray, IKistlContext ctx)
        {
            if (string.IsNullOrEmpty(jsonArray)) return new List<DataObjectModel>();

            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            using (var ms = new MemoryStream(GetEncoder().GetBytes(jsonArray)))
            {
                var result = (IEnumerable<JavaScriptObjectMoniker>)s.ReadObject(ms);
                if (result == null)
                {
                    return new List<DataObjectModel>();
                }
                else
                {
                    return result.Select(i => i.GetDataObject(ctx));
                }
            }
        }

        public static DataObjectModel FromJSON(this string json, IKistlContext ctx)
        {
            if (string.IsNullOrEmpty(json)) return null;

            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(JavaScriptObjectMoniker));
            using (var ms = new MemoryStream(GetEncoder().GetBytes(json)))
            {
                var result = (JavaScriptObjectMoniker)s.ReadObject(ms);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result.GetDataObject(ctx);
                }
            }
        }
    }
}
