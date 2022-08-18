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

        /// <summary>
        /// Customerを保存する
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 顧客取得
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<CustomerModel> GetCustomerById(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (customer == null)
                throw new ArgumentNullException(nameof(customerId));

            return CustomerDto.FromCustomerEntity(customer);
        }

        /// <summary>
        /// 顧客の削除
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task Delete(string customerId)
        {
            var entity = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (entity != null)
            {
                _dbContext.Customers.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 住所の保存
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task SaveAddress(string customerId, CustomerAddressModel address)
        {
            var customerEntity = await _dbContext.Customers.FirstAsync(x => x.CustomerId == customerId);
            var addressEntity = CustomerAddressDto.FromModel(address);

            addressEntity.CustomerId = customerEntity.CustomerId;
            _dbContext.Address.Add(addressEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
