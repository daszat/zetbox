using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Kistl.API.Configuration
{
    /// <summary>
    /// Configuration of Kistl
    /// </summary>
    [XmlRoot("KistlConfig", Namespace="http://dasz.at/Kistl/")]
    public class KistlConfig : MarshalByRefObject
    {
        private static KistlConfig _Current = null;

        /// <summary>
        /// Current Configuration
        /// </summary>
        public static KistlConfig Current
        {
            get
            {
                if (_Current == null) throw new InvalidOperationException("Configuration was not setuped yet");
                return _Current;
            }
        }

        /// <summary>
        /// Checks, if config is initialized yet
        /// </summary>
        internal static bool IsInitialized
        {
            get
            {
                return _Current != null;
            }
        }

        /// <summary>
        /// Inits the config
        /// </summary>
        /// <param name="config">Path to the Config File. May be null or empty. Then DefaultConfig.xml is loaded</param>
        internal static void Init(string file)
        {
            if (Configuration.KistlConfig.IsInitialized) throw new InvalidOperationException("Configuration already setuped");
            if (!string.IsNullOrEmpty(file))
            {
                _Current = FromFile(file);
            }
            else
            {
                // Try to load default config
                _Current = FromFile("DefaultConfig.xml");
            }
        }

        [XmlIgnore]
        public string ConfigFilePath { get; set; }

        [XmlElement(IsNullable = false)]
        public string ConfigName { get; set; }

        [XmlElement(IsNullable = true)]
        public ServerConfig Server { get; set; }

        [XmlElement(IsNullable = true)]
        public ClientConfig Client { get; set; }

        [XmlArray(IsNullable = false)]
        public string[] SourceFileLocation { get; set; }

        [XmlElement(IsNullable = false)]
        public string Address;

        public class ServerConfig
        {
            [XmlAttribute]
            public bool StartServer { get; set; }

            [XmlElement(IsNullable = false)]
            public string DatabaseProvider { get; set; }

            [XmlElement(IsNullable = false)]
            public string ConnectionString {get; set;}

            [XmlElement(IsNullable = false)]
            public string DocumentStore { get; set; }
        }

        public class ClientConfig
        {
            [XmlAttribute]
            public bool StartClient { get; set; }

            [XmlAttribute]
            public bool ThrowErrors { get; set; }

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
        /// <param name="s">configuration file w/ or w/o path</param>
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
