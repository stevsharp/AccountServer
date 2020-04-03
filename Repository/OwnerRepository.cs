using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext) : base(repositoryContext){}

        public void CreateOwner(Owner owner)
        {
            this.Create(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            this.Delete(owner);
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await FindAll()
                        .OrderBy(x => x.Name)
                        .ToListAsync();
        }

        public async Task<Owner> GetOwnerByIdAsync(string ownerId)
        {
            return await FindByCondition(x => x.Id.Equals(ownerId))
                            .FirstOrDefaultAsync();
        }

        public async Task<Owner> GetOwnerWithDetailsAsync(string ownerId)
        {
            return await FindByCondition(x => x.Id.Equals(ownerId))
                            .Include(ac => ac.Accounts)
                            .FirstOrDefaultAsync();
        }

        public void UpdateOwner(Owner owner)
        {
            this.Update(owner);
        }
    }
}
