using Mahzan.SqLite.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.SqLite
{
    public class MahzanSqLiteContext :DbContext
    {

        public DbSet<AspNetUsers> AspNetUsers { get; set; }

        private readonly string _databasePath;

        public MahzanSqLiteContext(string databasePath) 
        {
            _databasePath = databasePath;

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) 
        {
            dbContextOptionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
