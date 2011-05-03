using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public interface IPropertyLoader
    {
        void Reload();
    }

    public class DefaultPropertyLoader : IPropertyLoader
    {
        private readonly Action _loadAction;

        public DefaultPropertyLoader(Action loadAction)
        {
            if (loadAction == null) throw new ArgumentNullException("loadAction");

            _loadAction = loadAction;
        }

        public void Reload()
        {
            _loadAction();
        }
    }
}
