using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Repository.Entities
{
    public class CustomerAddressEntity
    {
        [Key]
        public string CustomerAddressId { get; set; } = Guid.NewGuid().ToString();

        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        /// <summary>
        /// 都道府県
        /// </summary>
        public string? Region { get; set; }  

        /// <summary>
        /// 市区町村
        /// </summary>
        public string? City { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsDeliveryUse { get; set; } = false;

        public string CustomerId { get; set; } = null!;
        public virtual CustomerEntity? Customer { get; set; }

    }
}
