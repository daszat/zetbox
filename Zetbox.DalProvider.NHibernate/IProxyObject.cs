
namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    /// <summary>
    /// The required information to map from a proxy object to its Zetbox wrapper
    /// </summary>
    public interface IProxyObject
    {
        int ID { get; set; }
        Type ZetboxWrapper { get; }
        Type ZetboxProxy { get; }
    }
}
