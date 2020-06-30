using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        [HttpGet]
        public decimal GetBalance()
        {

            return 0;
        }
    }
}