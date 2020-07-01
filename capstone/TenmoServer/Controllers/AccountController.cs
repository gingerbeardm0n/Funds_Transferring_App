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
        private IUserAccountDAO userAccountDAO;
        UserAccountDAO access = new UserAccountDAO();

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

            decimal balance = access.ReturnBalance(userID);

            return balance;
        }

        [Authorize]
        [HttpPost("transfer")]
        public ActionResult CreateTransfer(Transfers transfer)
        {
            bool result = userAccountDAO.AddTransfer(transfer);

            if (result)
            {
                return Created("", transfer);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}