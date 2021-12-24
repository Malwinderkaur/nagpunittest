using BrokerApp.Service.IManager;
using BrokerApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquityController : ControllerBase
    {
        private readonly IEquityManager _equityManager;

        public EquityController(IEquityManager equityManager)
        {
            _equityManager = equityManager;
        }

        [HttpPost]
        [Route("buy")]
        public ActionResult<string> BuyEntity([FromBody] EquityTransferRequest equityTransferRequest)
        {
            return _equityManager.Buy(equityTransferRequest);
        }

        [HttpPost]
        [Route("sell")]
        public ActionResult<string> SellEntity([FromBody] EquityTransferRequest equityTransferRequest)
        {
            return _equityManager.Sell(equityTransferRequest);
        }
    }
}
