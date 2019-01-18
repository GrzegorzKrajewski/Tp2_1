namespace GUI.ViewModel
{
    public class SelectionChangedMessage
    {
        public ProductVM Product;

        public SelectionChangedMessage(ProductVM product)
        {
            this.Product = product;
        }
    }
}
