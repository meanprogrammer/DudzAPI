using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DudzAPI.Models
{
    public class TFPLimit
    {
        public Int64 CounterPartyIDTo { get; set; }
        public Int64 CounterPartyIDFrom { get; set; }
        public string ShortnameTo { get; set; }
        public string ShortnameFrom { get; set; }
        public string CountryTo { get; set; }
        public string CountryFrom { get; set; }
        public decimal SublimitAmountInUSDTo { get; set; }
        public decimal SublimitAmountInUSDFrom { get; set; }
        public Int64 SublimitTypeIdTo { get; set; }
        public Int64 SublimitTypeIdFrom { get; set; }
        public string SublimitTypeTo { get; set; }
        public string SublimitTypeFrom { get; set; }
        public decimal FinalLimit { get; set; }
    }
}