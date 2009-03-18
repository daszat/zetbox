
namespace Kistl.App.Test
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="LastTest")]
    [System.Diagnostics.DebuggerDisplay("LastTest")]
    public class LastTest__Implementation__ : BaseServerDataObject_EntityFramework, LastTest
    {

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(LastTest));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_LastTest != null)
            {
                OnToString_LastTest(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<LastTest> OnToString_LastTest;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_LastTest != null) OnPreSave_LastTest(this);
        }
        public event ObjectEventHandler<LastTest> OnPreSave_LastTest;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_LastTest != null) OnPostSave_LastTest(this);
        }
        public event ObjectEventHandler<LastTest> OnPostSave_LastTest;




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