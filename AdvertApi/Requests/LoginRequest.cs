﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Requests{
    public class LoginRequest{
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
