using mediatorCqrs.Application.DTOs.Referesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.DTOs.CustomerDto
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public byte[] passwordhash { get; set; }

        public byte[] passwordsalt { get; set; }

        
    }
}
