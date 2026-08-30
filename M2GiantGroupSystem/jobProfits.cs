using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    public class jobProfits
    {

        public string JobType { get; set; }

        public int NumberOfJobs { get; set; }

        public decimal TotalRevenue { get; set; }

       // public decimal LabourCost { get; set; }

        public decimal FuelCost { get; set; }

        public decimal DumpingCost { get; set; }

        public decimal AssetCost { get; set; }

        public decimal TotalCost
        {
            get
            {
                return 
                     FuelCost
                     + DumpingCost
                     + AssetCost;
            }
        }

        public decimal Profit
        {
            get
            {
                return TotalRevenue - TotalCost;
            }
        }

        public decimal ProfitMargin
        {
            get
            {
                if (TotalRevenue == 0)
                    return 0;

                return (Profit / TotalRevenue) * 100;
            }
        }

    }//class
}
