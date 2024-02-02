using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.UpdateAccountDTO
{
    public class UpdateAccountDTO
    {
        //public int Id { get; set; }
        public string FullName { get; set; }


        public string? Email { get; set; }
        public string? Gender { get; set; }


        //public string? PasswordHash { get; set; }

        public int Status { get; set; }


        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
