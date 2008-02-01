using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class BinarySerializer
    {
        #region ToBinary
        public static void ToBinary(bool val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(DateTime val, System.IO.BinaryWriter sw)
        {
            sw.Write(val.ToBinary());
        }

        public static void ToBinary(int val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(float val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(double val, System.IO.BinaryWriter sw)
        {
            sw.Write(val);
        }

        public static void ToBinary(string val, System.IO.BinaryWriter sw)
        {
            if (val != null) { sw.Write(true); sw.Write(val); } else sw.Write(false);
        }

        public static void ToBinary(ObjectType val, System.IO.BinaryWriter sw)
        {
            ToBinary(val.Namespace, sw);
            ToBinary(val.Classname, sw);
        }

        public static void ToBinary(bool? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(DateTime? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value.ToBinary()); } else sw.Write(false);
        }

        public static void ToBinary(int? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(float? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(double? val, System.IO.BinaryWriter sw)
        {
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        public static void ToBinary(IEnumerable<IDataObject> val, System.IO.BinaryWriter sw)
        {
            foreach (IDataObject obj in val)
            {
                ToBinary(true, sw);
                obj.ToStream(sw);
            }

            ToBinary(false, sw);
        }
        #endregion

        #region FromBinary
        public static void FromBinary(out bool val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean();
        }

        public static void FromBinary(out DateTime val, System.IO.BinaryReader sr)
        {
            val = DateTime.FromBinary(sr.ReadInt64());
        }

        public static void FromBinary(out int val, System.IO.BinaryReader sr)
        {
            val = sr.ReadInt32();
        }

        public static void FromBinary(out float val, System.IO.BinaryReader sr)
        {
            val = sr.ReadSingle();
        }

        public static void FromBinary(out double val, System.IO.BinaryReader sr)
        {
            val = sr.ReadDouble();
        }

        public static void FromBinary(out string val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean() ? sr.ReadString() : null;
        }

        public static void FromBinary(out ObjectType val, System.IO.BinaryReader sr)
        {
            // HASS!!!! Gebt mir C++ friends wieder!
            // Man kann Properties oder Indexer nicht als out Parameter mitgeben!
            string tmpN, tmpC;
            FromBinary(out tmpN, sr);
            FromBinary(out tmpC, sr);

            val = new ObjectType();
            val.Namespace = tmpN;
            val.Classname = tmpC;
        }

        public static void FromBinary(out bool? val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean() ? (bool?)sr.ReadBoolean() : null;
        }

        public static void FromBinary(out DateTime? val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean() ? (DateTime?)DateTime.FromBinary(sr.ReadInt64()) : null;
        }

        public static void FromBinary(out int? val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean() ? (int?)sr.ReadInt32() : null;
        }

        public static void FromBinary(out float? val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean() ? (float?)sr.ReadSingle() : null;
        }

        public static void FromBinary(out double? val, System.IO.BinaryReader sr)
        {
            val = sr.ReadBoolean() ? (double?)sr.ReadDouble() : null;
        }

        public static void FromBinary<T>(out List<T> val, System.IO.BinaryReader sr, IKistlContext ctx) where T : IDataObject
        {
            val = new List<T>();
            while (sr.ReadBoolean())
            {
                long pos = sr.BaseStream.Position;
                ObjectType objType;
                BinarySerializer.FromBinary(out objType, sr);

                sr.BaseStream.Seek(pos, System.IO.SeekOrigin.Begin);

                IDataObject obj = objType.NewDataObject();
                obj.FromStream(ctx, sr);

                val.Add((T)obj);
            }
        }
        #endregion
    }
}
