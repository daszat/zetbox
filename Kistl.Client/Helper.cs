using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client
{
    public class Helper
    {
        public static void HandleError(Exception ex)
        {
            System.Windows.MessageBox.Show(ex.ToString());
        }

        public static IClientObject GetClientObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            IClientObjectFactory objFactory = Activator.CreateInstance(t) as IClientObjectFactory;
            if (objFactory == null) throw new ApplicationException("Cannot create instance");

            IClientObject obj = objFactory.GetClientObject();

            // TODO: Lt. Metadaten dynamisch laden
            Kistl.API.ICustomClientActions customActions = new Kistl.App.Projekte.CustomClientActions();
            customActions.Attach(obj);

            return obj;
        }
    }
}
