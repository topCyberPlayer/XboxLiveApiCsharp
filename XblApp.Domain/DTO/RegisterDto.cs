﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Domain.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Gamertag { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
