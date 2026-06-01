using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeManager.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using var db = new TimeManagerDbContext();
            db.Database.EnsureCreated();
        }
    }
}
