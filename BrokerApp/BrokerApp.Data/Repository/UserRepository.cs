using BrokerApp.Data.DatabaseContext;
using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokerApp.Data.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly BrokerDbContext _context;
        public UserRepository(BrokerDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
        {
            return _context.User.FirstOrDefault(u => u.Id == userId);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            _context.User.Update(user);
            _context.SaveChanges();
        }

    }
}
