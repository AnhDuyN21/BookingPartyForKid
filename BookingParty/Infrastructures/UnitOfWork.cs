using Application;
using Application.Interfaces;
using Application.Repositories;
using Infrastructures.Repositories;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        private readonly IAccountRepository _accountRepository;
        private readonly IPartyRepository _partyRepository;


        public UnitOfWork(AppDbContext dbContext, 
            IAccountRepository accountRepository,
            IPartyRepository partyRepository)

        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _partyRepository = partyRepository;

        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IPartyRepository PartyRepository => _partyRepository;

        public ICurrentTime CurrentRepository => throw new NotImplementedException();
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

