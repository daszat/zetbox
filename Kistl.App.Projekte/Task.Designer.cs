using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Collections;
using System.Data.Linq;
using Kistl.API;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Autogeneriert
    /// Daten-Objekt Definition
    /// </summary>
    [Table(Name = "Tasks")]
    public sealed class Task : BaseDataObject
    {
        private int _ID = Helper.INVALIDID;
        [Column(IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never, Storage = "_ID")]
        public override int ID { get { return _ID; } set { _ID = value; } }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime DatumVon { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime DatumBis { get; set; }
        [Column(UpdateCheck = UpdateCheck.Never)]
        public double Aufwand { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public int fk_Projekt { get; set; }


        public event ToStringHandler<Task> OnToString = null;

        public override string ToString()
        {
            if (OnToString != null)
            {
                ToStringEventArgs e = new ToStringEventArgs();
                OnToString(this, e);
                return e.Result;
            }
            return base.ToString();
        }
    }

    /// <summary>
    /// Autogeneriert
    /// Server BL Implementierung.
    /// </summary>
    public class TaskServer : ServerObject<Task>
    {
    }

    /// <summary>
    /// Autogeneriert
    /// Client BL Implementierung.
    /// </summary>
    public class TaskClient : ClientObject<Task>
    {
    }

}
