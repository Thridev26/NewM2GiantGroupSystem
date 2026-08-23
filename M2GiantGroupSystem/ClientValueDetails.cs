using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    public class ClientValueDetails
    {
        public int ClientID { get; set; }

        public string Client { get; set; }

        public string ClientType { get; set; }

        public int NumberOfRequests { get; set; }

        public int CompletedJobs { get; set; }

        public decimal TotalAmountQuoted { get; set; }

        public decimal TotalAmountPaid { get; set; }

        public decimal AverageJobValue { get; set; }

        public int CancelledRejectedRequests { get; set; }

        public DateTime LastJobDate { get; set; }
    }
}
