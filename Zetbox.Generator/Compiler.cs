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

namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    public abstract class Compiler
    {
        private readonly static log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Compiler));

        private readonly ILifetimeScope _container;
        private readonly IEnumerable<AbstractBaseGenerator> _generatorProviders;
        private readonly ZetboxConfig _config;

        public Compiler(ILifetimeScope container, IEnumerable<AbstractBaseGenerator> generatorProviders)
        {
            _container = container;
            _generatorProviders = generatorProviders;
            _config = _container.Resolve<ZetboxConfig>();
        }

        protected abstract bool CompileSingle(AbstractBaseGenerator gen, Dictionary<string, string> buildProps, string workingPath, string target);

        public bool IsFallback { get; private set; }
        public void GenerateCode()
        {
            if (_generatorProviders.Count() == 0)
            {
                _log.Warn("No BaseDataObjectGenerators found. Exiting the Generator.");
                return;
            }

            using (_log.InfoTraceMethodCall("GenerateCode", "Generating DAL-classes"))
            {
                var workingPath = _config.Server.CodeGenWorkingPath;
                if (String.IsNullOrEmpty(workingPath))
                {
                    throw new ConfigurationException("CodeGenWorkingPath is not defined in the current configuration file.");
                }

                if (!Directory.Exists(workingPath))
                {
                    _log.InfoFormat("Creating destination directory: [{0}]", workingPath);
                    Directory.CreateDirectory(workingPath);
                }

                GenerateTo(workingPath);
                CompileCodeOnStaThread(workingPath);
                PublishOutput();
                _log.Info("Finished generating Code");
            }
        }

        public void GenerateFallback()
        {
            _log.Info("Preparing env. to generate fallback");
            IsFallback = true;
            GenerateCode();
        }

        public void CompileCode()
        {
            using (_log.InfoTraceMethodCall("CompileCode", "Compiling the code"))
            {
                CompileCodeOnStaThread(_config.Server.CodeGenWorkingPath);
                PublishOutput();
            }
        }

        private void CompileCodeOnStaThread(string workingPath)
        {
            Exception failed = null;
            bool success = false;
            var staThread = new Thread(() => { try { success = CompileCode(workingPath); } catch (Exception ex) { failed = ex; } });
            if (staThread.TrySetApartmentState(ApartmentState.STA))
            {
                _log.Info("Successfully set STA on compile thread");
            }
            else
            {
                _log.Warn("STA not set on compile thread");
            }
            staThread.Name = "Compile";
            staThread.Start();
            staThread.Join();

            if (failed != null)
            {
                throw new InvalidOperationException("Compilation failed", failed);
            }

            if (!success)
            {
                throw new InvalidOperationException("Compilation failed");
            }
        }

        private void GenerateTo(string workingPath)
        {
            _log.InfoFormat("Generating Code to [{0}]", workingPath);
            // TODO: use TaskExecutor to optimally use multicores
            // nhibernate on mono triggers a runtime fault with 2.10.x
            //if (Environment.GetEnvironmentVariable("ZETBOX_SERIALIZE_COMPILATION") == "yes")
            //{
            _log.Warn("Serializing generation threads.");

            var ctx = _container.Resolve<IZetboxServerContext>();
            foreach (var gen in _generatorProviders)
            {
                gen.Generate(ctx, workingPath, this);
            }

            //}
            //else
            //{
            //    GenerateParallelTo(workingPath);
            //}
        }

        private void GenerateParallelTo(string workingPath)
        {
            List<Exception> failed = new List<Exception>();
            var threads = new List<Thread>();
            foreach (var gen in _generatorProviders)
            {
                // decouple from loop variable
                var generator = gen;
                var genThread = new Thread(() =>
                {
                    try
                    {
                        using (var innerContainer = _container.BeginLifetimeScope())
                        {
                            generator.Generate(innerContainer.Resolve<IZetboxServerContext>(), workingPath, this);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(String.Format("Error generating [{0}]:", generator.Description), ex);
                        lock (failed)
                        {
                            failed.Add(ex);
                        }
                    }
                });
                genThread.Name = generator.BaseName;
                genThread.Start();
                threads.Add(genThread);
            }
            foreach (var t in threads)
            {
                t.Join();
            }

            if (failed.Count > 0)
            {
                // TODO: Introduce own exception
                throw new ApplicationException(String.Format("Failed code generation in {0} threads", failed.Count), failed.First());
            }
        }

        private static string GetConfiguration()
        {
            // TODO: read this from config file
#if DEBUG
            return "Debug";
#else
            return "Release";
#endif
        }

        private bool CompileCode(string workingPath)
        {
            using (_log.InfoTraceMethodCall("CompileCode", "Compile Code on STA thread to " + workingPath))
            {
                var zetboxApiPath = GetApiPath();

                _log.DebugFormat("zetboxApiPath = [{0}]", zetboxApiPath);

                // TODO: move MsBuild logging to log4net
                if (File.Exists("TemplateCodegenLog.txt"))
                    File.Delete("TemplateCodegenLog.txt");

                string binPath = GetBinaryBasePath(workingPath);

                _log.DebugFormat("binPath = [{0}]", binPath);

                Directory.CreateDirectory(binPath);

                var buildProps = new Dictionary<string, string>()
                {
                    { "Configuration", GetConfiguration() },
                    { "OutputPathOverride", binPath },
                    { "ZetboxAPIPathOverride", zetboxApiPath },
                };

                try
                {
                    var result = true;
                    var compileOrder = _generatorProviders
                        .GroupBy(k => k.CompileOrder)
                        .OrderBy(i => i.Key);
                    foreach (var gens in compileOrder)
                    {
                        foreach (var gen in gens)
                        {
                            result &= CompileSingle(gen, buildProps, workingPath, "Build");
                        }
                    }

                    // Additional Targets
                    var additionalTargets = _generatorProviders
                        .Where(i => i.AdditionalTargets.Count() > 0)
                        .GroupBy(k => k.CompileOrder)
                        .OrderBy(i => i.Key);
                    foreach (var gens in additionalTargets)
                    {
                        foreach (var gen in gens)
                        {
                            foreach (var target in gen.AdditionalTargets)
                            {
                                result &= CompileSingle(gen, buildProps, workingPath, target);
                            }
                        }
                    }

                    return result;
                }
                finally
                {
                }
            }
        }

        private static string GetBinaryBasePath(string workingPath)
        {
            return Path.GetFullPath(Helper.PathCombine(workingPath, "bin", GetConfiguration()));
        }

        private static string GetApiPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private void PublishOutput()
        {
            var outputPath = _config.Server.CodeGenOutputPath;
            if (!String.IsNullOrEmpty(outputPath))
            {
                _log.InfoFormat("Publishing results to [{0}]", outputPath);
                try
                {
                    PreparePublishOutput(outputPath);
                }
                catch (IOException)
                {
                    Thread.Sleep(1000);
                    // try again, might be locked by something
                    PreparePublishOutput(outputPath);
                }

                if (_config.Server.CodeGenBinaryOutputPath != null && _config.Server.CodeGenBinaryOutputPath.Length > 0)
                {
                    // source
                    var binaryBasePath = Path.GetFullPath(GetBinaryBasePath(outputPath));
                    // target
                    foreach (var binaryOutputPath in _config.Server.CodeGenBinaryOutputPath.Select(p => Path.GetFullPath(p)))
                    {
                        _log.InfoFormat("Deploying binaries from [{0}] to CodeGenBinaryOutputPath [{1}]", binaryBasePath, binaryOutputPath);
                        DirectoryCopy(binaryBasePath, binaryOutputPath);

                        // Case #1382: Recompile to regenerate PDB's
                        // CompileCode(outputPath);
                    }
                }
            }
        }

        private void PreparePublishOutput(string outputPath)
        {
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
            Directory.Move(_config.Server.CodeGenWorkingPath, outputPath);
        }

        // adapted from http://msdn.microsoft.com/en-us/library/bb762914.aspx
        private static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            // Copy the files.
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                _log.InfoFormat("copying [{0}] => [{1}]", file, temppath);
                file.CopyTo(temppath, true);
            }

            // Copy the subdirectories.
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }

        #region GetLists

        public static readonly string[] FallbackModules = new[] { "ZetboxBase", "GUI", "SchemaMigration" };

        public IQueryable<ObjectClass> GetObjectClassList(IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            if (!IsFallback)
            {
                return ctx.GetQuery<ObjectClass>();
            }
            else
            {
                return ctx.GetQuery<ObjectClass>().ToArray().Where(i => FallbackModules.Contains(i.Module.Name)).AsQueryable();
            }
        }

        public IQueryable<Interface> GetInterfaceList(IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            if (!IsFallback)
            {
                return ctx.GetQuery<Interface>();
            }
            else
            {
                return ctx.GetQuery<Interface>().ToArray().Where(i => FallbackModules.Contains(i.Module.Name)).AsQueryable();
            }
        }

        public IQueryable<Enumeration> GetEnumList(IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            if (!IsFallback)
            {
                return ctx.GetQuery<Enumeration>();
            }
            else
            {
                return ctx.GetQuery<Enumeration>().ToArray().Where(i => FallbackModules.Contains(i.Module.Name)).AsQueryable();
            }
        }

        public IQueryable<CompoundObject> GetCompoundObjectList(IZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            if (!IsFallback)
            {
                return ctx.GetQuery<CompoundObject>();
            }
            else
            {
                return ctx.GetQuery<CompoundObject>().ToArray().Where(i => FallbackModules.Contains(i.Module.Name)).AsQueryable();
            }
        }

        #endregion
    }
}
