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
    using Zetbox.API.Utils;
    using System.IO;
    using Autofac;
    using System.Net.Mail;
    using Zetbox.API.Configuration;

    public interface IProblemReporter
    {
        void Report(string message, string description, System.Drawing.Bitmap screenshot, Exception exeption);
    }

    public class LoggingProblemReporter : IProblemReporter
    {
        public void Report(string message, string description, System.Drawing.Bitmap screenshot, Exception exeption)
        {
            Logging.Log.Error(string.Format("{0}\n{1}\n", message, description), exeption);
        }
    }

    public class FogBugzProblemReporter : IProblemReporter
    {
        [Feature]
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder.Register<FogBugzProblemReporter>(c => new FogBugzProblemReporter())
                    .As<IProblemReporter>()
                    .SingleInstance();                
            }
        }

        public void Report(string message, string description, System.Drawing.Bitmap screenshot, Exception exeption)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
                var filename = Helper.PathCombine(ProgramFilesx86(), "FogBugz", "Screenshot", "screenshot.exe");
                if (!File.Exists(filename))
                {
                    filename = Helper.PathCombine(ProgramFilesx86(), "Screenshot", "screenshot.exe"); ;
                    if (!File.Exists(filename))
                    {
                        Logging.Log.Error("FogBugz screenshot tool not found. Maybe it's not installed. screenshot.exe neither found in %ProgramFilesx86%\\FogBugz\\Screenshot nor %ProgramFilesx86%\\Screenshot");
                    }
                }

                si.FileName = filename;
                si.Arguments = "/picture";
                System.Diagnostics.Process.Start(si);
            }
            catch (Exception ex)
            {
                Logging.Log.Error(ex.ToString());
                throw;
            }
        }

        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }

    public class MailProblemReporter : IProblemReporter
    {
        [Feature]
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder.Register<MailProblemReporter>(c => new MailProblemReporter(c.Resolve<IMailSender>()))
                    .As<IProblemReporter>()
                    .SingleInstance();
            }
        }

        private readonly IMailSender _mail;

        public MailProblemReporter(IMailSender mail)
        {
            if (mail == null) throw new ArgumentNullException("mail");
            _mail = mail;
        }

        public void Report(string message, string description, System.Drawing.Bitmap screenshot, Exception exeption)
        {
            // TODO: Hardocded mail addresses
            var msg = new MailMessage("office@dasz.at", "fogbugz@dasz.at");
            msg.Subject = message != null ? message.Replace("\n", "").Replace("\r", "") : string.Empty;
            msg.Body = string.Format("{0}\n\n{1}\n\nException:\n{2}", message, description, exeption);

            if (screenshot != null)
            {
                var ms = new MemoryStream();
                screenshot.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                msg.Attachments.Add(new Attachment(ms, "screenshot.png"));
            }
            _mail.Send(msg);
        }
    }
}
