using Application.Interfaces;
using Application.Services;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.PartyDTO;
using Application.ViewModel.UpdateAccountDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> GetPartyListByCity([Required] string City)
        {   
            var party = await _partyService.GetPartyAsync(City);
            return Ok(party);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParty()
        {
            var party = await _partyService.GetAllPartyAsync();
            return Ok(party);
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

        [Authorize(Roles = "Host")]
        [HttpPost]
        public async Task<IActionResult> CreatePartyVIP([FromBody] CreatePartyDTO createPartyDTO)
        {
            //var userIdClaim = User.FindFirst("Id");

            //if ( userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            //{
            //    // Không tìm thấy UserID trong token
            //    return Unauthorized("Unable to retrieve UserID from token.");
            //}
            if (ModelState.IsValid)
            {
                var response = await _partyService.CreatePartyVIP(createPartyDTO);
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
        [Authorize(Roles = "Host")]
        [HttpPut]
        public async Task<IActionResult> UpdatePartyById(int id, [FromBody] CreatePartyDTO updatepartyDTO)
        {
            int userIdClaim;
            var userIdClaimValue = User.FindFirstValue("Id");

            if (!int.TryParse(userIdClaimValue, out userIdClaim))
            {
                return BadRequest("Invalid user ID claim");
            }

            // Check if the user is the owner of the party
            //var isOwner = await _partyService.CheckOwner(userIdClaim, id);

            //if (!isOwner.Success)
            //{
            //    // User is not the owner, forbid the update
            //    return BadRequest("You do not have permission"); ;
            //}

            var updatedParty = await _partyService.UpdatePartyAsync(id, updatepartyDTO);
            if (!updatedParty.Success)
            {
                return NotFound(updatedParty);
            }
            return Ok(updatedParty);

        }

    }
}
