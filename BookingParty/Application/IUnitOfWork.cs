
using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork
    {
       public IAccountRepository AccountRepository { get; }
        public IPartyRepository PartyRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
