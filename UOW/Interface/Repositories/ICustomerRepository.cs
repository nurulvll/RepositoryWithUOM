using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Interface.Repositories
{
    public interface ICustomerRepository
    {
        Customer SaveCustomer(Customer cus);
    }
}
