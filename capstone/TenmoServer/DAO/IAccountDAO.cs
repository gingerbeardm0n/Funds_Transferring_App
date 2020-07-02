using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        decimal GetMyBalance(int UserID);

        bool AddTransfer(TransferLog transfer);

        bool UpdateMyBalance(TransferData transferData);

        decimal GetUserBalance(TransferData transferData);

        bool UpdateUserBalance(TransferData transferData);
    }
}
