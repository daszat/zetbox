
namespace Kistl.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;

    public sealed class HtpasswdIdentitySource
        : IIdentitySource
    {
        public static readonly string Path = ".htpasswd";

        public IEnumerable<IdentitySourceItem> GetAllIdentities()
        {
            if (File.Exists(Path))
            {
                using (var sr = new StreamReader(Path))
                {
                    while (!sr.EndOfStream)
                    {
                        var fields = sr.ReadLine().Split(':');
                        if (fields.Length == 2)
                        {
                            yield return new IdentitySourceItem() { DisplayName = fields[0], UserName = fields[0] };
                        }
                    }
                }
            }
        }

        public sealed class Module
            : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);
                builder.RegisterType<HtpasswdIdentitySource>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
    }
}
