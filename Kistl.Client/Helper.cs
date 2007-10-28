using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client
{
    public class Helper
    {
        public static void HandleError(Exception ex)
        {
            System.Windows.MessageBox.Show(ex.ToString());
        }
    }
}
