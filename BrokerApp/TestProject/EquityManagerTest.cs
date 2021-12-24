using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using BrokerApp.Service.Manager;
using BrokerApp.Shared;
using BrokerApp.Shared.Models;
using BrokerApp.Test;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using Xunit;

namespace TestProject
{
    public class EquityManagerTest
    {
        private EquityManager equityManager;
        private readonly IEquityRepository equityRepository;
        private readonly IUserRepository userRepository;

        public EquityManagerTest()
        {
            equityRepository = Substitute.For<IEquityRepository>();
            userRepository = Substitute.For<IUserRepository>();
        }

        [Fact]
        public void Buy_NullReference()
        {
            Equity equity = null;
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(equity);
            equityManager = new EquityManager(new FixedUtility(true), equityRepository, userRepository);
            Action throwingAction = () =>
            {
                equityManager.Buy(new EquityTransferRequest());
            };
            Assert.Throws<NullReferenceException>(throwingAction);
        }

        [Fact]
        public void Buy_False_OutOfTimeLimit()
        {
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(new Equity());
            userRepository.GetUserById(Arg.Any<int>()).Returns(new User { Id = 1, Funds = 5000 });
            equityManager = new EquityManager(new FixedUtility(false),equityRepository,userRepository);
            var result = equityManager.Buy(new EquityTransferRequest());
            result.Equals(Constants.InvalidTimings);
        }

        [Fact]
        public void Buy_False_LesserUserFunds()
        {
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(new Equity { Id=1});
            userRepository.GetUserById(Arg.Any<int>()).Returns(new User { Id = 1, Funds = 5000 });
            equityManager = new EquityManager(new FixedUtility(true), equityRepository, userRepository);
            var result = equityManager.Buy(new EquityTransferRequest());
            result.Equals(Constants.InvalidAmount);
        }

        [Fact]
        public void Buy_True()
        {
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(new Equity());
            userRepository.GetUserById(Arg.Any<int>()).Returns(new User { Id = 1, Funds = 5000 });
            equityManager = new EquityManager(new FixedUtility(true),equityRepository,userRepository);
            var result = equityManager.Buy(new EquityTransferRequest());
            result.Equals(Constants.Success);
        }

        [Fact]
        public void Sell_NullReference()
        {
            Equity equity = null;
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(equity);
            equityManager = new EquityManager(new FixedUtility(true), equityRepository, userRepository);
            Action throwingAction = () =>
            {
                equityManager.Sell(new EquityTransferRequest());
            };
            Assert.Throws<NullReferenceException>(throwingAction);
        }

        [Fact]
        public void Sell_True()
        {
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(new Equity { HolderId=2});
            userRepository.GetUserById(Arg.Any<int>()).Returns(new User { Id = 2, Funds = 5000 });
            equityManager = new EquityManager(new FixedUtility(true), equityRepository, userRepository);
            var result = equityManager.Sell(new EquityTransferRequest());
            result.Equals(Constants.Success);
        }

        [Fact]
        public void Sell_IncorrectHolder()
        {
            equityRepository.GetEquityById(Arg.Any<int>()).Returns(new Equity {  });
            userRepository.GetUserById(Arg.Any<int>()).Returns(new User { Id = 2, Funds = 5000 });
            equityManager = new EquityManager(new FixedUtility(true), equityRepository, userRepository);
            var result = equityManager.Sell(new EquityTransferRequest());
            result.Equals(Constants.InvalidHolder);
        }
    }
}
