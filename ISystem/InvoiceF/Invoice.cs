using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISystem.ItemF;
using ISystem.CustomerF;
using System.Windows.Controls;
using System.Windows;

namespace ISystem.InvoiceF
{
    /// <summary>
    /// 
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Invoice Number
        /// </summary>
        public string iInvoiceNumber { get; set; }

        /// <summary>
        /// Invoice Number
        /// </summary>
        public string sCustomerNumber { get; set; }

        /// <summary>
        /// Invoice Date
        /// </summary>
        public DateTime sInvoiceDate { get; set; }

        /// <summary>
        /// Customer for invoice
        /// </summary>
        public Customer cCustomer { get; set; }

        /// <summary>
        /// List of Items
        /// </summary>
        public List<Item> lItemList { get; set; }

        /// <summary>
        /// total of invoice
        /// </summary>
        public double dTotal { get; set; }

        

        /// <summary>
        /// Used to mark an inventory for save
        /// </summary>
        public int iModified { get; set; } = 0; //0- for unmodified  1- delete  2- Add 3- Update

        /// <summary>
        /// Grid item
        /// </summary>
        private Grid myGrid;
        /// <summary>
        /// Datepicker for the invoice
        /// </summary>
        private DatePicker dp;
        /// <summary>
        /// footer displaying total
        /// </summary>
        private Grid footer;
        /// <summary>
        /// Total textbox
        /// </summary>
        private TextBlock tblk_mw_total;

        

        /// <summary>
        /// Creates a empty invoice for data access to use
        /// </summary>
        public Invoice()
        {
           
        }

        /// <summary>
        /// constructor for a invoice user when returning an invoice from database
        /// </summary>
        /// <param name="invnumber">invoice number</param>
        /// <param name="cust">customer</param>
        /// <param name="datetime">data of purchase</param>
        public Invoice(string invnumber, Customer cust, DateTime datetime)
        {
            try
            {
                iInvoiceNumber = invnumber;
                sCustomerNumber = cust.CUSTOMERNUMBER.ToString();
                cCustomer = cust;
                lItemList = null;
                sInvoiceDate = datetime;
                dTotal = 0;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
           
        /// <summary>
        /// adds and item to the invoice adding one to an existing item, or adding a new item
        /// </summary>
        /// <param name="itm">item to add</param>
        public void addItem(Item itm)
        {
            try
            {
                bool contains = false;
                foreach (Item i in lItemList)
                {
                    if (i.iItemNumber == itm.iItemNumber)
                    {
                        contains = true;
                        itm = i;
                    }
                }
                if (contains)
                {
                    DAItem.AddOne(itm, this);
                    itm.iModified = 3;
                }
                else {
                    itm.iNumber = 1;
                    itm.iModified = 2;
                    DAItem.AddNewtoInvoice(itm, this);
                    lItemList.Add(itm);
                }

                myGrid.Children.Clear();
                myGrid.Children.Add(myStack());
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Calculates the total from the items in the list
        /// </summary>
        public void calcTotal()
        {
            try
            {
                dTotal = 0;
                foreach (var item in lItemList)
                {
                    dTotal += item.dCost * item.iNumber;
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Override of the to string method 
        /// </summary>
        /// <returns>new string</returns>
        public override string ToString()
        {
            try
            {
                return "Invoice #: " + iInvoiceNumber + "\n" +
                        "Customer #: " + sCustomerNumber + "\t" +
                        sInvoiceDate.ToString("d") + "\t" +
                        "Total: " + dTotal.ToString("c");
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// Called to enable edits on invoice, customer, and all items
        /// </summary>
        public void EnableEdit()
        {
            try
            {
                //iterates through the list to enable edits on each
                foreach (Item i in lItemList)
                    i.EnableEdits();
                cCustomer.EnableEdits();

                //enables the date field
                dp.Focusable = true;
                dp.IsHitTestVisible = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// updates the total textblock with a new total
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void updatetotal(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Double tots = new double();
                foreach (Item i in lItemList)
                    tots += i.itemTotal();
                tblk_mw_total.Text = "Total =                 " + tots + "         ";
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Invoice Grid used to display Invoice, Invoice Number, and Invoice date
        /// </summary>
        /// <returns></returns>
        public Grid InvoiceGrid()
        {
            try
            {
                Grid InGrid = new Grid();
                InGrid.Height = 100;
                InGrid.Width = 300;
                StackPanel myStack = new StackPanel();

                TextBlock invtext = new TextBlock();
                Thickness m = new Thickness();
                m.Top = -20;
                invtext.Margin = m;
                invtext.Height = 70;
                invtext.FontSize = 64;
                invtext.Text = "Invoice";
                myStack.Children.Add(invtext);

                StackPanel BottomStack = new StackPanel();
                BottomStack.Orientation = Orientation.Horizontal;

                TextBlock invoicelbl = new TextBlock();
                m = new Thickness();
                m.Top = 20;
                invoicelbl.Margin = m;
                invoicelbl.Height = 30;
                invoicelbl.Text = "Invoice Number:   ";
                BottomStack.Children.Add(invoicelbl);


                TextBlock invoiceInfo = new TextBlock();
                m = new Thickness();
                m.Top = 20;
                invoiceInfo.Margin = m;
                invoiceInfo.Height = 30;
                invoiceInfo.Text = iInvoiceNumber + "         ";
                BottomStack.Children.Add(invoiceInfo);

                dp = new DatePicker();
                m = new Thickness();
                m.Top = 10;
                dp.Margin = m;
                dp.Height = 23;
                dp.SelectedDate = sInvoiceDate;
                dp.IsHitTestVisible = false;
                dp.Focusable = false;
                BottomStack.Children.Add(dp);

                myStack.Children.Add(BottomStack);
                InGrid.Children.Add(myStack);
                return InGrid;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// validates the customers zip and the all items cost fields dealing with saving 
        /// </summary>
        /// <returns></returns>
        public bool validates()
        {
            try
            {
                if (!cCustomer.validates)
                {
                    return false;
                }

                foreach (Item i in lItemList)
                {
                    if (!i.validates)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return false;
            }
        }

        /// <summary>
        /// Saves the Invoice updates cost incase cost of items has changed
        /// </summary>
        internal void Save()
        {
            try
            {
                //Update Invoice
                sInvoiceDate = DateTime.Parse(dp.Text);
                DAInvoice.Save(this);
                //Update invoiceItems cost
                foreach (Item i in lItemList)
                    i.UpdateCost(int.Parse(iInvoiceNumber));

                //Update Customer
                cCustomer.Save();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Disables edits on invoice, customer, and items
        /// </summary>
        public void DisableEdit()
        {
            try
            {
                foreach (Item i in lItemList)
                    i.DisableEdits();
                cCustomer.DisableEdits();
                dp.Focusable = false;
                dp.IsHitTestVisible = false;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Returns the grid for the layout of the items and total
        /// </summary>
        /// <returns></returns>
        public Grid ItemGrid()
        {
            try
            {
                myGrid = new Grid();
                myGrid.Children.Add(myStack());
                return myGrid;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// Stacks each item and add the footer
        /// </summary>
        /// <returns></returns>
        private StackPanel myStack()
        {
            try
            {
                StackPanel mystack = new StackPanel();
                foreach (Item i in lItemList)
                    mystack.Children.Add(i.InvoiceItem(int.Parse(iInvoiceNumber), this));
                //add footer

                footer = new Grid();
                footer.HorizontalAlignment = HorizontalAlignment.Right;

                tblk_mw_total = new TextBlock();
                Double tots = new double();
                // add the total for each item together
                foreach (Item i in lItemList)
                    tots += i.itemTotal();

                tblk_mw_total.Text = "Total =                 " + tots.ToString("c") + "         ";

                footer.Children.Add(tblk_mw_total);
                mystack.Children.Add(footer);

                return mystack;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// Deletes the selected item from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteSelectedItem(object sender, RoutedEventArgs e)
        {
            try
            {
                Item del = null;
                // searches for the item with the button containing the tag with the item number and removes it from the database
                foreach (Item i in lItemList)
                {
                    if (i.iItemNumber == (int)((Button)sender).Tag)
                    {
                        del = i;
                        DAItem.RemovefromInvoice(del, this);
                    }
                }
                // Removes the item from the item list
                lItemList.Remove(del);
                //rebuilds the itemstack
                myGrid.Children.Clear();
                myGrid.Children.Add(myStack());
                //enables editing
                EnableEdit();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
    }
}
