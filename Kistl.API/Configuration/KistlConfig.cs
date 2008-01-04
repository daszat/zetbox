using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Kistl.API.Configuration
{
    [XmlRoot("KistlConfig", Namespace="http://dasz.at/Kistl/")]
    public class KistlConfig
    {
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
            public string ConnectionString {get; set;}
        }

        public class ClientConfig
        {
            [XmlAttribute]
            public bool StartClient { get; set; }
        }

        /// <summary>
        /// Serializer
        /// </summary>
        private static XmlSerializer xml = new XmlSerializer(typeof(KistlConfig));

        /// <summary>
        /// Serialize this Object to a Stream
        /// </summary>
        /// <param name="s"></param>
        public void ToStream(Stream s)
        {
            xml.Serialize(s, this);
        }

        /// <summary>
        /// Serialize this Object to a File
        /// </summary>
        /// <param name="s"></param>
        public void ToFile(string filename)
        {
            using (XmlTextWriter w = new XmlTextWriter(filename, Encoding.Default))
            {
                xml.Serialize(w, this);
            }
        }

        /// <summary>
        /// Deserialize a TraceMessage from a Stream
        /// </summary>
        /// <param name="s">Stream with XML</param>
        /// <returns>TraceMessage</returns>
        public static KistlConfig FromStream(Stream s)
        {
            return (KistlConfig)xml.Deserialize(s);
        }

        /// <summary>
        /// Deserialize a TraceMessage from a TextReader
        /// </summary>
        /// <param name="s">Stream with XML</param>
        /// <returns>TraceMessage</returns>
        public static KistlConfig FromStream(TextReader s)
        {
            return (KistlConfig)xml.Deserialize(s);
        }

        public static KistlConfig FromFile(string filename)
        {
            using (XmlTextReader r = new XmlTextReader(filename))
            {
                return (KistlConfig)xml.Deserialize(r);
            }
        }
    }
}
