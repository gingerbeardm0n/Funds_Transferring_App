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

        ConsoleService CSAccess = new ConsoleService();
        API_User api_User = new API_User();

        public decimal GetMyBalance()
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
                return response.Data;
            }
        }

        public List<API_User> GetUsers()
        {
            List<API_User> userList = new List<API_User>();

            RestRequest request = new RestRequest(API_BASE_URL + "get_users");

            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
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
                return response.Data;
            }
        }

        public TransferData UpdateBalance(TransferData transferData)
        {
            decimal currentBalance = GetMyBalance();//todo ASK JOHN refactor to not have to call this method in both the AccountAPI and the ConsoleService
            
            RestRequest request = new RestRequest(API_BASE_URL + "transfer");
            request.AddJsonBody(transferData);
            IRestResponse<TransferData> response = client.Post<TransferData>(request);

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

                return response.Data;
            }
            
        }













    }
}
