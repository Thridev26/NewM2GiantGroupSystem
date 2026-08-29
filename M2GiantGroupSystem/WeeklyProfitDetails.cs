using System;

namespace M2GiantGroupSystem
{
    internal class WeeklyProfitDetails
    {
        public int JobID { get; set; }

        public DateTime Date { get; set; }

        public decimal Income { get; set; }

        public decimal FuelCost { get; set; }

        public decimal DumpingCost { get; set; }

        public decimal TotalExpense
        {
            get
            {
                return FuelCost + DumpingCost;
            }
        }

        public decimal Profit
        {
            get
            {
                return Income - TotalExpense;
            }
        }
    }
}