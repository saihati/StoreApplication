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
using System.Data;
using System.ComponentModel;
using System.Windows.Navigation;
using ISystem.ItemF;
using ISystem.InvoiceF;
using ISystem.CustomerF;
using System.Globalization;

namespace ISystem
{
    /// <summary>
    /// Interaction logic for def.xaml
    /// </summary>
    public partial class def : Window
    {

        /// <summary>
        /// window constructor
        /// </summary>
        public def()
        {
            try
            {
                InitializeComponent();
                List<Item> totalitems = DAItem.GetList();
                foreach (Item i in totalitems)
                    cmb_def_Item.Items.Add(i);
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        
        /// <summary>
        /// Item Name
        /// </summary>
        string itemName;
               
        /// <summary>
        /// item Description
        /// </summary>
        string itemDescription;

        /// <summary>
        /// Item Cost
        /// </summary>
        double Cost;
        
      

        /// <summary>
        /// Populates edit fields when item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //when there is a value in the text box
                if (cmb_def_Item.SelectedIndex != -1)
                {
                    btn_def_EditSave.IsEnabled = true;
                    btn_def_Delete.IsEnabled = true;
                    Item itm = ((Item)cmb_def_Item.SelectedItem);
                    tbx_def_inumber.Text = itm.iItemNumber.ToString();
                    tbx_def_IDesc1.Text = itm.sDescription;
                    tbx_def_IName1.Text = itm.sItemName;
                    tbx_def_Cost1.Text = string.Format("{0:0.00}", itm.dCost);
                    btn_def_EditItem.IsEnabled = true;

                    //when there is no value in the text box
                }
                if (cmb_def_Item.SelectedIndex == -1)
                {
                    btn_def_Add.IsEnabled = true;
                    btn_def_EditSave.IsEnabled = false;
                    btn_def_Delete.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// enable text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbx_def_IName.IsEnabled = true;
                tbx_def_IDesc.IsEnabled = true;
                tbx_def_Cost.IsEnabled = true;
                btn_def_Save.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex); 
            }
        }

        /// <summary>
        /// adding an item
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveADD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Double dbl = new double();
                if (Double.TryParse(tbx_def_Cost.Text, out dbl) && tbx_def_IName.Text != "" && tbx_def_IDesc.Text != "" && tbx_def_Cost.Text != "")
                {
                    DAItem.AddItem(tbx_def_IName.Text, tbx_def_IDesc.Text, dbl);
                    List<Item> totalitems = DAItem.GetList();
                    cmb_def_Item.Items.Clear();
                    foreach (Item i in totalitems)
                        cmb_def_Item.Items.Add(i);
                    tbx_def_IName.IsEnabled = false;
                    tbx_def_IDesc.IsEnabled = false;
                    tbx_def_Cost.IsEnabled = false;

                    tbx_def_IName.Text = "";
                    tbx_def_IDesc.Text = "";
                    tbx_def_Cost.Text = "";
                    btn_def_Save.IsEnabled = false;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbx_def_Cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Double dbl = new double();
                if (Double.TryParse(tbx_def_Cost.Text, out dbl))
                {
                    //String pat = "";

                    tblk_def_CostBad.Visibility = Visibility.Hidden;
                    btn_def_Save.IsEnabled = true;
                }
                else
                {
                    if (tbx_def_Cost.Text != "")
                        tblk_def_CostBad.Visibility = Visibility.Visible;
                    btn_def_Save.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        
        /// <summary>
        /// enabling the text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tbx_def_IName1.IsEnabled = true;
                tbx_def_IDesc1.IsEnabled = true;
                tbx_def_Cost1.IsEnabled = true;
                btn_def_EditSave.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
            
        }
        
                
       
        /// <summary>
        /// the delete button
        /// it deletes items that not in the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_def_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int itemNumber = Convert.ToInt32(tbx_def_inumber.Text);
                bool itemDeleted = DAItem.deleteItem(itemName, itemDescription, Cost, itemNumber);
                if (itemDeleted)
                {
                    MessageBox.Show("Item deleted Successfully!");
                }
                else
                {
                    MessageBox.Show("Item exists in invoice");
                }

                List<Item> totalitems = DAItem.GetList();
                cmb_def_Item.Items.Clear();
                foreach (Item i in totalitems)
                    cmb_def_Item.Items.Add(i);


                tbx_def_IName.IsEnabled = false;
                tbx_def_IDesc.IsEnabled = false;
                tbx_def_Cost.IsEnabled = false;

                tbx_def_inumber.Text = "";
                tbx_def_IName1.Text = "";
                tbx_def_IDesc1.Text = "";
                tbx_def_Cost1.Text = "";
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);

            }


        }

        /// <summary>
        /// validates the cost of an add item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostonAddChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                Double dbl = new double();
                if (Double.TryParse(tbx_def_Cost.Text, out dbl))
                {
                    //String pat = "";

                    tblk_def_CostBad.Visibility = Visibility.Hidden;
                    btn_def_Save.IsEnabled = true;
                }
                else
                {
                    if (tbx_def_Cost.Text != "")
                        tblk_def_CostBad.Visibility = Visibility.Visible;
                    btn_def_Save.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Validates the cost for an edit item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostValidate(object sender, TextChangedEventArgs e)
        {
            try
            {
                Double dbl = new double();
                if (Double.TryParse(tbx_def_Cost1.Text, out dbl))
                {
                    //String pat = "";

                    tblk_def_BadCost.Visibility = Visibility.Hidden;
                    btn_def_EditSave.IsEnabled = true;
                }
                else
                {
                    if (tbx_def_Cost1.Text != "")
                        tblk_def_BadCost.Visibility = Visibility.Visible;
                    btn_def_EditSave.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Commits to edits of items and updates database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_def_EditSave_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                itemName = tbx_def_IName1.Text;
                itemDescription = tbx_def_IDesc1.Text;
                int itemNumber = 2;

                Double db2 = new double();
                if (Double.TryParse(tbx_def_Cost1.Text, out db2) && tbx_def_IName1.Text != "" && tbx_def_IDesc1.Text != "" && tbx_def_Cost1.Text != "")
                {
                    Cost = db2;
                    itemNumber = int.Parse(tbx_def_inumber.Text);
                    DAItem.editItem(itemName, itemDescription, Cost, itemNumber);

                    List<Item> totalitems = DAItem.GetList();
                    cmb_def_Item.Items.Clear();
                    foreach (Item i in totalitems)
                        cmb_def_Item.Items.Add(i);


                    //disabling the boxes
                    tbx_def_IName1.IsEnabled = false;
                    tbx_def_IDesc1.IsEnabled = false;
                    tbx_def_Cost1.IsEnabled = false;
                    btn_def_EditSave.IsEnabled = false;
                    btn_def_EditItem.IsEnabled = false;



                    //clearing the boxes
                    tbx_def_IName1.Text = "";
                    tbx_def_IDesc1.Text = "";
                    tbx_def_Cost1.Text = "";
                    tbx_def_inumber.Text = "";


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
    }
    }

