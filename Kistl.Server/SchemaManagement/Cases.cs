using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
// TODO: Das gehört angeschaut.
using Kistl.Server.Generators.EntityFramework.Implementation;
using Kistl.Server.Generators.Extensions;
using System.IO;

namespace Kistl.Server.SchemaManagement
{
    internal class Cases
    {
        #region Fields
        private IKistlContext schema;
        private IKistlContext savedSchema;
        private ISchemaProvider db;
        private TextWriter report;
        #endregion

        internal Cases(IKistlContext schema, IKistlContext savedSchema, ISchemaProvider db, TextWriter report)
        {
        }

        // Add all IsCase_ + DoCase_ Methods
    }
}
