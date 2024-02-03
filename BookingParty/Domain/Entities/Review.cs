using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public int? PartyID { get; set; }
        public int? BookingID { get; set; }
        public Rating Rating { get; set; }
        public string? Comment {  get; set; }
        public virtual Account? Account { get; set; }
        public virtual Booking? Booking { get; set; }
        public virtual Party? Party { get; set; }
    }
    public enum Rating
    {
        VeryBad = 0,
        Bad = 1,
        Medium = 2,
        Good = 3,
        VeryGood = 4
    }
}
