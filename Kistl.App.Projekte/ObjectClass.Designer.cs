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

namespace Kistl.App.Base
{
    /// <summary>
    /// Autogeneriert
    /// Daten-Objekt Definition
    /// </summary>
    [Table(Name = "ObjectClasses")]
    public class ObjectClass : API.BaseDataObject
    {
        private int _ID = Helper.INVALIDID;
        [Column(IsDbGenerated = true, IsPrimaryKey = true, UpdateCheck = UpdateCheck.Never, Storage = "_ID")]
        public override int ID { get { return _ID; } set { _ID = value; } }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public string ClassName { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public string Namespace { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public string TableName { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public string ServerObject { get; set; }
        
        [Column(UpdateCheck = UpdateCheck.Never)]
        public string ClientObject { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public string DataObject { get; set; }

        private EntitySet<ObjectProperty> _Properties = new EntitySet<ObjectProperty>();

        [Association(Storage = "_Properties", OtherKey = "fk_ObjectClass")]
        [ServerObject(FullName = "Kistl.App.Base.ObjectPropertyServer, Kistl.App.Projekte")]
        [ClientObject(FullName = "Kistl.App.Base.ObjectPropertyClient, Kistl.App.Projekte")]
        [XmlIgnore] // Das verschwinded dann bald!
        public EntitySet<ObjectProperty> Properties
        {
            get
            {
                return _Properties;
            }
            set
            {
                _Properties.Assign(value);
            }
        }

        public event ToStringHandler<ObjectClass> OnToString = null;

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
    public class ObjectClassServer : ServerObject<ObjectClass>
    {
    }

    /// <summary>
    /// Autogeneriert
    /// Client BL Implementierung.
    /// </summary>
    public class ObjectClassClient : ClientObject<ObjectClass>
    {
        /// <summary>
        /// Autogeneriert, um die angehängten Listen zu bekommen
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IEnumerable GetArrayOfPropertiesFromXML(string xml)
        {
            return xml.FromXmlString<List<ObjectProperty>>();
        }
    }
}
