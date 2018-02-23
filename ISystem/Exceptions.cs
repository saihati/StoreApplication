using System;
using System.Windows;

namespace ISystem
{
    public class Exceptions
    {
        /// <summary>
        /// used to display try catch errors
        /// </summary>
        /// <param name="ex"></param>
        internal static void Spool(Exception ex)
        {
            try {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
            } catch
            {

            }
        }
    }
}