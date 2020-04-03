using System.Threading.Tasks;

namespace LoggerService
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        void Save();
        Task SaveAsync();
    }

}
