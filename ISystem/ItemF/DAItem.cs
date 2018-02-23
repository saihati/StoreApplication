using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using ISystem.ItemF;
using System.Windows;
using System.Windows.Controls;
using ISystem.InvoiceF;

namespace ISystem.ItemF
{
    class DAItem
    {
        /// <summary>
        /// Item number property
        /// </summary>
        public int iItemNumber { get; set; }
        /// <summary>
        /// Item name property
        /// </summary>
        public string sItemName { get; set; }
        /// <summary>
        /// Item cost property
        /// </summary>
        public double dCost { get; set; }
        /// <summary>
        /// Item amount property
        /// </summary>
        public int iNumber { get; set; } = 1;
        /// <summary>
        /// Item InvoiceNumber property
        /// </summary>
        public int iInvoiceNumber { get; set; }
        /// <summary>
        /// Item Description property
        /// </summary>
        public string sDescription { get; set; }
        /// <summary>
        /// Connection string for database
        /// </summary>
        private static string sConnectionString;


        /// <summary>
        /// Returns a list of all items
        /// </summary>
        /// <returns></returns>
        public static List<Item> GetList()
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                List<Item> ItemList = new List<Item>();
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * from ITEMS", conn);

                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Item temp = new Item(Convert.ToInt32(reader[0]),reader[1].ToString(), reader[2].ToString() ,Convert.ToDouble(reader[3]));
                            ItemList.Add(temp);
                        }
                        
                    }
                }
                return ItemList;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }

        }
        /// <summary>
        /// Add item to invoice
        /// </summary>
        /// <param name="itm">Item to add</param>
        /// <param name="invoice">Invoice to add to</param>
        internal static void AddNewtoInvoice(Item itm, Invoice invoice)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        
                        OleDbCommand command = new OleDbCommand("INSERT INTO INVOICEITEMS(InvoiceNumber, ItemNumber, Cost, NumberofItems) " +
                                                     "VALUES( ?, ? , ?,?)", conn);
                        command.Parameters.AddWithValue("InvoiceNumber", invoice.iInvoiceNumber);
                        command.Parameters.AddWithValue("ItemNumber", itm.iItemNumber);
                        command.Parameters.AddWithValue("Cost", itm.dCost);
                        command.Parameters.AddWithValue("NumberofItems", itm.iNumber);
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
        /// Removes item from invoice
        /// </summary>
        /// <param name="del">Item to delete</param>
        /// <param name="invoice">Invoice to delete from</param>
        public static void RemovefromInvoice(Item del, Invoice invoice)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();

                        OleDbCommand command = new OleDbCommand("DELETE FROM INVOICEITEMS WHERE InvoiceNumber = ? AND ItemNumber = ?", conn);
                        command.Parameters.AddWithValue("InvoiceNumber", invoice.iInvoiceNumber);
                        command.Parameters.AddWithValue("ItemNumber", del.iItemNumber);
                       
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
        /// Adding duplicate item to invoice
        /// </summary>
        /// <param name="itm">Item to add</param>
        /// <param name="inv">Invoice to add to</param>
        public static void AddOne(Item itm, Invoice inv)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                int ret;
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * " +
                            "FROM INVOICEITEMS " +
                            "WHERE InvoiceNumber = ? " +
                            "AND ItemNumber = ? ", conn);

                        command.Parameters.AddWithValue("InvoiceNumber", inv.iInvoiceNumber);
                        command.Parameters.AddWithValue("ItemNumber", itm.iItemNumber);

                        reader = command.ExecuteReader();
                        reader.Read();
                        ret = int.Parse(reader[4].ToString());

                        reader = null;
                        command = new OleDbCommand("UPDATE INVOICEITEMS "+
                            "SET NumberofItems = ? " +
                            "WHERE InvoiceNumber = ? " +
                            "AND ItemNumber = ?", conn);

                        command.Parameters.AddWithValue("NumberofItems", ++ret);
                        command.Parameters.AddWithValue("InvoiceNumber", inv.iInvoiceNumber);
                        command.Parameters.AddWithValue("ItemNumber",itm.iItemNumber);

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
        /// Updates cost of the item
        /// </summary>
        /// <param name="itm">Item to update</param>
        /// <param name="inv">Invoice number</param>
        internal static void UpdateItemCost(Item itm, int inv)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        
                        OleDbCommand command = new OleDbCommand("UPDATE INVOICEITEMS " +
                            "SET COST = ? " +
                            "WHERE InvoiceNumber = ? " +
                            "AND ItemNumber = ?", conn);

                        command.Parameters.AddWithValue("Cost", itm.dCost);
                        command.Parameters.AddWithValue("InvoiceNumber", inv);
                        command.Parameters.AddWithValue("ItemNumber", itm.iItemNumber);

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
        /// Returns a single item based on item number
        /// </summary>
        /// <param name="itemNum">Item Number</param>
        /// <returns>Item</returns>
        public static Item GetItem(int itemNumber)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                Item tempItem = new Item();
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * FROM ITEMS" +
                            " WHERE ItemNumber = ?", conn);
                        command.Parameters.AddWithValue("ItemNumber", itemNumber);
                        reader = command.ExecuteReader();
                        reader.Read();
                        tempItem.iNumber = itemNumber;
                        tempItem.sItemName = reader[1].ToString();
                        tempItem.sDescription = reader[2].ToString();
                        tempItem.dCost = Convert.ToDouble(reader[3]);
                    }
                }
                return tempItem;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }
        /// <summary>
        /// Gets the amount of the same items on an invoice
        /// </summary>
        /// <param name="iInvoiceNumber"></param>
        /// <param name="iItemNumber"></param>
        /// <returns></returns>
        public static int getiNumber(int iInvoiceNumber, int iItemNumber)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";
                int ret;
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();
                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * " +
                            "FROM INVOICEITEMS " +
                            "WHERE InvoiceNumber = ? " +
                            "AND ItemNumber = ? ", conn);

                        command.Parameters.AddWithValue("InvoiceNumber", iInvoiceNumber);
                        command.Parameters.AddWithValue("ItemNumber", iItemNumber);

                        reader = command.ExecuteReader();
                        reader.Read();
                        ret = int.Parse(reader[4].ToString());
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return 0;
            }
        }

        /// <summary>
        /// Updates item name in the database
        /// </summary>
        /// <param name="itemNumber">Item number</param>
        /// <param name="itemName">New name for item</param>
        public static void UpdateName(int itemNumber, string itemName)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        OleDbCommand command = new OleDbCommand("UPDATE ITEMS " +
                            "SET ItemName = ?  " +
                            "WHERE ItemNumber = ?", conn);

                        command.Parameters.AddWithValue("ItemName", itemName);
                        command.Parameters.AddWithValue("ItemNumber", itemNumber);

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
        /// Updates item cost
        /// </summary>
        /// <param name="itemNumber">Item Number</param>
        /// <param name="itemCost">New Item Cost</param>
        public static void UpdateCost(int itemNumber, double itemCost)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        OleDbCommand command = new OleDbCommand("UPDATE ITEMS " +
                            "SET Cost = ?  " +
                            "WHERE ItemNumber = ?", conn);

                        command.Parameters.AddWithValue("Cost", itemCost);
                        command.Parameters.AddWithValue("ItemNumber", itemNumber);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        public static void UpdateAmount(int iInvoiceNumber, int iItemNumber, int iNumber)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        OleDbCommand command = new OleDbCommand("UPDATE INVOICEITEMS " +
                            "SET NumberofItems = ?  " +
                            "WHERE InvoiceNumber = ? " +
                            "AND ItemNumber = ?", conn);

                        command.Parameters.AddWithValue("NumberofItems", iNumber);
                        command.Parameters.AddWithValue("InvoiceNumber", iInvoiceNumber);
                        command.Parameters.AddWithValue("ItemNumber", iItemNumber);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        public static void AddItem (string itemName, string itemDescription, Double Cost )
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();
                        OleDbCommand command = new OleDbCommand("INSERT INTO ITEMS ( ItemName, ItemDescription, Cost) VALUES( ?, ?, ?)", conn);

                        command.Parameters.AddWithValue("ItemName", itemName);
                        command.Parameters.AddWithValue("ItemDescription", itemDescription);
                        command.Parameters.AddWithValue("Cost", Cost);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {

                Exceptions.Spool(ex);
            }
        }

        public static void editItem(string itemName, string itemDescription, Double Cost, int itemNumber)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                      //  conn.Open();
                        OleDbCommand command = new OleDbCommand("UPDATE ITEMS SET ItemName= ? , ItemDescription= ? , Cost= ? WHERE ItemNumber= ?", conn);
                            conn.Open();

                        command.Parameters.AddWithValue("ItemName", itemName);
                        command.Parameters.AddWithValue("ItemDescription", itemDescription);
                        command.Parameters.AddWithValue("Cost", Cost);
                        command.Parameters.AddWithValue("ItemNumber", itemNumber);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Item List updated Successfully");
                    }
                }

            }
            catch (Exception ex)
            {

                Exceptions.Spool(ex);
            }
        }

        public static bool deleteItem(string itemName, string itemDescription, Double Cost, int itemNumber)
        {
            try
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ISystem.mdb";

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        conn.Open();

                        OleDbDataReader reader = null;
                        OleDbCommand command = new OleDbCommand("SELECT * FROM  INVOICEITEMS " +
                            "WHERE ItemNumber = ? ", conn);
                        command.Parameters.AddWithValue("ItemNumber", itemNumber);
                        reader = command.ExecuteReader();
                        int count = Convert.ToInt32(reader.Read());
                        if (count == 0)
                        {
                            command = new OleDbCommand("DELETE * FROM ITEMS WHERE ItemNumber = ? ", conn);
                            command.Parameters.AddWithValue("ItemNumber", itemNumber);
                            command.ExecuteNonQuery();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return false;
            }
        }


    }

    }
    // Done - Return a single item (itemNumber)
    // Done - Edit item (itemNumber)
        // Done - UpdateName
        // Done - UpdateCost
    // Add item
    // Delete item




