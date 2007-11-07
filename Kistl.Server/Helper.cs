using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
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

        /// <summary>
        /// Datacontext zurückgeben
        /// TODO: Das hat da eigentlich gar nix zu suchen!
        /// </summary>
        /// <returns></returns>
        internal static Kistl.API.KistlDataContext GetDataContext()
        {
            return new Kistl.API.KistlDataContext("Data Source=localhost\\sqlexpress; Initial Catalog=Kistl;Integrated Security=true");
        }

    }
}
