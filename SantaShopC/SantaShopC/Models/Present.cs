using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantaShopC.Models
{
    public class Present
    {
        [Dapper.Contrib.Extensions.Key]
        public int PresentIDpresente_id { get; set; }
        public string nome { get; set; }
        public int quantidade { get; set; }
    }
}
