using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Das muss man auf diese Methode machen, weil ich beim Ableiten keine Chance habe, 
    /// fremde Objekte zu erweitern.
    /// Autogeneriet lt. Metadaten
    /// </summary>
    public partial class CustomClientActions : API.ICustomClientActions
    {
        public void Attach(Kistl.API.IClientObject client)
        {
            if (client is API.ClientObject<Projekt>)
            {
                API.ClientObject<Projekt> impl = client as API.ClientObject<Projekt>;
            }
            if (client is API.ClientObject<Task>)
            {
                API.ClientObject<Task> impl = client as API.ClientObject<Task>;
            }
        }

        public void Attach(Kistl.API.IDataObject obj)
        {
            if (obj is Projekt)
            {
                Projekt impl = obj as Projekt;
                impl.OnToString += new Kistl.API.ToStringHandler<Projekt>(Projekt_OnToString);
            }

            if (obj is Task)
            {
                Task impl = obj as Task;
                impl.OnToString += new Kistl.API.ToStringHandler<Task>(Task_OnToString);
            }
        }
    }
}
