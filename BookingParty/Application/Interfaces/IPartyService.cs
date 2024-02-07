using Application.ServiceResponse;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.PartyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPartyService
    {
        Task<ServiceResponse<IEnumerable<PartyDTO>>> GetPartyAsync(string search);
        Task<ServiceResponse<IEnumerable<PartyDTO>>> GetAllPartyAsync();
        Task<ServiceResponse<PartyDTO>>CreateParty(CreatePartyDTO createPartyDTO);
        Task<ServiceResponse<PartyDTO>> CreatePartyVIP(CreatePartyDTO createPartyDTO);
        Task<ServiceResponse<PartyDTO>> UpdatePartyAsync(int id, CreatePartyDTO updatePartyDTO);
        Task<ServiceResponse<PartyDTO>> CheckOwner(int owner ,int id);


    }
}
