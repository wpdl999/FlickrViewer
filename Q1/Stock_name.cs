using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
   public class Stock_name { 
    
        public string Symbol { get; set; }
        public string date { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string close { get; set; }

        public List<Stock_name> sName_List { get; set; }
    }
}
