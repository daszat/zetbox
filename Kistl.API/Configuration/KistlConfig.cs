using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Kistl.API.Configuration
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException()
            : base("There was a problem with the configuration")
        {
        }
        public ConfigurationException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Configuration of Kistl
    /// </summary>
    [XmlRoot("KistlConfig", Namespace = "http://dasz.at/Kistl/")]
    public class KistlConfig : MarshalByRefObject
    {

        /// <summary>
        /// Create an empty configuration
        /// </summary>
        public KistlConfig() { }

        /// <summary>
        /// Path to the Config File
        /// </summary>
        [XmlIgnore]
        public string ConfigFilePath { get; set; }

        /// <summary>
        /// Configuration Name
        /// </summary>
        [XmlElement(IsNullable = false)]
        public string ConfigName { get; set; }

        /// <summary>
        /// Server Configuration
        /// </summary>
        [XmlElement(IsNullable = true)]
        public ServerConfig Server { get; set; }

        /// <summary>
        /// Client Configuration
        /// </summary>
        [XmlElement(IsNullable = true)]
        public ClientConfig Client { get; set; }

        /// <summary>
        /// Location (Path) to Assemblies - TODO: Will be replaced!
        /// </summary>
        [XmlArray(IsNullable = false)]
        public string[] SourceFileLocation { get; set; }

        /// <summary>
        /// Address of the WCF Service - not used yet!
        /// </summary>
        [XmlElement(IsNullable = false)]
        public string Address;

        /// <summary>
        /// Server Configuration
        /// </summary>
        public class ServerConfig
        {
            /// <summary>
            /// If running in a Client host: Should a Server be started.
            /// </summary>
            [XmlAttribute]
            public bool StartServer { get; set; }

            /// <summary>
            /// Database Provider Name
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string DatabaseProvider { get; set; }

            /// <summary>
            /// Connectionstring to Database.
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string ConnectionString { get; set; }

            /// <summary>
            /// Path of the Document Store
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string DocumentStore { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string KistlDataContextType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string ServerObjectHandlerType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string ServerObjectSetHandlerType { get; set; }
        }

        /// <summary>
        /// Client Configuration
        /// </summary>
        public class ClientConfig
        {
            /// <summary>
            /// Should a Client be started.
            /// </summary>
            [XmlAttribute]
            public bool StartClient { get; set; }

            /// <summary>
            /// Used in Debug Mode - should Exceptions be thrown.
            /// </summary>
            [XmlAttribute]
            public bool ThrowErrors { get; set; }

            /// <summary>
            /// Path to the Document Store - TODO: Will be replaced by a Document Cache
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string DocumentStore { get; set; }
        }

        /// <summary>
        /// Serializer
        /// </summary>
        private static XmlSerializer xml = new XmlSerializer(typeof(KistlConfig));

        /// <summary>
        /// Serialize this Object to a Stream
        /// </summary>
        /// <param name="s">Stream</param>
        public void ToStream(Stream s)
        {
            xml.Serialize(s, this);
        }

        /// <summary>
        /// Serialize this Object to a File
        /// </summary>
        /// <param name="filename">configuration file w/ or w/o path</param>
        public void ToFile(string filename)
        {
            using (XmlTextWriter w = new XmlTextWriter(filename, Encoding.Default))
            {
                xml.Serialize(w, this);
            }
        }

        /// <summary>
        /// Deserialize from a Stream
        /// </summary>
        /// <param name="s">Stream with XML</param>
        /// <returns>Current Configuration</returns>
        public static KistlConfig FromStream(Stream s)
        {
            return (KistlConfig)xml.Deserialize(s);
        }

        /// <summary>
        /// Deserialize from a TextReader
        /// </summary>
        /// <param name="s">Stream with XML</param>
        /// <returns>Current Configuration</returns>
        public static KistlConfig FromStream(TextReader s)
        {
            return (KistlConfig)xml.Deserialize(s);
        }

        /// <summary>
        /// Deserialize from a TextReader
        /// </summary>
        /// <param name="filename">configuration file w/ or w/o path</param>
        /// <returns>Current Configuration</returns>
        public static KistlConfig FromFile(string filename)
        {
            using (XmlTextReader r = new XmlTextReader(filename))
            {
                KistlConfig result = (KistlConfig)xml.Deserialize(r);
                result.ConfigFilePath = filename;
                return result;
            }
        }
    }
}
