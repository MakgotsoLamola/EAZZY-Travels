using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

/// Sends account-related emails (login codes, password reset links,
/// welcome messages) using the SMTP settings in Web.config, and
/// generates the codes/tokens used for email-based authentication.
public static class EmailHelper
{
    public static void SendEmail(string toEmail, string subject, string body)
    {
        string host = ConfigurationManager.AppSettings["SmtpHost"];
        int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        string user = ConfigurationManager.AppSettings["SmtpUser"];
        string pass = ConfigurationManager.AppSettings["SmtpPass"];
        string fromName = ConfigurationManager.AppSettings["SmtpFromName"];

        using (var client = new SmtpClient(host, port))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(user, pass);
            client.EnableSsl = true;

            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(user, fromName);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                client.Send(mail);
            }
        }
    }

    /// Generates a 6-digit numeric one-time code for login verification.
    public static string GenerateOtp()
    {
        byte[] randomBytes = new byte[4];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        int value = BitConverter.ToInt32(randomBytes, 0) & int.MaxValue;
        int code = value % 900000 + 100000; // always 6 digits, 100000-999999
        return code.ToString();
    }

    /// Generates an unguessable token used in password-reset links.
    public static string GenerateResetToken()
    {
        return Guid.NewGuid().ToString("N");
    }
}
