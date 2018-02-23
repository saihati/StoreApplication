using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISystem.InvoiceF
{
    class InvoiceList: List<Invoice>, INotifyCollectionChanged
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="invoices">List of Invoices</param>
        public InvoiceList(List<Invoice> invoices)
        {
            try
            {
                foreach (Invoice item in invoices)
                {
                    this.Add(item);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Spool(ex);
            }
        }
        /// <summary>
        /// Event that notifies when the collection changes
        /// </summary>
        #pragma warning disable CS0067
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #pragma warning restore CS0067
    }
}