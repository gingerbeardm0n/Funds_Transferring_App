﻿using System;
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
    public class UserAccountDAO
    {
        //private string connectionString;

        //public UserAccountDAO(string databaseconnectionString)
        //{
        //    connectionString = databaseconnectionString;
        //}

        private string SqlGetBalance = @"SELECT balance FROM accounts WHERE user_id = @UserId;";
        
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=tenmo;Integrated Security=True";

        public decimal ReturnBalance(int userID)
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
    }
}
