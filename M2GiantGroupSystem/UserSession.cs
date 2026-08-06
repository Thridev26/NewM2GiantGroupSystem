using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    public static class UserSession
    {
        // These will store the data after a successful login
        public static int StaffID { get; set; }
        public static int AccessLevel { get; set; }
        public static string UserName { get; set; }
        public static string FirstName { get; set; }

        public static string LastName { get; set; }
    }
}
