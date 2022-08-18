using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Repository.Db
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Entities.CustomerEntity> Customers { get; set; }
        public virtual DbSet<Entities.CustomerAddressEntity> Address { get; set; }
    }


}
