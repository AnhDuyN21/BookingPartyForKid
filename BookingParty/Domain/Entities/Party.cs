using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Party : BaseEntity
    {

        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address {  get; set; }
        public DateTime PartyDateTime { get; set; }
        public float Price {  get; set; }
        public int PartyTheme { get; set; }
        public string PartyPackage {  get; set; }
        //Account
        public virtual Account CreatedBy { get; set; }
    }
}
