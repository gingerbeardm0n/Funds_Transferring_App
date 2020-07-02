using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        decimal GetBalance(int UserID);

        bool AddTransfer(TransferLog transfer);

        bool UpdateBalance(TransferData transferData);
    }
}
