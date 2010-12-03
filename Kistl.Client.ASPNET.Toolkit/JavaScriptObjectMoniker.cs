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
        
        public JavaScriptObjectMoniker(DataObjectViewModel m)
        {
            Init(m);
        }
        
        public JavaScriptObjectMoniker(IKistlContext ctx, IDataObject obj)
        {
            DataObjectViewModel m = DataObjectViewModel.Fetch(KistlContextManagerModule.ViewModelFactory, ctx, obj);
            Init(m);
        }

        private void Init(DataObjectViewModel m)
        {
            ID = m.ID;
            Type = m.GetInterfaceType().ToSerializableType();
            Text = m.Name;
        }

        public DataObjectViewModel GetDataObject(IKistlContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            IDataObject obj;
            
            if (ID <= Helper.INVALIDID)
            {
                obj = ctx.Create(KistlContextManagerModule.IftFactory(Type.GetSystemType()));
                ID = obj.ID;
            }
            else
            {
                obj = (IDataObject)ctx.Find(KistlContextManagerModule.IftFactory(Type.GetSystemType()), ID);
            }

            return DataObjectViewModel.Fetch(KistlContextManagerModule.ViewModelFactory, ctx, obj);
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
                s.WriteObject(ms, ifType.ToSerializableType());
                return GetEncoder().GetString(ms.ToArray());
            }
        }

        public static string ToJSON(this DataObjectViewModel obj)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(JavaScriptObjectMoniker));
            using (var ms = new MemoryStream())
            {
                s.WriteObject(ms, new JavaScriptObjectMoniker(obj));
                return GetEncoder().GetString(ms.ToArray());
            }
        }

        public static string ToJSONArray(this IEnumerable<DataObjectViewModel> list)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            using (var ms = new MemoryStream())
            {
                s.WriteObject(ms, list.Select(i => new JavaScriptObjectMoniker(i)));
                return GetEncoder().GetString(ms.ToArray());
            }
        }

        public static IEnumerable<DataObjectViewModel> FromJSONArray(this string jsonArray, IKistlContext ctx)
        {
            if (string.IsNullOrEmpty(jsonArray)) return new List<DataObjectViewModel>();

            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(IEnumerable<JavaScriptObjectMoniker>));
            using (var ms = new MemoryStream(GetEncoder().GetBytes(jsonArray)))
            {
                var result = (IEnumerable<JavaScriptObjectMoniker>)s.ReadObject(ms);
                if (result == null)
                {
                    return new List<DataObjectViewModel>();
                }
                else
                {
                    return result.Select(i => i.GetDataObject(ctx));
                }
            }
        }

        public static DataObjectViewModel FromJSON(this string json, IKistlContext ctx)
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
