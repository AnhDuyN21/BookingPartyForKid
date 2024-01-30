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
       

        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository)

        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;

        }
        public IAccountRepository AccountRepository => _accountRepository;

        public ICurrentTime CurrentRepository => throw new NotImplementedException();
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

