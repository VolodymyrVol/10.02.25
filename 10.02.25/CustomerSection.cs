using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._02._25
{
    internal class CustomerSection
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
