using Bolg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.ViewModels
{
    public class IndexViewModle
    {
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public bool NextPage { get; set; }
        public string Category { get; set; }
 
        public IEnumerable<Post> Posts { get; set; }
    }
}
