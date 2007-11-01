using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server
{
    public class Helper
    {
        public static void HandleError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
