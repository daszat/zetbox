using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client
{
    /// <summary>
    /// Client Helper Methods
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Auch das könnte man besser implementieren
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleError(Exception ex)
        {
            System.Windows.MessageBox.Show(ex.ToString());
        }
    }
}
