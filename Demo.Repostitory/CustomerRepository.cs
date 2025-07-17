using Demo.Core.Interface.Repositories;
using Demo.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repostitory
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        public CustomerRepository(SqlConnection context, SqlTransaction transaction)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public Customer SaveCustomer(Customer cust)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                var command = CreateCommand(@"INSERT INTO Items(

 Name
) VALUES (

 @Name

)");



                command.Parameters.Add("@Name", SqlDbType.NChar).Value = cust.Name;
              

                cust.Id = Convert.ToInt32(command.ExecuteScalar());

                return cust;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Customer Update(Customer cust)
        {
            try
            {
                string sqlText = "";
                int rowcount = 0;

                string query = @"update Student set 

     Name=@Name                   
    ,Address=@Address
    where  Id= @Id"
;

                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@Id", SqlDbType.BigInt).Value = cust.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cust.Name;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = cust.Address;

                rowcount = command.ExecuteNonQuery();


                if (rowcount <= 0)
                {
                }

                return cust;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer GetAll(int Id)
        {
            string sqlText = "";
            List<Customer> VMs = new List<Customer>();
            DataTable dt = new DataTable();

            try
            {
                sqlText = @"


                 Select
                 Id
                ,Name
                ,Address
               
                from Student 
                 where 1=1";

                if (Id != 0)
                {
                    sqlText += @" And Id=@Id";
                }

                SqlDataAdapter objComm = CreateAdapter(sqlText);

                if (Id != 0)
                {
                    objComm.SelectCommand.Parameters.AddWithValue("@Id", Id);
                }
                objComm.Fill(dt);

                //var req = new BankEntryMaster();
                //VMs.Add(req);
                //VMs = dt.ToList<BankEntryMaster>();

                return new Customer();


            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
