using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    internal class WeeklyIncomeDetails
    {
        public int PaymentID { get; set; }

        public DateTime PaymentDate { get; set; }

        public int JobID { get; set; }

        public string ClientName { get; set; }

        public decimal AmountPaid { get; set; }

    }
}
