using mediatorCqrs.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.DTOs
{
    public class CreateUserDtos :BaseDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string lastName { get; set; }
        public bool isActive { get; set; }
    }
}
