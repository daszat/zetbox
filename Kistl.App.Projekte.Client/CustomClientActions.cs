using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Implementierung durch den Entwickler der Custom Actions für den Client
    /// </summary>
    public partial class CustomClientActions : API.Client.ICustomClientActions
    {
        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Projekt_OnToString(Projekt obj, Kistl.API.ToStringEventArgs e)
        {
            e.Result = obj.Name;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void Task_OnToString(Task obj, Kistl.API.ToStringEventArgs e)
        {
            e.Result = string.Format("{0} [{1}] ({2} - {3})",
                obj.Name, obj.Aufwand, obj.DatumVon.ToShortDateString(), obj.DatumBis.ToShortDateString());
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void ObjectClass_OnToString(Base.ObjectClass obj, Kistl.API.ToStringEventArgs e)
        {
            e.Result = obj.ClassName;
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        void ObjectProperty_OnToString(Base.ObjectProperty obj, Kistl.API.ToStringEventArgs e)
        {
            e.Result = string.Format("{0} {1}", obj.DataType, obj.PropertyName);
        }
    }
}
