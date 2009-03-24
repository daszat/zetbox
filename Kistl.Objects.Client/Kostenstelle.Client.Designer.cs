
namespace Kistl.App.Zeiterfassung
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Kostenstelle")]
    public class Kostenstelle__Implementation__ : Kistl.App.Zeiterfassung.Zeitkonto__Implementation__, Kostenstelle
    {
    
		public Kostenstelle__Implementation__()
		{
            {
            }
        }


		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Kostenstelle));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Kostenstelle != null)
            {
                OnToString_Kostenstelle(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Kostenstelle> OnToString_Kostenstelle;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Kostenstelle != null) OnPreSave_Kostenstelle(this);
        }
        public event ObjectEventHandler<Kostenstelle> OnPreSave_Kostenstelle;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Kostenstelle != null) OnPostSave_Kostenstelle(this);
        }
        public event ObjectEventHandler<Kostenstelle> OnPostSave_Kostenstelle;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}


#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

#endregion

    }


}