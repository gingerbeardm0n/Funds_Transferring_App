using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Core;
using System.Web;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private string connectionString;

        public AccountDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public decimal GetBalance(int userID)
        {
            ReturnUser access = new ReturnUser();
            //access.UserId = 1; --> hardcoded to help test
            decimal balance = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT balance FROM accounts WHERE user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        balance = Convert.ToDecimal(reader["balance"]);
                    }

                    return balance;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdateBalance(TransferData transferData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET balance = @newBalance WHERE account_id = @account_id;", conn);
                    cmd.Parameters.AddWithValue("@newBalance", transferData.AmountToIncrease);
                    cmd.Parameters.AddWithValue("@account_id", transferData.UserIDToIncrease);
                    
                    

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            
        }


        public bool AddTransfer(TransferLog transfer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO transfer VALUES" +
                        "(@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)", conn);
                    cmd.Parameters.AddWithValue("@transfer_type_id", 2);
                    cmd.Parameters.AddWithValue("@transfer_status_id", 2);
                    cmd.Parameters.AddWithValue("@account_from", transfer.accountFrom);
                    cmd.Parameters.AddWithValue("@account_to", transfer.accountTo);
                    cmd.Parameters.AddWithValue("@amount", transfer.amount);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }



        public List<TransferLog> DisplayTransfers()
        {
            List<TransferLog> TransferHistory = new List<TransferLog>();
            ReturnUser access = new ReturnUser();
            TransferLog transferAccess = new TransferLog();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt FROM users", conn);
                    cmd.Parameters.AddWithValue("@", access);
                    SqlDataReader reader = cmd.ExecuteReader();

                    return TransferHistory;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

    }

}

