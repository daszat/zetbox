
namespace Kistl.Microsoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Outlook = global::Microsoft.Office.Interop.Outlook;
    using System.IO;

    /// <summary>
    /// Sends MailMessages using Outlook
    /// </summary>
    public class OutlookMailSender : IMailSender, IDisposable
    {
        public void Send(MailMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");

            Outlook.Application app = new Outlook.Application();
            var ns = app.GetNamespace("MAPI");
            ns.Logon();
            var fld = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox);
            Outlook.MailItem mail = (Outlook.MailItem)fld.Items.Add();

            foreach (var to in msg.To)
            {
                var r = mail.Recipients.Add(to.Address);
                r.Type = (int)Outlook.OlMailRecipientType.olTo;
            }
            foreach (var to in msg.CC)
            {
                var r = mail.Recipients.Add(to.Address);
                r.Type = (int)Outlook.OlMailRecipientType.olCC;
            }
            foreach (var to in msg.Bcc)
            {
                var r = mail.Recipients.Add(to.Address);
                r.Type = (int)Outlook.OlMailRecipientType.olBCC;
            }

            mail.Subject = msg.Subject;
            if (msg.IsBodyHtml)
            {
                mail.HTMLBody = msg.Body;
            }
            else
            {
                mail.Body = msg.Body;
            }

            foreach (var a in msg.Attachments)
            {
                var tmpFile = CreateTempFile(a.Name);
                using (var fs = File.OpenWrite(tmpFile))
                {
                    a.ContentStream.CopyTo(fs);
                }
                var olAttachment = mail.Attachments.Add(tmpFile, Type.Missing, Type.Missing, a.Name);
            }

            mail.Display();
            ns.Logoff();
        }

        private List<string> _tempFiles = new List<string>();

        protected string CreateTempFile(string filename)
        {
            // TODO: Move that to a global helper and delete files on shutdown
            var tmp = Path.GetTempFileName();
            if (File.Exists(tmp)) File.Delete(tmp);
            Directory.CreateDirectory(tmp);
            _tempFiles.Add(tmp);
            return Path.Combine(tmp, filename);
        }

        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .RegisterType<OutlookMailSender>()
                    .As<IMailSender>()
                    .SingleInstance(); // Stateless
            }
        }

        public void Dispose()
        {
            foreach (var tmp in _tempFiles)
            {
                try
                {
                    if (Directory.Exists(tmp))
                    {
                        Directory.Delete(tmp, true);
                    }
                }
                catch
                {
                    // Who cares
                }
            }
            _tempFiles.Clear();
        }
    }
}
