using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.ClientObjects
{
    public class ClientObjectGenerator
        : BaseDataObjectGenerator
    {
        public override string Description { get { return "Client"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Client"; } }
        public override string BaseName { get { return "Client"; } }
        public override string ProjectGuid { get { return "{80F37FB5-66C6-45F2-9E2A-F787B141D66C}"; } }
    }
}
