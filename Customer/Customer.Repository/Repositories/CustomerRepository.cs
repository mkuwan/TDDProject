using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Models;
using Customer.Domain.Repositories;
using Customer.Repository.Db;
using Customer.Repository.Dto;
using Customer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;
        public CustomerRepository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> SaveCustomer(CustomerModel customer)
        {
            var entity = CustomerDto.FromCustomerModel(customer);

            var dbEntity = await _dbContext.Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefaultAsync();

            if (dbEntity == null)
                _dbContext.Customers.Add(entity);
            else
            {
                dbEntity.CustomerName = customer.CustomerName;
                dbEntity.Email = customer.Email;
            }
            
            return await _dbContext.SaveChangesAsync();
        }

        public Task<CustomerModel> GetCustomerById(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
