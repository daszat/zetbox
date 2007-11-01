using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client
{
    public class ObjectBroker
    {
        public static IClientObject GetClientObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type " + type);

            IClientObject obj = Activator.CreateInstance(t) as IClientObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            // TODO: Lt. Metadaten dynamisch laden
            Kistl.API.ICustomClientActions customActions = new Kistl.App.Projekte.CustomClientActions();
            customActions.Attach(obj);

            return obj;
        }
    }
}
