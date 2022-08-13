using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.Domain.Repositories;

namespace Customer.Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void CreateCustomer(string name)
        {
            
        }


        // ---- 以下のメソッドは練習用のサンプルとして作ったもので、本来は上のCreateCustomerを都度修正していくことになります ----
        public bool CreateCustomer01(string name)
        {
            return true;
        }

        public bool CreateCustomer02(string name)
        {
            return true;
        }

        public bool CreateCustomer02_modified(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;


            return true;

        }

        public bool CreateCustomer02_modified02(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            char[] forbiddenChars = { '!', '@', '#', '$', '%', '&', '*', '\\' };
            if (name.Any(x => forbiddenChars.Contains(x)))
                return false;

            return true;
        }





    }
}
