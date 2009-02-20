using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using Kistl.API;
    using Kistl.API.Client;

namespace Kistl.App.Base
{
// no collection entry needed for Kistl.App.Base.ObjectClass_ImplementsInterfaces29CollectionEntry
}

namespace Kistl.App.Base
{
// no collection entry needed for Kistl.App.Base.TypeRef_GenericArguments46CollectionEntry
}

namespace Kistl.App.GUI
{
// no collection entry needed for Kistl.App.GUI.Template_Menu41CollectionEntry
}

namespace Kistl.App.GUI
{
// no collection entry needed for Kistl.App.GUI.Visual_Children35CollectionEntry
}

namespace Kistl.App.GUI
{
// no collection entry needed for Kistl.App.GUI.Visual_ContextMenu40CollectionEntry
}

namespace Kistl.App.Projekte
{
// no collection entry needed for Kistl.App.Projekte.Projekt_Mitarbeiter3CollectionEntry
}

namespace Kistl.App.Zeiterfassung
{
	using Kistl.App.Projekte;
// no collection entry needed for Kistl.App.Zeiterfassung.Zeitkonto_Mitarbeiter22CollectionEntry
}

namespace Kistl.App.Projekte
{
    [System.Diagnostics.DebuggerDisplay("Kunde_EMailsCollectionEntry__Implementation__")]
    public class Kunde_EMailsCollectionEntry__Implementation__ : BaseClientCollectionEntry, INewCollectionEntry<Kunde, System.String>
    {
    
// ID is inherited

        /// <summary>
        /// Reference to the A-Side member of this CollectionEntry
        /// </summary>
        // parent property
        public virtual Kunde A
        {
            get
            {
                return _A;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_A != value)
                {
                    NotifyPropertyChanging("A");
                    _A = value;
                    NotifyPropertyChanged("A");
                }
            }
        }
        private Kunde _A;

        // proxy to A.ID
        public int fk_A
        {
            get { return A.ID; }
            set { A = this.Context.Find<Kunde>(value); }
        }


        /// <summary>
        /// the B-side value of this CollectionEntry
        /// </summary>
        // value type property
        public virtual string B
        {
            get
            {
                return _B;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_B != value)
                {
                    NotifyPropertyChanging("B");
                    _B = value;
                    NotifyPropertyChanged("B");
                }
            }
        }
        private string _B;

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._B, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._B, binStream);
        }

#endregion

	public override Type GetInterfaceType()
	{
		return typeof(INewCollectionEntry<Kunde, System.String>);
	}


    }
}
