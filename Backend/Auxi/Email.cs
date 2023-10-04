// ﻿using System.Net;
// using System.Net.Mail;

// string fromEmail = "musicsync.sup@gmail.com";
// string fromPW = "zkvrlmdosrcdbxqc";

// MailMessage mail = new MailMessage();

// mail.From = new MailAddress(fromEmail);
// mail.Subject= "Teste de envio de email";
// mail.To.Add(new MailAddress("sobolevski.academy@gmail.com"));
// mail.Body = "<html> <body> Corpo do email </body> </html>";
// mail.IsBodyHtml = true;

// SmtpClient smtp = new SmtpClient("smtp.gmail.com"){
//     Port = 587,
//     Credentials = new NetworkCredential(fromEmail, fromPW),
//     EnableSsl = true,
// };
// smtp.Send(mail);

using System.Net;
using System.Net.Mail;

namespace music_api.Auxi;

public static class SendEmail
{
    private static string emailDomain = Environment.GetEnvironmentVariable("EMAIL_GOOGLE");
    private static string passDomain  = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
    private static SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential(emailDomain, passDomain),
        EnableSsl = true,
    };
    
    public static void Send(string to, string subject, string body)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(emailDomain);
        mailMessage.Subject = subject;
        mailMessage.To.Add(new MailAddress(to));
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;

        smtpClient.Send(mailMessage);
    }


    public static void SendEmailValidation(string userEmail, string username, string bodyCotent){
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(emailDomain);
        mailMessage.Subject = "Music Sync Email Validation";
        mailMessage.To.Add(new MailAddress(userEmail));
        mailMessage.Body = GenerateEmailValidationBody(bodyCotent, username);
        smtpClient.Send(mailMessage);
    }



    private static string GenerateEmailValidationBody(string token, string username){
        string sla = """<html><body><header><div class="ms-logo"><img src="https://i.ibb.co/CsYDtgh/transparent-logo-with-border.png" alt="logo-musicsync" width="100px"><h1>Music Sync</h1></div><div class="content"><h2>Welcome to Music Sync</h2></div></header><div class="row"\><main><div class="header"><h3>Confirm your email</h3></div><div class="content"><p>Hi {{USER.NAME}}, welcome to Music Sync</p><p>To complete your registration with Music Sync, enter the code below.</p><div class="token-area"><h2>{{TOKEN}}</h2></div></div></main><div class="row"\></body></html>""";
        sla.Replace("{{USER.NAME}}", username);
        sla.Replace("{{TOKEN}}", token);

        return sla;
    }
}