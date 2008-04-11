using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace API.Tests
{
    [Serializable]
    public class XMLObject : ICloneable, INotifyPropertyChanged
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public bool BoolProperty { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj is XMLObject)
            {
                XMLObject x = (XMLObject)obj;
                return
                       this.BoolProperty == x.BoolProperty
                    && this.IntProperty == x.IntProperty
                    && this.StringProperty == x.StringProperty;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
