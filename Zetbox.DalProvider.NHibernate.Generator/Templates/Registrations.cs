
namespace Kistl.DalProvider.NHibernate.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    public partial class Registrations
        : Templates.Registrations
    {
        public Registrations(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string shortName)
            : base(_host, ctx, shortName)
        {
        }
    }
}
