using Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Id), "iddd")]
    public partial class EditProductPage : ContentPage, INotifyPropertyChanged
    {
        private Product selected;
        private List<Category> listCatigories;
        private Category selectedCategory;
        private int index;
        private string photoPath;

        private List<Product> listProducts;
        private  int id;

        public  int Id { get => id; set => id = value; } 
        public List<Category> ListCatigories { get => listCatigories; set { listCatigories = value; Signal(); } }
        public List<Product> ListProducts { get => listProducts; set => listProducts = value; }
        public Category SelectedCategory { get => selectedCategory; set { selectedCategory = value; Signal(); } }
        public Product Selected { get => selected; set { selected = value; Signal(); } }
        public int Index { get => index; set { index = value; Signal(); } }
        public string PhotoPath { get => photoPath; set { photoPath = value; Signal(); } }


        public EditProductPage()
        {
            InitializeComponent();
            BindingContext = this;
            //ListCatigories = DB.GetInstance().GetCategoryList().Result;
            //ListProducts = DB.GetInstance().GetProductList().Result;
            //if (Id == 0)
            //    Selected = new Product();
            //else
            //    Selected = ListProducts.FirstOrDefault(s => s.Id == Id);

            //PhotoPath = Selected.PathImage;
            //SelectedCategory = SelectedProduct.Category;

            //if (SelectedCategory != null)
            //    picker.SelectedIndex = ListCatigories.IndexOf(ListCatigories.FirstOrDefault(s => s.Id == SelectedCategory.Id));
       }
        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Save(object sender, EventArgs e)
        {
            Selected.PathImage = PhotoPath;
            if (Selected.Id != 0)
            {
                DB.GetInstance().EditProduct(Selected, SelectedCategory);
            }

            else
            {
                List<Product> products = DB.GetInstance().GetProductList().Result;
                if (products.Count != 0)
                {
                    Product product = products.Last();

                    Selected.Id = product.Id + 1;
                }
                else
                {
                    Selected.Id = 1;
                }
                DB.GetInstance().AddProduct(Selected, SelectedCategory);
            }
        }

        private async void Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
        }

        private async void Photo(object sender, EventArgs e)
        {
            TakePhotoAsync();
        }
        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {photoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }


    }
}