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
namespace Zetbox.App.LicenseManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class LicenseActions
    {
        [Invocation]
        public static void ToString(License obj, MethodReturnEventArgs<string> e)
        {
            e.Result = $"{obj.Licensee}: {obj.ValidFrom} - {obj.ValidThru}, {obj.Description}";
        }

        [Invocation]
        public static void NotifyCreated(License obj)
        {
            obj.ValidThru = DateTime.Today.AddYears(1);
        }

        [Invocation]
        public static void Check(License obj, MethodReturnEventArgs<bool> e, System.Object certificate)
        {
            e.Result = obj.IsValid() && obj.IsSignatureValid(certificate);
        }
        [Invocation]
        public static void IsValid(License obj, MethodReturnEventArgs<bool> e)
        {
            var today = DateTime.Today;
            e.Result = obj.ValidFrom >= today && obj.ValidThru <= today;
        }

        private static byte[] ComputeHash(License obj)
        {
            using (var sha = SHA512.Create())
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                sw.Write(obj.Licensee);
                sw.Write(obj.Licensor);
                sw.Write(obj.ValidFrom);
                sw.Write(obj.ValidThru);
                sw.Write(obj.LicenseSubject);

                return sha.ComputeHash(ms.GetBuffer());
            }
        }

        [Invocation]
        public static void IsSignatureValid(License obj, MethodReturnEventArgs<bool> e, System.Object certificate)
        {
            X509Certificate2 cert;
            if (certificate is X509Certificate2)
            {
                cert = (X509Certificate2)certificate;
            }
            else if (certificate is byte[])
            {
                cert = new X509Certificate2((byte[])certificate);
            }
            else
            {
                throw new ArgumentException("certificate", "certificate is neither a X509Certificate2 or a byte[]");
            }

            var cng_public = (System.Security.Cryptography.RSACng)cert.PublicKey.Key;
            var hash = ComputeHash(obj);
            e.Result = cng_public.VerifyHash(hash, Convert.FromBase64String(obj.Signature), HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
        }

        [Invocation]
        public static void Sign(License obj, Zetbox.App.LicenseManagement.PrivateKey certificate)
        {
            var key = new X509Certificate2(Convert.FromBase64String(certificate.Certificate), certificate.Password);
            var cng_private = (System.Security.Cryptography.RSACng)key.GetRSAPrivateKey();
            var hash = ComputeHash(obj);
            obj.Signature = Convert.ToBase64String(cng_private.SignHash(hash, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1));
        }
    }
}
