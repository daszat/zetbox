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
using System.Reflection;
using System.Security.Cryptography;
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

            var appId = new AppId()
            {
                name = envConfig.ClickOnce.Product + ".exe",
                version = "1.0.0.0",
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

            // set the startup paramters
            var entryPointCli = doc.SelectSingleNode("/asmv1:assembly/asmv2:entryPoint/asmv2:commandLine", nsmgr);
            entryPointCli.Attributes["file"].Value = Path.GetFileName(clientName.CodeBase);
            entryPointCli.Attributes["parameters"].Value = envConfig.ClientParameters;

            // insert deployed files
            var dependencyList = doc.SelectSingleNode("/asmv1:assembly", nsmgr);
            var lastPrerequisite = doc.SelectNodes("/asmv1:assembly/asmv2:dependency", nsmgr).OfType<XmlNode>().Last();
            foreach (var baseName in new[] { "Common", "Client" })
            {
                foreach (var pattern in new[] { "*.dll", "*.dll.deploy", "*.exe", "*.exe.deploy", "*.exe.config", "*.exe.config.deploy" })
                {
                    foreach (var file in Directory.EnumerateFiles(Path.Combine(envConfig.BinaryTarget, baseName), pattern, SearchOption.AllDirectories))
                    {
                        InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, file, nsmgr);
                    }
                }
            }

            // Add Client EXE and config
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, envConfig.ClientExe, nsmgr);
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, envConfig.ClientExe + ".config", nsmgr);
            InsertClickOnceDependency(envConfig, dependencyList, lastPrerequisite, Path.Combine(envConfig.BinaryTarget, "Configs", Path.GetFileNameWithoutExtension(envConfig.ClientExe) + ".xml"), nsmgr);

            // save to template
            doc.Save(GetManifestTemplateName(envConfig));
        }

        private static void FillClickOnceAssemblyId(AssemblyName assemblyName, XmlNode assemblyIdentity)
        {
            SetOrReplaceAttribute(assemblyIdentity, "name", null, assemblyName.Name);
            SetOrReplaceAttribute(assemblyIdentity, "version", null, assemblyName.Version.ToString());
            if (string.IsNullOrEmpty(assemblyName.CultureInfo.Name))
            {
                SetOrReplaceAttribute(assemblyIdentity, "language", null, "neutral");
            }
            else
            {
                SetOrReplaceAttribute(assemblyIdentity, "language", null, assemblyName.CultureInfo.Name);
            }
            SetOrReplaceAttribute(assemblyIdentity, "processorArchitecture", null, assemblyName.ProcessorArchitecture.ToString().ToLowerInvariant());
            var pkToken = assemblyName.GetPublicKeyToken();
            if (pkToken != null && pkToken.Length > 0)
            {
                SetOrReplaceAttribute(assemblyIdentity, "publicKeyToken", null, FormatKey(pkToken));
            }
        }

        private static AssemblyName FillAppId(EnvConfig envConfig, XmlNode assemblyIdentity, AppId appId)
        {
            var client = Assembly.ReflectionOnlyLoadFrom(envConfig.ClientExe);
            var clientName = client.GetName();

            FillClickOnceAssemblyId(clientName, assemblyIdentity);
            SetOrReplaceAttribute(assemblyIdentity, "publicKeyToken", null, string.Join(string.Empty, clientName.GetPublicKeyToken().Select(b => string.Format(CultureInfo.InvariantCulture, "{0:x2}", b))));
            SetOrReplaceAttribute(assemblyIdentity, "type", null, "win32");

            // asmv1 wants a filename, not an assembly name
            assemblyIdentity.Attributes["name"].Value = Path.GetFileName(client.CodeBase);
            return clientName;
        }

        private static void InsertClickOnceDependency(EnvConfig envConfig, XmlNode dependencyList, XmlNode lastPrerequisite, string file, XmlNamespaceManager nsmgr)
        {
            file = Path.GetFullPath(file);
            if (!File.Exists(file))
            {
                Program.LogAction("Skipping [{0}]: does not exist.", file);
            }

            var fullBinaryTarget = Path.GetFullPath(envConfig.BinaryTarget);
            if (!file.StartsWith(fullBinaryTarget))
            {
                Program.LogAction("Skipping [{0}]: not part of BinaryTarget=[{1}].", file, fullBinaryTarget);
                return;
            }
            var codebase = file.Substring(fullBinaryTarget.Length + 1);

            var doc = dependencyList.OwnerDocument;

            if (file.EndsWith(".dll") || file.EndsWith(".dll.deploy") || file.EndsWith(".exe") || file.EndsWith(".exe.deploy"))
            {
                // TODO: do not deploy fallback to client and remove this.
                if (file.Contains("Fallback")) return;

                var dependency = doc.CreateNode(XmlNodeType.Element, "dependency", ASMv2_NS);
                var dependentAssembly = doc.CreateNode(XmlNodeType.Element, "dependentAssembly", ASMv2_NS);
                SetOrReplaceAttribute(dependentAssembly, "dependencyType", null, "install");
                SetOrReplaceAttribute(dependentAssembly, "allowDelayedBinding", null, "true");
                SetOrReplaceAttribute(dependentAssembly, "codebase", null, codebase.Replace('/', '\\'));
                SetOrReplaceAttribute(dependentAssembly, "size", null, string.Format(CultureInfo.InvariantCulture, "{0}", new FileInfo(file).Length));

                var assemblyIdentity = doc.CreateNode(XmlNodeType.Element, "assemblyIdentity", ASMv2_NS);

                FillClickOnceAssemblyId(Assembly.ReflectionOnlyLoadFrom(Path.Combine(envConfig.BinaryTarget, file)).GetName(), assemblyIdentity);
                dependentAssembly.AppendChild(assemblyIdentity);

                var hash = CreateHashNode(file, nsmgr, doc);

                dependentAssembly.AppendChild(hash);
                dependency.AppendChild(dependentAssembly);
                dependencyList.InsertAfter(dependency, lastPrerequisite);
            }
            else if (file.EndsWith(".manifest"))
            {
                var dependency = doc.CreateNode(XmlNodeType.Element, "dependency", ASMv2_NS);
                var dependentAssembly = doc.CreateNode(XmlNodeType.Element, "dependentAssembly", ASMv2_NS);
                SetOrReplaceAttribute(dependentAssembly, "dependencyType", null, "install");
                SetOrReplaceAttribute(dependentAssembly, "codebase", null, codebase.Replace('/', '\\'));
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
                SetOrReplaceAttribute(fileNode, "name", null, codebase.Replace('/', '\\'));
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
            }
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
            Program.LogAction("signing with xmlsec1");

            var docTemplate = new XmlDocument();
            docTemplate.Load(templateName);
            var nsmgr = CreateDefaultXmlNsmgr(docTemplate);

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

            docTemplate.Save(templateName + ".xmlsec1");

            var pw = _passphrase == null ? string.Empty : string.Format("--pwd \"{0}\"", _passphrase);

            // resign manifest RelData
            var relDataArgs = string.Format("--sign {0} {1} {2} --node-xpath \"//*[local-name()='RelData']\" --enabled-key-data rsa,x509 --output \"{3}.reldata\" \"{4}.xmlsec1\"",
                                            pw,
                                            envConfig.ClickOnce.KeyFile.EndsWith("pfx") ? "--pkcs12" : "--privkey-pem",
                                            envConfig.ClickOnce.KeyFile,
                                            outputName,
                                            templateName);
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
            proc = Process.Start(new ProcessStartInfo("xmlsec1", finalArgs) { UseShellExecute = false });
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
                throw new InvalidOperationException(string.Format("xmlsec1 complained about {0}", finalArgs));
            }
        }

        private static void SignMage(EnvConfig envConfig, string templateName, string outputName)
        {
            Program.LogAction("signing with mage");
            var pw = _passphrase == null ? string.Empty : string.Format("-Password {0}", _passphrase);
            var args = string.Format("-Sign {0} -ToFile {1} -Certfile {2} {3}", templateName, outputName, envConfig.ClickOnce.KeyFile, pw);
            var proc = Process.Start(Path.Combine(Environment.GetEnvironmentVariable("WindowsSdkDir"), "Bin", "NETFX 4.0 Tools", "mage.exe"), args);
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
            return Path.Combine(envConfig.BinaryTarget, Path.GetFileNameWithoutExtension(envConfig.ClientExe) + ".application");
        }

        private static string GetAppTemplateName(EnvConfig envConfig)
        {
            return GetAppName(envConfig) + ".tmpl";
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

        private static string FormatKey(byte[] data)
        {
            return string.Join(string.Empty, data.Select(i => i.ToString("x2")));
        }
        #endregion
    }
}
