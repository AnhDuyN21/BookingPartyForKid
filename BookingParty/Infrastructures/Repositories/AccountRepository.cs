using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public Task<bool> CheckEmailNameExited(string email) =>
            _dbContext.Accounts.AnyAsync(u => u.Email == email);

        public Task<bool> CheckPhoneNumberExited(string phonenumber) =>
          _dbContext.Accounts.AnyAsync(u => u.PhoneNumber == phonenumber);
        public Task<bool> FindUserById(int userId) =>
            _dbContext.Accounts.AnyAsync(u => u.Id == userId);
        public async Task<Account> GetUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(
                record => record.Email == email && record.PasswordHash == passwordHash
            );
            if (user is null)
            {
                throw new Exception("Email & password is not correct");
            }

            return user;
        }

        public async Task<Account> GetUserByConfirmationToken(string token)
        {
            return await _dbContext.Accounts.SingleOrDefaultAsync(
                u => u.ConfirmationToken == token
            );
        }

        public async Task<IEnumerable<Account>> SearchAccountByNameAsync(string name)
        {
            return await _dbContext.Accounts.Where(u => u.FullName.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Account>> SearchAccountByRoleNameAsync(string roleName)
        {
            return await _dbContext.Accounts
                .Where(u => u.Role.Contains(roleName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetSortedAccountAsync()
        {
            return await _dbContext.Accounts.OrderByDescending(a => a.CreatedDate).ToListAsync();
        }
    }
}
