using Application.Interfaces;
using Application.Services;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.PartyDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingParty.Controllers
{
    public class PartyController : BaseController
    {
        private readonly IPartyService _partyService;
        public PartyController(IPartyService partyService)
        {
            _partyService = partyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPartyList()
        {
            var User = await _partyService.GetPartyAsync();
            return Ok(User);
        }
        [Authorize(Roles = "Host")]
        [HttpPost]
        public async Task<IActionResult> CreateParty([FromBody] CreatePartyDTO createPartyDTO)
        {
            //var userIdClaim = User.FindFirst("Id");

            //if ( userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            //{
            //    // Không tìm thấy UserID trong token
            //    return Unauthorized("Unable to retrieve UserID from token.");
            //}
            if (ModelState.IsValid)
            {
                var response = await _partyService.CreateParty(createPartyDTO);
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
    }
}
