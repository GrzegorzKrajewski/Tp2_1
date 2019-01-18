using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Data;
using GalaSoft.MvvmLight.Messaging;
using GUI.Model;
using GUI.ViewModel;
using UnitTests;


namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {

        /// <summary>
        /// Testy kontrolera
        /// </summary>

        [TestMethod]
        public void AddProductTest()
        {
            Controller controller = new Controller(new TestRepo());
            Product p = new Product();
            controller.AddProduct(p);
            Assert.IsTrue(controller.GetAllProducts().Contains(p));
        }

        [TestMethod]
        public void DeleteProducts()
        {
            Controller controller = new Controller(new TestRepo());
            Product p = controller.GetAllProducts()[3];
            controller.DeleteProduct(p);
            Assert.IsFalse(controller.GetAllProducts().Contains(p));
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            Controller controller = new Controller(new TestRepo());
            List<Product> list = controller.GetAllProducts();
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(list[i].ProductID, i);
            }
        }

        /// <summary>
        /// Testy vm.details
        /// </summary>

        [TestMethod]
        public void HandleSelectionChangedMessage()
        {
            Details details = new Details();
            ProductVM product = new ProductVM(new TestRepo().GetAllProducts()[3]);
            Messenger.Default.Send(new SelectionChangedMessage(product));
            Assert.AreEqual(details.DisplayedProduct.ProductId, product.ProductId);
        }

        [TestMethod]
        public void SendDisplayedProductChangeTest()
        {
            Details details = new Details();
            bool messageFlag = false;
            Messenger.Default.Register<DisplayedProductChangedMessage>(this, (msg) => { messageFlag = true; });
            details.DisplayedProduct.Color = "BLUE";
            Assert.IsTrue(messageFlag);
        }

        /// <summary>
        /// Testy listy produktow vm
        /// </summary>
        [TestMethod]
        public void TestGetAllProductsCommand()
        {
            List vm = new List(new TestRepo());
            vm.GetProducts.Execute(null);
            Assert.AreEqual(vm.Controller.GetAllProducts().Count, 4);
        }

        [TestMethod]
        public void TestAddProduct()
        {
            List vm = new List(new TestRepo());
            ProductVM p = new ProductVM();
            p.ProductId = 77;
            p.ProductNumber = "BK-653";
            Messenger.Default.Send(new DisplayedProductChangedMessage(p));
            vm.AddProduct.Execute(null);
            System.Threading.Thread.Sleep(20);
            Assert.AreEqual(vm.Controller.GetAllProducts()[4].ProductID, p.ProductId);
            vm.SelectedProduct = p;
            Assert.IsFalse(vm.AddProduct.CanExecute(null));
        }

        [TestMethod]
        public void TestDeleteProduct()
        {
            List vm = new List(new TestRepo());
            ProductVM p = new ProductVM(vm.Controller.GetAllProducts()[3]);
            vm.SelectedProduct = p;
            vm.DeleteProduct.Execute(null);
            System.Threading.Thread.Sleep(20);
            Assert.IsFalse(vm.Controller.GetAllProducts().Contains(p.Product1));
            vm.SelectedProduct = p;
            Assert.IsTrue(vm.DeleteProduct.CanExecute(null));
            vm.SelectedProduct = null;
            Assert.IsFalse(vm.DeleteProduct.CanExecute(null));
        }

        [TestMethod]
        public void TestUpdateProduct()
        {
            List vm = new List(new TestRepo());
            ProductVM p = new ProductVM(vm.Controller.GetAllProducts()[3]);
            vm.SelectedProduct = p;
            ProductVM displayedProduct = new ProductVM();
            int id = p.ProductId;
            DateTime date = p.ModifiedDate;
            displayedProduct.ProductId = p.ProductId + 10;
            Messenger.Default.Send(
                new DisplayedProductChangedMessage(displayedProduct));
            vm.UpdateProduct.Execute(null);
            Assert.AreEqual(p.ProductId, id + 10);
            Assert.IsTrue(date < p.ModifiedDate);
        }

    }
}