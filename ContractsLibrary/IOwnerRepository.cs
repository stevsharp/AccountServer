using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggerService
{
    public interface IOwnerRepository : IRepositoryBase<Owner> 
    {
        Task<PagedList<ShapedEntity>> GetAllOwnersAsync(OwnerParameters ownerParameters);
        PagedList<ShapedEntity> GetOwners(OwnerParameters ownerParameters);
        Task<ShapedEntity> GetOwnerByIdAsync(string ownerId, string fields);
        Task<Owner> GetOwnerByIdAsync(string ownerId);
        Task<Owner> GetOwnerWithDetailsAsync(string ownerId);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }

}
