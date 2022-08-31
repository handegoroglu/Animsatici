using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace animsatici
{
    internal class Reminding
    {
        public Guid id { get; set; } = new Guid();
        public DateTime dateTime { get; set; }
        public string message { get; set; }
        public bool isActive { get; set; }
    }
}
