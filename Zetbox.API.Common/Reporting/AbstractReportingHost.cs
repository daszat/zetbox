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

namespace Zetbox.API.Common.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using Arebis.CodeGeneration;
    using Arebis.CodeGenerator.Templated;
    using Zetbox.API;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.IO;
    using MigraDoc.Rendering;
    using MigraDoc.RtfRendering;
    using ControlKinds = Zetbox.NamedObjects.Gui.ControlKinds;
    using PdfSharp.Pdf;
    using PdfSharp.Pdf.IO;
    using Zetbox.API.Utils;

    /// <summary>
    /// a <see cref="IGenerationHost"/> to use the pre-compiled Templates from the given assembly
    /// </summary>
    /// Copied and adapted from Arebis.CodeGenerator.Templated.GenerationHost

#if RELEASE
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public abstract class AbstractReportingHost : IGenerationHost, IDisposable
    {
        private Dictionary<string, TemplateInfo> templates;
        private NameValueCollection settings;
        private readonly IFileOpener _fileOpener;
        private readonly ITempFileService _tempFileService;
        private readonly IReportingErrorReporter _errorReporter;
        private TextWriter contextWriter = null;

        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        public AbstractReportingHost(IFileOpener fileOpener, ITempFileService tempFileService, IReportingErrorReporter errorReporter)
            : this(null, null, fileOpener, tempFileService, errorReporter)
        {
        }

        /// <summary>
        /// Creates a new reporting host
        /// </summary>
        /// <param name="overrideTemplateNamespace">null or empty, if default templates should be used, else a assembly with templates.</param>
        /// <param name="overrideTemplateAssembly">null, if default templates should be used, else a assembly with templates.</param>
        /// <param name="fileOpener"></param>
        /// <param name="tempFileService"></param>
        /// <param name="errorReporter"></param>
        public AbstractReportingHost(string overrideTemplateNamespace, Assembly overrideTemplateAssembly, IFileOpener fileOpener, ITempFileService tempFileService, IReportingErrorReporter errorReporter)
        {
            if (fileOpener == null) throw new ArgumentNullException("fileOpener");
            if (tempFileService == null) throw new ArgumentNullException("tempFileService");
            if (errorReporter == null) throw new ArgumentNullException("errorReporter");

            _fileOpener = fileOpener;
            _tempFileService = tempFileService;
            _errorReporter = errorReporter;

            var settings = new NameValueCollection();
            settings["overrideReportTemplateNamespace"] = overrideTemplateNamespace;
            settings["overrideReportTemplateAssembly"] = overrideTemplateAssembly != null ? overrideTemplateAssembly.FullName : null;

            // use this.GetType() to prepare a change:
            // This class is abstract. Anyone who provides report templates should derive from this class (empty implementation).
            // This will distinguish between different reports.
            settings["baseReportTemplateNamespace"] = this.GetType().Namespace;
            settings["baseReportTemplateAssembly"] = this.GetType().Assembly.FullName;

            // Default Inititalization
            Initialize(settings);

            ErrorsAreFatal = false;
        }

        public ITempFileService TempService
        {
            get
            {
                return _tempFileService;
            }
        }

        public void Initialize(NameValueCollection settings)
        {
            if (settings == null) { throw new ArgumentNullException("settings"); }

            // Store settings:
            this.settings = settings;

            // Setup generation language settings:
            GenerationLanguage.DefaultNameSpace = "Arebis.DynamicAssembly";
            GenerationLanguage.DefaultBaseClass = "Arebis.CodeGeneration.CodeTemplate";
            GenerationLanguage.CodeBuilders["vb"] = typeof(VBCodeBuilder);
            GenerationLanguage.CodeBuilders["c#"] = typeof(CSCodeBuilder);
            GenerationLanguage.DefaultTemplateLanguage = "c#";

            // Default to T3 syntax:
            new Arebis.CodeGenerator.Templated.Syntax.T3Syntax().Setup(this.settings);

            // Run GenerationSetup if any:
            foreach (string generationSetupTypeName in settings.GetValues("generatorsetup") ?? new string[0])
            {
                try
                {
                    ((IGenerationSetup)Activator.CreateInstance(Type.GetType(generationSetupTypeName)))
                    .Setup(this.settings);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Failed to setup generator by '{0}': {1}", generationSetupTypeName, ex.Message));
                }
            }

            // Build initial context writer:
            this.contextWriter = new StringWriter();

            // Initialize compiled templates:
            this.templates = new Dictionary<string, TemplateInfo>();
        }

        public void CallTemplate(string templateClass, params object[] parameters)
        {
            this.CallTemplateToContext(templateClass, parameters);
        }

        public void CallTemplateToFile(string templateClass, string outputfile, params object[] parameters)
        {
            throw new NotSupportedException();
        }

        private void CallTemplateToContext(string templateClass, params object[] parameters)
        {
            Type t = null;
            if (!string.IsNullOrEmpty(Settings["overrideReportTemplateAssembly"]))
            {
                var templateName = String.Format(
                    "{0}.{1}, {2}",
                    Settings["overrideReportTemplateNamespace"],
                    templateClass,
                    Settings["overrideReportTemplateAssembly"]);
                t = Type.GetType(templateName);
            }

            if (t == null)
            {
                var defaultName = String.Format(
                    "{0}.{1}, {2}",
                    Settings["baseReportTemplateNamespace"],
                    templateClass,
                    Settings["baseReportTemplateAssembly"]);
                t = Type.GetType(defaultName);

                if (t == null)
                {
                    throw new ArgumentOutOfRangeException("templateClass", String.Format("No class found for {0}", templateClass));
                }
            }

            var template = (CodeTemplate)Activator.CreateInstance(t, new object[] { this }.Concat(parameters).ToArray());
            template.Generate();
        }

        public virtual void WriteFile(string filename, string content)
        {
            throw new NotSupportedException();
        }

        public NameValueCollection Settings
        {
            get { return this.settings; }
        }

        public void WriteOutput(string str)
        {
            this.contextWriter.Write(str);
        }

        public string NewLineString
        {
            get { return Environment.NewLine; }
        }

        void IGenerationHost.Log(string fmt, params object[] args)
        {
        }

        public void Dispose()
        {
            if (contextWriter != null) contextWriter.Dispose();

            // Dispose templates:
            if (this.templates != null)
            {
                foreach (TemplateInfo item in this.templates.Values)
                {
                    item.Dispose();
                }
                this.templates = null;
            }
        }

        private Stream GetMDDLStream()
        {
            // Copy stream
            MemoryStream stream = new MemoryStream();
            StreamWriter sw = new StreamWriter(stream);
            System.Diagnostics.Debug.WriteLine(contextWriter.ToString());
            sw.Write(contextWriter.ToString());
            sw.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private Document GetDocument()
        {
            var s = GetMDDLStream();
            var errors = new DdlReaderErrors();
            DdlReader rd = new DdlReader(s, errors);
            try
            {
                var doc = rd.ReadDocument();
                LogErrors(errors, null, s);
                ReportErrors(errors, null, s);
                return doc;
            }
            catch (Exception ex)
            {
                LogErrors(errors, ex, s);
                ReportErrors(errors, ex, s);
                throw;
            }
        }

        /// <summary>
        /// Whether or not errors are fatal. Set to true to fail with an exception, else the user receives a report and the document is processed as is.
        /// </summary>
        public bool ErrorsAreFatal
        {
            get;
            set;
        }

        private void LogErrors(DdlReaderErrors errors, Exception ex, System.IO.Stream mddl)
        {
            if (ex != null)
            {
                Zetbox.API.Utils.Logging.Log.Error("Exception during report creation", ex);
            }

            if (errors != null)
            {
                foreach (DdlReaderError e in errors)
                {
                    switch (e.ErrorLevel)
                    {
                        case DdlErrorLevel.Error:
                            Zetbox.API.Utils.Logging.Log.Error(e.ToString());
                            break;
                        case DdlErrorLevel.Warning:
                            Zetbox.API.Utils.Logging.Log.Warn(e.ToString());
                            break;
                        default:
                            Zetbox.API.Utils.Logging.Log.Info(e.ToString());
                            break;
                    }
                }
            }
        }

        private void ReportErrors(DdlReaderErrors errors, Exception ex, System.IO.Stream mddl)
        {
            if (ex != null || (errors != null && errors.ErrorCount > 0))
            {
                if (ErrorsAreFatal)
                {
                    var msg = new StringBuilder("Errors while rendering document:");
                    if (ex != null)
                    {
                        msg.Append("\n" + ex.ToString());
                    }
                    foreach (var error in errors)
                    {
                        msg.Append("\n" + error.ToString());
                    }
                    throw new InvalidOperationException(msg.ToString(), ex);
                }

                _errorReporter.ReportErrors(errors, ex, mddl);
            }
        }

        public virtual void Save(string filename)
        {
            if (string.IsNullOrEmpty(filename)) throw new ArgumentNullException("filename");
            if (filename.ToLower().EndsWith(".rtf"))
            {
                RtfDocumentRenderer rtf = new RtfDocumentRenderer();
                var workingDir = Path.GetDirectoryName(filename);
                rtf.Render(GetDocument(), filename, workingDir);
            }
            else
            {
                PdfDocumentRenderer pdf = new PdfDocumentRenderer(true, PdfFontEmbedding.None);
                pdf.Document = GetDocument();
                pdf.RenderDocument();
                pdf.Save(filename);
            }
        }

        public virtual Stream GetStream()
        {
            PdfDocumentRenderer pdf = new PdfDocumentRenderer(true, PdfFontEmbedding.None);
            pdf.Document = GetDocument();
            pdf.RenderDocument();
            MemoryStream s = new MemoryStream();
            pdf.Save(s, false);
            s.Position = 0;
            return s;
        }

        private class NonClosingMemStream : MemoryStream
        {
            public override void Close()
            {
                // DO NOTHING!
            }
        }

        public virtual Stream GetStreamRtf()
        {
            var rtf = new RtfDocumentRenderer();
            var workingDir = _tempFileService.CreateFolder();

            var s = new NonClosingMemStream();
            rtf.Render(GetDocument(), s, workingDir);
            s.Position = 0;
            return s;
        }

        public virtual string SaveTemp(string filename)
        {
            var tmp = _tempFileService.Create(filename);
            Save(tmp);
            return tmp;
        }

        public virtual string Open(string filename)
        {
            var tmp = _tempFileService.Create(filename);
            Save(tmp);
            _fileOpener.ShellExecute(tmp);
            return tmp;
        }
    }
}
