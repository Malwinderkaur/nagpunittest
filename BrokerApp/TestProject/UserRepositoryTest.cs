using BrokerApp.Data.DatabaseContext;
using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BrokerApp.Test
{
    public class UserRepositoryTest
    {
        private readonly BrokerDbContext mockContext;
        private readonly UserRepository userRepository;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<BrokerDbContext>().UseInMemoryDatabase(databaseName: "BrokerDb").Options;
            mockContext = new BrokerDbContext(options);
            mockContext.User.Add(new User {Id=1,Funds = 1 });
            mockContext.SaveChanges();
            userRepository = new UserRepository(mockContext);
        }

        [Fact]
        public void GetUserById_Valid_Test()
        {
            var result = userRepository.GetUserById(1);
            Assert.NotNull(result);
            dispose(mockContext);
        }

        [Fact]
        public void UpdateUser_NullArgument_Test()
        {
            Action throwingAction = () =>
            {
                userRepository.UpdateUser(null);
            };
            Assert.Throws<ArgumentNullException>(throwingAction);
            dispose(mockContext);
        }

        private void dispose(BrokerDbContext mockContext)
        {
            mockContext.Database.EnsureDeleted();
            mockContext.Dispose();
        }
    }
}
