using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Author
    {
        public string Name { get; set; }
        public string Organization { get; set; }

        public List<Article> Articles { get; set; }
    }
}
