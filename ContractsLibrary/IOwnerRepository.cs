using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerService
{
    public interface IOwnerRepository : IRepositoryBase<Owner> 
    {
        Task<PagedList<Entity>> GetAllOwnersAsync(OwnerParameters ownerParameters);
        PagedList<Entity> GetOwners(OwnerParameters ownerParameters);
        Task<Owner> GetOwnerByIdAsync(string ownerId);
        Task<Owner> GetOwnerWithDetailsAsync(string ownerId);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }

}
