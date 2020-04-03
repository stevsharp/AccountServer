using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account> , IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext){}

        public async Task<IEnumerable<Account>> AccountsByOwner(string ownerId)
        {
            return await FindByCondition(a => a.OwnerId.Equals(ownerId)).ToListAsync();
        }
    }
}
