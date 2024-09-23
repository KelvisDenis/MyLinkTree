using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTree.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkTree.Infrastruture.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op):base(op){

        }

        public DbSet<LinkModel> LinkSet { get; set; }
        public DbSet<UserModel> UserSet { get; set; }   
    }
}