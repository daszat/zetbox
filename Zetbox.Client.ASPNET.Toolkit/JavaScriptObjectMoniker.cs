// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Toolkit
{
    [DataContract]
    [Serializable]
    public class JavaScriptObjectMoniker
    {
        [DataMember(Name = "ID")]
        public int ID { get; set; }
        [DataMember(Name = "Type")]
        public SerializableType Type { get; set; }
        [DataMember(Name = "Text")]
        public string Text { get; set; }

        public JavaScriptObjectMoniker()
        {
        }
        
        public JavaScriptObjectMoniker(DataObjectViewModel m)
        {
            Init(m);
        }
        
        public JavaScriptObjectMoniker(IZetboxContext ctx, IDataObject obj)
        {
            DataObjectViewModel m = DataObjectViewModel.Fetch(ZetboxContextManagerModule.ViewModelFactory, ctx, null, obj);
            Init(m);
        }

        private void Init(DataObjectViewModel m)
        {
            ID = m.ID;
            Type = m.GetInterfaceType().ToSerializableType();
            Text = m.Name;
        }

        public DataObjectViewModel GetDataObject(IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            IDataObject obj;
            
            if (ID <= Helper.INVALIDID)
            {
                obj = ctx.Create(ZetboxContextManagerModule.IftFactory(Type.GetSystemType()));
                ID = obj.ID;
            }
            else
            {
                obj = (IDataObject)ctx.Find(ZetboxContextManagerModule.IftFactory(Type.GetSystemType()), ID);
            }

            return DataObjectViewModel.Fetch(ZetboxContextManagerModule.ViewModelFactory, ctx, null, obj);
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

        public static IEnumerable<DataObjectViewModel> FromJSONArray(this string jsonArray, IZetboxContext ctx)
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

        public static DataObjectViewModel FromJSON(this string json, IZetboxContext ctx)
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
