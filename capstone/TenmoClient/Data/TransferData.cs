using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    /// <summary>
    /// Model to provide information about UserID and Amount when sending TE Bucks
    /// </summary>
    class TransferData
    {
        public int UserIDToIncrease { get; set; }
        public decimal AmountToIncrease { get; set; }
    }
}
