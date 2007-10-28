using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Kistl.API;

namespace Kistl.App.Projekte
{
    [Table(Name="Projekte")]
    public class Projekt : API.IDataObject
    {
        [Column(IsDbGenerated=true, IsPrimaryKey=true, UpdateCheck=UpdateCheck.Never)]
        public int ID { get; set; }

        [Column(UpdateCheck=UpdateCheck.Never)]
        public string Name { get; set; }

        private EntitySet<Task> _Tasks = new EntitySet<Task>();

        [Association(Storage = "_Tasks", OtherKey = "fk_Projekt")]
        [ServerObject(FullName = "Kistl.App.Projekte.TaskServer, Kistl.App.Projekte")]
        [ClientObject(FullName = "Kistl.App.Projekte.TaskClient, Kistl.App.Projekte")]
        [XmlIgnore]
        public EntitySet<Task> Tasks
        {
            get
            {
                return _Tasks;
            }
            set
            {
                _Tasks.Assign(value);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
    
    public class ProjektServer : API.IServerObject
    {
        public string GetList(DataContext ctx)
        {
            return ServerObjectHelper.GetList<Projekt>(ctx).ToXmlString();
        }

        public string GetListOf(DataContext ctx, int ID, string property)
        {
            return ServerObjectHelper.GetListOf<Projekt>(ctx, ID, property).ToXmlString();
        }

        public string GetObject(DataContext ctx, int ID)
        {
            return ServerObjectHelper.GetObject<Projekt>(ctx, ID).ToXmlString();
        }

        public void SetObject(DataContext ctx, string xml)
        {
            ServerObjectHelper.SetObject<Projekt>(ctx, xml.FromXmlString<Projekt>());
        }
    }

    public class ProjektClient : API.IClientObject
    {
        public IEnumerable GetArrayFromXML(string xml)
        {
            return xml.FromXmlString<List<Kistl.App.Projekte.Projekt>>();
        }

        public IEnumerable GetArrayOfTasksFromXML(string xml)
        {
            return xml.FromXmlString<List<Kistl.App.Projekte.Task>>();
        }

        public IDataObject GetObjectFromXML(string xml)
        {
            return xml.FromXmlString<Kistl.App.Projekte.Projekt>();
        }
    }
}
