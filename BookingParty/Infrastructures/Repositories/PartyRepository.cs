using Application.Interfaces;
using Application.Repositories;
using Application.ViewModel.PartyDTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<PartyDTO>> GetPartyBy(string city) 
        {
            var allParty =  _dbContext.Partys.AsQueryable();
            if (!string.IsNullOrEmpty(city))
            {
                allParty = allParty.Where(p => p.City.Contains(city));
            }
                
            var result = allParty.Select(p => new PartyDTO
            {
                Id = p.Id,
                Title = p.Title,
                City    = p.City,
                DateTime = p.DateTime,
                Theme = p.Theme,
                Status = p.Status,
            });
            return result; 
        }

    }

}

