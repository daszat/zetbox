// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Zetbox.Client.Bootstrapper
{
    public static class Helper
    {
        public static void CopyAllTo(this System.IO.Stream src, System.IO.Stream dest)
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

        public static string GetLegalFileName(this string path)
        {
            Path.GetInvalidFileNameChars().ToList().ForEach(c => path = path.Replace(c, '_'));
            return path;
        }

        public static string Protect(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return Convert.ToBase64String(
                    System.Security.Cryptography.ProtectedData.Protect(
                        Encoding.UTF8.GetBytes(str), null, System.Security.Cryptography.DataProtectionScope.CurrentUser)
                    );
        }

        public static string Unprotect(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return Encoding.UTF8.GetString(
                    System.Security.Cryptography.ProtectedData.Unprotect(
                        Convert.FromBase64String(str), null, System.Security.Cryptography.DataProtectionScope.CurrentUser)
                    );
        }
    }
}
