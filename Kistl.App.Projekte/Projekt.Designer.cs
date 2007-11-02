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
    public class Projekt : API.BaseDataObject
    {
        private int _ID = Helper.INVALIDID;
        [Column(IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never, Storage = "_ID")]
        public override int ID { get { return _ID; } set { _ID = value; } }

        [Column(UpdateCheck = UpdateCheck.Never)]
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
    public class ProjektServer : API.ServerObject<Projekt>
    {
    }

    /// <summary>
    /// Autogeneriert, um die angehängten Listen zu bekommen
    /// </summary>
    public class ProjektClient : ClientObject<Projekt>
    {
        public IEnumerable GetArrayOfTasksFromXML(string xml)
        {
            IEnumerable result = xml.FromXmlString<List<Task>>();
            //AttachClientEvents(result);
            return result;
        }
    }
}
