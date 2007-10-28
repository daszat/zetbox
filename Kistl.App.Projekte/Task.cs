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
    /// Datacontract
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

        public override string ToString()
        {
            return string.Format("{0} [{1}] ({2} - {3})", 
                Name, Aufwand, DatumVon.ToShortDateString(), DatumBis.ToShortDateString());
        }
    }

    public class TaskServer : API.IServerObject
    {
        public string GetList(DataContext ctx)
        {
            return ServerObjectHelper.GetList<Task>(ctx).ToXmlString();
        }

        public string GetListOf(DataContext ctx, int ID, string property)
        {
            return ServerObjectHelper.GetListOf<Task>(ctx, ID, property).ToXmlString();
        }

        public string GetObject(DataContext ctx, int ID)
        {
            return ServerObjectHelper.GetObject<Task>(ctx, ID).ToXmlString();
        }

        public void SetObject(DataContext ctx, string xml)
        {
            ServerObjectHelper.SetObject<Task>(ctx, xml.FromXmlString<Task>());
        }
    }

    public class TaskClient : API.IClientObject
    {
        public IEnumerable GetArrayFromXML(string xml)
        {
            return xml.FromXmlString<List<Kistl.App.Projekte.Task>>();
        }

        public IDataObject GetObjectFromXML(string xml)
        {
            return xml.FromXmlString<Kistl.App.Projekte.Task>();
        }
    }
}
