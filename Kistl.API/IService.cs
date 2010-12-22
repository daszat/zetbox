using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ServiceDescriptorAttribute : Attribute
    {
        public ServiceDescriptorAttribute()
        {
        }
    }


    /// <summary>
    /// Interface fo an ZBox Custom Service
    /// </summary>
    public interface IService
    {
        void Start();
        void Stop();
    }
}
