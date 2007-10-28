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
    /// <summary>
    /// Autogeneriert
    /// </summary>
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

        public event ToStringHandler<Projekt> OnToString = null;

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
    public class ProjektServer : API.IServerObjectFactory
    {
        public IServerObject GetServerObject()
        {
            return new API.ServerObject<Projekt>();
        }
    }

    /// <summary>
    /// Autogeneriert
    /// </summary>
    public class ProjektClient : API.IClientObjectFactory
    {
        public IClientObject GetClientObject()
        {
            return new ProjektClientImpl();
        }
    }

    /// <summary>
    /// Autogeneriert, um die angehängten Listen zu bekommen
    /// </summary>
    public class ProjektClientImpl : ClientObject<Projekt>
    {
        public IEnumerable GetArrayOfTasksFromXML(string xml)
        {
            return xml.FromXmlString<List<Task>>();
        }
    }
}
