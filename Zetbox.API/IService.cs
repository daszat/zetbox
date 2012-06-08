using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API
{
    /// <summary>
    /// Interface fo an Zetbox Custom Service
    /// </summary>
    public interface IService
    {
        void Start();
        void Stop();

        string DisplayName { get; }
        string Description { get; }
    }

    public interface IServiceControlManager
    {
        void Start();
        void Stop();
    }
}
