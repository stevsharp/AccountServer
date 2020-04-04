﻿using Entities;
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
        private ISortHelper<Owner> _sortHelper;
        public OwnerRepository(RepositoryContext repositoryContext, ISortHelper<Owner> sortHelper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
        }

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
            var owners = FindByCondition(o => o.DateOfBirth.Year >= ownerParameters.MinYearOfBirth &&
                                            o.DateOfBirth.Year <= ownerParameters.MaxYearOfBirth);

            SearchByName(ref owners, ownerParameters.Name);

            _sortHelper.ApplySort(owners, ownerParameters.OrderBy);

            return await PagedList<Owner>.ToPagedList(owners,
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
