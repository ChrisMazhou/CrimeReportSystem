using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.Models
{
    public class GridResultModel<T>
    {
        public GridResultModel(List<T> data, int totalRecords)
        {
            Results = data;
            RecordCount = totalRecords;
        }

        public List<T> Results { get; set; }

        public int RecordCount { get; set; }
    }
}
