using Application.Interfaces;
using Application.ViewModel.AccountDTO;

using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountList()
        {
            var User = await _accountService.GetAccountAsync();
            return Ok(User);
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreatedAccountDTO createdAccountDTO)
        {
            //Dòng này kiểm tra xem dữ liệu đầu vào (trong trường hợp này là createdAccountDTO)
            //đã được kiểm tra tính hợp lệ bằng các quy tắc mô hình (model validation) hay chưa.
            //Nếu dữ liệu hợp lệ, nó tiếp tục kiểm tra và xử lý.
            if (ModelState.IsValid)
            {
                var response = await _accountService.CreateAccountAsync(createdAccountDTO);
                if (!response.Success)
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] AccountDTO accountDTO)
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

        [Authorize(Roles = "Admin")]
        [Authorize]
        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] ChangePasswordDTO changePasswordDto)
        {
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetSortedAccount()
        {
            var result = await _accountService.GetSortedAccountsAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
    }
}

