
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
    using System.Runtime.InteropServices;
using Kistl.Client.Presentables;
    using Kistl.API.Utils;

    /// <summary>
    /// Sends MailMessages using Outlook
    /// </summary>
    public class OutlookMailSender : IMailSender
    {
        private const int E_ABORT = -2147467260;

        private readonly IViewModelFactory _vmf;
        private readonly ITempFileService _tmpService;

        public OutlookMailSender(IViewModelFactory vmf, ITempFileService tmpService)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (tmpService == null) throw new ArgumentNullException("tmpService");
            _vmf = vmf;
            _tmpService = tmpService;
        }

        public void Send(MailMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");

            Outlook.Application app = new Outlook.Application();
            var ns = app.GetNamespace("MAPI");
            ns.Logon();

            try
            {
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
                    var tmpFile = _tmpService.Create(a.Name);
                    using (var fs = File.OpenWrite(tmpFile))
                    {
                        a.ContentStream.CopyTo(fs);
                    }
                    var olAttachment = mail.Attachments.Add(tmpFile, Type.Missing, Type.Missing, a.Name);
                }

                mail.Display();
            }
            catch (COMException ex)
            {
                Logging.Client.Error("Unable to send mail throug Outlook", ex);
                _vmf.ShowMessage(ex.ErrorCode == E_ABORT 
                        ? OutlookMailSenderResources.AbortErrorMessage 
                        : OutlookMailSenderResources.GenericErrorMessage, 
                    OutlookMailSenderResources.ErrorCaption);
            }
            finally
            {
                ns.Logoff();
            }
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
    }
}
