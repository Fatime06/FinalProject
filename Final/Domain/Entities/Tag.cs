using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tag : Audit
    {
        public string Name { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
    }
}
