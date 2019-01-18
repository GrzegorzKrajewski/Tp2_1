using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace GUI.ViewModel
{
    public class Details
    {
        public Details()
        {
            DisplayedProduct = new ProductVM();
            DisplayedProduct.PropertyChanged += HandleDisplayedProductsFieldChange;
            Messenger.Default.Register<SelectionChangedMessage>(this, HandleSelectionChanged);

        }

        public ProductVM DisplayedProduct { get; }

        public void HandleSelectionChanged(SelectionChangedMessage msg)
        {
            if (msg.Product != null)
            {
                DisplayedProduct.CopyFieldsFrom(msg.Product);
            }
            else
            {
                DisplayedProduct.Color = null;
                DisplayedProduct.ModifiedDate = DateTime.MinValue;
                DisplayedProduct.ProductId = 0;
                DisplayedProduct.Name = null;
                DisplayedProduct.ProductNumber = null;
            }
        }

        public void HandleDisplayedProductsFieldChange(object product, PropertyChangedEventArgs e)
        {
            Messenger.Default.Send(new DisplayedProductChangedMessage(DisplayedProduct));
        }
    }
}
