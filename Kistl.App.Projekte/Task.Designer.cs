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
    /// </summary>
    [Table(Name = "Tasks")]
    public class Task : IDataObject
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never)]
        public int ID { get; set; }
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
    /// </summary>
    public class TaskServer : API.IServerObjectFactory
    {
        public IServerObject GetServerObject()
        {
            return new API.ServerObject<Task>();
        }
    }

    /// <summary>
    /// Autogeneriert
    /// </summary>
    public class TaskClient : API.IClientObjectFactory
    {
        public IClientObject GetClientObject()
        {
            return new ClientObject<Task>();
        }
    }
}
