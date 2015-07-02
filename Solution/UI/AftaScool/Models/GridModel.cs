using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.Models
{
    public class GridModel
    {
        public int? CurrentPage { get; set; }

        public int? RecordsPerPage { get; set; }

        public string SortKey { get; set; }

        public string SortOrder { get; set; }

        public string Searchfor { get; set; }
    }
}
