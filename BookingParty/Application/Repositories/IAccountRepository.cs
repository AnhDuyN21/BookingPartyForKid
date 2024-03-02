﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetUserByEmailAndPasswordHash(string email, string passwordHash);

        Task<bool> CheckEmailNameExited(string username);
        Task<bool> CheckPhoneNumberExited(string phonenumber);

        Task<Account> GetUserByConfirmationToken(string token);
        Task<IEnumerable<Account>> SearchAccountByNameAsync(string name);
        Task<IEnumerable<Account>> SearchAccountByRoleNameAsync(string roleName);
        //Task<IEnumerable<Account>> GetSortedAccountAsync();
    }
}
