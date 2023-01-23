using mediatorCqrs.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace mediatorCqrs.Domain
{
    public class User : BaseDomainEntity
    {
        public string name { get; set; }
        public string email { get; set; }
        public string lastName { get; set; }
        public bool isActive { get; set; }
    }
}
