using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class UserAccountDAO
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=tenmo;Integrated Security=True";
        private string SqlGetBalance = @"SELECT balance FROM accounts WHERE user_id = @UserId;";

        public decimal ReturnBalance(string UserId)
        {
            User access = new User();
            decimal balance = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlGetBalance, conn);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    SqlDataReader reader = cmd.ExecuteReader();

                    return balance;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
