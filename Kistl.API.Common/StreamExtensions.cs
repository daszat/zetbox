
namespace Kistl.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class StreamExtensions
    {
        public static void WriteAllTo(this Stream source, Stream dest)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (dest == null) throw new ArgumentNullException("dest");

            byte[] buffer = new byte[1024*4];
            int readBytes = -1;
            while (readBytes != 0)
            {
                readBytes = source.Read(buffer, 0, buffer.Length);
                dest.Write(buffer, 0, readBytes);
            }
        }
    }
}
