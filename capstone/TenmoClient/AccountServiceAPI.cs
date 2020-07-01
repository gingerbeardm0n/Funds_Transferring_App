using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    class AccountServiceAPI : AuthService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/account/";
        // Wnat to use the restlient defined in the parent class    -   private readonly IRestClient client = new RestClient();

        public decimal GetBalance()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "balance");//changed from "get_balance" to "balance"
            //get's don't have bodies
            IRestResponse<decimal> response = client.Get<decimal>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("An error occurred communicating with the server.");

            }
            else if (!response.IsSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(response.Data.ToString()))
                {
                    throw new Exception("An error message was received: " + response.Data);
                }
                else
                {
                    throw new Exception("An error response was received from the server. The status code is " + (int)response.StatusCode);
                }

            }
            else
            {
                // this was done at login so dont need to do it again, token was assign at login    client.Authenticator = new JwtAuthenticator(response.Data.ToString());
                return response.Data;
            }
        }

        






    }
}
