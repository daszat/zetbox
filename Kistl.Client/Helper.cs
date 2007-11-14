using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

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
        public static IClientObject GetClientObject(ObjectType type)
        {
            if (type == null) throw new ArgumentException("Type is null");
            if (string.IsNullOrEmpty(type.FullNameClientObject)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type.FullNameClientObject);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            IClientObject obj = Activator.CreateInstance(t) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            return obj;
        }

        private static List<Kistl.App.Base.ObjectClass> _ObjectClasses = null;

        public static List<Kistl.App.Base.ObjectClass> ObjectClasses
        {
            get
            {
                if (_ObjectClasses == null)
                {
                    Kistl.App.Base.ObjectClassClient client = new Kistl.App.Base.ObjectClassClient();
                    _ObjectClasses = client.GetList();
                }

                return _ObjectClasses;
            }
        }
    }
}
