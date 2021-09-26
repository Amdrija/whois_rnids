using System.Threading.Tasks;
using RNIDS.WHOIS.Core.Models;

namespace RNIDS.WHOIS.Application.Interfaces.Repositories
{
    public interface IEmailReminderRepository
    {
        public void Create(string email, Domain domain);
    }
}