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

namespace Zetbox.DalProvider.Client.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.Generator;

    public class ClientGenerator
        : AbstractBaseGenerator
    {
        public ClientGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string ExtraSuffix { get { return "Client"; } }
        public override string Description { get { return "Client"; } }
        public override string TargetNameSpace { get { return "Zetbox.Objects.Client"; } }
        public override string BaseName { get { return "Client"; } }
        public override string ProjectGuid { get { return "{80F37FB5-66C6-45F2-9E2A-F787B141D66C}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        public override IEnumerable<string> RequiredNamespaces
        {
            get { return new[] { "Zetbox.API.Client", "Zetbox.DalProvider.Base", "Zetbox.DalProvider.Client" }; }
        }
    }
}
