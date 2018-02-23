using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows;
using ISystem.ItemF;
using ISystem.CustomerF;

namespace ISystem.InvoiceF
{
    class DAInvoice
    {
        /// <summary>
        /// Database connection string
        /// </summary>
        private static string sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
        /// <summary>
        /// Returns list of all invoices
        /// </summary>
        /// <returns>Invoice List</returns>
        public static List<Invoice> ListInvoices()
        {
            try
            {
                List<Invoice> InvoiceList = new List<Invoice>();
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT InvoiceNumber from INVOICE", conn);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            InvoiceList.Add(GetInvoice(reader[0].ToString()));
                        }
                    }
                }
                return InvoiceList;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// creates a new invoice
        /// </summary>
        /// <returns> the new invoice</returns>
        public static Invoice NewInvoice()
        {
            
            //create a new invoice
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        //create a UNK customer
                        Customer tmpC = DACustomer.NewCustomer();
                        OleDbCommand command = new OleDbCommand("INSERT INTO INVOICE(CustomerNumber, InvoiceDate) " +
                                                     "VALUES( ?, ?)", conn);
                        command.Parameters.AddWithValue("CustomerNumber", tmpC.CUSTOMERNUMBER);
                        command.Parameters.AddWithValue("InvoiceDate", DateTime.Today.ToShortDateString());
                        command.ExecuteNonQuery();
                        OleDbDataReader reader = null;
                        command = new OleDbCommand("SELECT * FROM  INVOICE " +
                            "WHERE CustomerNumber = ? ", conn);
                        command.Parameters.AddWithValue("CustomerNumber", tmpC.CUSTOMERNUMBER);

                        reader = command.ExecuteReader();
                        reader.Read();

                        return GetInvoice(reader[0].ToString());
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
        /// deletes invoices with the name UNK when the program starts
        /// </summary>
        internal static void DeleteUNK()
        {
            try
            {

                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        //get all UNK customers
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * FROM  CUSTOMER " +
                            "WHERE FirstName = ? AND LastName = ? ", conn);
                        command.Parameters.AddWithValue("FirstName", "UNK");
                        command.Parameters.AddWithValue("LastName", "UNK");
                        reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            //delete all their items
                            DeleteUNKItems(reader[0].ToString());
                        }


                         command = new OleDbCommand("DELETE * FROM CUSTOMER WHERE FirstName = ? ", conn);
                        command.Parameters.AddWithValue("FirstName", "UNK");
                        command.ExecuteNonQuery();

                       
                    }
                }


            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);

            }
        }

        /// <summary>
        /// Deleted invoice items on UNK customer items
        /// </summary>
        /// <param name="cust"></param>
        private static void DeleteUNKItems(string cust)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        //get invoice number for customer
                        //get all UNK customers
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * FROM  INVOICE " +
                            "WHERE CustomerNumber = ? ", conn);
                        command.Parameters.AddWithValue("CustomerNumber", cust);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            OleDbCommand commanddel = new OleDbCommand("DELETE * FROM INVOICEITEMS WHERE InvoiceNumber = ? ", conn);
                            commanddel.Parameters.AddWithValue("InvoiceNumber", reader[0].ToString());
                            commanddel.ExecuteNonQuery();
                        }

                        command = new OleDbCommand("DELETE * FROM INVOICE WHERE CustomerNumber = ? ", conn);
                        command.Parameters.AddWithValue("CustomerNumber", cust);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                
            }
        }

        /// <summary>
        /// Deletes and invoice
        /// </summary>
        /// <param name="inv">Invoice</param>
        internal static void Delete(Invoice inv)
        {
            try
            {
                
               
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        
                        OleDbCommand command = new OleDbCommand("DELETE * FROM INVOICE WHERE INVOICENUMBER = ? ", conn);
                        command.Parameters.AddWithValue("InvoiceNumber", inv.iInvoiceNumber);
                        command.ExecuteNonQuery();
                        command = new OleDbCommand("DELETE * FROM INVOICEITEMS WHERE INVOICENUMBER = ? ", conn);
                        command.Parameters.AddWithValue("InvoiceNumber", inv.iInvoiceNumber);
                        command.ExecuteNonQuery();
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                
            }

        }

        /// <summary>
        /// Returns a single invoice
        /// </summary>
        /// <param name="invoiceNumber">Invoice Number</param>
        /// <returns>Invoice</returns>
        public static Invoice GetInvoice(string invoiceNumber)
        {
            try
            {
                Invoice tempInvoice = new Invoice();
                tempInvoice.iInvoiceNumber = invoiceNumber;
               
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * from INVOICE " +
                            "WHERE InvoiceNumber = ?", conn);
                        command.Parameters.AddWithValue("InvoiceNumber", invoiceNumber);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            tempInvoice.sCustomerNumber = reader[1].ToString();
                            tempInvoice.sInvoiceDate = Convert.ToDateTime(reader[2]);
                        }
                    }
                }
                tempInvoice.cCustomer = DACustomer.GetCustomer(Convert.ToInt32(tempInvoice.sCustomerNumber));
                tempInvoice.lItemList = GetInvoiceItems(invoiceNumber);
                tempInvoice.calcTotal();
                return tempInvoice;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }
        /// <summary>
        /// Returns list of invoice items
        /// </summary>
        /// <param name="invoiceNumber">Invoice Number</param>
        /// <returns>Item List</returns>
        public static List<Item> GetInvoiceItems(string invoiceNumber)
        {
            try
            {
                List<Item> ItemsList = new List<Item>();
                
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * FROM INVOICEITEMS " +
                            " WHERE InvoiceNumber = ?", conn);
                        command.Parameters.AddWithValue("InvoiceNumber", invoiceNumber);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            ItemsList.Add(new Item(Convert.ToInt32(reader[2]), Convert.ToDouble(reader[3]), Convert.ToInt32(reader[4])));
                        }
                    }
                }
                return ItemsList;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// saves an invoice
        /// </summary>
        /// <param name="invoice">invoice to save</param>
        public static void Save(Invoice invoice)
        {
            try
            {
                

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        OleDbCommand command = new OleDbCommand("UPDATE INVOICE " +
                            "SET InvoiceDate = ?  " +
                            ", CustomerNumber = ?  " +                          
                            "WHERE InvoiceNumber = ?", conn);

                        command.Parameters.AddWithValue("InvoiceDate", invoice.sInvoiceDate.ToShortDateString());
                        command.Parameters.AddWithValue("CustomerNumber", invoice.sCustomerNumber);
                        command.Parameters.AddWithValue("InvoiceNumber", invoice.iInvoiceNumber);

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
