using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampLMS.Data
{
    public class Settings
    {
        private static string connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                }

                return connectionString;
            }
        }
    }
}
