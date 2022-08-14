using Customer.Domain.Models;

namespace Customer.Domain.Repositories;

public interface ICustomerRepository
{
    Task<int> SaveCustomer(CustomerModel customer);

    Task<CustomerModel> GetCustomerById(string customerId);
}