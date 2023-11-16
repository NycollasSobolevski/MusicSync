
using System.Net;
using System.Net.Mail;

namespace music_api.Auxi;

public class SendEmail : IMailService
{
    private static string emailDomain = Environment.GetEnvironmentVariable("EMAIL_GOOGLE");
    private static string passDomain  = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
    private SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential( emailDomain, passDomain),
        EnableSsl = true,
    };
    
    public void Send(string to, string subject, string body)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(emailDomain);
        mailMessage.Subject = subject;
        mailMessage.To.Add(new MailAddress(to));
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;

        smtpClient.Send(mailMessage);
    }


    public void SendEmailValidation(string userEmail, string username, string bodyCotent){
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(emailDomain);
        mailMessage.Subject = "Music Sync Email Validation";
        mailMessage.To.Add(new MailAddress(userEmail));
        mailMessage.Body = GenerateEmailValidationBody(bodyCotent, username);
        mailMessage.IsBodyHtml = true;
        smtpClient.Send(mailMessage);
    }

    public void SendEmailRecoveryPassword(string userEmail, string username, string bodyCotent) {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(emailDomain);
        mailMessage.Subject = "Music Sync Password Recovery";
        mailMessage.To.Add(new MailAddress(userEmail));
        mailMessage.Body = GenerateEmailRecoveryPasswordBody(bodyCotent, username);
        mailMessage.IsBodyHtml = true;
        smtpClient.Send(mailMessage);
    }

    private string GenerateEmailRecoveryPasswordBody (string token, string username) {
        string body = @"<html><body><header><div class='ms-logo'><img src='https://i.ibb.co/CsYDtgh/transparent-logo-with-border.png' alt='logo-musicsync' width='100px'><h1>Music Sync</h1></div><div class='content'><h2>Welcome Again</h2></div></header><div class='row'\><main><div class='header'><h3>Validate your email</h3></div><div class='content'><p>Hi {{USER.NAME}}</p><p>To complete your recovery password solicitation with Music Sync, enter the code below.</p><div class='token-area'><h2>{{TOKEN}}</h2></div></div></main><div class='row' \'></body></html>";
        body = body.Replace("{{USER.NAME}}", username);
        body = body.Replace("{{TOKEN}}", token);
        return body;
    }
    private string GenerateEmailValidationBody(string token, string username){
        string body = @"<html><body><header><div class='ms-logo'><img src='https://i.ibb.co/CsYDtgh/transparent-logo-with-border.png' alt='logo-musicsync' width='100px'><h1>Music Sync</h1></div><div class='content'><h2>Welcome to Music Sync</h2></div></header><div class='row'\><main><div class='header'><h3>Confirm your email</h3></div><div class='content'><p>Hi {{USER.NAME}}, welcome to Music Sync</p><p>To complete your registration with Music Sync, enter the code below.</p><div class='token-area'><h2>{{TOKEN}}</h2></div></div></main><div class='row'\></body></html>";
        body = body.Replace("{{USER.NAME}}", username);
        body = body.Replace("{{TOKEN}}", token);
        System.Console.WriteLine(username + token);

        return body;
    }
}