using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.FrozenObjects
{
    public class FreezingGenerator
        : BaseDataObjectGenerator
    {
        public override string TemplateProviderPath { get { return this.GetType().Namespace; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Frozen"; } }
        public override string BaseName { get { return "Frozen"; } }
        public override string ProjectGuid { get { return "{CA615374-AEA3-4187-BF73-584CCD082766}"; } }
    }
}
