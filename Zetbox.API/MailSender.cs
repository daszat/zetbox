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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Mail;
    using Autofac;
    using Zetbox.API.Configuration;

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

        [Feature]
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

        [Feature]
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
