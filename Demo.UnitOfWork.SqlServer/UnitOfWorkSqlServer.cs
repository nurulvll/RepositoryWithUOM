using Core.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServer : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWorkSqlServer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IUnitOfWorkAdapter Create()
        {

            var connectionString = _configuration.GetConnectionString("DBContext");
            return new UnitOfWorkSqlServerAdapter(connectionString);
            
        }
    }
}
