using System;
using System.Windows;
using ISystem.ItemF;

namespace ISystem
{
    public class Events
    {
        public static void ItemNumberChanged(Item itm, int number)
        {
            itm.iNumber = number;
            //return null;
        }
    }
}