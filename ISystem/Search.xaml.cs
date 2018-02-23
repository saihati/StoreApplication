using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ISystem.InvoiceF;
using ISystem.CustomerF;
using ISystem.ItemF;

namespace ISystem
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        /// <summary>
        /// Form that will be returned when search is completed
        /// </summary>
        public Invoice InvoiceForm;
        /// <summary>
        /// Viewable Invoice List that is used in the listbox
        /// </summary>
        private InvoiceList UIInvoices;
        /// <summary>
        /// List used to re-populate the UI Invoices List
        /// </summary>
        private InvoiceList mainList;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Search()
        {
            try
            {
                InitializeComponent();
                // Gets the list of invoices from the database
                mainList = new InvoiceList(DAInvoice.ListInvoices());
                // Copies list to UI Invoices list
                UIInvoices = new InvoiceList(mainList);
                // Sets listbox datasource
                lb_srch_Invoices.ItemsSource = UIInvoices;
                // Populate Comboboxes
                GetInvoiceNums();
                GetInvoiceDates();
                GetInvoiceTotals();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Populates the Invoice Numbers Combobox
        /// </summary>
        private void GetInvoiceNums()
        {
            try
            {
                foreach (Invoice item in UIInvoices)
                {
                    cb_InvoiceNum.Items.Add(item.iInvoiceNumber);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
            
        }
        /// <summary>
        /// Populates the Invoice Dates Combobox
        /// </summary>
        private void GetInvoiceDates()
        {
            try
            {
                foreach (Invoice item in UIInvoices)
                {
                    cb_InvoiceDate.Items.Add(item.sInvoiceDate);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Populates the Invoice Totals Combobox
        /// </summary>
        private void GetInvoiceTotals()
        {
            try
            {
                foreach (Invoice item in UIInvoices)
                {
                    cb_TotalCost.Items.Add(item.dTotal.ToString("c"));
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Submits the selected invoice and returns it for the window to display
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Args</param>
        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InvoiceForm = (Invoice)lb_srch_Invoices.SelectedItem;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Updates the listbox based on the Invoice Number Combobox selection
        /// </summary>
        /// <param name="sender">Combobox</param>
        /// <param name="e">Args</param>
        private void cb_InvoiceNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Checks to make sure that the selected index is not the reset value
                ComboBox temp = (ComboBox)sender;
                if (temp.SelectedIndex != -1)
                {
                    // Reset other comboboxes
                    //cb_InvoiceDate.SelectedIndex = -1;
                    //cb_TotalCost.SelectedIndex = -1;
                    // Clear items out of Listbox source
                    UIInvoices.Clear();
                    string invoiceNum = ((ComboBox)sender).SelectedItem.ToString();
                    // Add items that match into listbox
                    foreach (Invoice item in DAInvoice.ListInvoices())
                    {
                        if (item.iInvoiceNumber == invoiceNum)
                        {
                            if (cb_TotalCost.SelectedIndex != -1 && cb_InvoiceDate.SelectedIndex != -1)
                            {
                                if (item.dTotal.ToString("c") == cb_TotalCost.SelectedItem.ToString() && item.sInvoiceDate == (DateTime)cb_InvoiceDate.SelectedItem)
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else if (cb_TotalCost.SelectedIndex != -1)
                            {
                                if (item.dTotal.ToString("c") == cb_TotalCost.SelectedItem.ToString())
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else if (cb_InvoiceDate.SelectedIndex != -1)
                            {
                                if (item.sInvoiceDate == (DateTime)cb_InvoiceDate.SelectedItem)
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else
                            {
                                UIInvoices.Add(item);
                            }
                        }
                        //if (item.iInvoiceNumber == invoiceNum)
                        //{
                        //    UIInvoices.Add(item);
                        //}
                    }
                    lb_srch_Invoices.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Updates the listbox based on the Invoice Date Combobox selection
        /// </summary>
        /// <param name="sender">Combobox</param>
        /// <param name="e">Args</param>
        private void cb_InvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Checks to make sure that the selected index is not the reset value
                ComboBox temp = (ComboBox)sender;
                if (temp.SelectedIndex != -1)
                {
                    // Reset other comboboxes
                    //cb_InvoiceNum.SelectedIndex = -1;
                    //cb_TotalCost.SelectedIndex = -1;
                    // Clear items out of Listbox source
                    UIInvoices.Clear();
                    DateTime invoiceDate = (DateTime)((ComboBox)sender).SelectedItem;
                    // Add items that match into listbox
                    foreach (Invoice item in mainList)
                    {
                        if (item.sInvoiceDate == invoiceDate)
                        {
                            if (cb_TotalCost.SelectedIndex != -1 && cb_InvoiceNum.SelectedIndex != -1)
                            {
                                if (item.dTotal.ToString("c") == cb_TotalCost.SelectedItem.ToString() && item.iInvoiceNumber == cb_InvoiceNum.SelectedItem.ToString())
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else if (cb_InvoiceNum.SelectedIndex != -1)
                            {
                                if (item.iInvoiceNumber == (string)cb_InvoiceNum.SelectedItem)
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else if (cb_TotalCost.SelectedIndex != -1)
                            {
                                if (item.dTotal.ToString("c") == cb_TotalCost.SelectedItem.ToString())
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else
                            {
                                UIInvoices.Add(item);
                            }
                        }
                        //if (item.sInvoiceDate == invoiceDate)
                        //{
                        //    UIInvoices.Add(item);
                        //}
                    }
                    lb_srch_Invoices.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
            
        }
        /// <summary>
        /// Updates the listbox based on the Total Cost Combobox selection
        /// </summary>
        /// <param name="sender">Combobox</param>
        /// <param name="e">Args</param>
        private void cb_TotalCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Checks to make sure that the selected index is not the reset value
                ComboBox temp = (ComboBox)sender;
                if (temp.SelectedIndex != -1)
                {
                    // Reset other comboboxes
                    //cb_InvoiceNum.SelectedIndex = -1;
                    //cb_InvoiceDate.SelectedIndex = -1;
                    // Clear items out of Listbox source
                    UIInvoices.Clear();
                    string invoiceTotal = temp.SelectedItem.ToString();
                    // Add items that match into listbox
                    foreach (Invoice item in mainList)
                    {
                        if (item.dTotal.ToString("c") == invoiceTotal)
                        {
                            if (cb_InvoiceNum.SelectedIndex != -1 && cb_InvoiceDate.SelectedIndex != -1)
                            {
                                if (item.iInvoiceNumber == cb_InvoiceNum.SelectedItem.ToString() && item.sInvoiceDate == (DateTime)cb_InvoiceDate.SelectedItem)
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else if (cb_InvoiceNum.SelectedIndex != -1)
                            {
                                if (item.iInvoiceNumber == (string)cb_InvoiceNum.SelectedItem)
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else if (cb_InvoiceDate.SelectedIndex != -1)
                            {
                                if (item.sInvoiceDate == (DateTime)cb_InvoiceDate.SelectedItem)
                                {
                                    UIInvoices.Add(item);
                                }
                            }
                            else
                            {
                                UIInvoices.Add(item);
                            }
                        }
                    }
                    lb_srch_Invoices.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Enables the "Select Invoice" button when selection is made
        /// </summary>
        /// <param name="sender">Listbox</param>
        /// <param name="e">Args</param>
        private void lb_srch_Invoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                btn_Load_Invoice.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
    }
}

