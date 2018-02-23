using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ISystem.CustomerF
{
    public class Customer
    {
        /// <summary>
        /// The Customers Number
        /// </summary>
        public int CUSTOMERNUMBER { get; set; }

        /// <summary>
        /// Customers first Name
        /// </summary>
        public string FIRSTNAME { get; set; }

        /// <summary>
        /// Customers Last Name
        /// </summary>
        public string LASTNAME { get; set; }

        /// <summary>
        /// Customers Street Adress
        /// </summary>
        public string STREETADDRESS { get; set; }

        /// <summary>
        /// Customers City
        /// </summary>
        public string CITY { get; set; }

        /// <summary>
        /// Customers State
        /// </summary>
        public string STATE { get; set; }

        /// <summary>
        /// Customers Zip
        /// </summary>
        public string ZIP { get; set; }

        /// <summary>
        /// Does data validate
        /// </summary>
        public bool validates { get; set; } = true;

        /// <summary>
        /// list of states
        /// </summary>
        private List<string> states;

        /// <summary>
        /// Textbox to hold the zip
        /// </summary>
        private TextBox tbx_cust_Zip;

        /// <summary>
        /// Combobox that holds the state
        /// </summary>
        private ComboBox cbo_cust_State;

        /// <summary>
        /// Textbox to hold the city
        /// </summary>
        private TextBox tbx_cust_City;

        /// <summary>
        /// textbox to hold the street
        /// </summary>
        private TextBox tbx_cust_Street;

        /// <summary>
        /// textbox to hold the customers last name
        /// </summary>
        private TextBox tbx_cust_CustomerLName;

        /// <summary>
        /// textbox to hold the customers first name
        /// </summary>
        private TextBox tbx_cust_CustomerFName;





        /// <summary>
        /// Customer Constructor pulls from database
        /// </summary>
        /// <param name="cnumber"> Customer name</param>
        public Customer(int cnumber)
        {
            try
            {
                states = new List<string>(new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY" });
                CUSTOMERNUMBER = cnumber;
                //DACustomer and get customer info from database. This will create a temp customer and copy back the attributes
                Customer tempcust = DACustomer.GetCustomer(cnumber);

                this.FIRSTNAME = tempcust.FIRSTNAME;
                this.LASTNAME = tempcust.LASTNAME;
                this.STREETADDRESS = tempcust.STREETADDRESS;
                this.CITY = tempcust.CITY;
                this.STATE = tempcust.STATE;
                this.ZIP = tempcust.ZIP;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// creates a new customer from scratch
        /// </summary>
        /// <param name="number">customer number</param>
        /// <param name="first">customer first name</param>
        /// <param name="last">customer last name</param>
        /// <param name="street">customer street</param>
        /// <param name="city">customer city</param>
        /// <param name="state">customer state</param>
        /// <param name="zip">customer zip</param>
        public Customer(int number, string first, string last, string street, string city, string state, string zip)
        {
            try
            { 
                this.CUSTOMERNUMBER = number;
                this.FIRSTNAME = first;
                this.LASTNAME = last;
                this.STREETADDRESS = street;
                this.CITY = city;
                this.STATE = state;
                this.ZIP = zip;
                states = new List<string>(new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY" });
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        
        /// <summary>
        /// enables editing
        /// </summary>            
        public void EnableEdits()
        {
            try
            {
                tbx_cust_CustomerFName.IsHitTestVisible = true;
                tbx_cust_CustomerLName.IsHitTestVisible = true;
                tbx_cust_Street.IsHitTestVisible = true;
                tbx_cust_City.IsHitTestVisible = true;
                tbx_cust_Zip.IsHitTestVisible = true;
                cbo_cust_State.IsHitTestVisible = true;

                tbx_cust_CustomerFName.Focusable = true;
                tbx_cust_CustomerLName.Focusable = true;
                tbx_cust_Street.Focusable = true;
                tbx_cust_City.Focusable = true;
                tbx_cust_Zip.Focusable = true;
                cbo_cust_State.Focusable = true;
            }catch(Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }

        /// <summary>
        /// Returns the name
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            try
            { 
                return FIRSTNAME + " " + LASTNAME;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
        }

        /// <summary>
        /// gets the grid for the customer
        /// </summary>
        /// <returns></returns>
        public Grid getGrid()
        {
            try
            {
                Grid myGrid = new Grid();
                myGrid.Height = 100;
                myGrid.Width = 370;
                myGrid.HorizontalAlignment = HorizontalAlignment.Right;

                TextBlock tblk_cust_Customertxt = new TextBlock();
                Thickness m = tblk_cust_Customertxt.Margin;
                m.Left = 60;
                tblk_cust_Customertxt.Margin = m;
                tblk_cust_Customertxt.Text = "Customer:";
                myGrid.Children.Add(tblk_cust_Customertxt);


                //<TextBox x:Name="tbx_mw_CustomerName" HorizontalAlignment="Left" Height="23" Margin="152,-5,0,0" TextWrapping="Wrap" Text="CustomerName" VerticalAlignment="Top" Width="208" BorderBrush="{x:Null}" FontSize="16" FontFamily="Tahoma" TextAlignment="Right"/>
                tbx_cust_CustomerFName = new TextBox();
                m.Left = 30; m.Top = -60;
                tbx_cust_CustomerFName.Margin = m;
                tbx_cust_CustomerFName.Width = 104;
                tbx_cust_CustomerFName.TextAlignment = TextAlignment.Center;
                tbx_cust_CustomerFName.Height = 18;
                tbx_cust_CustomerFName.Text = FIRSTNAME;
                tbx_cust_CustomerFName.IsHitTestVisible = false;
                tbx_cust_CustomerFName.Focusable = false;
                myGrid.Children.Add(tbx_cust_CustomerFName);

                tbx_cust_CustomerLName = new TextBox();
                m.Left = 250; m.Top = -60;
                tbx_cust_CustomerLName.Margin = m;
                tbx_cust_CustomerLName.Width = 104;
                tbx_cust_CustomerLName.TextAlignment = TextAlignment.Center;
                tbx_cust_CustomerLName.Height = 18;
                tbx_cust_CustomerLName.Text = LASTNAME;
                tbx_cust_CustomerLName.IsHitTestVisible = false;
                tbx_cust_CustomerLName.Focusable = false;
                myGrid.Children.Add(tbx_cust_CustomerLName);

                tbx_cust_Street = new TextBox();
                m.Left = 140; m.Top = -20;
                tbx_cust_Street.Margin = m;
                tbx_cust_Street.Width = 215;
                tbx_cust_Street.TextAlignment = TextAlignment.Center;
                tbx_cust_Street.Height = 18;
                tbx_cust_Street.Text = STREETADDRESS;
                tbx_cust_Street.IsHitTestVisible = false;
                tbx_cust_Street.Focusable = false;
                myGrid.Children.Add(tbx_cust_Street);

                //<TextBox x:Name="tbx_mw_CustomerStreet" HorizontalAlignment="Left" Height="23" Margin="152,18,0,0" TextWrapping="Wrap" Text="CustomerStreet" VerticalAlignment="Top" Width="208" BorderBrush="{x:Null}" FontSize="16" FontFamily="Tahoma" TextAlignment="Right"/>
                tbx_cust_City = new TextBox();
                m.Left = 26; m.Top = 20;
                tbx_cust_City.Margin = m;
                tbx_cust_City.Width = 100;
                tbx_cust_City.TextAlignment = TextAlignment.Center;
                tbx_cust_City.Height = 18;
                tbx_cust_City.Text = CITY;
                tbx_cust_City.IsHitTestVisible = false;
                tbx_cust_City.Focusable = false;
                myGrid.Children.Add(tbx_cust_City);

                cbo_cust_State = new ComboBox();
                m.Left = 190; m.Top = 20;
                cbo_cust_State.Margin = m;
                cbo_cust_State.Width = 60;
                cbo_cust_State.Height = 20;
                foreach (string i in states)
                    cbo_cust_State.Items.Add(i);
                cbo_cust_State.SelectedItem = STATE;
                cbo_cust_State.IsHitTestVisible = false;
                cbo_cust_State.Focusable = false;
                myGrid.Children.Add(cbo_cust_State);

                tbx_cust_Zip = new TextBox();
                m.Left = 305; m.Top = 20;
                tbx_cust_Zip.Margin = m;
                tbx_cust_Zip.Width = 50;
                tbx_cust_Zip.TextAlignment = TextAlignment.Center;
                tbx_cust_Zip.Height = 18;
                tbx_cust_Zip.Text = ZIP;
                tbx_cust_Zip.IsHitTestVisible = false;
                tbx_cust_Zip.Focusable = false;
                tbx_cust_Zip.TextChanged += new TextChangedEventHandler(ZIP_changed);
                myGrid.Children.Add(tbx_cust_Zip);

                return myGrid;
            }
            catch(Exception ex)
            {
                Exceptions.Spool(ex);
                return null;
            }
}

        /// <summary>
        /// event when the zip changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZIP_changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                int str;
                if (int.TryParse(tbx_cust_Zip.Text, out str) && tbx_cust_Zip.Text.Count() == 5)
                {
                    tbx_cust_Zip.Foreground = Brushes.Black;
                    validates = true;
                }
                else
                {
                    tbx_cust_Zip.Foreground = Brushes.Red;
                    validates = false;
                }
            }
            catch(Exception ex)
            {
                Exceptions.Spool(ex);
            }
}

      
        /// <summary>
        /// Saves the customer to the database
        /// </summary>
        public void Save()
        {
            try
            { 

                ZIP = tbx_cust_Zip.Text;
                STATE = cbo_cust_State.SelectedItem.ToString();
                CITY = tbx_cust_City.Text;
                STREETADDRESS = tbx_cust_Street.Text;
                LASTNAME = tbx_cust_CustomerLName.Text;
                FIRSTNAME = tbx_cust_CustomerFName.Text;
                DACustomer.Save(this);
            }
            catch(Exception ex)
            {
                Exceptions.Spool(ex);
            }
}

        /// <summary>
        /// disables edits 
        /// </summary>
        public void DisableEdits()
        {
            try {
                tbx_cust_CustomerFName.IsHitTestVisible = false;
                tbx_cust_CustomerLName.IsHitTestVisible = false;
                tbx_cust_Street.IsHitTestVisible = false;
                tbx_cust_City.IsHitTestVisible = false;
                tbx_cust_Zip.IsHitTestVisible = false;
                cbo_cust_State.IsHitTestVisible = false;

                tbx_cust_CustomerFName.Focusable = false;
                tbx_cust_CustomerLName.Focusable = false;
                tbx_cust_Street.Focusable = false;
                tbx_cust_City.Focusable = false;
                tbx_cust_Zip.Focusable = false;
                cbo_cust_State.Focusable = false;
            }
            catch(Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
    }
}
