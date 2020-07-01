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
        public decimal GetBalance(string userId)
        {
            decimal balance = access.ReturnBalance();

            return balance;
        }
    }
}