using BrokerApp.Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Data.IRepository
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        void UpdateUser(User user);
    }
}
