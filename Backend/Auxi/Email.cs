// // ﻿using System.Net;
// // using System.Net.Mail;

// // string fromEmail = "musicsync.sup@gmail.com";
// // string fromPW = "zkvrlmdosrcdbxqc";

// // MailMessage mail = new MailMessage();

// // mail.From = new MailAddress(fromEmail);
// // mail.Subject= "Teste de envio de email";
// // mail.To.Add(new MailAddress("sobolevski.academy@gmail.com"));
// // mail.Body = "<html> <body> Corpo do email </body> </html>";
// // mail.IsBodyHtml = true;

// // SmtpClient smtp = new SmtpClient("smtp.gmail.com"){
// //     Port = 587,
// //     Credentials = new NetworkCredential(fromEmail, fromPW),
// //     EnableSsl = true,
// // };
// // smtp.Send(mail);

// using System.Net;
// using System.Net.Mail;

// namespace music_api.Auxi;

// public static class SendEmail
// {
//     private static string emailDomain = "";
//     private static string passDomain  = "";

//     private static MailMessage mailMessage = new MailMessage();

//     private static SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
//     {
//         Port = 587,
//         Credentials = new NetworkCredential(emailDomain, passDomain),
//         EnableSsl = true,
//     };
    
//     public static void Send(string to, string subject, string body)
//     {
//         mailMessage.From = new MailAddress(emailDomain);
//         mailMessage.Subject = subject;
//         mailMessage.To.Add(new MailAddress(to));
//         mailMessage.Body = body;
//         mailMessage.IsBodyHtml = true;

//         smtpClient.Send(mailMessage);
//     }
// }