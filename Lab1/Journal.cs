using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Journal
    {
        public string Name { get; set; }
        public string Frequency { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }

        public List<Article> Articles { get; set; }
    }
}
