using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// Class the gets the directory of the database
    /// created by: Nigel Mansell
    /// </summary>
    public static class pchrDB
    {
        /// <summary>
        /// creates a connection to the database
        /// </summary>
        /// <returns>connection</returns>
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=|DataDirectory|\\pchr42563.mdf;" +
            "Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
