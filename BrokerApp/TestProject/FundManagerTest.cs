using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using BrokerApp.Service.Manager;
using BrokerApp.Shared;
using BrokerApp.Shared.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BrokerApp.Test
{
    public class FundManagerTest
    {
        private FundManager fundManager;
        private readonly IUserRepository mockUserRepository;

        public FundManagerTest()
        {
            mockUserRepository = Substitute.For<IUserRepository>();
            fundManager = new FundManager(mockUserRepository);
        }

        [Fact]
        public void Add_NullReference()
        {
            User user = null;
            mockUserRepository.GetUserById(Arg.Any<int>()).Returns(user);
            Action throwingAction = () =>
            {
                fundManager.Add(new AddFundsRequest());
            };
            Assert.Throws<NullReferenceException>(throwingAction);
        }

        [Fact]
        public void Add_True()
        {
            mockUserRepository.GetUserById(Arg.Any<int>()).Returns(new User());
            var result = fundManager.Add(new AddFundsRequest { Amount=1});
            result.Equals(Constants.Success);
        }
    }
}
