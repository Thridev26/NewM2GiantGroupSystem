using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    internal class InvoiceDetails2
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
    }
}
