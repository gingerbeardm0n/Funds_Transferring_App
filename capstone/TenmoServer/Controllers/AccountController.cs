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

        public AccountController(IUserDAO userDAO, IAccountDAO accountDAO)
        {
            this.userDAO = userDAO;
            this.accountDAO = accountDAO;
        }


        [Authorize]
        [HttpGet("balance")]
        public decimal GetBalance()
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

            decimal balance = accountDAO.GetBalance(userID);

            return balance;
        }

        [Authorize]
        [HttpPut("updateBalance")]
        public bool UpdateBalance(decimal newBalance)
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

            accountDAO.UpdateBalance(userID, newBalance);

            return true;
        }


        [Authorize]
        [HttpPost("transfer")]
        public ActionResult CreateTransfer(Transfer transfer)
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
        [HttpGet("get_users")]
        public List<User> GetUsers()
        {
            List<User> users = userDAO.GetUsers();


            return users;
        }



    }
}