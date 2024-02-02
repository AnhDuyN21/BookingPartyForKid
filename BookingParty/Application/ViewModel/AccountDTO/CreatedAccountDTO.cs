using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.AccountDTO
{
    public class CreatedAccountDTO
    {
        public string FullName { get; set; }



        public string? Email { get; set; }
        public string? Gender { get; set; }


        public string? Password { get; set; }


        public string? PhoneNumber { get; set; }
        //public  int? Status { get; set; }
        //public string? Role { get; set; }
    }
}
