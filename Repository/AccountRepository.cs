using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Helpers;
using Entities.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account> , IAccountRepository
    {
        private ISortHelper<Account> _sortHelper;
        public AccountRepository(RepositoryContext repositoryContext, ISortHelper<Account> sortHelper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<IEnumerable<Account>> AccountsByOwner(string ownerId)
        {
            return await FindByCondition(a => a.OwnerId.Equals(ownerId)).ToListAsync();
        }

        public Account GetAccountByOwner(string ownerId, string id)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId) && a.Id.Equals(id)).SingleOrDefault();
        }

        public async Task<PagedList<Account>> GetAccountsByOwner(string ownerId , AccountParameters parameters)
        {
            var condition = FindByCondition(a => a.OwnerId.Equals(ownerId));
            return await PagedList<Account>.ToPagedList(condition,
                    parameters.PageNumber,
                    parameters.PageSize);
        }
    }
}
