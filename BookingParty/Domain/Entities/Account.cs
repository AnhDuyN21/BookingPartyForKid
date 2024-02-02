using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account:BaseEntity
    {
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        //Party
        public virtual ICollection<Party>? Party { get; set; }

    }
}
