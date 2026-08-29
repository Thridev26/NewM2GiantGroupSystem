using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    internal class WeeklyExpenseDetails
    {
        public int JobID { get; set; }

        public DateTime EndDate { get; set; }

        public decimal FuelCost { get; set; }

        //public decimal LabourCost { get; set; }

        public decimal DumpingCost { get; set; }

        public decimal TotalExpense
        {
            get
            {
                return FuelCost + DumpingCost;
            }
        }

    }
}
