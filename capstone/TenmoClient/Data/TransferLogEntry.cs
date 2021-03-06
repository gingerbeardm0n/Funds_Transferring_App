﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class TransferLogEntry
    {
        public int TransferId { get; set; }
        public int TransferTypeId { get; set; }
        public int TransferStatusId { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public string UserNameTo { get; set; }
        public string UserNameFrom { get; set; }
    }
}
