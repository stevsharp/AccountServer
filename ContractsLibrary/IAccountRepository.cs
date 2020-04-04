using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerService
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> AccountsByOwner(string ownerId);
        Task<PagedList<Account>> GetAccountsByOwner(string ownerId, AccountParameters parameters);
        Account GetAccountByOwner(string ownerId, string id);
    }

}
