using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Application.Commons;
using Application.Interfaces;
using Application.ServiceResponse;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.UpdateAccountDTO;
using AutoMapper;
using Domain.Entities;



namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreatedAccountDTO createdAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(createdAccountDTO.Email);
            var existed = await _unitOfWork.AccountRepository.CheckPhoneNumberExited(createdAccountDTO.PhoneNumber);

            if (exist)
            {
                response.Success = false;
                response.Message = "Email is existed";
                return response;
            }
            else if (existed)
            {
                response.Success = false;
                response.Message = "Phone is existed";
                return response;
            }
            try
            {
                var account = _mapper.Map<Account>(createdAccountDTO);
                account.PasswordHash = Utils.HashPassword.HashWithSHA256(createdAccountDTO.Password);
                account.Role = "Guest";
                //chuyển đổi gender thành chữ thường
                account.Gender = createdAccountDTO.Gender.ToLower();

                account.Status = true;

                await _unitOfWork.AccountRepository.AddAsync(account);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var accountDTO = _mapper.Map<AccountDTO>(account);
                    response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                    response.Success = true;
                    response.Message = "User created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
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


        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                _unitOfWork.AccountRepository.SoftRemove(exist);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the account.";
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

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync()
        {
            // var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            // var result = _mapper.Map<List<AccountDTO>>(accounts);
            // return result;

            var _response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetAllAsync();

                var accountDTOs = new List<AccountDTO>();

                foreach (var acc in accounts)
                {
                    if (acc.Status)
                    {
                        accountDTOs.Add(_mapper.Map<AccountDTO>(acc));
                    }
                }

                if (accountDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Account retrieved successfully";
                    _response.Data = accountDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Not have Account";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }
        public async Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var response = new ServiceResponse<AccountDTO>();

            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Account found";
                response.Data = _mapper.Map<AccountDTO>(exist);
            }

            return response;
        }
        //public async Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, AccountDTO accountDTO)
        //{
        //    var response = new ServiceResponse<AccountDTO>();

        //    try
        //    {
        //        var existingUser = await _unitOfWork.AccountRepository.GetByIdAsync(id);

        //        if (existingUser == null)
        //        {
        //            response.Success = false;
        //            response.Message = "Account not found.";
        //            return response;
        //        }

        //        if (existingUser.Status == false)
        //        {
        //            response.Success = false;
        //            response.Message = "Account is deleted in system";
        //            return response;
        //        }


        //        // Map accountDT0 => existingUser
        //        var updated = _mapper.Map(accountDTO, existingUser);
        //        updated.PasswordHash = Utils.HashPassword.HashWithSHA256(accountDTO.PasswordHash);

        //        _unitOfWork.AccountRepository.Update(existingUser);

        //        var updatedUserDto = _mapper.Map<AccountDTO>(updated);
        //        var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

        //        if (isSuccess)
        //        {
        //            response.Data = updatedUserDto;
        //            response.Success = true;
        //            response.Message = "Account updated successfully.";
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "Error updating the account.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = "Error";
        //        response.ErrorMessages = new List<string> { ex.Message };
        //    }

        //    return response;
        //}
        public async Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, UpdateAccountDTO accountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            try
            {
                var existingUser = await _unitOfWork.AccountRepository.GetByIdAsync(id);

                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "Account not found.";
                    return response;
                }

                if (existingUser.Status == false)
                {
                    response.Success = false;
                    response.Message = "Account is deleted in system";
                    return response;
                }


                // Map accountDT0 => existingUser
                var updated = _mapper.Map(accountDTO, existingUser);
                

                _unitOfWork.AccountRepository.Update(existingUser);

                var updatedUserDto = _mapper.Map<AccountDTO>(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    response.Data = updatedUserDto;
                    response.Success = true;
                    response.Message = "Account updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the account.";
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
        public async Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto)
        {
            var response = new ServiceResponse<string>();

            // Kiểm tra xem người dùng có tồn tại không
            var user = await _unitOfWork.AccountRepository.GetByIdAsync(userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Account not found";
                return response;
            }
            // Kiểm tra xem người dùng đang cố gắng thay đổi mật khẩu của chính họ hay không
            if (user.Id != userId)
            {
                response.Success = false;
                response.Message = "Unauthorized to change password for another account";
                return response;
            }

            // Xác minh mật khẩu cũ
            var hashedOldPassword = Utils.HashPassword.HashWithSHA256(changePasswordDto.OldPassword);
            if (user.PasswordHash != hashedOldPassword)
            {
                response.Success = false;
                response.Message = "Incorrect old password";
                return response;
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới (nếu cần)
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                response.Success = false;
                response.Message = "New password and confirmation do not match";
                return response;
            }

            // Băm mật khẩu mới
            var hashedNewPassword = Utils.HashPassword.HashWithSHA256(changePasswordDto.NewPassword);

            // Lưu mật khẩu mới vào cơ sở dữ liệu
            user.PasswordHash = hashedNewPassword;
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSuccess)
            {
                response.Success = true;
                response.Message = "Password changed fail.";
                return response;
            }

            response.Success = true;
            response.Message = "Password changed successfully.";
            return response;

        }



        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> SearchAccountByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accounts = await _unitOfWork.AccountRepository.SearchAccountByNameAsync(name);

                var accountDTOs = new List<AccountDTO>();

                foreach (var acc in accounts)
                {
                    if (!acc.Status)
                    {
                        accountDTOs.Add(_mapper.Map<AccountDTO>(acc));
                    }
                }

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not have Account";
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

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> SearchAccountByRoleNameAsync(string roleName)
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accounts = await _unitOfWork.AccountRepository.SearchAccountByRoleNameAsync(roleName);

                var accountDTOs = new List<AccountDTO>();

                foreach (var acc in accounts)
                {
                    if (!acc.Status)
                    {
                        accountDTOs.Add(_mapper.Map<AccountDTO>(acc));
                    }
                }

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not have Account";
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

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetSortedAccountsAsync()
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetSortedAccountAsync();

                var accountDTOs = new List<AccountDTO>();

                foreach (var acc in accounts)
                {
                    if (!acc.Status)
                    {
                        accountDTOs.Add(_mapper.Map<AccountDTO>(acc));
                    }
                }

                if (accountDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = accountDTOs;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not have Account";
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
    
    }
}
    

