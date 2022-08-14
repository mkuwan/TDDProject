using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Models;
using Customer.Repository.Entities;

namespace Customer.Repository.Dto
{
    public static class CustomerDto
    {
       public static CustomerEntity FromCustomerModel(CustomerModel model)
       {
            var entity = new CustomerEntity
            {
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                Email = model.Email
            };

            return entity;
       }
    }
}
