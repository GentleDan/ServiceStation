using ServiceStationBusinessLogic.HelperModels;
using System;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace ServiceStationBusinessLogic.BusinessLogic
{
    public class MailLogic
    {
        private static string smtpClientHost;
        private static int smtpClientPort;
        private static string mailLogin;
        private static string mailPassword;
        private static string mailName;

        public static void MailConfig(MailConfig config)
        {
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
            mailName = config.MailName;
        }

        public static void MailSend(MailSendInfo info)
        {
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword) || string.IsNullOrEmpty(mailName))
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text) || string.IsNullOrEmpty(info.FileName))
            {
                return;
            }

            using (var emailMessage = new MimeMessage())
            {
                emailMessage.From.Add(new MailboxAddress(info.FileName, mailLogin));
                emailMessage.To.Add(new MailboxAddress(info.FileName, info.MailAddress));
                emailMessage.Subject = info.Subject;

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpClientHost, smtpClientPort, false);
                    client.Authenticate(mailLogin, mailPassword);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
        }
    }
}
