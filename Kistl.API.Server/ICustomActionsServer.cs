using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    /// <summary>
    /// Interface für Server Custom Actions. Jedes Custom Actions Objekt muss selbst die 
    /// gewünschten Events attachen
    /// Getrennt, damit man ja nicht Server & Clientaktionen vermischt
    /// </summary>
    public interface ICustomServerActions
    {
        void Attach(IDataObject obj);
    }

}
