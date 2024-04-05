using Teaching.Models;
using Teaching.ViewModels;

namespace Teaching.Interface
{
    public interface IMailRepository
    {
        public Task SendEmailAsync(MailViewModel mailViewModel, bool isAuthorized);
    }
}
