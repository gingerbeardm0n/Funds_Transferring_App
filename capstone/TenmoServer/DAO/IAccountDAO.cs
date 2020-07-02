﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        decimal GetBalance(int UserID);

        bool AddTransfer(Transfer transfer);

        bool UpdateBalance(int accountID, decimal balanceIncrease);
    }
}
