﻿using Application.ViewModel.PartyDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPartyRepository : IGenericRepository<Party>
    {
        Task<IEnumerable<PartyDTO>> GetPartyBy(string search);
    }
}
