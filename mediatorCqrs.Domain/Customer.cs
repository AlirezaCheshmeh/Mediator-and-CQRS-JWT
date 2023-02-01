using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public byte[] passwordhash { get; set; }

        public byte[] passwordsalt { get; set; }


        public string RefreshToken { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; }
        public DateTime TokenExpieres { get; set; }
    }

}
