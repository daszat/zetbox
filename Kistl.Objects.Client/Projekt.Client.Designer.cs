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
using Kistl.API.Client;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Autogeneriert
    /// Client BL Implementierung.
    /// </summary>
    public class ProjektClient : ClientObject<Projekt>
    {
        /// <summary>
        /// Autogeneriert, um die angehängten Listen zu bekommen
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public IEnumerable GetArrayOfTasksFromXML(string xml)
        {
            return xml.FromXmlString<List<Task>>();
        }
    }
}
