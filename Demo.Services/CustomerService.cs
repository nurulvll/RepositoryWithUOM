using Core.Interface;
using Demo.Core.Interface.Services;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveCutomer(Customer cus)
        {

            var context = _unitOfWork.Create();

            var customer = context.Repositories.CustomerRepository.SaveCustomer(cus);

            context.SaveChanges();

        }



    }
}
