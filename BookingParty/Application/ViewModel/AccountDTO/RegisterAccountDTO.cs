﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.RegisterAccountDTO
{
    public class RegisterAccountDTO
    {
        
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
