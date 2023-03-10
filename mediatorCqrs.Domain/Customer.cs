using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Domain
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public byte[] passwordhash { get; set; }

        public byte[] passwordsalt { get; set; }


        public virtual Refreshtoken refreshToken { get; set; }


    }

}
