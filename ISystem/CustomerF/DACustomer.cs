using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.Windows;

namespace ISystem.CustomerF
{
    class DACustomer
    {
        /// <summary>
        /// connection string
        /// </summary>
        private static string sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

        /// <summary>
        /// Gets a customer from customer number
        /// </summary>
        /// <param name="customernumber">customer number to return</param>
        /// <returns></returns>
        internal static Customer GetCustomer(int customernumber)
        {
            //Access Database and get customer info
            try
            {
                
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                       
                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * from  CUSTOMER " +
                            "WHERE CustomerNumber = ?", conn);

                        command.Parameters.AddWithValue("CustomerNumber", customernumber);
                        reader = command.ExecuteReader();
                        reader.Read();


                        return new Customer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }


        }

        /// <summary>
        /// Creates a new UNK customer
        /// </summary>
        /// <returns></returns>
        public static Customer NewCustomer()
        {
            try
            {
              
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        
                        OleDbCommand command = new OleDbCommand("INSERT INTO CUSTOMER(FirstName, LastName, StreetAddress, City, State, ZIP) " +
                                                     "VALUES( 'UNK', 'UNK','UNK','UNK','UT','0')", conn);
                        command.ExecuteNonQuery();
                        OleDbDataReader reader = null;
                        command = new OleDbCommand("SELECT * FROM  CUSTOMER " +
                            "WHERE FirstName = ? AND LastName = ? ", conn);
                        command.Parameters.AddWithValue("FirstName", "UNK");
                        command.Parameters.AddWithValue("LastName", "UNK");
                        reader = command.ExecuteReader();
                        reader.Read();
                        return GetCustomer(int.Parse(reader[0].ToString()));
                    }
                }

            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// Save a customer to teh database
        /// </summary>
        /// <param name="cu">customer to save</param>
        public static void Save(Customer cu)
        {
            try
            {

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        OleDbCommand command = new OleDbCommand("UPDATE CUSTOMER " +
                            "SET FirstName = ?  " +
                            ", LastName = ?  " +
                            ", StreetAddress = ?  " +
                            ", City = ?  " +
                            ", State = ?  " +
                            ", ZIP = ?  " +
                            "WHERE CustomerNumber = ?", conn);

                        command.Parameters.AddWithValue("FirstName", cu.FIRSTNAME);
                        command.Parameters.AddWithValue("LastName", cu.LASTNAME);
                        command.Parameters.AddWithValue("StreetAddress", cu.STREETADDRESS);
                        command.Parameters.AddWithValue("City", cu.CITY);
                        command.Parameters.AddWithValue("State", cu.STATE);
                        command.Parameters.AddWithValue("ZIP", cu.ZIP);
                        command.Parameters.AddWithValue("CustomerNumber", cu.CUSTOMERNUMBER);

                        command.ExecuteNonQuery();

                        
                    }
                }

            }
            catch (Exception ex)
            {

                Exceptions.Spool(ex);
            }
        }
    }
}
