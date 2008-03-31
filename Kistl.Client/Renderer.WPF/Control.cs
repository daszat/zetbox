using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.GUI
{
    /// <summary>
    /// All render-independent functionality of a specific control
    /// </summary>
    public class Control
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
