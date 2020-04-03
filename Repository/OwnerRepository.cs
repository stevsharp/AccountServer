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

        public async Task<PagedList<Owner>> GetAllOwnersAsync(OwnerParameters ownerParameters)
        {

            //var list = await FindAll()
            //            .OrderBy(x => x.Name)
            //            .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
            //            .Take(ownerParameters.PageSize)
            //            .ToListAsync();

            var owners = FindByCondition(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth &&
                                            o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth);

            SearchByName(ref owners, ownerParameters.Name);

            return PagedList<Owner>.ToPagedList(owners,
                    ownerParameters.PageNumber,
                    ownerParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Owner> owners, string ownerName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(ownerName))
                return;

            owners = owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
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
