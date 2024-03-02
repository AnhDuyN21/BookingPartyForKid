using Application.Interfaces;
using Application.ServiceResponse;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.PartyDTO;
using Application.ViewModel.UpdateAccountDTO;
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
        public async Task<ServiceResponse<PartyDTO>> CreatePartyVIP(CreatePartyDTO createPartyDTO)
        {
            var response = new ServiceResponse<PartyDTO>();
            try
            {
                var party = _mapper.Map<Party>(createPartyDTO);

                party.PackageOption = "VIP";
                party.Status = true;
                await _unitOfWork.PartyRepository.AddAsync(party);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var partyDTO = _mapper.Map<PartyDTO>(createPartyDTO);
                    response.Success = true;
                    response.Message = "Party created successfully.";
                    response.Data = partyDTO;
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
        public async Task<ServiceResponse<PartyDTO>> UpdatePartyAsync(int id, CreatePartyDTO updatePartyDTO)
        {
            var response = new ServiceResponse<PartyDTO>();

            try
            {
                var existingParty = await _unitOfWork.PartyRepository.GetByIdAsync(id);

                if (existingParty == null)
                {
                    response.Success = false;
                    response.Message = "Party not found.";
                    return response;
                }

                if (existingParty.Status == false)
                {
                    response.Success = false;
                    response.Message = "Party is deleted in system";
                    return response;
                }



                // Map accountDT0 => existingUser
                var updated = _mapper.Map(updatePartyDTO, existingParty);


                _unitOfWork.PartyRepository.Update(existingParty);

                var updatedPartyDTO = _mapper.Map<PartyDTO>(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    response.Data = updatedPartyDTO;
                    response.Success = true;
                    response.Message = "Party updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the Party.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
        //public async Task<ServiceResponse<PartyDTO>> CheckOwner(int owner, int id)
        //{
        //    var response = new ServiceResponse<PartyDTO>();

        //    var exist = await _unitOfWork.PartyRepository.GetByIdAsync(id);
        //    if (exist == null)
        //    {
        //        response.Success = false;
        //        response.Message = "Party is not existed";
        //    }
        //    if(exist.Acc != owner)
        //    {
        //        response.Success = false;
                
        //    }
        //    else
        //    {
        //        response.Success = true;
        //        response.Message = "Party found";
               
        //    }

        //    return response;
        //}

    }
}
