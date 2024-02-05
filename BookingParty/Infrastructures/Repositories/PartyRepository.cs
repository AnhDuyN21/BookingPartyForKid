using Application.Interfaces;
using Application.Repositories;
using Application.ViewModel.PartyDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class PartyRepository : GenericRepository<Party>, IPartyRepository
    {
        private readonly AppDbContext _dbContext;
        public PartyRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

    }

}

