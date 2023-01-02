using Business.DTOs;

namespace Business.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequestDto mailRequest);
}
