
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Mail;
    using Autofac;

    public interface IMailSender
    {
        void Send(MailMessage msg);
    }

    /// <summary>
    /// Sends an MailMessage through the System.Net.Mail.SmtpClient class
    /// Can be configured in the app.config file, see http://msdn.microsoft.com/en-us/library/w355a94k.aspx
    /// </summary>
    /// <remarks>
    /// <code>
    /// <configuration>
    ///   <system.net>
    ///     <mailSettings>
    ///       <smtp deliveryMethod="Network">
    ///         <network
    ///           host="localhost"
    ///           port="25"
    ///           defaultCredentials="true"
    ///         />
    ///       </smtp>
    ///     </mailSettings>
    ///   </system.net>
    /// </configuration>
    /// </code>
    /// </remarks>
    public class SmtpMailSender : IMailSender
    {
        public void Send(MailMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Send(msg);
        }

        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .RegisterType<SmtpMailSender>()
                    .As<IMailSender>()
                    .SingleInstance(); // Stateless
            }
        }
    }

    /// <summary>
    /// Sends an MailMessage using the mailto:// shell execute method
    /// </summary>
    public class MailtoMailSender : IMailSender
    {
        public void Send(MailMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");

            var sb = new StringBuilder();

            var to = msg.To.FirstOrDefault();
            if (to == null) throw new ArgumentException("to field contains no adress", "msg");
            sb.AppendFormat("mailto:{0}?", to.Address);

            if (!string.IsNullOrEmpty(msg.Subject))
            {
                sb.AppendFormat("subject={0}&", Uri.EscapeUriString(msg.Subject));
            }

            if (!string.IsNullOrEmpty(msg.Body))
            {
                sb.AppendFormat("body={0}&", Uri.EscapeUriString(msg.Body));
            }

            if (msg.CC.Count > 0)
            {
                sb.AppendFormat("cc={0}", string.Join( " ", msg.CC.Select(i => i.Address).ToArray()));
            }

            if (msg.Bcc.Count > 0)
            {
                sb.AppendFormat("bcc={0}", string.Join(" ", msg.Bcc.Select(i => i.Address).ToArray()));
            }

            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
            si.UseShellExecute = true;
            si.FileName = sb.ToString();
            System.Diagnostics.Process.Start(si);       
        }

        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .RegisterType<MailtoMailSender>()
                    .As<IMailSender>()
                    .SingleInstance(); // Stateless
            }
        }
    }
}
