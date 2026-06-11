using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    internal class InvoiceDetails3
    {
        public int JobID { get; set; }
        public string ClientName { get; set; }
        public string SiteAddress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobTypeName { get; set; }
        public decimal JobRate { get; set; }
        public decimal DetailValue { get; set; }
        public decimal LineTotal { get; set; }
        public decimal QuoteAmount { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal LineItemsSubtotal { get; set; }  // sum of all line totals
        public decimal TravelFee { get; set; }          // back-calculated
        public decimal VATAmount { get; set; }          // back-calculated
    }
}
