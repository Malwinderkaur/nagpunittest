using BrokerApp.Data.DatabaseContext;
using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokerApp.Data.Repository
{
    public class EquityRepository: IEquityRepository
    {
        private readonly BrokerDbContext _context;

        public EquityRepository(BrokerDbContext context)
        {
            _context = context;
        }
        public Equity GetEquityById(int equityId)
        {
            return _context.Equity.FirstOrDefault(e => e.Id == equityId);
        }

        public void UpdateEquity(Equity equity)
        {
            if (equity == null)
            {
                throw new ArgumentNullException();
            }
            _context.Equity.Update(equity);
            _context.SaveChanges();
        }
    }
}
