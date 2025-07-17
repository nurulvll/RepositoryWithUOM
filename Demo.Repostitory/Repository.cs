using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repostitory
{
    public abstract class Repository
    {

        protected SqlConnection _context;
        protected SqlTransaction _transaction;
        protected SqlCommand CreateCommand(string query)
        {
            return new SqlCommand(query, _context, _transaction);
        }
        protected SqlDataAdapter CreateAdapter(string query)
        {
            var cmd = new SqlCommand(query, _context, _transaction);
            return new SqlDataAdapter(cmd);
        }
        protected SqlDataAdapter CreateAdapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }


    }
}
