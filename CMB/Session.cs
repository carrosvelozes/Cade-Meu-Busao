using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMB
{
    public class Session
    {
        private static int userId;
        private static string username;

        public static int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        public static void ClearSession()
        {
            userId = 0;
            username = string.Empty;
        }
    }
}
