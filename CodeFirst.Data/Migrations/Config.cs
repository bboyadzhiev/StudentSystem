using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Data.Migrations
{
    public static class Config
    {
        public static string ConnectionString { get; private set; }
        public static void SetConnectionString(string catalog, string serverPath){
            ConnectionString = @"Data Source=" + serverPath + @";Initial Catalog=" + catalog + ";Integrated Security=True";
        }
    }
}
