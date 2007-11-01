using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server
{
    internal class ObjectBroker
    {
        public static API.IServerObject GetServerObject(string type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type is empty");

            Type t = Type.GetType(type);
            if (t == null) throw new ApplicationException("Invalid Type");

            API.IServerObject obj = Activator.CreateInstance(t) as API.IServerObject;
            if (obj == null) throw new ApplicationException("Cannot create instance");

            // TODO: Lt. Metadaten dynamisch laden
            Kistl.API.ICustomServerActions customActions = Activator.CreateInstance(Type.GetType("Kistl.App.Projekte.CustomServerActions, Kistl.App.Projekte")) as API.ICustomServerActions;
            customActions.Attach(obj);

            return obj;
        }
    }
}
