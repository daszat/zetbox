using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server
{
    /// <summary>
    /// Server Helper
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// TODO: Das ist nicht die beste implementierung...
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
