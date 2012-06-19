// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PrepareEnv
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    LogTitle("Usge: PrepareEnv Path\\To\\Config\\Folder");
                    return 1;
                }

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                var envConfigDir = args[0];
                var envConfig = (EnvConfig)new XmlSerializer(typeof(EnvConfig)).Deserialize(File.OpenRead(Path.Combine(envConfigDir, "env.xml")));

                PrepareEnvConfig(envConfig, envConfigDir);
                InstallBinaries(envConfig);
                InstallConfigs(envConfig);
                EnforceConnectionString(envConfig);
                EnforceAppServer(envConfig);
                DeployDatabaseTemplate(envConfig);

                return 0;
            }
            catch (Exception ex)
            {
                LogTitle(ex.ToString());
                return 1;
            }
        }

        /// <summary>
        /// Load the proper Npgsql library for the current Platform.
        /// </summary>
        /// <remarks>
        /// Npgsql is platform-dependent due to EF-Provider, which is not supported on mono.
        /// To keep PrepareEnv a single .exe, we embed the both dlls and load them conditionally.
        /// </remarks>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = GetAssemblyResourceStream(args.Name))
            {
                if (stream == null)
                    return null;

                checked
                {
                    var buf = new byte[stream.Length];
                    stream.Read(buf, 0, (int)stream.Length);
                    LogAction(string.Format("Loaded {0} from resource", args.Name));
                    return Assembly.Load(buf);
                }
            }
        }

        private static Stream GetAssemblyResourceStream(string assemblyNameString)
        {
            var assemblyName = new AssemblyName(assemblyNameString);

            string resourceName;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    LogAction("using mono2.0");
                    resourceName = string.Format("PrepareEnv.EmbeddedLibs.Npgsql201193binmono20.{0}.dll", assemblyName.Name);
                    break;
                case PlatformID.Win32NT:
                    LogAction("using ms.net3.5sp1");
                    resourceName = string.Format("PrepareEnv.EmbeddedLibs.Npgsql201193binmsnet35sp1.{0}.dll", assemblyName.Name);
                    break;
                default:
                    return null;
            }

            return typeof(Program).Assembly.GetManifestResourceStream(resourceName);
        }

        private static void PrepareEnvConfig(EnvConfig envConfig, string envConfigDir)
        {
            if (envConfig.AppServer != null)
            {
                envConfig.AppServer.Type = ExpandEnvVars(envConfig.AppServer.Type);
                envConfig.AppServer.Uri = ExpandEnvVars(envConfig.AppServer.Uri);
            }

            envConfig.BinarySource = PrepareConfigPath(envConfig.BinarySource);
            envConfig.BinaryTarget = PrepareConfigPath(envConfig.BinaryTarget);

            if (string.IsNullOrEmpty(envConfig.ConfigSource))
            {
                envConfig.ConfigSource = envConfigDir;
            }
            else
            {
                envConfig.ConfigSource = PrepareConfigPath(envConfig.ConfigSource);
            }


            if (envConfig.DatabaseSource != null)
            {
                envConfig.DatabaseSource.Provider = ExpandEnvVars(envConfig.DatabaseSource.Provider);
                envConfig.DatabaseSource.Schema = ExpandEnvVars(envConfig.DatabaseSource.Schema);
                envConfig.DatabaseSource.Value = ExpandEnvVars(envConfig.DatabaseSource.Value);
            }

            if (envConfig.DatabaseTarget != null)
            {
                envConfig.DatabaseTarget.Provider = ExpandEnvVars(envConfig.DatabaseTarget.Provider);
                envConfig.DatabaseTarget.Schema = ExpandEnvVars(envConfig.DatabaseTarget.Schema);
                envConfig.DatabaseTarget.Value = ExpandEnvVars(envConfig.DatabaseTarget.Value);
            }
        }

        private static void DeployDatabaseTemplate(EnvConfig envConfig)
        {
            if (envConfig.DatabaseSource == null || envConfig.DatabaseTarget == null)
                return;

            LogTitle("Deploying Database Template");

            // Delete Target
            using (var schemaProvider = SchemaProvider.SchemaProviderFactory.CreateInstance(envConfig.DatabaseTarget.Schema))
            {
                LogAction("drop target database contents");
                schemaProvider.Open(envConfig.DatabaseTarget.Value);
                schemaProvider.DropAllObjects();
            }

            if (envConfig.DatabaseSource.Schema != "EMPTY" && !string.IsNullOrEmpty(envConfig.DatabaseSource.Value) && !string.IsNullOrEmpty(envConfig.DatabaseTarget.Value))
            {
                // Copy database
                using (var schemaProvider = SchemaProvider.SchemaProviderFactory.CreateInstance(envConfig.DatabaseTarget.Schema))
                {
                    LogAction("copying database");
                    schemaProvider.Copy(envConfig.DatabaseSource.Value, envConfig.DatabaseTarget.Value);
                }
            }
        }

        /// <summary>
        /// Copy from envConfig.BinarySource to envConfig.BinaryTarget; fetch files from Modules\ and Data\
        /// </summary>
        /// <param name="envConfig"></param>
        private static void InstallBinaries(EnvConfig envConfig)
        {
            LogTitle("Installing Binaries");

            // if source is empty or source and target are the same, binaries do not have to be copied
            if (!string.IsNullOrEmpty(envConfig.BinarySource) && envConfig.BinarySource != envConfig.BinaryTarget)
            {
                var sourcePaths = ExpandPath(envConfig.BinarySource);
                var isWildcard = sourcePaths.Count() > 1;

                foreach (var source in sourcePaths)
                {
                    LogAction("copying Binaries from " + source);
                    if (isWildcard && !Directory.Exists(source)) continue;

                    CopyFolder(source, envConfig.BinaryTarget);

                    var bootstrapperSource = Path.Combine(source, "Bootstrapper");
                    if (Directory.Exists(bootstrapperSource))
                    {
                        // Bootstrapper has to be available in the web root
                        CopyFolder(bootstrapperSource, PathX.Combine(envConfig.BinaryTarget, "HttpService", "Bootstrapper"));
                    }
                }
            }

            var moduleTarget = Path.Combine(envConfig.BinaryTarget, "Modules");
            if (Directory.Exists("Modules"))
            {
                LogAction("copying Modules");
                CopyFolder("Modules", moduleTarget);
            }

            var dataTarget = Path.Combine(envConfig.BinaryTarget, "Data");
            if (Directory.Exists("Data"))
            {
                LogAction("copying Data");
                CopyFolder("Data", dataTarget);
            }

            ReplaceNpgsql(envConfig);
        }

        private static IEnumerable<string> ExpandPath(string source)
        {
            List<string> result = new List<string>();

            if (source.Contains("*"))
            {
                var split = source.Split('*');
                if (split.Length != 2) throw new ArgumentOutOfRangeException("source", "only one wildcard is supported yet");

                var baseSource = split[0] + "*";
                var tail = split[1].TrimStart(Path.DirectorySeparatorChar).TrimStart(Path.AltDirectorySeparatorChar);
                var path = Path.GetDirectoryName(baseSource);
                var filter = Path.GetFileName(baseSource);

                result.AddRange(Directory.GetDirectories(path, filter).Select(i => Path.Combine(i, tail)));
            }
            else
            {
                result.Add(source);
            }

            return result;
        }

        private static void ReplaceNpgsql(EnvConfig envConfig)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    LogAction("deploying Npgsql for Zetbox.Server.Service.exe");
                    File.Copy(
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "Server"), "Npgsql.Mono"), "Npgsql.dll"),
                        Path.Combine(envConfig.BinaryTarget, "Npgsql.dll"),
                        true);
                    File.Delete(Path.Combine(envConfig.BinaryTarget, "Mono.Security.dll"));

                    LogAction("deploying Npgsql for HttpService");
                    File.Copy(
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "Server"), "Npgsql.Mono"), "Npgsql.dll"),
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "HttpService"), "bin"), "Npgsql.dll"),
                        true);
                    File.Delete(Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "HttpService"), "bin"), "Mono.Security.dll"));
                    break;
                case PlatformID.Win32NT:
                    LogAction("deploying Npgsql for HttpService");
                    File.Copy(
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "Server"), "Npgsql.Microsoft"), "Npgsql.dll"),
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "HttpService"), "bin"), "Npgsql.dll"),
                        true);
                    File.Copy(
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "Server"), "Npgsql.Microsoft"), "Mono.Security.dll"),
                        Path.Combine(Path.Combine(Path.Combine(envConfig.BinaryTarget, "HttpService"), "bin"), "Mono.Security.dll"),
                        true);
                    break;
                default:
                    LogAction(string.Format("Unkown platform '{0}'", Environment.OSVersion.Platform));
                    return;
            }
        }

        /// <summary>
        /// copy configs from envConfig.ConfigSource to envConfig.BinaryTarget\Configs;
        /// deploy app.configs to their proper resting place beside the executable
        /// </summary>
        /// <param name="envConfig"></param>
        private static void InstallConfigs(EnvConfig envConfig)
        {
            LogTitle("Installing Configs");
            var configTargetDir = Path.Combine(envConfig.BinaryTarget, "Configs");
            // only copy the actual environment's configs, no recursion!
            CopyFolder(envConfig.ConfigSource, configTargetDir);
            // find all app.configs
            foreach (var appConfigFile in Directory.GetFiles(configTargetDir, "*.config"))
            {
                var targetAssemblyFile = Path.GetFileNameWithoutExtension(appConfigFile);
                if (targetAssemblyFile.EndsWith(".Web", StringComparison.InvariantCultureIgnoreCase))
                {
                    var wwwRootName = targetAssemblyFile.Substring(0, targetAssemblyFile.Length - 4);
                    // copy to all relevant WwwRoots
                    foreach (var targetWwwRootPath in FindDirs(wwwRootName, envConfig.BinaryTarget))
                    {
                        var targetFile = Path.Combine(targetWwwRootPath, "Web.config");
                        LogAction(string.Format("copying config to {0}", targetFile));
                        File.Copy(appConfigFile, targetFile, true);
                    }
                }
                else
                {
                    // copy to all relevant executables/dlls
                    foreach (var targetAssemblyPath in FindFiles(targetAssemblyFile, envConfig.BinaryTarget))
                    {
                        var targetFile = targetAssemblyPath + ".config";
                        LogAction(string.Format("copying config to {0}", targetFile));
                        File.Copy(appConfigFile, targetFile, true);
                    }
                }
            }
        }

        /// <summary>
        /// forces all configuration's connection string to be envConfig.DatabaseTarget
        /// </summary>
        /// <param name="envConfig"></param>
        private static void EnforceConnectionString(EnvConfig envConfig)
        {
            if (envConfig.DatabaseTarget == null)
                return;

            LogTitle("Enforcing connection string");
            foreach (var configPath in Directory.GetFiles(Path.Combine(envConfig.BinaryTarget, "Configs"), "*.xml"))
            {
                var doc = new XmlDocument();
                doc.Load(configPath);

                // create prefix<->namespace mappings (if any)
                var nsMgr = new XmlNamespaceManager(doc.NameTable);
                nsMgr.AddNamespace("k", "http://dasz.at/Zetbox/");

                // check whether this is a ZetboxConfig
                var configSet = doc.SelectNodes("/k:ZetboxConfig", nsMgr);
                if (configSet.Count == 0)
                    continue; // nope, ignore!

                var serverNode = doc.SelectSingleNode("/k:ZetboxConfig/k:Server[@StartServer='true']", nsMgr);
                if (serverNode == null)
                    continue; // no startable server, ignore!

                // Select a database called "Zetbox"
                var databaseNode = doc.SelectSingleNode("/k:ZetboxConfig/k:Server[@StartServer=true]/k:ConnectionStrings/k:Database[@Name=Zetbox]", nsMgr);
                if (databaseNode == null)
                {
                    databaseNode = doc.CreateElement("Database", "http://dasz.at/Zetbox/");
                    var connectionStringsNode = doc.SelectSingleNode("/k:ZetboxConfig/k:Server[@StartServer=true]/k:ConnectionStrings", nsMgr);
                    if (connectionStringsNode == null)
                    {
                        connectionStringsNode = doc.CreateElement("ConnectionStrings", "http://dasz.at/Zetbox/");
                        serverNode.PrependChild(connectionStringsNode);
                    }
                    connectionStringsNode.PrependChild(databaseNode);
                }
                EnsureAttribute(doc, databaseNode, "Name");
                databaseNode.Attributes["Name"].Value = "Zetbox";
                EnsureAttribute(doc, databaseNode, "Schema");
                databaseNode.Attributes["Schema"].Value = envConfig.DatabaseTarget.Schema;
                EnsureAttribute(doc, databaseNode, "Provider");
                databaseNode.Attributes["Provider"].Value = envConfig.DatabaseTarget.Provider;
                databaseNode.InnerText = envConfig.DatabaseTarget.Value;

                LogAction(string.Format("set connection string in {0}", configPath));
                doc.Save(configPath);
            }
        }

        private static void EnforceAppServer(EnvConfig envConfig)
        {
            if (envConfig.AppServer == null || string.IsNullOrEmpty(envConfig.AppServer.Uri))
                return;

            LogTitle("Enforcing app server");
            foreach (var configPath in Directory.GetFiles(envConfig.BinaryTarget, "*.config", SearchOption.AllDirectories))
            {
                var doc = new XmlDocument();
                doc.Load(configPath);

                var endpoints = doc.SelectNodes("/configuration/system.serviceModel/client/endpoint");
                foreach (XmlElement endpoint in endpoints)
                {
                    endpoint.Attributes["address"].Value = envConfig.AppServer.Uri;
                    LogAction(string.Format("set endpoint address in {0}", configPath));
                }

                var serviceUris = doc.SelectNodes("/configuration/appSettings/add[@key='serviceUri']");
                foreach (XmlElement serviceUri in serviceUris)
                {
                    serviceUri.Attributes["value"].Value = envConfig.AppServer.Uri;
                    LogAction(string.Format("set serviceUri address in {0}", configPath));
                }

                doc.Save(configPath);
            }
        }

        private static void EnsureAttribute(XmlDocument doc, XmlNode databaseNode, string attributeName)
        {
            if (databaseNode.Attributes[attributeName] == null)
            {
                var schemaAttr = doc.CreateAttribute(attributeName);
                databaseNode.Attributes.Append(schemaAttr);
            }
        }

        #region Utilities

        private static void EnsureDirectory(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        private static void CopyFolder(string sourceDir, string targetDir)
        {
            CopyTopFiles(sourceDir, targetDir);
            foreach (var folder in Directory.GetDirectories(sourceDir))
            {
                var target = Path.Combine(targetDir, Path.GetFileName(folder));
                CopyFolder(folder, target);
            }
        }

        private static void CopyTopFiles(string sourceDir, string targetDir)
        {
            EnsureDirectory(targetDir);
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var target = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(file, target, true);
            }
        }

        private static IEnumerable<string> FindDirs(string dirName, string baseDirectory)
        {
            var result = new List<string>();
            foreach (var folder in Directory.GetDirectories(baseDirectory).Select(path => Path.GetFileName(path)))
            {
                if (folder == dirName)
                {
                    result.Add(Path.Combine(baseDirectory, folder));
                }
                result.AddRange(FindDirs(dirName, Path.Combine(baseDirectory, folder)));
            }
            return result;
        }

        private static IEnumerable<string> FindFiles(string fileName, string baseDirectory)
        {
            var result = new List<string>();
            foreach (var file in Directory.GetFiles(baseDirectory).Select(path => Path.GetFileName(path)))
            {
                if (file == fileName)
                {
                    result.Add(Path.Combine(baseDirectory, file));
                }
            }
            foreach (var folder in Directory.GetDirectories(baseDirectory).Select(path => Path.GetFileName(path)))
            {
                result.AddRange(FindFiles(fileName, Path.Combine(baseDirectory, folder)));
            }
            return result;
        }

        private static readonly Regex EnvVar = new Regex("%([a-zA-Z0-9_]+)%");

        /// <summary>
        /// Replaces %FOO% environment variable references and translates the (back-)slashes to the platform's preferred form.
        /// </summary>
        private static string PrepareConfigPath(string input)
        {
            input = ExpandEnvVars(input);

            // canonicalize slashiness in paths from the configuration
            input = PathX.Combine(input.Split('\\', '/'));

            return input;
        }

        /// <summary>
        /// Replaces %FOO% environment variable references.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ExpandEnvVars(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var envVars = Environment.GetEnvironmentVariables().Keys.Cast<string>().ToLookup(s => s);

            foreach (var repl in EnvVar.Matches(input).Cast<Match>()
                .Select(m => new { str = m.Groups[0].Value, name = m.Groups[1].Value })
                .Where(m => envVars.Contains(m.name))
                .ToList())
            {
                input = input.Replace(repl.str, Environment.GetEnvironmentVariable(repl.name));
            }
            return input;
        }

        private static void LogTitle(string msg)
        {
            Console.WriteLine(msg);
        }

        private static void LogAction(string msg)
        {
            Console.Write("    ");
            Console.WriteLine(msg);
        }

        #endregion
    }
}
