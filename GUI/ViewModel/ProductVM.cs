using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Data;

namespace GUI.ViewModel
{
    public class ProductVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propErrors = new Dictionary<string, List<string>>();
        public Product Product1 { get; set; }

        public ProductVM(Product p)
        {
            Product1 = p;
        }

        public ProductVM()
        {
            Product1 = new Product();
        }

        public void CopyFieldsFrom(ProductVM product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Color = product.Color;
            ModifiedDate = product.ModifiedDate;
            ProductNumber = product.ProductNumber;
        }

        public int ProductId
        {
            get => Product1.ProductID;
            set
            {
                Product1.ProductID = value;
                OnPropertyChanged();
                OnPropertyChanged($"DisplayName");

            }
        }

        public string Name
        {
            get => Product1.Name;
            set
            {
                Product1.Name = value;
                OnPropertyChanged();
                OnPropertyChanged($"DisplayName");
            }
        }

        public string Color
        {
            get => Product1.Color;
            set
            {
                Product1.Color = value;
                OnPropertyChanged();
            }
        }

        public string ProductNumber
        {
            get => Product1.ProductNumber;
            set
            {
                Product1.ProductNumber = value;
                OnPropertyChanged();
            }
        }

        public DateTime ModifiedDate
        {
            get => Product1.ModifiedDate;
            set
            {
                Product1.ModifiedDate = value;
                OnPropertyChanged();
            }
        }

        public string DisplayName => String.Concat(Product1.ProductID, ": ", Product1.Name);

        public Product GetProduct1 => Product1;

        public bool HasErrors
        {
            get
            {
                List<string> err = _propErrors.Values.FirstOrDefault(list => list.Count > 0);
                return err != null;
            }
        }


        private void Validate()
        {
            List<string> nameList = new List<string>();
            if (Name == null)
            {
                nameList.Add("Name cannot be null.");
            }
            else if (Name.Length > 50)
            {
                nameList.Add("Name cannot be longer than 50 characters.");
            }
            _propErrors["Name"] = nameList;
            if (nameList.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("Name"));
            }
            List<string> colorList = new List<string>();
            if (Color != null && Color.Length > 15)
            {
                colorList.Add("Color cannot be longer than 15 characters.");
            }
            _propErrors["Color"] = colorList;
            if (colorList.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("Color"));
            }
            List<string> productNumberList = new List<string>();
            if (ProductNumber == null)
            {
                productNumberList.Add("getProduct1 number cannot be null.");
            }
            else if (ProductNumber.Length > 25)
            {
                productNumberList.Add("getProduct1 number cannot be longer than 25 characters.");
            }
            _propErrors["ProductNumber"] = productNumberList;
            if (productNumberList.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("ProductNumber"));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Validate();
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            _propErrors.TryGetValue(propertyName, out var err);
            return err;
        }
    }


}
