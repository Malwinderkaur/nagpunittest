using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrokerApp.Data.DatabaseModels
{
    public class Equity
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public int? HolderId { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
