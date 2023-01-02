using Business.DTOs;

namespace Business.Interfacesl;

public interface IMailService
{
    Task SendEmailAsync(MailRequestDTO mailRequestDTO);
}
