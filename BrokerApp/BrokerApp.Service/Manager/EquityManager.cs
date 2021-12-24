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
    public class EquityManager: IEquityManager
    {
        private readonly IUtility _utility;
        private readonly IEquityRepository _equityRepository;
        private readonly IUserRepository _userRepository;
        public EquityManager(IUtility utility,IEquityRepository equityRepository,IUserRepository userRepository)
        {
            _utility = utility;
            this._equityRepository = equityRepository;
            this._userRepository = userRepository;
        }

        public string Buy(EquityTransferRequest equityTransferRequest)
        {
            var user = _userRepository.GetUserById(equityTransferRequest.UserId);
            var equity = _equityRepository.GetEquityById(equityTransferRequest.EquityId);
            if(user==null || equity == null)
            {
                throw new NullReferenceException();
            }
            if (equity.HolderId == equityTransferRequest.UserId)
            {
                return Constants.AlreadySold;
            }
            if (user.Funds < equity.Price)
            {
                return Constants.InvalidAmount;
            }
            if (!_utility.CheckIfWithinTimeLimits())
            {
                return Constants.InvalidTimings;
            }
            equity.HolderId = equityTransferRequest.UserId;
            user.Funds = user.Funds - equity.Price;
            _equityRepository.UpdateEquity(equity);
            _userRepository.UpdateUser(user);
            return Constants.Success;
        }

        public string Sell(EquityTransferRequest equityTransferRequest)
        {
            var equity = _equityRepository.GetEquityById(equityTransferRequest.EquityId);
            var user = _userRepository.GetUserById(equityTransferRequest.UserId);
            if (equity == null || user == null)
            {
                throw new NullReferenceException();
            }
            if (equityTransferRequest.UserId != equity.HolderId)
            {
                return Constants.InvalidHolder;
            }
            if (!_utility.CheckIfWithinTimeLimits())
            {
                return Constants.InvalidTimings;
            }
            var brokerage = calculateBrokerage(equity.Price);
            var updatedFunds = equity.Price + user.Funds - brokerage;
            if (updatedFunds < 0)
            {
                return Constants.InvalidAmount;
            }
            user.Funds = updatedFunds;
            equity.HolderId = null;
            _equityRepository.UpdateEquity(equity);
            _userRepository.UpdateUser(user);
            return Constants.Success;
        }

        private int calculateBrokerage(int equityValue)
        {
            var brokerage = (int)Math.Round((0.05 / 100) * equityValue);
            if (brokerage < 20)
            {
                return 20;
            }
            return brokerage;
        }
    }
}
