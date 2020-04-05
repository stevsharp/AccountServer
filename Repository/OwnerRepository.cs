using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Entities.Helpers;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        private readonly ISortHelper<Owner> _sortHelper;
        private readonly IDataShaper<Owner> _dataShaper;
        public OwnerRepository(RepositoryContext repositoryContext, 
            ISortHelper<Owner> sortHelper,
            IDataShaper<Owner> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public void CreateOwner(Owner owner)
        {
            this.Create(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            this.Delete(owner);
        }

        public async Task<PagedList<ShapedEntity>> GetAllOwnersAsync(OwnerParameters ownerParameters)
        {
            var owners = FindByCondition(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth &&
                                            o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth);

            SearchByName(ref owners, ownerParameters.Name);

            _sortHelper.ApplySort(owners, ownerParameters.OrderBy);

            var shapedOwners = _dataShaper.ShapeData(owners, ownerParameters.Fields);

            return await PagedList<ShapedEntity>.ToPagedListAsync(shapedOwners, ownerParameters.PageNumber, ownerParameters.PageSize);
        }

        public PagedList<ShapedEntity> GetOwners(OwnerParameters ownerParameters)
        {
            var owners = FindByCondition(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth &&
                                        o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth);

            SearchByName(ref owners, ownerParameters.Name);

            var sortedOwners = _sortHelper.ApplySort(owners, ownerParameters.OrderBy);
            var shapedOwners = _dataShaper.ShapeData(sortedOwners, ownerParameters.Fields);

            return PagedList<ShapedEntity>.ToPagedList(shapedOwners,
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
            var owner = await FindByCondition(x => x.Id.Equals(ownerId))
                             .DefaultIfEmpty(new Owner())
                             .FirstOrDefaultAsync();

            return owner;
        }

        public async Task<ShapedEntity> GetOwnerByIdAsync(string ownerId, string fields)
        {
            var owner = await FindByCondition(x => x.Id.Equals(ownerId))
                            .FirstOrDefaultAsync();

            return _dataShaper.ShapeData(owner, fields);
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
