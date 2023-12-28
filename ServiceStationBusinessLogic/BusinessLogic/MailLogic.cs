using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using ServiceStationBusinessLogic.HelperModels;

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
            try
            {
                if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0)
                    return;

                if (string.IsNullOrEmpty(mailLogin) || string.IsNullOrEmpty(mailPassword) || string.IsNullOrEmpty(mailName))
                    return;

                if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text) || string.IsNullOrEmpty(info.FileName))
                    return;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailName, mailLogin));
                message.To.Add(new MailboxAddress("To User", info.MailAddress));
                message.Subject = info.Subject;

                var body = new TextPart("plain")
                {
                    Text = info.Text
                };

                var multipart = new Multipart("mixed");
                multipart.Add(body);

                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(System.IO.File.OpenRead(info.FileName), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "attachment.pdf"
                };

                multipart.Add(attachment);
                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpClientHost, smtpClientPort, SecureSocketOptions.StartTls);
                    client.Authenticate(mailLogin, mailPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки письма: {ex.Message}");
            }
        }
    }
}
