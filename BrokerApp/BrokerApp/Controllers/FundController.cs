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
    public class FundController : ControllerBase
    {
        private readonly IFundManager _fundManager;

        public FundController(IFundManager fundManager)
        {
            _fundManager = fundManager;
        }

        [HttpPost]
        [Route("addFunds")]
        public ActionResult<string> AddFunds([FromBody] AddFundsRequest addFundsRequest)
        {
            return _fundManager.Add(addFundsRequest);
        }
    }
}
