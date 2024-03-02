using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.PartyDTO
{
    public class CreatePartyDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Price { get; set; }
        public string Theme { get; set; }
    }
}
