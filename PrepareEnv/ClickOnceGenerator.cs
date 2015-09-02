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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using Mono.Security;
using Mono.Security.X509;

namespace PrepareEnv
{
    static class ClickOnceGenerator
    {
        static readonly string ASMv1_NS = "urn:schemas-microsoft-com:asm.v1";
        static readonly string ASMv2_NS = "urn:schemas-microsoft-com:asm.v2";
        static readonly string XMLDSIG_NS = "http://www.w3.org/2000/09/xmldsig#";
        static readonly string XMLDSIG_IDENTITY = "urn:schemas-microsoft-com:HashTransforms.Identity";
        static readonly string XMLDSIG_SHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";
        static readonly string MS_RELDATA_NS = "http://schemas.microsoft.com/windows/rel/2005/reldata";
        static readonly string R_NS = "urn:mpeg:mpeg21:2003:01-REL-R-NS";
        static readonly string AUTHENTICODE_NS = "http://schemas.microsoft.com/windows/pki/2005/Authenticode";
        static readonly string CLICKONCE_V2_NS = "urn:schemas-microsoft-com:clickonce.v2";

        private static readonly HashAlgorithm _sha1 = HashAlgorithm.Create("SHA1");
        private static string _passphrase;

        /// <summary>
        /// ClickOnce "assemblyIdentity" describing the application. Note that this does not mean the .exe: the name will be used for the shortcut
        /// and the publicKeyToken is from the signing key of the ClickOnce manifest.
        /// </summary>
        private struct AppId
        {
            public string name, version, publicKeyToken, language, processorArchitecture, type;
        }

        /// <summary>
        /// Builds the ClickOnce manifest for the client, using the configuration from env.xml and manifest templates from the Templates directory.
        /// </summary>
        /// <param name="envConfig"></param>
        internal static void BuildClickOnce(EnvConfig envConfig)
        {
            if (envConfig.ClickOnce == null) return;

            var origCurrDir = Environment.CurrentDirectory;
            try
            {
                Program.LogTitle("Creating ClickOnce manifest");
                Environment.CurrentDirectory = envConfig.BinaryTarget;
                Program.LogDetail("Changed CWD to BinaryTarget: [{0}]", Environment.CurrentDirectory);
                var appId = new AppId()
                {
                    name = envConfig.ClickOnce.Product + ".exe",
                    version = envConfig.ClickOnce.DeploymentVersion,
                    publicKeyToken = FormatKey(PublicKeyTokenFromPfx(envConfig.ClickOnce.KeyFile)),
                    language = "neutral",
                    processorArchitecture = "bar",
                    type = "win32",
                };

                CreateClickOnceManifest(envConfig, appId);
                Sign(envConfig, appId, GetManifestTemplateName(envConfig), GetManifestName(envConfig));
                CreateClickOnceAppplication(envConfig, appId);
                Sign(envConfig, appId, GetAppTemplateName(envConfig), GetAppName(envConfig));
            }
            finally
            {
                Environment.CurrentDirectory = origCurrDir;
                Program.LogDetail("Changed CWD back to [{0}]", Environment.CurrentDirectory);
            }
        }

        private static void CreateClickOnceManifest(EnvConfig envConfig, AppId appId)
        {
            var doc = LoadXmlFromResource("PrepareEnv.Templates.ClickOnce.manifest.xml");
            var nsmgr = CreateDefaultXmlNsmgr(doc);

            // fixup primary asmv1:assemblyIdentity
            var assemblyIdentity1 = doc.SelectSingleNode("/asmv1:assembly/asmv1:assemblyIdentity", nsmgr);

            var clientName = FillAppId(envConfig, assemblyIdentity1, appId);

            // fixup entryPoint's assemblyIdentity
            var entryPointId = doc.SelectSingleNode("/asmv1:assembly/asmv2:entryPoint/asmv2:assemblyIdentity", nsmgr);
            FillClickOnceAssemblyId(clientName, entryPointId);

            // fixup Icon
            var description = doc.SelectSingleNode("/asmv1:assembly/asmv1:description", nsmgr);
            description.Attributes["asmv2:iconFile"].Value = Path.GetFileName(envConfig.ClientExe);

            // set the startup paramters
            var entryPointCli = doc.SelectSingleNode("/asmv1:assembly/asmv2:entryPoint/asmv2:commandLine", nsmgr);
            entryPointCli.Attributes["file"].Value = Path.GetFileName(envConfig.ClientExe);
            entryPointCli.Attributes["parameters"].Value = envConfig.ClientParameters;

            // insert deployed files
            var dependencyList = doc.SelectSingleNode("/asmv1:assembly", nsmgr);
            var lastPrerequisite = doc.SelectNodes("/asmv1:assembly/asmv2:dependency", nsmgr).OfType<XmlNode>().Last();
            foreach (var baseName in new[] { "Common", "Client" })
            {
                foreach (var pattern in new[] { "*.dll", "*.exe", "*.exe.config" })
                {
                    foreach (var file in Directory.EnumerateFiles(baseName, pattern, SearchOption.AllDirectories))
                    {
                        InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, file, nsmgr);
                    }
                }
            }

            // Add Client EXE and config
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, envConfig.ClientExe, nsmgr);
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, envConfig.ClientExe + ".config", nsmgr);
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, Path.Combine("Configs", Path.GetFileNameWithoutExtension(envConfig.ClientExe) + ".xml"), nsmgr);

            // save to template
            doc.Save(GetManifestTemplateName(envConfig));
        }

        private static void FillClickOnceAssemblyId(System.Reflection.Assembly assemblyDef, XmlNode assemblyIdentity)
        {
            var assemblyName = assemblyDef.GetName();
            var isX86 = assemblyName.ProcessorArchitecture == System.Reflection.ProcessorArchitecture.X86;

            SetOrReplaceAttribute(assemblyIdentity, "name", null, assemblyName.Name);
            SetOrReplaceAttribute(assemblyIdentity, "version", null, assemblyName.Version.ToString());
            SetOrReplaceAttribute(assemblyIdentity, "language", null, string.IsNullOrEmpty(assemblyName.CultureInfo.Name) ? "neutral" : assemblyName.CultureInfo.Name);
            SetOrReplaceAttribute(assemblyIdentity, "processorArchitecture", null, isX86 ? "x86" : "msil");
            var puplicKeyToken = assemblyName.GetPublicKeyToken();
            if (puplicKeyToken != null && puplicKeyToken.Length > 0)
            {
                SetOrReplaceAttribute(assemblyIdentity, "publicKeyToken", null, FormatKey(puplicKeyToken));
            }
            else
            {
                assemblyIdentity.Attributes.RemoveNamedItem("publicKeyToken");
            }
        }

        private static System.Reflection.Assembly FillAppId(EnvConfig envConfig, XmlNode assemblyIdentity, AppId appId)
        {
            var client = System.Reflection.Assembly.ReflectionOnlyLoadFrom(envConfig.ClientExe);

            FillClickOnceAssemblyId(client, assemblyIdentity);
            // this seems to be a constant
            SetOrReplaceAttribute(assemblyIdentity, "type", null, "win32");

            // asmv1 wants a filename, not an assembly name
            SetOrReplaceAttribute(assemblyIdentity, "name", null, Path.GetFileName(envConfig.ClientExe));

            SetOrReplaceAttribute(assemblyIdentity, "version", null, appId.version);
            SetOrReplaceAttribute(assemblyIdentity, "publicKeyToken", null, appId.publicKeyToken);
            return client;
        }

        private static void InsertClickOnceDependency(EnvConfig envConfig, XmlNode dependencyList, XmlNode lastPrerequisite, string file, XmlNamespaceManager nsmgr)
        {
            var deployTarget = Path.Combine(GetClickOnceOutputPath(envConfig), file);
            var doc = dependencyList.OwnerDocument;

            // avoid the pseudo/testing resource assemblies, as clickonce client dies on unknown cultures
            if (file.ToLowerInvariant().EndsWith(".resources.dll") || file.ToLowerInvariant().EndsWith(".resources.dll.deploy") || file.ToLowerInvariant().EndsWith(".resources.exe") || file.ToLowerInvariant().EndsWith(".resources.exe.deploy"))
            {
                if (file.ToLowerInvariant().Contains("x-zb-"))
                {
                    Program.LogDetail("Skipping pseudo culture file: [{0}]", file);
                    return;
                }
            }

            if (file.EndsWith(".dll") || file.EndsWith(".dll.deploy") || file.EndsWith(".exe") || file.EndsWith(".exe.deploy"))
            {
                // TODO: do not deploy fallback to client and remove this.
                if (file.Contains("Fallback")) return;

                var dependency = doc.CreateNode(XmlNodeType.Element, "dependency", ASMv2_NS);
                var dependentAssembly = doc.CreateNode(XmlNodeType.Element, "dependentAssembly", ASMv2_NS);
                SetOrReplaceAttribute(dependentAssembly, "dependencyType", null, "install");
                SetOrReplaceAttribute(dependentAssembly, "allowDelayedBinding", null, "true");
                SetOrReplaceAttribute(dependentAssembly, "codebase", null, file.Replace('/', '\\'));
                SetOrReplaceAttribute(dependentAssembly, "size", null, string.Format(CultureInfo.InvariantCulture, "{0}", new FileInfo(file).Length));

                var assemblyIdentity = doc.CreateNode(XmlNodeType.Element, "assemblyIdentity", ASMv2_NS);

                FillClickOnceAssemblyId(System.Reflection.Assembly.ReflectionOnlyLoadFrom(file), assemblyIdentity);
                dependentAssembly.AppendChild(assemblyIdentity);

                var hash = CreateHashNode(file, nsmgr, doc);

                dependentAssembly.AppendChild(hash);
                dependency.AppendChild(dependentAssembly);
                dependencyList.InsertAfter(dependency, lastPrerequisite);

                deployTarget += ".deploy";
            }
            else if (file.EndsWith(".manifest"))
            {
                var dependency = doc.CreateNode(XmlNodeType.Element, "dependency", ASMv2_NS);
                var dependentAssembly = doc.CreateNode(XmlNodeType.Element, "dependentAssembly", ASMv2_NS);
                SetOrReplaceAttribute(dependentAssembly, "dependencyType", null, "install");
                SetOrReplaceAttribute(dependentAssembly, "codebase", null, file.Replace('/', '\\'));
                SetOrReplaceAttribute(dependentAssembly, "size", null, string.Format(CultureInfo.InvariantCulture, "{0}", new FileInfo(file).Length));

                var manifest = new XmlDocument();
                manifest.Load(file);
                var manifestNsmgr = CreateDefaultXmlNsmgr(manifest);
                var srcAssemblyId = manifest.SelectSingleNode("/asmv1:assembly/asmv1:assemblyIdentity", manifestNsmgr);

                var assemblyIdentity = doc.CreateNode(XmlNodeType.Element, "assemblyIdentity", ASMv2_NS);
                foreach (var attrName in new[] { "name", "version", "language", "processorArchitecture", "publicKeyToken", "type" })
                {
                    SetOrReplaceAttribute(assemblyIdentity, attrName, null, srcAssemblyId.Attributes[attrName].Value);
                }

                dependentAssembly.AppendChild(assemblyIdentity);

                var hash = CreateHashNode(file, nsmgr, doc);

                dependentAssembly.AppendChild(hash);
                dependency.AppendChild(dependentAssembly);
                dependencyList.InsertAfter(dependency, lastPrerequisite);
            }
            else
            {
                var fileNode = doc.CreateNode(XmlNodeType.Element, "file", ASMv2_NS);
                SetOrReplaceAttribute(fileNode, "name", null, file.Replace('/', '\\'));
                SetOrReplaceAttribute(fileNode, "size", null, string.Format(CultureInfo.InvariantCulture, "{0}", new FileInfo(file).Length));

                var hash = doc.CreateNode(XmlNodeType.Element, "hash", ASMv2_NS);
                var transforms = doc.CreateNode(XmlNodeType.Element, "Transforms", XMLDSIG_NS);
                var transform = doc.CreateNode(XmlNodeType.Element, "Transform", XMLDSIG_NS);
                SetOrReplaceAttribute(transform, "Algorithm", null, XMLDSIG_IDENTITY);
                transforms.AppendChild(transform);
                hash.AppendChild(transforms);
                var digestMethod = doc.CreateNode(XmlNodeType.Element, "DigestMethod", XMLDSIG_NS);
                hash.AppendChild(digestMethod);
                var digestValue = doc.CreateNode(XmlNodeType.Element, "DigestValue", XMLDSIG_NS);
                hash.AppendChild(digestValue);

                UpdateSha1(hash, file, nsmgr);

                fileNode.AppendChild(hash);

                dependencyList.InsertAfter(fileNode, lastPrerequisite);

                deployTarget += ".deploy";
            }

            Directory.CreateDirectory(Path.GetDirectoryName(deployTarget));
            File.Copy(file, deployTarget, true);
        }

        private static XmlNode CreateHashNode(string file, XmlNamespaceManager nsmgr, XmlDocument doc)
        {
            var hash = doc.CreateNode(XmlNodeType.Element, "hash", ASMv2_NS);
            var transforms = doc.CreateNode(XmlNodeType.Element, "Transforms", XMLDSIG_NS);
            var transform = doc.CreateNode(XmlNodeType.Element, "Transform", XMLDSIG_NS);
            SetOrReplaceAttribute(transform, "Algorithm", null, XMLDSIG_IDENTITY);
            transforms.AppendChild(transform);
            hash.AppendChild(transforms);
            var digestMethod = doc.CreateNode(XmlNodeType.Element, "DigestMethod", XMLDSIG_NS);
            hash.AppendChild(digestMethod);
            var digestValue = doc.CreateNode(XmlNodeType.Element, "DigestValue", XMLDSIG_NS);
            hash.AppendChild(digestValue);

            UpdateSha1(hash, file, nsmgr);
            return hash;
        }

        private static void Sign(EnvConfig envConfig, AppId appId, string templateName, string outputName)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    SignXmlSec1(envConfig, templateName, outputName, appId);
                    break;
                case PlatformID.Win32NT:
                    SignMage(envConfig, templateName, outputName);
                    break;
                default:
                    throw new NotSupportedException(string.Format("Signing on platform '{0}' is not supported", Environment.OSVersion.Platform));
            }
        }

        private static void SignXmlSec1(EnvConfig envConfig, string templateName, string outputName, AppId appId)
        {
            Program.LogAction("signing with xmlsec1: [{0}] => [{1}]", templateName, outputName);

            // load manifest input xml
            var docTemplate = new XmlDocument();
            docTemplate.Load(templateName);
            var nsmgr = CreateDefaultXmlNsmgr(docTemplate);

            // insert publisher identity
            var pfx = UnlockPfx(File.ReadAllBytes(envConfig.ClickOnce.KeyFile));
            var cert = pfx.Certificates.Cast<X509Certificate>().Single();
            var publisherName = cert.SubjectName;
            // as described on http://msdn.microsoft.com/en-us/library/dd996956.aspx
            var issuerKeyHash = FormatKey(_sha1.ComputeHash(cert.PublicKey));

            var publisherIdentity = docTemplate.CreateElement("publisherIdentity", ASMv2_NS);
            SetOrReplaceAttribute(publisherIdentity, "name", null, publisherName);
            SetOrReplaceAttribute(publisherIdentity, "issuerKeyHash", null, issuerKeyHash);

            docTemplate.ChildNodes.OfType<XmlElement>().Last().AppendChild(publisherIdentity);

            var fusionTemplateName = templateName + ".fusion";
            docTemplate.Save(fusionTemplateName);

            //
            // Calculate ManifestInformation Hash
            // ==================================
            // The Fusion XML engine is always preserving whitespace, therefore we need to
            // use a specially configured XmlDocument to normalize and sign the Manifest.
            // 
            byte[] hash;
            {
                var fusionDoc = new XmlDocument();
                fusionDoc.PreserveWhitespace = true;
                fusionDoc.Load(fusionTemplateName);

                var transform = new XmlDsigExcC14NTransform();
                transform.LoadInput(fusionDoc);
                hash = _sha1.ComputeHash((MemoryStream)transform.GetOutput());
            }

            // Load SignatureBlock into DOM
            var signatureTemplate = LoadXmlFromResource("PrepareEnv.Templates.SignatureBlock.xml");
            foreach (XmlNode sigNode in signatureTemplate.DocumentElement.ChildNodes)
            {
                var newChild = docTemplate.ImportNode(sigNode, deep: true);
                docTemplate.DocumentElement.AppendChild(newChild);
                foreach (XmlNode assemblyId in newChild.SelectNodes("//as:assemblyIdentity", nsmgr))
                {
                    // authenticode assemblyIdentity looks like asmv1:assemblyIdentity
                    FillAppId(envConfig, assemblyId, appId);
                }
            }

            // Set ManifestInformation Hash
            var manifestInfo = docTemplate.SelectSingleNode("//as:ManifestInformation", nsmgr);
            SetOrReplaceAttribute(manifestInfo, "Hash", null, FormatKey(hash));

            // Set AuthenticodePublisher's SubjectName
            var subjectName = docTemplate.SelectSingleNode("//as:AuthenticodePublisher/as:X509SubjectName", nsmgr);
            subjectName.InnerText = publisherName;

            // Sign everything
            Program.LogDetail("saving to xmlsec1 template: [{0}]", templateName + ".xmlsec1");
            docTemplate.Save(templateName + ".xmlsec1");

            var pw = _passphrase == null ? string.Empty : string.Format("--pwd \"{0}\"", _passphrase);

            // resign manifest RelData
            var relDataArgs = string.Format("--sign {0} {1} {2} --node-xpath \"//*[local-name()='RelData']\" --enabled-key-data rsa,x509 --output \"{3}.reldata\" \"{4}.xmlsec1\"",
                                            pw,
                                            envConfig.ClickOnce.KeyFile.EndsWith("pfx") ? "--pkcs12" : "--privkey-pem",
                                            envConfig.ClickOnce.KeyFile,
                                            outputName,
                                            templateName);
            Program.LogDetail("signing reldata to [{0}.reldata]", outputName);
            var proc = Process.Start(new ProcessStartInfo("xmlsec1", relDataArgs) { UseShellExecute = false });
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
                throw new InvalidOperationException(string.Format("xmlsec1 complained about {0}", relDataArgs));
            }

            // resign complete manifest
            var finalArgs = string.Format("--sign {0} {1} {2} --enabled-key-data rsa,x509 --output \"{3}\" \"{3}.reldata\"",
                                          pw,
                                          envConfig.ClickOnce.KeyFile.EndsWith("pfx") ? "--pkcs12" : "--privkey-pem",
                                          envConfig.ClickOnce.KeyFile,
                                          outputName);
            Program.LogDetail("signing final to : [{0}]", outputName);
            proc = Process.Start(new ProcessStartInfo("xmlsec1", finalArgs) { UseShellExecute = false });
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
                throw new InvalidOperationException(string.Format("xmlsec1 complained about {0}", finalArgs));
            }
        }

        private static string LocateMage()
        {
            var candidates = new[] {
                    Environment.GetEnvironmentVariable("WindowsSdkDir"),
                    Environment.GetEnvironmentVariable("MagePath"),
                    // Obsolete, but keep for compatibility
                    @"C:\Program Files\Microsoft SDKs\Windows\v8.1",
                    @"C:\Program Files\Microsoft SDKs\Windows\v8.1A",
                    @"C:\Program Files\Microsoft SDKs\Windows\v8.0",
                    @"C:\Program Files\Microsoft SDKs\Windows\v8.0A",
                    @"C:\Program Files\Microsoft SDKs\Windows\v7.1",
                    @"C:\Program Files\Microsoft SDKs\Windows\v7.1A",
                    @"C:\Program Files\Microsoft SDKs\Windows\v6.0",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A",
                    @"C:\Program Files (x86)\Microsoft SDKs\Windows\v6.0",
                }
            .Concat(Environment.GetEnvironmentVariable("PATH").Split(';'))
            .Where(p => p != null && Directory.Exists(p))
            .ToList();

            var result = candidates.FirstOrDefault(p =>
                    File.Exists(Path.Combine(p, "mage.exe")));
            if (result != null)
            {
                result = Path.Combine(result, "mage.exe");
            }

            // For compatibility
            if (result == null)
            {
                result = candidates.FirstOrDefault(p =>
                    File.Exists(Path.Combine(p, "Bin", "NETFX 4.0 Tools", "mage.exe")));
                if (result != null)
                {
                    result = Path.Combine(result, "Bin", "NETFX 4.0 Tools", "mage.exe");
                }
            }

            if (result == null)
                throw new InvalidOperationException("%WindowsSdkDir% or %MagePath% is empty or doesn't exist, also mage.exe could not be found on %PATH%.");
            return result;
        }

        private static void SignMage(EnvConfig envConfig, string templateName, string outputName)
        {
            Program.LogAction("signing with mage: [{0}]", outputName);
            var pw = _passphrase == null ? string.Empty : string.Format("-Password {0}", _passphrase);
            var args = string.Format("-Sign {0} -ToFile {1} -Certfile {2} {3}", templateName, outputName, envConfig.ClickOnce.KeyFile, pw);
            var proc = Process.Start(LocateMage(), args);
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
                throw new InvalidOperationException(string.Format("mage.exe complained about {0}", args));
            }
        }

        private static void CreateClickOnceAppplication(EnvConfig envConfig, AppId appId)
        {
            var doc = LoadXmlFromResource("PrepareEnv.Templates.ClickOnce.application.xml");
            var nsmgr = CreateDefaultXmlNsmgr(doc);

            // primary asmv1:assemblyIdentity
            var assemblyIdentity1 = doc.SelectSingleNode("/asmv1:assembly/asmv1:assemblyIdentity", nsmgr);
            // name of the application manifest
            SetOrReplaceAttribute(assemblyIdentity1, "name", null, Path.GetFileName(GetAppName(envConfig)));
            // deployment version
            SetOrReplaceAttribute(assemblyIdentity1, "version", null, envConfig.ClickOnce.DeploymentVersion);
            // key token of the signing key
            SetOrReplaceAttribute(assemblyIdentity1, "publicKeyToken", null, FormatKey(PublicKeyTokenFromPfx(envConfig.ClickOnce.KeyFile)));
            // from the underlying .exe
            SetOrReplaceAttribute(assemblyIdentity1, "language", null, "neutral");
            SetOrReplaceAttribute(assemblyIdentity1, "processorArchitecture", null, "msil");

            // application description
            var description = doc.SelectSingleNode("/asmv1:assembly/asmv1:description", nsmgr);
            SetOrReplaceAttribute(description, "publisher", ASMv2_NS, envConfig.ClickOnce.Publisher);
            SetOrReplaceAttribute(description, "product", ASMv2_NS, envConfig.ClickOnce.Product);
            if (!string.IsNullOrEmpty(envConfig.ClickOnce.SuiteName))
            {
                SetOrReplaceAttribute(description, "suiteName", null, envConfig.ClickOnce.SuiteName);
            }
            if (!string.IsNullOrEmpty(envConfig.ClickOnce.SupportUrl))
            {
                SetOrReplaceAttribute(description, "supportUrl", null, envConfig.ClickOnce.SupportUrl);
            }
            if (!string.IsNullOrEmpty(envConfig.ClickOnce.ErrorReportUrl))
            {
                SetOrReplaceAttribute(description, "errorReportUrl", null, envConfig.ClickOnce.ErrorReportUrl);
            }

            // deployment options
            var deploymentProvider = doc.SelectSingleNode("/asmv1:assembly/asmv2:deployment/asmv2:deploymentProvider", nsmgr);
            SetOrReplaceAttribute(deploymentProvider, "codebase", null, envConfig.ClickOnce.UpdateUrl);

            // insert manifest file
            var dependencyList = doc.SelectSingleNode("/asmv1:assembly", nsmgr);
            var lastPrerequisite = doc.SelectNodes("/asmv1:assembly/co.v2:compatibleFrameworks", nsmgr).OfType<XmlNode>().Last();
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, GetManifestName(envConfig), nsmgr);

            // save to template
            doc.Save(GetAppTemplateName(envConfig));
        }

        #region XML Utilities

        private static XmlDocument LoadXmlFromResource(string resourceName)
        {
            var doc = new XmlDocument();

            using (var templateStream = typeof(Program).Assembly.GetManifestResourceStream(resourceName))
            using (var templateReader = new StreamReader(templateStream))
                doc.Load(templateReader);

            return doc;
        }

        private static XmlNamespaceManager CreateDefaultXmlNsmgr(XmlDocument doc)
        {
            XmlNamespaceManager nsmgr;

            nsmgr = new XmlNamespaceManager(doc.NameTable);
            // map namespaces to the same prefixes as used in the template
            nsmgr.AddNamespace(string.Empty, ASMv2_NS);
            nsmgr.AddNamespace("asmv1", ASMv1_NS);
            nsmgr.AddNamespace("asmv2", ASMv2_NS); // XPath always requires a namespace
            nsmgr.AddNamespace("dsig", XMLDSIG_NS);
            nsmgr.AddNamespace("r", R_NS);
            nsmgr.AddNamespace("as", AUTHENTICODE_NS);
            nsmgr.AddNamespace("msrel", MS_RELDATA_NS);
            nsmgr.AddNamespace("co.v2", CLICKONCE_V2_NS);

            return nsmgr;
        }

        private static void SetOrReplaceAttribute(XmlNode node, string attrName, string namespaceURI, string attrValue)
        {
            var doc = node.OwnerDocument;
            var attr = node.Attributes[attrName] ?? (namespaceURI == null ? doc.CreateAttribute(attrName) : doc.CreateAttribute(attrName, namespaceURI));
            attr.Value = attrValue;
            node.Attributes.SetNamedItem(attr);
        }

        #endregion

        #region Other Utilities

        private static string GetClickOnceOutputPath(EnvConfig envConfig)
        {
            return "ClickOnceClient";
        }

        // will be copied by InsertClickOnceDependency
        private static string GetManifestName(EnvConfig envConfig)
        {
            return envConfig.ClientExe + ".manifest";
        }

        private static string GetManifestTemplateName(EnvConfig envConfig)
        {
            return GetManifestName(envConfig) + ".tmpl";
        }

        private static string GetAppName(EnvConfig envConfig)
        {
            return Path.Combine(GetClickOnceOutputPath(envConfig), Path.GetFileNameWithoutExtension(envConfig.ClientExe) + ".application");
        }

        // template should not go to ClickOnceOutputPath
        private static string GetAppTemplateName(EnvConfig envConfig)
        {
            return Path.GetFileNameWithoutExtension(envConfig.ClientExe) + ".application.tmpl";
        }

        private static void UpdateSha1(XmlNode hash, string filename, XmlNamespaceManager nsmgr)
        {
            if (!File.Exists(filename) && File.Exists(filename + ".deploy"))
                filename += ".deploy";

            Program.LogDetail("Hashing [{0}]", filename);

            var bytes = File.ReadAllBytes(filename);
            var hashBytes = _sha1.ComputeHash(bytes);
            SetOrReplaceAttribute(hash.SelectSingleNode("./dsig:DigestMethod", nsmgr), "Algorithm", null, XMLDSIG_SHA1);
            hash.SelectSingleNode("./dsig:DigestValue", nsmgr).InnerText = Convert.ToBase64String(hashBytes);

            Program.LogDetail("Hashed to [{0}] == [{1}]", Convert.ToBase64String(hashBytes), string.Join("", hashBytes.Select(b => string.Format(CultureInfo.InvariantCulture, "{0:x2}", b))));
        }

        [DebuggerNonUserCode] // ignore the exception-based control flow in this method
        private static byte[] PublicKeyTokenFromPfx(string filename)
        {
            var data = File.ReadAllBytes(filename);
            try
            {
                return new StrongName(data).PublicKeyToken;
            }
            catch
            {
                // check magic number for PKCS12
                if (data.Length == 0 || data[0] != 0x30)
                    throw; // awww

                var pfx = UnlockPfx(data);

                try
                {
                    return new StrongName((RSA)pfx.Keys[0]).PublicKeyToken;
                }
                catch
                {
                    Console.Error.WriteLine();
                    Console.Error.WriteLine("Error while trying to read key file");
                    Console.Error.WriteLine("Hint: The PFX file must contain the signing RSA key as first entry");
                    Console.Error.WriteLine();
                    throw;
                }
            }
        }

        [DebuggerNonUserCode] // ignore the exception-based control flow in this method
        private static PKCS12 UnlockPfx(byte[] data)
        {
            PKCS12 pfx;

            try
            {
                pfx = new PKCS12(data);
            }
            catch
            {
                try
                {
                    pfx = new PKCS12(data, string.Empty);
                }
                catch
                {
                    try
                    {
                        if (_passphrase == null)
                        {
                            Console.Write("Please enter the passphrase for the KeyFile (will be visible when typed): ");
                            _passphrase = Console.ReadLine();
                        }
                        pfx = new PKCS12(data, _passphrase);
                    }
                    catch
                    {
                        _passphrase = null;
                        throw;
                    }
                }
            }
            return pfx;
        }

        private static string FormatKey(byte[] data)
        {
            // return placeholder, needed by mage.exe ("Internal error, please try again. The form specified for the subject is not one supported or known by the specified trust provider.")
            // yay for helpful error messages
            if (data == null || data.Length == 0)
                return "0000000000000000";
            return string.Join(string.Empty, data.Select(i => i.ToString("x2")));
        }
        #endregion
    }
}
