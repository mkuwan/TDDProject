using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Models;
using Customer.Repository.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Customer.Repository.Dto
{
    internal static class CustomerAddressDto
    {
        public static CustomerAddressEntity FromModel(CustomerAddressModel model)
        {
            var entity = new CustomerAddressEntity()
            {
                CustomerAddressId = model.CustomerAddressId,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Region = model.Region,
                City = model.City,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                AddressLine3 = model.AddressLine3,
                Phone = model.Phone,
                IsDeliveryUse = model.IsDeliveryUse,
            };

            return entity;
        }

        public static CustomerAddressModel FromEntity(CustomerAddressEntity entity)
        {
            return new CustomerAddressModel(entity.CustomerAddressId, entity.PostalCode,
                entity.Country, entity.Region, entity.City,
                entity.AddressLine1, entity.AddressLine2, entity.AddressLine3,
                entity.Phone, entity.IsDeliveryUse);
        }
    }
}
