using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Data;
using GalaSoft.MvvmLight.Messaging;
using GUI.Model;
using Service;

namespace GUI.ViewModel
{
    public class List : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        private ProductVM _displayed = new ProductVM();

        public List() : this(new DataRepository()) { }
        public List(IDataRepository repo)
        {
            Controller = new Controller(repo);
            Messenger.Default.Register<DisplayedProductChangedMessage>(this, DisplayedChanged);
            GetProducts = new RelayCommand(GetAll);
            AddProduct = new RelayCommand(Add, () => SelectedProduct == null && !_displayed.HasErrors);
            DeleteProduct = new RelayCommand(Delete, () => SelectedProduct != null);
            UpdateProduct = new RelayCommand(Update, () => SelectedProduct != null && !_displayed.HasErrors);
            Deselect = new RelayCommand(DeselectProduct);
            GetAll();

        }


        private void Add()
        {
            Product p = _displayed.GetProduct1;
            Task.Run(() => Controller.AddProduct(p));
            ProductsCollection.Add(_displayed);
            _displayed = new ProductVM();
            SelectedProduct = null;
        }
        private void Update()
        {
            SelectedProduct1.CopyFieldsFrom(_displayed);
            SelectedProduct1.ModifiedDate = DateTime.Now;
            Task.Run(() => Controller.UpdateProduct());
        }
        private void Delete()
        {
            Product p = SelectedProduct1.GetProduct1;
            Task.Run(() => Controller.DeleteProduct(p));
            ProductsCollection.Remove(SelectedProduct1);
        }


        private void GetAll()
        {
            ProductsCollection.Clear();
            List<Product> products = Controller.GetAllProducts();
            foreach (var product in products)
            {
                ProductsCollection.Add(new ProductVM(product));
            }
        }

        public void DeselectProduct()
        {
            SelectedProduct = null;
        }

        public ProductVM SelectedProduct
        {
            get => SelectedProduct1;

            set
            {
                SelectedProduct1 = value;
                OnPropertyChanged();
                Messenger.Default.Send(new SelectionChangedMessage(SelectedProduct1));
            }
        }

        public ObservableCollection<ProductVM> ProductsCollection { get; set; } = new ObservableCollection<ProductVM>();

        public Controller Controller { get; set; }
        public ICommand GetProducts { get; }

        public ICommand AddProduct { get; }

        public ICommand DeleteProduct { get; }

        public ICommand UpdateProduct { get; }

        public ICommand Deselect { get; }
        public ProductVM SelectedProduct1 { get; set; }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void DisplayedChanged(DisplayedProductChangedMessage message)
        {
            if (message.product != null)
            {
                _displayed.CopyFieldsFrom(message.product);
            }
        }
    }
}
