using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Party : BaseEntity
    {
        public string? Title {  get; set; }
        public string? Description { get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public DateTime? DateTime { get; set; }
        public int Price { get; set; }
        public string Theme { get; set; }
        public string? PackageOption {  get; set; }
        public int Max_People {  get; set; }
        public string Image_url {  get; set; }
        public int HostID {  get; set; }

        //Relationship
        public virtual Account? Account { get; set; }
        public virtual ICollection<Booking>? Booking { get; set; }
        public virtual ICollection<Review>? Review { get; set; }
        public Party()
        {
            // Khi chưa có booking nào thì list sẽ rỗng chứ không bằng null
            Booking = new List<Booking>();
        }
    }

}
