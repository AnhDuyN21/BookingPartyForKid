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
        public int Id { get; set; }
        public string? Title {  get; set; }
        public string? City { get; set; }
        public DateTime? DateTime { get; set; }
        public string Theme { get; set; }
        public bool Status {  get; set; }
    }
}
