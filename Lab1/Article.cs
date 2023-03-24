using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Article
    {
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public Journal Journal { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
