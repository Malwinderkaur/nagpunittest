﻿using BrokerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Service.IManager
{
    public interface IFundManager
    {
        string Add(AddFundsRequest addFundsRequest);
    }
}
