using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    /// <summary>
    /// Model to provide information about UserID and Amount when sending TE Bucks
    /// </summary>
    public class TransferData
    {
        public int AccountIDToIncrease { get; set; }
        public decimal TransferAmount { get; set; }
       
    }
}
