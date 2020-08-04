using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youyan_DataService.EF
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MySql")
        {
            
        }

        public DbSet<Model.device_state> Device_States { get; set; }
        public DbSet<Model.working_duration> Working_Durations { get; set; }
    }

}
