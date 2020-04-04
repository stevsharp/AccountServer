using Entities;
using Entities.Helpers;
using Entities.Models;
using LoggerService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private ISortHelper<Owner> _ownerSortHelper;
        private ISortHelper<Account> _accountSortHelper;
        private IDataShaper<Owner> _ownerDataShaper;
        private IDataShaper<Account> _accountDataShaper;
        public IOwnerRepository Owner
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new OwnerRepository(_repoContext, _ownerSortHelper, _ownerDataShaper);
                }

                return _owner;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext, _accountSortHelper, _accountDataShaper);
                }

                return _account;
            }
        }

        public RepositoryWrapper(RepositoryContext repoContext, ISortHelper<Owner> ownerSortHelper,
        ISortHelper<Account> accountSortHelper, IDataShaper<Owner> ownerDataShaper,
        IDataShaper<Account> accountDataShaper)
        {
            _repoContext = repoContext;
            _ownerSortHelper = ownerSortHelper;
            _accountSortHelper = accountSortHelper;
            _ownerDataShaper = ownerDataShaper;
            _accountDataShaper = accountDataShaper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
