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

        /// <summary>
        /// Helper Function for generic access
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IClientObject GetClientObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            IClientObject obj = Activator.CreateInstance(t) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }
    }
}
