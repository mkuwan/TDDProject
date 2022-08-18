using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models
{
    public class CustomerAddressModel
    {
        public string CustomerAddressId { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? Region { get; set; }

        public string? City { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }

        public string? Phone { get; set; }
        public bool IsDeliveryUse { get; set; }

        public CustomerAddressModel(string? customerAddressId, string? postalCode, string? country, string? region, string? city, string? addressLine1, string? addressLine2, string? addressLine3, string? phone, bool isDeliveryUse)
        {
            CustomerAddressId = customerAddressId ?? Guid.NewGuid().ToString();

            PostalCode = postalCode;
            Country = country;
            Region = region;
            City = city;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            Phone = phone;
            IsDeliveryUse = isDeliveryUse;
        }
    }
}
