using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZetboxApp.Wizard
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

        public static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

    }
}
