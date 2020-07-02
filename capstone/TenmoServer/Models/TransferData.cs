using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    /// <summary>
    /// Model to ACCEPT information about UserID and Amount when RECEIVING TE Bucks
    /// </summary>
    public class TransferData
    {
        public int UserIDToIncrease { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
