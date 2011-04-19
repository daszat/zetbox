using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Kistl.API.Utils;

namespace Kistl.API.Configuration
{
    [Serializable]
    public class ConfigurationException
        : Exception
    {
        public ConfigurationException()
            : base("There was a problem with the configuration")
        {
        }
        public ConfigurationException(string message)
            : base(message)
        {
        }
        public ConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected ConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Configuration of Kistl
    /// </summary>
    [XmlRoot("KistlConfig", Namespace = "http://dasz.at/Kistl/")]
    [Serializable]
    public class KistlConfig
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
        [XmlElement(IsNullable = false)]
        public ServerConfig Server { get; set; }

        [XmlIgnore]
        public bool ServerSpecified { get; set; }

        /// <summary>
        /// Client Configuration
        /// </summary>
        [XmlElement(IsNullable = false)]
        public ClientConfig Client { get; set; }

        [XmlIgnore]
        public bool ClientSpecified { get; set; }

        /// <summary>
        /// Location (Path) to Assemblies
        /// <see cref="AssemblyLoader.SearchPath"/>
        /// </summary>
        [XmlArray(IsNullable = false)]
        public string[] AssemblySearchPaths { get; set; }

        /// <summary>
        /// Server Configuration
        /// </summary>
        [Serializable]
        public class ServerConfig
        {
            /// <summary>
            /// If running in a Client host: Should a Server be started.
            /// </summary>
            [XmlAttribute]
            public bool StartServer { get; set; }

            [Serializable]
            public class Database
            {
                /// <summary>
                /// Keyname of the connection string
                /// </summary>
                [XmlAttribute]
                public string Name { get; set; }

                /// <summary>
                /// Name of schema provider
                /// </summary>
                [XmlAttribute("Schema")]
                public string SchemaProvider { get; set; }

                /// <summary>
                /// Name of database provider
                /// </summary>
                [XmlAttribute("Provider")]
                public string DatabaseProvider { get; set; }

                /// <summary>
                /// The connection string
                /// </summary>
                [XmlText]
                public string ConnectionString { get; set; }
            }

            public Database GetConnectionString(string key)
            {
                var result = ConnectionStrings.Where(i => i.Name == key).ToArray();
                if (result.Length == 0)
                {
                    throw new ArgumentOutOfRangeException("key", string.Format("No connection string with key '{0}' found", key));
                }
                else if (result.Length > 1)
                {
                    throw new InvalidOperationException(string.Format("Found {0} connection strings with key '{0}' found", result.Length, key));
                }
                else
                {
                    return result.Single();
                }
            }

            /// <summary>
            /// Collection of connection strings
            /// </summary>
            [XmlArray(IsNullable = false)]
            public Database[] ConnectionStrings { get; set; }

            /// <summary>
            /// Path of the Document Store
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string DocumentStore { get; set; }

            /// <summary>
            /// Where the Generator will put its files while working. Contents 
            /// of this directory are nuked on each run.
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string CodeGenWorkingPath { get; set; }

            /// <summary>
            /// Where the Generator will store its results when successful. 
            /// Contents of this directory are replaced on each successful run.
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string CodeGenOutputPath { get; set; }

            /// <summary>
            /// Where the Generator will publish the generated binaries after a successful compile.
            /// </summary>
            [XmlArray(IsNullable = false)]
            public string[] CodeGenBinaryOutputPath { get; set; }

            /// <summary>
            /// Where the Generator will store the contents of the 
            /// <see cref="CodeGenOutputPath"/> before replacing it. Leave 
            /// this configuration parameter empty to suppress archival.
            /// </summary>
            [XmlElement(IsNullable = false)]
            public string CodeGenArchivePath { get; set; }

            /// <summary>
            /// AutoFac modules to load
            /// </summary>
            [XmlArray("Modules")]
            [XmlArrayItem("Module", typeof(string))]
            public string[] Modules { get; set; }

            /// <summary>
            /// Client Files
            /// </summary>
            [XmlArray("ClientFilesLocations")]
            [XmlArrayItem("Location")]
            public ClientFilesLocation[] ClientFilesLocations { get; set; }

            [Serializable]
            public class ClientFilesLocation
            {
                [XmlAttribute("Name")]
                public string Name { get; set; }
                [XmlAttribute("Exclude")]
                public string Exclude { get; set; }
                [XmlText]
                public string Value { get; set; }
            }
        }

        /// <summary>
        /// Client Configuration
        /// </summary>
        [Serializable]
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
            /// If a Guid is specified, the given Application is launched
            /// </summary>
            [XmlElement(IsNullable = true)]
            public Guid? Application { get; set; }

            /// <summary>
            /// Overrides the current system culture
            /// </summary>
            [XmlElement(IsNullable = true)]
            public string Culture { get; set; }

            /// <summary>
            /// Overrides the current system ui culture
            /// </summary>
            [XmlElement(IsNullable = true)]
            public string UICulture { get; set; }

            /// <summary>
            /// </summary>
            [XmlElement(IsNullable = true)]
            public bool? DevelopmentEnvironment { get; set; }

            [XmlIgnore]
            public bool DevelopmentEnvironmentSpecified { get; set; }

            /// <summary>
            /// AutoFac modules to load
            /// </summary>
            [XmlArray("Modules")]
            [XmlArrayItem("Module", typeof(string))]
            public string[] Modules { get; set; }
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
        /// <param name="fallbackBaseName">A configuration name to search in the %zenv% environmentvariable, if none is specified in the first parameter</param>
        /// <returns>Current Configuration</returns>
        public static KistlConfig FromFile(string filename, string fallbackBaseName)
        {
            filename = String.IsNullOrEmpty(filename) ? GetDefaultConfigName(fallbackBaseName) : filename;

            if (!File.Exists(filename))
                throw new FileNotFoundException(String.Format("Configuration file [{0}] not found", filename), filename);

            using (var r = new XmlTextReader(filename))
            {
                KistlConfig result = (KistlConfig)xml.Deserialize(r);
                result.ConfigFilePath = filename;
                return result;
            }
        }

        public static string GetDefaultConfigName(string basename)
        {
            return GetDefaultConfigName(basename, "Configs");
        }

        public static string GetDefaultConfigName(string basename, string baseDir)
        {
            var zenv = Environment.GetEnvironmentVariable("zenv");
            if (String.IsNullOrEmpty(zenv))
            {
                Logging.Log.WarnOnce("No zenv explicitely set, using [Local]");
                zenv = "Local";
            }
            var file = Path.IsPathRooted(zenv)
                ? Path.Combine(zenv, basename)
                : Path.GetFullPath(Helper.PathCombine(baseDir, zenv, basename));
            Logging.Log.InfoFormat("Got zenv=[{0}], trying file=[{1}]", zenv, file);
            while (!Path.IsPathRooted(zenv) && !File.Exists(file) && !String.IsNullOrEmpty(zenv))
            {
                Logging.Log.InfoFormat("rooted={0}, exists={1}, zenv_empty={2}", Path.IsPathRooted(zenv), File.Exists(file), String.IsNullOrEmpty(zenv));
                // this will reduce zenv directory component-wise until nothing is left
                zenv = Path.GetDirectoryName(zenv);
                file = Path.GetFullPath(Helper.PathCombine(baseDir, zenv, basename));
                Logging.Log.InfoFormat("Got zenv=[{0}], trying file=[{1}]", zenv, file);
            }
            if (!File.Exists(file))
            {
                Logging.Log.WarnFormat("No default configuration found for zenv=[{0}], basename=[{1}]", Environment.GetEnvironmentVariable("zenv"), basename);
                var defaultConfig = Path.Combine(baseDir, "DefaultConfig.xml");
                if (File.Exists(defaultConfig))
                {
                    Logging.Log.WarnFormat("using default config from [{0}]", defaultConfig);
                    return defaultConfig;
                }
                else
                {
                    throw new FileNotFoundException(String.Format("Could not find [{0}] in zenv [{1}]", basename, Environment.GetEnvironmentVariable("zenv")), basename);
                }
            }
            else
            {
                Logging.Log.InfoFormat("Default configuration found for zenv=[{0}], basename=[{1}]: [{2}]", Environment.GetEnvironmentVariable("zenv"), basename, Path.GetFullPath(file));
                return file;
            }
        }

        /// <summary>
        /// The WorkingFolder of this application contains all local state of the app.
        /// The Path is [LocalApplicationData]\dasz\Kistl\[Current Configuration Name]\[AppDomain.FriendlyName]\
        /// </summary>
        /// <remarks>
        /// eg.: C:\Users\Arthur\AppData\Local\dasz\Kistl\Arthur's Configuration\Kistl.Client.exe\
        /// </remarks>
        [XmlIgnore]
        public string WorkingFolder
        {
            get
            {
                string workingFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                workingFolder = Path.Combine(workingFolder, "dasz");
                workingFolder = Path.Combine(workingFolder, "zbox");
                workingFolder = Path.Combine(workingFolder, Helper.GetLegalPathName(this.ConfigName));

                // TODO: very bad idea because this may change when passing the config between AppDomains
                workingFolder = Path.Combine(workingFolder, Helper.GetLegalPathName(AppDomain.CurrentDomain.FriendlyName));

                System.IO.Directory.CreateDirectory(workingFolder);
                return workingFolder;
            }
        }
    }
}
