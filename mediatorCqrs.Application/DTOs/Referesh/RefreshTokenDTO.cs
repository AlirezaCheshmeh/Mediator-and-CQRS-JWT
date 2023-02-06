using mediatorCqrs.Application.DTOs.CustomerDto;
using mediatorCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Application.DTOs.Referesh
{
    public class RefreshTokenDTO
    {
        
        public int RefID { get; set; }
        public string Rtoken { get; set; }
        public DateTime Create { get; set; }
        public DateTime Expire { get; set; }

        public CustomerDTO customerDTO { get; set; }
        public int cusId { get; set; }

    }
}
