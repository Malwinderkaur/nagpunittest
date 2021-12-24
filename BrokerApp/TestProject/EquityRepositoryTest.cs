using BrokerApp.Data.DatabaseContext;
using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using BrokerApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BrokerApp.Test
{
    public class EquityRepositoryTest
    {
        private readonly BrokerDbContext mockContext;
        private readonly IEquityRepository equityRepository;


        public EquityRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<BrokerDbContext>().UseInMemoryDatabase(databaseName: "BrokerDb").Options;
            mockContext = new BrokerDbContext(options);
            mockContext.Equity.Add(new Equity { Price = 1 });
            mockContext.SaveChanges();
            equityRepository = new EquityRepository(mockContext);
        }

        [Fact]
        public void GetEquityById_Valid_Test()
        {
            var result = equityRepository.GetEquityById(1);
            Assert.NotNull(result);
            mockContext.Equity.Remove(result);
            dispose(mockContext);
        }

        [Fact]
        public void UpdateEquity_NullArgument_Test()
        {
            Action throwingAction = () =>
            {
                equityRepository.UpdateEquity(null);
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
