using Core.Interface;
using Demo.Core.Interface.Repositories;
using Demo.Repostitory;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkSqlServerRepository(SqlConnection context,SqlTransaction transaction)
        {
            CustomerRepository = new CustomerRepository(context, transaction);
        }

        public ICustomerRepository CustomerRepository { get; }
    }
}
