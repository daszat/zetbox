
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The required information to map from a proxy object to its ZBox wrapper
    /// </summary>
    public interface IProxyObject
    {
        int ID { get; set; }
        Type ZBoxWrapper { get; }
        Type ZBoxProxy { get; }
    }
}
