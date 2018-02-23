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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ISystem.CustomerF;
using ISystem.ItemF;
using ISystem.InvoiceF;

namespace ISystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Current invoice item
        /// </summary>
        private Invoice invoiceCurrent;

        public MainWindow()
        {

            try
            {
                InitializeComponent();

                this.Top = 0;
                this.Left = 0;
                LoadCombo();
                btn_mw_EditInvoice.IsEnabled = false;
                btn_mw_DeleteInvoice.IsEnabled = false;
                DAInvoice.DeleteUNK();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// load the list of items
        /// </summary>
        private void LoadCombo()
        {
            try
            {
                cbo_mw_ItemList.Items.Clear();
                List<Item> totalitems = DAItem.GetList();
                foreach (Item i in totalitems)
                    cbo_mw_ItemList.Items.Add(i);
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Adds item from combobox to invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbo_mw_ItemList.SelectedIndex != -1)
                {
                    invoiceCurrent.addItem((Item)cbo_mw_ItemList.SelectedItem);
                    LoadEdit();
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Sets the window to editmode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadEdit();

            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Loads and invoice
        /// </summary>
        /// <param name="newinvoice">Invoice number</param>
        private void LoadInvoice(string newinvoice)
        {
            try
            {
                invoiceCurrent = DAInvoice.GetInvoice(newinvoice);
                grd_mw_Invoice.Children.Clear();
                grd_mw_Invoice.Children.Add(invoiceCurrent.InvoiceGrid());
                grd_mw_Customer.Children.Clear();
                grd_mw_Customer.Children.Add(invoiceCurrent.cCustomer.getGrid());
                grd_mw_InvoiceItems.Children.Clear();
                grd_mw_InvoiceItems.Children.Add(invoiceCurrent.ItemGrid());
                btn_mw_EditInvoice.IsEnabled = true;
                btn_mw_DeleteInvoice.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Brings up the search window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchClick(object sender, RoutedEventArgs e)
        {

            try
            {
                UnloadEdit();
                Search ser = new Search();
                if (ser.ShowDialog() == true)
                {
                    LoadInvoice(ser.InvoiceForm.iInvoiceNumber);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
            
        }

        /// <summary>
        /// Brings up the Edit items window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                UnloadEdit();
                def defWindow = new def();
                defWindow.ShowDialog();
                LoadCombo();
                if (invoiceCurrent != null)
                {
                    LoadInvoice(invoiceCurrent.iInvoiceNumber);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Creates a new invoice and displays it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewInvoiceClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //load UNK user
                invoiceCurrent = DAInvoice.NewInvoice();
                //load editing
                LoadInvoice(invoiceCurrent.iInvoiceNumber);
                LoadEdit();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        
        /// <summary>
        /// Deltes and invoice and creates an empty one to display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DAInvoice.Delete(invoiceCurrent);
                //load UNK user
                invoiceCurrent = DAInvoice.NewInvoice();
                //load editing
                LoadInvoice(invoiceCurrent.iInvoiceNumber);
                LoadEdit();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
            
        }

        /// <summary>
        /// Deletes invoices that have an UNK customer for cleanup
        /// </summary>
        private void DeleteTheUnkown()
        {
            try
            {
                DAInvoice.DeleteUNK();
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex); 
            }
        }

        /// <summary>
        /// Updates the cost box when item selection box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_mw_ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedIndex != -1)
                {
                    tbx_mw_Cost.Text = Convert.ToString(((Item)((ComboBox)sender).SelectedItem).dCost);
                }
                else
                {
                    tbx_mw_Cost.Text = "";
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex); 
            }
        }

        /// <summary>
        /// allows edits to be made
        /// </summary>
        private void LoadEdit()
        {
            try
            {
                invoiceCurrent.EnableEdit();
                cbo_mw_ItemList.IsHitTestVisible = true;
                cbo_mw_ItemList.Focusable = true;
                grid_AddItem.Visibility = Visibility.Visible;
                lbl_itemSelect.IsEnabled = true;
                cbo_mw_ItemList.IsEnabled = true;
                tbx_mw_Cost.IsEnabled = true;
                lbl_Cost.IsEnabled = true;
                btn_mw_AddItem.IsEnabled = true;
                btn_mw_Save.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex); 
            }
        }

        /// <summary>
        /// Disallows edits to be made
        /// </summary>
        private void UnloadEdit()
        {
            try
            {
                if (invoiceCurrent != null)
                    invoiceCurrent.DisableEdit();
                cbo_mw_ItemList.IsHitTestVisible = false;
                cbo_mw_ItemList.Focusable = false;
                grid_AddItem.Visibility = Visibility.Hidden;
                lbl_itemSelect.IsEnabled = false;
                cbo_mw_ItemList.IsEnabled = false;
                tbx_mw_Cost.IsEnabled = false;
                lbl_Cost.IsEnabled = false;
                btn_mw_AddItem.IsEnabled = false;
                btn_mw_Save.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex); 
            }
        }

        /// <summary>
        /// Saves current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_mw_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoiceCurrent.validates())
                {
                    tblk_mw_SaveError.Visibility = Visibility.Hidden;
                    UnloadEdit();
                    invoiceCurrent.Save();
                    LoadInvoice(invoiceCurrent.iInvoiceNumber);
                }
                else
                {
                    tblk_mw_SaveError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
            
        }

        private void btn_About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This Application was developed by:\n" +
                "Mohammed Alsaihati\n" +
                "Daniel Bigelow\n" +
                "Nathan Borup");
        }
    }
}
