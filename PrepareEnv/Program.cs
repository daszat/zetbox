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
        static bool debug = false;

        static int Main(string[] args)
        {
            try
            {
                string envConfigDir;

                if (args.Length == 2)
                {
                    if (args[0] == "--debug")
                    {
                        debug = true;
                        // shift
                        args = new[] { args[1] };
                    }
                    else
                    {
                        LogTitle("ERROR: unknown option [{0}]", args[0]);
                        PrintUsage();
                        return 1;
                    }
                }
                // no elsif: because of shift
                if (args.Length == 1)
                {
                    if (!Directory.Exists(args[0]))
                    {
                        LogTitle("ERROR: Cannot find directory [{0}]", args[0]);
                        PrintUsage();
                        return 1;
                    }
                    else
                    {
                        envConfigDir = args[0];
                    }
                }
                else
                {
                    PrintUsage();
                    return 1;
                }

                var envConfig = (EnvConfig)new XmlSerializer(typeof(EnvConfig)).Deserialize(File.OpenRead(Path.Combine(envConfigDir, "env.xml")));

                PrepareEnvConfig(envConfig, envConfigDir);

                InstallBinaries(envConfig);
                InstallConfigs(envConfig);

                InstallTestsBinaries(envConfig);
                InstallTestsConfigs(envConfig);

                InstallGeneratedBinaries(envConfig);
                InstallTestsGeneratedBinaries(envConfig);

                EnforceConnectionString(envConfig);
                EnforceAppServer(envConfig);

                ClickOnceGenerator.BuildClickOnce(envConfig);

                DeployDatabaseTemplate(envConfig);

                return 0;
            }
            catch (Exception ex)
            {
                LogTitle(ex.ToString());
                return 1;
            }
        }

        private static void PrintUsage()
        {
            LogTitle("Usage: PrepareEnv [--debug] Path\\To\\Config\\Folder");
        }

        private static void PrepareEnvConfig(EnvConfig envConfig, string envConfigDir)
        {
            if (envConfig.AppServer != null)
            {
                envConfig.AppServer.Type = ExpandEnvVars(envConfig.AppServer.Type);
                envConfig.AppServer.Uri = ExpandEnvVars(envConfig.AppServer.Uri);
            }

            envConfig.BinarySource = PrepareConfigPath(envConfig.BinarySource);
            envConfig.GeneratedSource = PrepareConfigPath(envConfig.GeneratedSource);
            envConfig.BinaryTarget = PrepareConfigPath(envConfig.BinaryTarget);
            envConfig.TestsTarget = PrepareConfigPath(envConfig.TestsTarget);

            envConfig.ClientExe = ExpandEnvVars(envConfig.ClientExe ?? "Zetbox.WPF.exe");
            envConfig.ClientParameters = PrepareConfigPath(envConfig.ClientParameters);

            if (envConfig.ClickOnce != null)
            {
                envConfig.ClickOnce.Publisher = ExpandEnvVars(envConfig.ClickOnce.Publisher);
                envConfig.ClickOnce.SuiteName = ExpandEnvVars(envConfig.ClickOnce.SuiteName);
                // provide a sensible default
                envConfig.ClickOnce.Product = ExpandEnvVars(envConfig.ClickOnce.Product) ?? string.Format("{0}'s zetbox client", envConfig.ClickOnce.Publisher);
                envConfig.ClickOnce.SupportUrl = ExpandEnvVars(envConfig.ClickOnce.SupportUrl);
                envConfig.ClickOnce.ErrorReportUrl = ExpandEnvVars(envConfig.ClickOnce.ErrorReportUrl);
                // if no URL is specified, the ClickOnce installer will use the .application location automatically
                envConfig.ClickOnce.UpdateUrl = ExpandEnvVars(envConfig.ClickOnce.UpdateUrl);
                envConfig.ClickOnce.KeyFile = PrepareConfigPath(envConfig.ClickOnce.KeyFile);
                envConfig.ClickOnce.DeploymentVersion = ExpandEnvVars(envConfig.ClickOnce.DeploymentVersion);
            }

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
            if (!string.IsNullOrEmpty(envConfig.BinarySource) && !string.IsNullOrEmpty(envConfig.BinaryTarget) && envConfig.BinarySource != envConfig.BinaryTarget)
            {
                var sourcePaths = ExpandPath(envConfig.BinarySource);
                var isWildcard = sourcePaths.Count() > 1;

                var copiedCommonFiles = new List<string>();
                var additionalCopiedFiles = new List<string>();
                foreach (var source in sourcePaths)
                {
                    if (isWildcard && !Directory.Exists(source)) continue;
                    LogAction("copying common Binaries from " + source);

                    if (Directory.Exists(Path.Combine(source, "Common")))
                    {
                        var copiedPaths = CopyFolder(Path.Combine(source, "Common"), Path.Combine(envConfig.BinaryTarget, "Common"));
                        copiedCommonFiles.AddRange(copiedPaths.Select(s => Path.GetFileName(s)));
                    }
                }

                foreach (var source in sourcePaths)
                {
                    if (isWildcard && !Directory.Exists(source)) continue;

                    LogAction("copying Binaries from " + source);

                    if (Directory.Exists(Path.Combine(source, "Server")))
                    {
                        var copiedPaths = CopyFolder(Path.Combine(source, "Server"), Path.Combine(envConfig.BinaryTarget, "Server"), copiedCommonFiles, null, CopyMode.RestoreHierarchie);
                        additionalCopiedFiles.AddRange(copiedPaths.Select(s => Path.GetFileName(s)));
                    }

                    if (Directory.Exists(Path.Combine(source, "Client")))
                    {
                        var copiedPaths = CopyFolder(Path.Combine(source, "Client"), Path.Combine(envConfig.BinaryTarget, "Client"), copiedCommonFiles, null, CopyMode.RestoreHierarchie);
                        additionalCopiedFiles.AddRange(copiedPaths.Select(s => Path.GetFileName(s)));
                    }

                    if (Directory.Exists(Path.Combine(source, "Modules")))
                    {
                        CopyFolder(Path.Combine(source, "Modules"), Path.Combine(envConfig.BinaryTarget, "Modules"));
                    }

                    if (Directory.Exists(Path.Combine(source, "Data")))
                    {
                        CopyFolder(Path.Combine(source, "Data"), Path.Combine(envConfig.BinaryTarget, "Data"));
                    }
                }

                additionalCopiedFiles.AddRange(copiedCommonFiles);

                LogAction("copying HttpService and associated binaries");
                foreach (var source in sourcePaths)
                {
                    if (isWildcard && !Directory.Exists(source)) continue;

                    if (Directory.Exists(Path.Combine(source, "HttpService")))
                    {
                        LogDetail("copying from " + source);
                        CopyFolder(Path.Combine(source, "HttpService"), Path.Combine(envConfig.BinaryTarget, "HttpService"));
                    }
                }
                LogDetail("copying from deployed Common");
                CopyFolder(Path.Combine(envConfig.BinaryTarget, "Common"), Path.Combine(envConfig.BinaryTarget, "HttpService", "bin", "Common"));
                LogDetail("copying from deployed Server");
                CopyFolder(Path.Combine(envConfig.BinaryTarget, "Server"), Path.Combine(envConfig.BinaryTarget, "HttpService", "bin", "Server"));

                LogAction("copying ASPNET and associated binaries");
                foreach (var source in sourcePaths)
                {
                    if (isWildcard && !Directory.Exists(source)) continue;

                    if (Directory.Exists(Path.Combine(source, "ASPNET")))
                    {
                        LogDetail("copying from " + source);
                        CopyFolder(Path.Combine(source, "ASPNET"), Path.Combine(envConfig.BinaryTarget, "ASPNET"));
                    }
                }
                LogDetail("copying from deployed Common");
                CopyFolder(Path.Combine(envConfig.BinaryTarget, "Common"), Path.Combine(envConfig.BinaryTarget, "ASPNET", "bin", "Common"));
                LogDetail("copying from deployed Server");
                CopyFolder(Path.Combine(envConfig.BinaryTarget, "Server"), Path.Combine(envConfig.BinaryTarget, "ASPNET", "bin", "Server"));
                LogDetail("copying from deployed Client");
                CopyFolder(Path.Combine(envConfig.BinaryTarget, "Client"), Path.Combine(envConfig.BinaryTarget, "ASPNET", "bin", "Client"));

                foreach (var source in sourcePaths)
                {
                    LogAction("copying executables from " + source);
                    CopyTopFiles(source, envConfig.BinaryTarget, additionalCopiedFiles);

                    LogAction("copying Bootstrapper from " + source);
                    if (isWildcard && !Directory.Exists(source)) continue;

                    var bootstrapperSource = Path.Combine(source, "Bootstrapper");
                    if (Directory.Exists(bootstrapperSource))
                    {
                        // Bootstrapper has to be available in the web root
                        CopyFolder(bootstrapperSource, Path.Combine(envConfig.BinaryTarget, "HttpService", "Bootstrapper"));
                    }
                }

                // The following will be needed when deploying
                if (Directory.Exists("Modules"))
                {
                    LogAction("copying local Modules");
                    CopyFolder("Modules", Path.Combine(envConfig.BinaryTarget, "Modules"));
                }

                if (Directory.Exists("Data"))
                {
                    LogAction("copying local Data");
                    CopyFolder("Data", Path.Combine(envConfig.BinaryTarget, "Data"));
                }
            }
        }

        /// <summary>
        /// Copy from envConfig.BinarySource to envConfig.TestsTarget
        /// </summary>
        /// <param name="envConfig"></param>
        private static void InstallTestsBinaries(EnvConfig envConfig)
        {
            LogTitle("Installing Tests Binaries");

            // if source is empty or source and target are the same, binaries do not have to be copied
            if (!string.IsNullOrEmpty(envConfig.TestsTarget) && !string.IsNullOrEmpty(envConfig.BinarySource) && envConfig.BinarySource != envConfig.TestsTarget)
            {
                var sourcePaths = ExpandPath(envConfig.BinarySource);
                var isWildcard = sourcePaths.Count() > 1;

                foreach (var source in sourcePaths)
                {
                    LogAction("copying Binaries from " + source);
                    if (isWildcard && !Directory.Exists(source)) continue;

                    CopyFolder(source, envConfig.TestsTarget, null, "*.exe", CopyMode.Flat); // does not handle sattelite assemblies
                    CopyFolder(source, envConfig.TestsTarget, null, "*.exe.config", CopyMode.Flat);
                    CopyFolder(source, envConfig.TestsTarget, null, "*.dll", CopyMode.Flat); // does not handle sattelite assemblies
                    CopyFolder(source, envConfig.TestsTarget, null, "*.dll.config", CopyMode.Flat);
                    CopyFolder(source, envConfig.TestsTarget, null, "*.pdb", CopyMode.Flat);
                    CopyFolder(source, envConfig.TestsTarget, null, "*.mdb", CopyMode.Flat);
                }

                // Replace fallback binaries when generated ones are available
                foreach (var generatedSource in new[] { Path.Combine(envConfig.BinarySource, "Common", "Generated"),
                    Path.Combine(envConfig.BinarySource, "Client", "Generated"),
                    Path.Combine(envConfig.BinarySource, "Server", "Generated") })
                {
                    var generatedSourcePaths = ExpandPath(generatedSource);

                    foreach (var path in generatedSourcePaths)
                    {
                        LogDetail("looking at [{0}]", path);
                        if (Directory.Exists(path))
                        {
                            LogDetail("found");
                            CopyTopFiles(path, envConfig.TestsTarget, null);
                        }
                        else
                        {
                            LogDetail("not found");
                        }
                    }
                }
            }
        }

        private static void InstallGeneratedBinaries(EnvConfig envConfig)
        {
            InstallGeneratedBinaries(envConfig.GeneratedSource, envConfig.BinaryTarget, CopyMode.RestoreHierarchie);
        }

        private static void InstallTestsGeneratedBinaries(EnvConfig envConfig)
        {
            InstallGeneratedBinaries(envConfig.GeneratedSource, envConfig.TestsTarget, CopyMode.Flat);
        }

        private static void InstallGeneratedBinaries(string source, string target, CopyMode copyMode)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target) || source == target)
                return;

            if (!Directory.Exists(source))
            {
                LogTitle("Skipping Generated Binaries: source ({0}) doesn't exist", source);
                return;
            }

            LogTitle("Installing Generated Binaries");

            var sourcePaths = ExpandPath(source);
            var isWildcard = sourcePaths.Count() > 1;

            foreach (var sourcePath in sourcePaths)
            {
                LogAction("copying Binaries from " + sourcePath);
                if (isWildcard && !Directory.Exists(sourcePath)) continue;

                CopyFolder(sourcePath, target, null, null, copyMode);
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
            if (!string.IsNullOrEmpty(envConfig.BinaryTarget))
            {
                var configTargetDir = Path.Combine(envConfig.BinaryTarget, "Configs");
                LogDetail("copy folder");
                // copy all configs
                CopyFolder(envConfig.ConfigSource, configTargetDir);
                LogDetail("deploy configs");
                // find all app.configs
                DeployConfigs(configTargetDir, envConfig.BinaryTarget);
            }
        }

        /// <summary>
        /// Deploys all app configs to reside by their proper binaries in the assemblyTargetDir.
        /// </summary>
        private static void DeployConfigs(string configSourceDir, string assemblyTargetDir)
        {
            foreach (var appConfigFile in Directory.GetFiles(configSourceDir, "*.config"))
            {
                var targetAssemblyFile = Path.GetFileNameWithoutExtension(appConfigFile);
                EnforceBindingRedirects(appConfigFile, assemblyTargetDir);

                if (targetAssemblyFile.EndsWith(".Web", StringComparison.InvariantCultureIgnoreCase))
                {
                    var wwwRootName = targetAssemblyFile.Substring(0, targetAssemblyFile.Length - 4);
                    // copy to all relevant WwwRoots
                    foreach (var targetWwwRootPath in FindDirs(wwwRootName, assemblyTargetDir))
                    {
                        var targetFile = Path.Combine(targetWwwRootPath, "Web.config");
                        Copy(appConfigFile, targetFile, true);
                    }
                }
                else
                {
                    // copy to all relevant executables/dlls
                    foreach (var targetAssemblyPath in FindFiles(targetAssemblyFile, assemblyTargetDir))
                    {
                        var targetFile = targetAssemblyPath + ".config";
                        Copy(appConfigFile, targetFile, true);
                    }
                }
            }
        }

        private static void EnforceBindingRedirects(string configPath, string assemblyTargetDir)
        {
            LogDetail("enforcing binding redirects for {0}", configPath);

            var doc = new XmlDocument();
            doc.Load(configPath);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ab", "urn:schemas-microsoft-com:asm.v1");

            var dependentAssemblies = doc.SelectNodes("/configuration/runtime/ab:assemblyBinding/ab:dependentAssembly", nsmgr);
            foreach (XmlElement dependentAssembly in dependentAssemblies)
            {
                var redirect = dependentAssembly.SelectSingleNode("ab:bindingRedirect", nsmgr);
                var assemblyIdentity = dependentAssembly.SelectSingleNode("ab:assemblyIdentity", nsmgr);
                if (redirect != null && assemblyIdentity != null)
                {
                    var name = assemblyIdentity.Attributes["name"].Value;
                    var files = FindFiles(name + ".dll", assemblyTargetDir);

                    if (files.Any())
                    {
                        var a = Assembly.LoadFile(Path.GetFullPath(files.FirstOrDefault()));
                        var newVersion = a.GetName().Version.ToString();
                        LogDetail("{0} => {1}", name, newVersion);
                        redirect.Attributes["oldVersion"].Value = "0.0.0.0-" + newVersion;
                        redirect.Attributes["newVersion"].Value = newVersion;
                    }
                    else 
                    {
                        LogDetail("File {0} not found", name);
                    }
                }
            }

            doc.Save(configPath);
        }

        /// <summary>
        /// copy configs from envConfig.ConfigSource to envConfig.BinaryTarget\Configs;
        /// deploy app.configs to their proper resting place beside the executable
        /// </summary>
        /// <param name="envConfig"></param>
        private static void InstallTestsConfigs(EnvConfig envConfig)
        {
            LogTitle("Installing Tests Configs");
            if (!string.IsNullOrEmpty(envConfig.TestsTarget) && !string.IsNullOrEmpty(envConfig.ConfigSource))
            {
                var configTargetDir = Path.Combine(envConfig.TestsTarget, "Configs");
                // copy all configs
                CopyFolder(envConfig.ConfigSource, configTargetDir);
                DeployConfigs(configTargetDir, envConfig.TestsTarget);
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

            foreach (var configPath in GetConfigFilenames(envConfig.BinaryTarget)
                .Concat(GetConfigFilenames(envConfig.TestsTarget)))
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

                LogAction("set connection string in {0}", configPath);
                doc.Save(configPath);
            }
        }

        /// <summary>
        /// Returns all filenames of Configs in the specified target directory. If the target directory does not exist an empty list is returned.
        /// </summary>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetConfigFilenames(string targetDir)
        {
            if (string.IsNullOrEmpty(targetDir))
            {
                return Enumerable.Empty<string>();
            }
            else
            {
                return Directory.GetFiles(Path.Combine(targetDir, "Configs"), "*.xml");
            }
        }

        /// <summary>
        /// Returns all filenames of app-configs in the specified target directory. If the target directory does not exist an empty list is returned.
        /// </summary>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetAppconfigFilenames(string targetDir)
        {
            if (string.IsNullOrEmpty(targetDir))
            {
                return Enumerable.Empty<string>();
            }
            else
            {
                return Directory.GetFiles(targetDir, "*.config", SearchOption.AllDirectories);
            }
        }

        private static void EnforceAppServer(EnvConfig envConfig)
        {
            if (envConfig.AppServer == null || string.IsNullOrEmpty(envConfig.AppServer.Uri))
                return;

            LogTitle("Enforcing app server");

            foreach (var configPath in GetAppconfigFilenames(envConfig.BinaryTarget)
                .Concat(GetAppconfigFilenames(envConfig.TestsTarget)))
            {
                var doc = new XmlDocument();
                doc.Load(configPath);

                var endpoints = doc.SelectNodes("/configuration/system.serviceModel/client/endpoint");
                foreach (XmlElement endpoint in endpoints)
                {
                    endpoint.Attributes["address"].Value = envConfig.AppServer.Uri;
                    LogAction("set endpoint address in {0}", configPath);
                }

                var serviceUris = doc.SelectNodes("/configuration/appSettings/add[@key='serviceUri']");
                foreach (XmlElement serviceUri in serviceUris)
                {
                    serviceUri.Attributes["value"].Value = envConfig.AppServer.Uri;
                    LogAction("set serviceUri address in {0}", configPath);
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
        private static IEnumerable<string> ExpandPath(string source)
        {
            List<string> result = new List<string>();

            if (source.Contains("*"))
            {
                var split = source.Split('*');
                if (split.Length != 2) throw new ArgumentOutOfRangeException("source", "only one wildcard is supported yet");

                var baseSource = Path.Combine(split[0].TrimEnd('/').TrimEnd('\\'), "*");
                var tail = split[1].TrimStart('/').TrimStart('\\');
                var path = Path.GetDirectoryName(baseSource);
                path = string.IsNullOrWhiteSpace(path) ? "." : path;
                var filter = Path.GetFileName(baseSource);

                result.AddRange(Directory.GetDirectories(path, filter).Select(i => Path.Combine(i, tail)).Where(i => Directory.Exists(i)));
            }

            if (result.Count == 0)
                result.Add(source);

            return result;
        }

        private static void EnsureDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                LogDetail("mkdir [{0}]", dir);
                Directory.CreateDirectory(dir);
            }
        }

        enum CopyMode
        {
            RestoreHierarchie,
            Flat,
        }

        private static List<string> CopyFolder(string sourceDir, string targetDir, IEnumerable<string> filesToIgnore = null, string filter = null, CopyMode mode = CopyMode.RestoreHierarchie)
        {
            var result = CopyTopFiles(sourceDir, targetDir, filesToIgnore, filter);
            foreach (var folder in Directory.GetDirectories(sourceDir))
            {
                string target;
                switch (mode)
                {
                    case CopyMode.RestoreHierarchie:
                        target = Path.Combine(targetDir, Path.GetFileName(folder));
                        break;
                    case CopyMode.Flat:
                        target = targetDir;
                        break;
                    default:
                        throw new NotSupportedException("CopyMode " + mode + " is not supported yet");
                }
                result.AddRange(CopyFolder(folder, target, filesToIgnore, filter, mode));
            }
            return result;
        }

        private static List<string> CopyTopFiles(string sourceDir, string targetDir, IEnumerable<string> filesToIgnore = null, string filter = null)
        {
            EnsureDirectory(targetDir);
            var files = string.IsNullOrEmpty(filter) ? Directory.GetFiles(sourceDir) : Directory.GetFiles(sourceDir, filter);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (filesToIgnore != null && filesToIgnore.Contains(fileName))
                    continue;

                var target = Path.Combine(targetDir, fileName);
                Copy(file, target, true);
            }
            return files.ToList();
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

        private static void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            LogDetail("copying [{0}] => [{1}]", sourceFileName, destFileName);
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        private static void Copy(Stream sourceStream, string destFileName, bool overwrite)
        {
            LogDetail("copying stream => [{0}]", destFileName);

            using (var destStream = new FileStream(destFileName, overwrite ? FileMode.Create : FileMode.CreateNew))
            {
                var buf = new byte[1024 * 8];
                int count;
                while ((count = sourceStream.Read(buf, 0, buf.Length)) > 0)
                {
                    destStream.Write(buf, 0, count);
                }
            }
        }

        private static void Delete(string path)
        {
            LogDetail("deleting [{0}]", path);
            File.Delete(path);
        }

        private static readonly Regex EnvVar = new Regex("%([a-zA-Z0-9_]+)%");

        /// <summary>
        /// Replaces %FOO% environment variable references and translates the (back-)slashes to the platform's preferred form.
        /// </summary>
        private static string PrepareConfigPath(string input)
        {
            input = ExpandEnvVars(input);

            // canonicalize slashiness in paths from the configuration
            if (!string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(Path.GetPathRoot(input)))
            {
                // Path.GetPathRoot returns "/" on linux
                input = Path.Combine(Path.GetPathRoot(input), Path.Combine(input.Replace(Path.GetPathRoot(input), "/").Split("\\/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)));
            }

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

        internal static void LogTitle(string msg, params object[] args)
        {
            Console.WriteLine(msg, args);
        }

        internal static void LogAction(string msg, params object[] args)
        {
            Console.Write("        ");
            Console.WriteLine(msg, args);
        }

        internal static void LogDetail(string msg, params object[] args)
        {
            if (debug)
            {
                Console.Write("        ");
                Console.WriteLine(msg, args);
            }
        }

        #endregion
    }
}
