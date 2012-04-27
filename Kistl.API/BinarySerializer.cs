
namespace Kistl.API
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    [Obsolete("Use KistlStreamWriter and KistlStreamReader instead")]
    public static class BinarySerializer
    {
        public static void ToStream(bool val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out bool val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void FromStreamConverter(Action<bool> consumer, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(bool? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out bool? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(DateTime val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out DateTime val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(DateTime? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out DateTime? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(Guid val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out Guid val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(Guid? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out Guid? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(double val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out double val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(double? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out double? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(float val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out float val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(float? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out float? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(int val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out int val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void FromStreamConverter(Action<int> conv, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(int? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out int? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(decimal val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out decimal val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(decimal? val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out decimal? val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(ICompoundObject val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream<T>(out T val, BinaryReader sr)
            where T : class, ICompoundObject, new()
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out ICompoundObject val, Type type, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(SerializableExpression e, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out SerializableExpression e, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(SerializableExpression[] expressions, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out SerializableExpression[] expressions, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(SerializableType type, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(SerializableType[] types, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out SerializableType type, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out SerializableType[] types, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(string val, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out string val, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void FromStreamConverter(Action<string> conv, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, BinaryWriter sw)
           where T : IStreamable
        {
            throw new NotImplementedException();
        }

        public static void FromStreamCollectionEntries<T>(IDataObject parent, ICollection<T> val, BinaryReader sr)
            where T : IStreamable, new()
        {
            throw new NotImplementedException();
        }

        public static void ToStream(byte[] bytes, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out byte[] bytes, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(ObjectNotificationRequest notificationRequest, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out ObjectNotificationRequest notificationRequest, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(ObjectNotificationRequest[] notificationRequests, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out ObjectNotificationRequest[] notificationRequests, BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(OrderByContract orderBy, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out OrderByContract orderBy, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(OrderByContract[] orderBys, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out OrderByContract[] orderBys, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            throw new NotImplementedException();
        }

        public static void ToStream(object value, BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public static void FromStream(out object value, Type type, BinaryReader sr)
        {
            throw new NotImplementedException();
        }
    }
}
