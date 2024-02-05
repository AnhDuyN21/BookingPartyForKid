using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.PartyDTO
{
    public class PartyDTO
    {
        public string? Title {  get; set; }
        public string? City { get; set; }
        public DateTime? DateTime { get; set; }
        public PartyTheme Theme { get; set; }
    }
}
