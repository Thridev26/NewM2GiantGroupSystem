using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    public class QuoteConversionDetails
    {
        public string RequestSource { get; set; }

        public int TotalRequests { get; set; }

        public int QuotesGenerated { get; set; }

        public int QuotesSent { get; set; }

        public int QuotesAccepted { get; set; }

        public int QuotesRejected { get; set; }

        public int RequestsCancelled { get; set; }

        public int JobsCreated { get; set; }

        public decimal AverageQuotedValue { get; set; }

        public decimal AcceptedQuoteRevenue { get; set; }

        public decimal ConversionRate
        {
            get
            {
                if (TotalRequests == 0)
                    return 0;

                return ((decimal)QuotesAccepted / TotalRequests) * 100;
            }
        }

    }//class
}