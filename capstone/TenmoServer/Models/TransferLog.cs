using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class TransferLog
    {
        public int transferId { get; }
        public int transferTypeId { get; set; }
        public int transferStatusId { get; set; }
        public int accountFrom { get; set; }
        public int accountTo { get; set; }
        public decimal amount { get; set; }
    }
}
