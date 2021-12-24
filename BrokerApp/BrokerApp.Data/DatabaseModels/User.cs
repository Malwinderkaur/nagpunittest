using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrokerApp.Data.DatabaseModels
{
    public class User
    {
        public User()
        {
            Equity = new HashSet<Equity>();
        }
        [Key]
        public int Id { get; set; }
        public int Funds { get; set; }
        public virtual ICollection<Equity> Equity { get; set; }

    }
}
