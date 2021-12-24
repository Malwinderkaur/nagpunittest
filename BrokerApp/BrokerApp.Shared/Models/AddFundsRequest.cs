using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Shared.Models
{
    public class AddFundsRequest
    {
        public int UserId { get; set; }
        public int Amount { get; set; }
    }
}
