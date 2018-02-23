using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISystem.ItemF;


namespace ISystem.ItemF
{
    class ItemList: List<Item>
    {
        /// <summary>
        /// Gets the total of the items
        /// </summary>
        /// <returns></returns>
        public double getTotal()
        {
            try
            {
                double total = 0;
                foreach (Item i in this)
                {
                    total += i.dCost;
                }
                return total;
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
                return 0.0;
            }
        }

        

    }
}
