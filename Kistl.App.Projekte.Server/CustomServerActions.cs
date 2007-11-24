using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Implementierung durch den Entwickler der Custom Actions für den Server
    /// </summary>
    public partial class CustomServerActions : API.Server.ICustomServerActions
    {
        #region Projekte
        /// <summary>
        /// PreSave für Projekte, beim Projektnamen "_action" hinzufügen.
        /// Sinnlos, aber ganz lustig
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="obj"></param>
        void Projekt_OnPreSetObject(Projekt obj)
        {
            if (obj.EntityState == System.Data.EntityState.Modified)
            {
                obj.AufwandGes = obj.Tasks.Sum(t => t.Aufwand);
            }

            /*var result = (from t in Kistl.API.Server.KistlDataContext.Current.GetTable<Task>()
                          where t.fk_Projekt == obj.ID
                          select t).Sum(s => s.Aufwand);*/
        }

        /// <summary>
        /// Überprüfung eines Tasks - sinnlos, aber ganz lustig.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="obj"></param>
        void Task_OnPreSetObject(Task obj)
        {
            if (obj.Aufwand < 0) throw new ApplicationException("Ungültiger Aufwand");
            if (obj.DatumBis < obj.DatumVon) throw new ApplicationException("Falsches Zeitalter");
        }

        /// <summary>
        /// Auch am Server ToString überschreiben.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Projekt_OnToString(Projekt obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// Auch am Server ToString überschreiben.
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

        #region ObjectClass
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void ObjectClass_OnToString(Base.ObjectClass obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Module.Namespace + "." + obj.ClassName;
        }
        #endregion

        #region Properties
        void impl_OnToString_BaseProperty(Base.BaseProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} {1}", obj.GetDataType(), obj.PropertyName);
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
            e.Result = obj.ReferenceObjectClassName;
        }

        void impl_OnGetDataType_BackReferenceProperty(Kistl.App.Base.BackReferenceProperty obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ReferenceObjectClassName;
        }
        #endregion
    }
}
