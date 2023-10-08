using Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private List<Product> productList;
        private Product selectedProduct;
        private List<Category> categories1;

        List<Category> categories { get => categories1; set { categories1 = value; Signal(); } }
        Category category;
        public List<Product> ProductList { get => productList; set { productList = value; Signal(); } }
        public Product SelectedProduct { get => selectedProduct; set { selectedProduct = value; Signal(); } }
        
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            ProductList = DB.GetInstance().GetProductList().Result;
            Routing.RegisterRoute("EditProductPage", typeof(EditProductPage));
            Routing.RegisterRoute("Edit", typeof(EditProductPage));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private async void Edit(object sender, EventArgs e)
        {
            //if (SelectedProduct != null && SelectedProduct.Id != null)
             //   await App.Current.MainPage.Navigation.PushModalAsync(new ProductPage(SelectedProduct));

        }

        private async void Add(object sender, EventArgs e)
        {
              await Shell.Current.GoToAsync($"Edit?iddd={7}");
              //  await Shell.Current.GoToAsync("EditProductPage");
        }

        private void Delete(object sender, EventArgs e)
        {
            if (SelectedProduct != null)
                DB.GetInstance().DeleteProduct(SelectedProduct);

            ProductList = DB.GetInstance().GetProductList().Result;

        }

        private async void OpenCategorirs(object sender, EventArgs e)
        {
           // await App.Current.MainPage.Navigation.PushModalAsync(new CatigoriesPage());
        }
    }
}
