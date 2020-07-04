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
        public TransferLog transferlog = new TransferLog();

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
        public ActionResult CreateTransfer(TransferLog transfer)
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

            transferlog.transferTypeId = 2;
            transferlog.transferStatusId = 2;
            transferlog.accountFrom = userID;
            transferlog.accountTo = transferData.AccountIDToIncrease;
            transferlog.amount = transferData.TransferAmount;

            bool transferLogAdded = accountDAO.AddTransfer(transferlog);

            if (transferLogAdded == true)
            {
                return true;
            }
            return false;
        }
    }
}
