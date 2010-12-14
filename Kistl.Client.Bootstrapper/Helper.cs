using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Kistl.Client.Bootstrapper
{
    public static class Helper
    {
        public static void CopyTo(this System.IO.Stream src, System.IO.Stream dest)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (dest == null) throw new ArgumentNullException("dest");

            if (src.CanSeek) src.Seek(0, SeekOrigin.Begin);

            var buffer = new byte[4096];
            int cnt;
            while ((cnt = src.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, cnt);
            }
        }

        public static T FromXmlString<T>(this string xmlStr)
        {
            if (xmlStr == null) throw new ArgumentNullException("xmlStr");

            using (var sr = new StringReader(xmlStr))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                return (T)xml.Deserialize(sr);
            }
        }

        public static string GetLegalPathName(this string path)
        {
            Path.GetInvalidPathChars().Union(new[] { ':' }).ToList().ForEach(c => path = path.Replace(c, '_'));
            return path;
        }

        public static string Protect(this string str)
        {
            return Encoding.UTF8.GetString(
                    System.Security.Cryptography.ProtectedData.Protect(
                        Encoding.UTF8.GetBytes(str), null, System.Security.Cryptography.DataProtectionScope.CurrentUser)
                    );
        }

        public static string Unprotect(this string str)
        {
            return Encoding.UTF8.GetString(
                    System.Security.Cryptography.ProtectedData.Unprotect(
                        Encoding.UTF8.GetBytes(str), null, System.Security.Cryptography.DataProtectionScope.CurrentUser)
                    );
        }
    }
}
