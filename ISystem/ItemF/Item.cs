using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ISystem.InvoiceF;
using System.Globalization;

namespace ISystem.ItemF
{
    public class Item
    {
        /// <summary>
        /// Item Number
        /// </summary>
        public int iItemNumber { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string sItemName { get; set; }

        /// <summary>
        /// Item Cost
        /// </summary>
        public double dCost { get; set; }

        /// <summary>
        /// Number of items
        /// </summary>
        public int iNumber { get; set; } = 1;

        /// <summary>
        /// invoice number of item
        /// </summary>
        public int iInvoiceNumber { get; set; }

        /// <summary>
        /// item desctiption
        /// </summary>
        public string sDescription { get; set; }

        /// <summary>
        /// Bool if data balidates
        /// </summary>
        public bool validates { get; set; } = false;

        /// <summary>
        /// Used to mark an item for removal from inventory
        /// </summary>
        public int iModified { get; set; } = 0; //0- for unmodified  1- delete  2- Add 3- Update



        /// <summary>
        /// combobox that holds number of items
        /// </summary>
        private ComboBox cboItem;

        /// <summary>
        /// text block of Item number
        /// </summary>
        private TextBlock tblkItem;

        /// <summary>
        /// text block of Items cost
        /// </summary>
        private TextBox tboxCost;

        /// <summary>
        ///text block of  Items total cost
        /// </summary>
        private TextBlock tblkTotal;

        /// <summary>
        /// text block of item name
        /// </summary>
        private TextBlock tblkName;

        /// <summary>
        /// Delete button to delete item
        /// </summary>
        private Button btnDelete;


        /// <summary>
        /// Creates a new item without seting number of items
        /// </summary>
        /// <param name="itemnumber">item number</param>
        /// <param name="itemname">item name</param>
        /// <param name="description"> item description</param>
        /// <param name="cost">item Cost</param>
        public Item(int itemnumber, string itemname, string description, double cost)
        {
            try
            {
                iItemNumber = itemnumber;
                sItemName = itemname;
                sDescription = description;
                dCost = cost;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        
        /// <summary>
        /// Empty constructor used in creating items from database
        /// </summary>
        public Item()
        {

        }


        /// <summary>
        /// Constructor for creating an item in an inventory list, this will include the cost paid rather than
        /// the cost of the item, as well as the number of items purchased
        /// </summary>
        public Item(int itemnumber, double cost, int numberof)
        {
            try
            {
                iItemNumber = itemnumber;
                dCost = cost;
                iNumber = numberof;
                //get info of item
                Item tempI = DAItem.GetItem(iItemNumber);
                sItemName = tempI.sItemName;
                sDescription = tempI.sDescription;
            }
            catch (Exception ex)
            {

                Exceptions.Spool(ex);
            }

        }

        /// <summary>
        /// Gets the total cost for the item
        /// </summary>
        /// <returns>total cost</returns>
        public double itemTotal()
        {
            try
            {
                return dCost * iNumber;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return 0.0;
            }
        }

        /// <summary>
        /// Creates the grid for each item
        /// </summary>
        /// <param name="iInvoiceNumber"> invoice number</param>
        /// <param name="inv">invoice</param>
        /// <returns></returns>
        public Grid InvoiceItem(int iInvoiceNumber, Invoice inv)
        {
            try
            {

                //get the iNumber from the database
                iNumber = DAItem.getiNumber(iInvoiceNumber, iItemNumber);

                this.iInvoiceNumber = iInvoiceNumber;


                Grid grd = new Grid();
                    grd.Height = 24;


                //Stack Pannel -set height 
                StackPanel stkPanel = new StackPanel();
                    stkPanel.Height = 24;
                    stkPanel.Width = 670;
                    stkPanel.Orientation = Orientation.Horizontal;

                //Combobox 
                cboItem = new ComboBox();
                    //set width
                    cboItem.Width = 40;
                    for (int i = 0; i <= 200; i++)
                        cboItem.Items.Add(i);
                    //Select number of items
                    cboItem.SelectedIndex = iNumber;
                    cboItem.SelectionChanged += new SelectionChangedEventHandler(CboItem_SelectionChanged);
                    cboItem.SelectionChanged += new SelectionChangedEventHandler(inv.updatetotal);
                    cboItem.IsEnabled = false;
                stkPanel.Children.Add(cboItem);


                //Item number - set width
                tblkItem = new TextBlock();
                    tblkItem.Width = 100;
                    tblkItem.Text = iItemNumber.ToString();
                    tblkItem.TextAlignment = TextAlignment.Center;
                stkPanel.Children.Add(tblkItem);

                //Item name  - set width 
                tblkName = new TextBlock();
                    tblkName.Width = 324;
                    tblkName.Text = sItemName;
                stkPanel.Children.Add(tblkName);




                // Item Cost - set width 
                tboxCost = new TextBox();
                    tboxCost.Width = 100;
                    tboxCost.TextAlignment = TextAlignment.Center;
                    tboxCost.IsEnabled = false;
                    tboxCost.TextChanged += new TextChangedEventHandler(Cost_TextChanged);
                    tboxCost.Text = string.Format("{0:0.00}", dCost);
                stkPanel.Children.Add(tboxCost);

                //Item Total -set width
                tblkTotal = new TextBlock();
                    tblkTotal.Width = 80;
                    tblkTotal.TextAlignment = TextAlignment.Right;
                    tblkTotal.Text = itemTotal().ToString("c");
                stkPanel.Children.Add(tblkTotal);

                btnDelete = new Button();
                    Thickness m = new Thickness();
                    m.Left = 4;
                    btnDelete.Margin = m;
                    btnDelete.Width = 20;
                    btnDelete.Content = "X";
                    btnDelete.Foreground = Brushes.Red;
                    btnDelete.Visibility = Visibility.Hidden;
                    btnDelete.Tag = iItemNumber;
                    btnDelete.Click += new RoutedEventHandler(inv.DeleteSelectedItem);
                stkPanel.Children.Add(btnDelete);

                grd.Children.Add(stkPanel);
                return grd;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// Disables editing on item
        /// </summary>
        public void DisableEdits()
        {
            try
            {
                cboItem.IsEnabled = false;
                tboxCost.IsEnabled = false;
                btnDelete.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// updates the cost in teh database
        /// </summary>
        /// <param name="invoicenumber"></param>
        public void UpdateCost(int invoicenumber)
        {
            try
            {
                dCost = Double.Parse(tboxCost.Text);
                DAItem.UpdateItemCost(this, invoicenumber);
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Enables editing of item
        /// </summary>
        public void EnableEdits()
        {
            try
            {
                cboItem.IsEnabled = true;
                tboxCost.IsEnabled = true;
                btnDelete.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Called if number of items combobox is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                iNumber = ((ComboBox)sender).SelectedIndex;
                DAItem.UpdateAmount(iInvoiceNumber, iItemNumber, iNumber);
                tblkTotal.Text = itemTotal().ToString();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// overrides the to string method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return sItemName;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// called if the cost changes to validate the data is a double
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Double dbl = new double();
                if (Double.TryParse(tboxCost.Text,  out dbl))
                {
                    tboxCost.Foreground = Brushes.Black;
                    validates = true;
                }
                else
                {
                    tboxCost.Foreground = Brushes.Red;
                    validates = false;
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        
    }
}
