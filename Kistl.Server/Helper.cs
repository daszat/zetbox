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

        public static API.IServerObject GetServerObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            API.IServerObjectFactory objFactory = Activator.CreateInstance(t) as API.IServerObjectFactory;
            if (objFactory == null) throw new ApplicationException("Cannot create instance");

            API.IServerObject obj = objFactory.GetServerObject();

            // TODO: Lt. Metadaten dynamisch laden
            Kistl.API.ICustomServerActions customActions = new Kistl.App.Projekte.CustomServerActions();
            customActions.Attach(obj);

            return obj;
        }
    }
}
