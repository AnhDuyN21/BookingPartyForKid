using System.Data.Common;
using Application.Commons;
using Application.Interfaces;
using Application.ServiceResponse;
using Application.Utils;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.AuthenAccountDTO;
using Application.ViewModel.RegisterAccountDTO;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _configuration;

        private IValidator<Account> _validator;
        private readonly IMapper _mapper;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            ICurrentTime currentTime,
            AppConfiguration configuration,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _currentTime = currentTime;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> LoginAsync(AuthenAccountDTO authenAccountDto)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var hashedPassword = Utils.HashPassword.HashWithSHA256(authenAccountDto.Password);
                var user = await _unitOfWork.AccountRepository.GetUserByEmailAndPasswordHash(authenAccountDto.Email,hashedPassword);
                //User không tồn tại
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }

                if (user.ConfirmationToken != null && !user.IsConfirmed)
                {
                    System.Console.WriteLine("Username: "+user.FullName);
                    System.Console.WriteLine("User token: " +user.ConfirmationToken + " IsConfirmed: "+user.IsConfirmed);
                    System.Console.WriteLine("Login Denied!");
                    response.Success = false;
                    response.Message = "Please confirm via link in your mail";
                    return response;
                }

                var token = user.GenerateJsonWebToken(
                    _configuration,
                    _configuration.JWTSection.SecretKey,
                    _currentTime.GetCurrentTime()
                );
                //Login thành công
                response.Success = true;
                response.Message = "Login successfully.";
                //Đưa ra Token
                response.Data = token;
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

        public async Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterAccountDTO registerAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();
            try
            {
                //Check xem email đã tồn tại trong DB hay chưa
                var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(registerAccountDTO.Email);
                if (exist)
                {
                    response.Success = false;
                    response.Message = "Email is existed";
                    return response;
                }

                var account = _mapper.Map<Account>(registerAccountDTO);
                account.PasswordHash = Utils.HashPassword.HashWithSHA256(registerAccountDTO.PasswordHash);
                // Tạo token ngẫu nhiên
                account.ConfirmationToken = Guid.NewGuid().ToString();
                //Set status
                account.Status = true;
                //Set role
                account.Role = "Host";
                await _unitOfWork.AccountRepository.AddAsync(account);

                var confirmationLink = $"https://localhost:7233/swagger/confirm?token={account.ConfirmationToken}";

                // Gửi email xác nhận
                var emailSent = await SendEmail.SendConfirmationEmail(account.Email, confirmationLink);
                if (!emailSent)
                {
                    // Xử lý khi gửi email không thành công
                    response.Success = false;
                    response.Message = "Error sending confirmation email.";
                    return response;
                }
                else
                {
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        var accountDTO = _mapper.Map<AccountDTO>(account);
                        response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                        response.Success = true;
                        response.Message = "Register successfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error saving the account.";
                    }
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
    }
}
