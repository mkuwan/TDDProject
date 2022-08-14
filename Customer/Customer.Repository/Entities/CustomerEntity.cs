using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Repository.Entities
{
    public sealed class CustomerEntity
    {
        public CustomerEntity()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
        }

        [Key]
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<CustomerAddress> CustomerAddresses { get; set; }

    }
}
