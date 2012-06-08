
namespace Zetbox.DalProvider.NHibernate.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public partial class Registrations
        : Templates.Registrations
    {
        public Registrations(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string shortName)
            : base(_host, ctx, shortName)
        {
        }
    }
}
