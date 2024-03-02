using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public int PartyID { get; set; }
        public int AccountID { get; set; }
        public int BookingID { get; set; }
        public string? Comment {  get; set; }
        //Relationship
        public virtual Account? Account { get; set; }
        public virtual Booking? Booking { get; set; }
        public virtual Party? Party { get; set; }
    }
}
