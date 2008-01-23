using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using System.Xml;
using System.IO;

namespace Kistl.App.Base
{
    public class CustomClientActions_KistlBase
    {
        #region ToString
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_ObjectClass(ObjectClass obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Module.Namespace + "." + obj.ClassName;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void OnToString_MethodInvokation(MethodInvocation obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.InvokeOnObjectClass.ClassName + "." + obj.Method.MethodName;
        }

        public void OnToString_BaseProperty(Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} {1}.{2}",
                obj.GetDataType(),
                obj.ObjectClass.ClassName,
                obj.PropertyName);
        }

        public void OnToString_Method(Kistl.App.Base.Method obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ObjectClass.Module.Namespace + "." +
                obj.ObjectClass.ClassName + "." + obj.MethodName;
        }

        public void OnToString_Assembly(Kistl.App.Base.Assembly obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.AssemblyName;
        }

        public void OnToString_Module(Kistl.App.Base.Module obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ModuleName;
        }

        public void OnToString_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "* " + e.Result;
        }

        public void OnToString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;
        }
        #endregion

        #region GetDataType
        public void OnGetDataType_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement BaseProperty.GetDataType()>";
        }

        public void OnGetDataType_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferenceObjectClass;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }

        public void OnGetDataType_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferenceProperty.ObjectClass;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }

        public void OnGetDataType_StringProperty(Kistl.App.Base.StringProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        public void OnGetDataType_DoubleProperty(Kistl.App.Base.DoubleProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        public void OnGetDataType_BoolProperty(Kistl.App.Base.BoolProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        public void OnGetDataType_IntProperty(Kistl.App.Base.IntProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        public void OnGetDataType_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }
        #endregion
    }
}