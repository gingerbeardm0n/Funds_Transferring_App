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

        bool UpdateMyBalance(TransferData transferData, int userID);

        decimal GetUserBalance(TransferData transferData);

        bool UpdateUserBalance(TransferData transferData);

        bool AddTransfer(TransferLog transfer);
    }
}
