using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API.Client;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Implementierung durch den Entwickler der Custom Actions für den Client
    /// </summary>
    public partial class CustomClientActions : API.Client.ICustomClientActions
    {
        #region Projekte
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Projekt_OnToString(Projekt obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Mitarbeiter_OnToString(Mitarbeiter obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Task_OnToString(Task obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.DatumVon.HasValue && obj.DatumBis.HasValue)
            {
                e.Result = string.Format("{0} [{1}] ({2} - {3})",
                    obj.Name, obj.Aufwand, obj.DatumVon.Value.ToShortDateString(), obj.DatumBis.Value.ToShortDateString());
            }
            else
            {
                e.Result = string.Format("{0} [{1}]",
                    obj.Name, obj.Aufwand);
            }
        }
        #endregion

        #region Method
        void imp_OnToString_Method(Kistl.App.Base.Method obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.MethodName;
        }
        #endregion

        #region Module
        void impl_OnToString_Module(Kistl.App.Base.Module obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ModuleName;
        }
        #endregion

        #region ObjectClass
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void ObjectClass_OnToString(ObjectClass obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            //Kistl.App.Base.ModuleClient mClient = new Kistl.App.Base.ModuleClient();
            //Kistl.App.Base.Module m = mClient.GetObject(obj.fk_Module);
            //e.Result = m.Namespace + "." + obj.ClassName;
            //e.Result = obj.GetObject<Module>("Module").Namespace + "." + obj.ClassName;
            e.Result = obj.Module.Namespace + "." + obj.ClassName;
        }
        #endregion

        #region Properties
        void impl_OnToString_BaseProperty(Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} {1}.{2}", 
                obj.GetDataType(), 
                obj.ObjectClass.ClassName, 
                obj.PropertyName);
        }

        void impl_OnToString_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "* " + e.Result;
        }

        void impl_OnToString_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;
        }

        void impl_OnGetDataType_BaseProperty(Kistl.App.Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement BaseProperty.GetDataType()>";
        }

        void impl_OnGetDataType_StringProperty(Kistl.App.Base.StringProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.String";
        }

        void impl_OnGetDataType_DoubleProperty(Kistl.App.Base.DoubleProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Double";
        }

        void impl_OnGetDataType_BoolProperty(Kistl.App.Base.BoolProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Boolean";
        }

        void impl_OnGetDataType_IntProperty(Kistl.App.Base.IntProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.Int32";
        }

        void impl_OnGetDataType_DateTimeProperty(Kistl.App.Base.DateTimeProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "System.DateTime";
        }

        void impl_OnGetDataType_ObjectReferenceProperty(Kistl.App.Base.ObjectReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferenceObjectClass;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }

        void impl_OnGetDataType_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            ObjectClass objClass = obj.ReferenceProperty.ObjectClass;
            e.Result = objClass.Module.Namespace + "." + objClass.ClassName;
        }
        #endregion
    }
}
