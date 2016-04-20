using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DudzAPI.Models
{
    public class TFPParam
    {
        public string InquiryReferenceNo { get; set; }
        public int IssuingBankName { get; set; }
        public int ConfirmingBankName { get; set; }
        public int TypeOfTradeTransaction { get; set; }
        public int ApplicantName { get; set; }
        public int BeneficiaryName { get; set; }
        public double Tenor { get; set; }
        public string Goods { get; set; }
        public double TotalTransactionValue { get; set; }
        public double AmountADBCovered { get; set; }
    }
}