using Application.Interfaces;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.UpdateAccountDTO;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BookingParty.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAccountList()
        {
            var User = await _accountService.GetAccountAsync();
            return Ok(User);
        }



        [HttpPost("RegisterGuest")]
        public async Task<IActionResult> RegisterGuest([FromBody] CreatedAccountDTO createdAccountDTO)
        {
            //Dòng này kiểm tra xem dữ liệu đầu vào (trong trường hợp này là createdAccountDTO)
            //đã được kiểm tra tính hợp lệ bằng các quy tắc mô hình (model validation) hay chưa.
            //Nếu dữ liệu hợp lệ, nó tiếp tục kiểm tra và xử lý.
            if (ModelState.IsValid)
            {
                var response = await _accountService.CreateAccountAsync(createdAccountDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            else
            {
                return BadRequest("Invalid request data.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateAccountDTO accountDTO)
        {
            var updatedUser = await _accountService.UpdateUserAsync(id, accountDTO);
            if (!updatedUser.Success)
            {
                return NotFound(updatedUser);
            }
            return Ok(updatedUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _accountService.DeleteUserAsync(id);
            if (!deletedUser.Success)
            {
                return NotFound(deletedUser);
            }
            return Ok(deletedUser);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var findaccountUser = await _accountService.GetAccountByIdAsync(id);
            if (!findaccountUser.Success)
            {
                return NotFound(findaccountUser);
            }
            return Ok(findaccountUser);
        }



        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            // Lấy thông tin người dùng từ ClaimsPrincipal
            var userIdClaim = User.FindFirst("Id");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                // Gọi phương thức đổi mật khẩu trong service và trả về kết quả
                var result = await _accountService.ChangePasswordAsync(userId, changePasswordDto);

                if (result.Success)
                {
                    return Ok(new { Message = result.Message });
                }
                else
                {
                    return BadRequest(new { Message = result.Message });
                }
            }
            else
            {
                // Trả về thông báo lỗi nếu không tìm thấy hoặc không thể chuyển đổi giá trị ID
                return BadRequest(new { Message = "User ID not found or invalid in the token." });
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet("{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var result = await _accountService.SearchAccountByNameAsync(name);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{role}")]
        public async Task<IActionResult> SearchByRole([FromRoute] string role)
        {
            var result = await _accountService.SearchAccountByRoleNameAsync(role);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public async Task<IActionResult> GetSortedAccount()
        //{
        //    var result = await _accountService.GetSortedAccountsAsync();

        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }

        //}
    }
}

