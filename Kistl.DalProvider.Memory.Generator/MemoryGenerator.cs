
namespace Kistl.DalProvider.Memory.Generator
{
    using System;
    using System.Collections.Generic;
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

        public static readonly string ExtraSuffix = "Memory";

        public override string Description { get { return MemoryGenerator.ExtraSuffix; } }
        public override string TargetNameSpace { get { return "Kistl.Objects." + MemoryGenerator.ExtraSuffix; } }
        public override string BaseName { get { return MemoryGenerator.ExtraSuffix; } }
        public override string ProjectGuid { get { return "{01E60FD5-CD96-466a-83B1-8EFC7452B47C}"; } }
    }
}
