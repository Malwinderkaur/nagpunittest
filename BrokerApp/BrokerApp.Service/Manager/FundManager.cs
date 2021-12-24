using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using BrokerApp.Service.IManager;
using BrokerApp.Shared;
using BrokerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Service.Manager
{
    public class FundManager: IFundManager
    {
        private readonly IUserRepository _userRepository;
        public FundManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Add(AddFundsRequest addFundsRequest)
        {
            if (addFundsRequest.Amount < 0)
            {
                return Constants.InvalidAmount;
            }
            var user = _userRepository.GetUserById(addFundsRequest.UserId);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            if (addFundsRequest.Amount > 100000)
            {
                addFundsRequest.Amount = addFundsRequest.Amount - (int)Math.Round((0.05 * 100) / addFundsRequest.Amount);
            }
            user.Funds = user.Funds + addFundsRequest.Amount;
            _userRepository.UpdateUser(user);
            return Constants.Success;
        }
    }
}
