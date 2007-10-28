using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public interface ICustomServerActions
    {
        void Attach(IServerObject server);
        void Attach(IDataObject obj);
    }

    /// <summary>
    /// Getrennt, damit man ja nicht Server & Clientaktionen vermischt
    /// </summary>
    public interface ICustomClientActions
    {
        void Attach(IClientObject client);
        void Attach(IDataObject obj);
    }
}
