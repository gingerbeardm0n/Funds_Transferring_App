using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.Models;
using TenmoServer.DAO;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        private readonly IAccountDAO accountDAO;
        private readonly IUserDAO userDAO;
        public TransferLogEntry transferlog = new TransferLogEntry();

        public AccountController(IUserDAO userDAO, IAccountDAO accountDAO)
        {
            this.userDAO = userDAO;
            this.accountDAO = accountDAO;
        }
        
        [Authorize]
        [HttpGet("balance")]
        public decimal GetMyBalance()
        {

            int userID = GetMyUserID();

            decimal balance = accountDAO.GetMyBalance(userID);

            return balance;
        }

        [Authorize]
        [HttpPost("transfer")]
        public ActionResult UpdateBalance(TransferData transferData)
        {
            int userID = GetMyUserID();
            decimal myBalance = accountDAO.GetMyBalance(userID);

            if (myBalance >= transferData.TransferAmount)
            {
                bool sender = accountDAO.UpdateMyBalance(transferData, userID);
                bool receiver = accountDAO.UpdateUserBalance(transferData);
                
                if (sender == true && receiver == true)
                {
                    bool transferAdded = AddTransfer(transferData);

                    if (transferAdded == true)
                    {
                        return Created("", transferData);
                    }
                } 
            }
            return BadRequest();
        }
     

        [Authorize]
        [HttpPost("insert")]
        public ActionResult CreateTransfer(TransferLogEntry transfer)
        {
            bool result = accountDAO.AddTransfer(transfer);

            if (result)
            {
                return Created("", transfer);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("users")]
        public List<User> GetUsers()
        {
            List<User> users = userDAO.GetUsers();
            
            return users;
        }

        [Authorize]
        [HttpGet("tansferHistory")]
        public decimal DisplayMyTransfers()
        {

            int userID = GetMyUserID();

            decimal balance = accountDAO.GetMyBalance(userID);

            return balance;
        }


        public int GetMyUserID()
        {
            var user = User.Identity.Name;
            int userID = -1;

            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    userID = int.Parse(claim.Value);
                }
            }
            return userID;
        }

        public bool AddTransfer(TransferData transferData)
        {
            int userID = GetMyUserID();

            transferlog.TransferTypeId = 2;
            transferlog.TransferStatusId = 2;
            transferlog.AccountFrom = userID;
            transferlog.AccountTo = transferData.AccountIDToIncrease;
            transferlog.Amount = transferData.TransferAmount;

            bool transferLogAdded = accountDAO.AddTransfer(transferlog);

            if (transferLogAdded == true)
            {
                return true;
            }
            return false;
        }
    }
}
