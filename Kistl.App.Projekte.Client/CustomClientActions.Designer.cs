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
    public partial class CustomClientActions : API.Client.ICustomClientActions
    {
        public void Attach(Kistl.API.IDataObject obj)
        {
            if (obj is Projekt)
            {
                Projekt impl = obj as Projekt;
                impl.OnToString += new Kistl.API.ToStringHandler<Projekt>(Projekt_OnToString);
            }

            if (obj is Mitarbeiter)
            {
                Mitarbeiter impl = obj as Mitarbeiter;
                impl.OnToString += new Kistl.API.ToStringHandler<Mitarbeiter>(Mitarbeiter_OnToString);
            }

            if (obj is Task)
            {
                Task impl = obj as Task;
                impl.OnToString += new Kistl.API.ToStringHandler<Task>(Task_OnToString);
            }

            if (obj is App.Base.ObjectClass)
            {
                App.Base.ObjectClass impl = obj as App.Base.ObjectClass;
                impl.OnToString +=new Kistl.API.ToStringHandler<Kistl.App.Base.ObjectClass>(ObjectClass_OnToString);
            }

            if (obj is App.Base.BaseProperty)
            {
                App.Base.BaseProperty impl = obj as App.Base.BaseProperty;
                impl.OnToString += new Kistl.API.ToStringHandler<Kistl.App.Base.BaseProperty>(BaseProperty_OnToString);
            }
        }
    }
}
