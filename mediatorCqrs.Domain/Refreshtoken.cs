using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Domain
{
    public class Refreshtoken
    {
        [Key]
        public int RefID { get; set; }
        public string Rtoken { get; set; }
        public DateTime Create { get; set; }
        public DateTime Expire { get; set; }

        public virtual Customer customer{ get; set; }

        public int cusId { get; set; }
    }
}
