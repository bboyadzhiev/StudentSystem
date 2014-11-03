using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Data.Migrations
{
    public class MigrationsContextFactory : IDbContextFactory<StudentSystemDbContext>
    {   
        public MigrationsContextFactory()
        { 
        }
       
        public StudentSystemDbContext Create()
        {
            return new StudentSystemDbContext(Config.ConnectionString);
        }

    }
}
