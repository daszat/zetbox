using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class TestStruct : IStruct
    {
        public int ID { get; set; }
        public bool IsReadonly { get; private set; }

        public void ToStream(System.IO.BinaryWriter sw)
        {
            BinarySerializer.ToBinary(ID, sw);
        }

        public void FromStream(System.IO.BinaryReader sr)
        {
            int _tmp;
            BinarySerializer.FromBinary(out _tmp, sr);
            ID = _tmp;
        }


        public object Clone()
        {
            return null;
        }


        public void AttachToObject(IPersistenceObject obj, string property)
        {
        }

        public void DetachFromObject(IPersistenceObject obj, string property)
        {
        }
    }
}
