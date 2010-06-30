
namespace Kistl.DalProvider.Memory.Generator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators;

    public class MemoryGenerator
        : BaseDataObjectGenerator
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Generator.Memory");

        internal static readonly string Suffix = "Memory";

        private readonly Server.Server _server;

        public MemoryGenerator(Server.Server server)
        {
            _server = server;
        }

        // TODO: #1569 Why not using const Suffix?
        public override string ExtraSuffix { get { return "Memory"; } }
        public override string Description { get { return ExtraSuffix; } }
        public override string TargetNameSpace { get { return "Kistl.Objects." + ExtraSuffix; } }
        public override string BaseName { get { return ExtraSuffix; } }
        public override string ProjectGuid { get { return "{01E60FD5-CD96-466a-83B1-8EFC7452B47C}"; } }

        protected override IEnumerable<string> Generate_Other(IKistlContext ctx)
        {
            var files = base.Generate_Other(ctx);

            // This file is manually included in ProjectFile.cs
            // TODO: only export frozen stuff
            _server.Publish(Path.Combine(CodeBasePath, "FrozenObjects.xml"), new[] { "*" });

            return files;
        }
    }
}
