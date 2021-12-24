using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Shared.Models
{
    public class EquityTransferRequest
    {
        public int UserId { get; set; }
        public int EquityId { get; set; }
    }
}
