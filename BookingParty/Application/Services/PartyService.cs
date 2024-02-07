using Application.Interfaces;
using Application.ServiceResponse;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.PartyDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PartyService : IPartyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PartyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PartyDTO>> CreateParty(CreatePartyDTO createPartyDTO)
        {
            var response = new ServiceResponse<PartyDTO>();
            try
            {
                var party = _mapper.Map<Party>(createPartyDTO);
                
                party.PackageOption = "Normal";
                party.Status = true;
                await _unitOfWork.PartyRepository.AddAsync(party);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if(isSuccess)
                {
                    var partyDTO = _mapper.Map<PartyDTO>(createPartyDTO);
                    response.Success = true;
                    response.Message = "Party created successfully.";
                    response.Data= partyDTO;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the party.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<PartyDTO>>> GetAllPartyAsync()
        {
            var respone = new ServiceResponse<IEnumerable<PartyDTO>>();
            try
            {


                var partys = await _unitOfWork.PartyRepository.GetAllAsync();
                var partyDTO = new List<PartyDTO>();
                foreach (var party in partys)
                {
                    if (party.Status)
                    {
                        partyDTO.Add(_mapper.Map<PartyDTO>(party));

                    }
                }
                if (partyDTO.Count > 0)
                {
                    respone.Success = true;
                    respone.Message = "Get all Party available successfully";
                    respone.Data = partyDTO;
                }
                else
                {
                    respone.Success = true;
                    respone.Message = "Not have Party";
                }

            }
            catch (Exception ex)
            {
                respone.Success = false;
                respone.Message = "Error";
                respone.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return respone;
        }

        public async Task<ServiceResponse<IEnumerable<PartyDTO>>> GetPartyAsync(string search)
        {
            var respone = new ServiceResponse<IEnumerable<PartyDTO>>();
            try
            {


                var partys = await _unitOfWork.PartyRepository.GetPartyBy(search);
                var partyDTO = new List<PartyDTO>();
                foreach (var party in partys)
                {
                    if (party.Status)
                    {
                        partyDTO.Add(_mapper.Map<PartyDTO>(party));
                        
                    }
                }
                if(partyDTO.Count > 0)
                {
                    respone.Success = true;
                    respone.Message = "Get all Party available successfully";
                    respone.Data = partyDTO;
                }
                else
                {
                    respone.Success = true;
                    respone.Message = "Not have Party";
                }

            }
            catch(Exception ex)
            {
                respone.Success = false;
                respone.Message = "Error";
                respone.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return respone;
        }
    }
}
