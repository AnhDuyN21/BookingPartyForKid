
using Application.ServiceResponse;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.AuthenAccountDTO;
using Application.ViewModel.RegisterAccountDTO;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterAccountDTO registerAccountDTO);
        public Task<ServiceResponse<string>> LoginAsync(AuthenAccountDTO accountDto);
    }
}