namespace GUI.ViewModel
{
    public class DisplayedProductChangedMessage
    {
        public ProductVM product;

        public DisplayedProductChangedMessage(ProductVM product)
        {
            this.product = product;
        }
    }
}
