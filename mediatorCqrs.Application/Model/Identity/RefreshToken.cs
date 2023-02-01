﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.Model.Identity
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Create { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
