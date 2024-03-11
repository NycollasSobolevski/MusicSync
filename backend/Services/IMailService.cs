
namespace music_api;

public interface IMailService {
  void Send(string to, string subject, string body);
  void SendEmailValidation(string userEmail, string username, string bodyCotent);
  void SendEmailRecoveryPassword(string userEmail, string username, string bodyCotent);
}