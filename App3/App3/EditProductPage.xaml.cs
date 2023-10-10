using Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Id_), "id")]
    public partial class EditProductPage : ContentPage, INotifyPropertyChanged
    {
        private Product selected;
        private List<Category> listCatigories;
        private Category selectedCategory;
        private int index;
        private string photoPath;

        private List<Product> listProducts;
        private int id_;

        public int Id_
        {
            get => id_;
            set {
                id_ = value;
                GetSelected();
            }


        }
        public List<Category> ListCatigories { get => listCatigories; set { listCatigories = value; Signal(); } }
        public List<Product> ListProducts { get => listProducts; set { listProducts = value; Signal(); } }
        public Category SelectedCategory { get => selectedCategory; set { selectedCategory = value; Signal(); } }
        public Product Selected { get => selected; set { selected = value; Signal(); } }
        public int Index { get => index; set { index = value; Signal(); } }
        public string PhotoPath { get => photoPath; set { photoPath = value; Signal(); } }


        public EditProductPage()
        {
            InitializeComponent();
            BindingContext = this;

            ListCatigories = DB.GetInstance().GetCategoryList().Result;
            ListProducts = DB.GetInstance().GetProductList().Result;

        }
        public void GetSelected()
        {
            if(Id_ != 0)
                Selected = DB.GetInstance().GetProductList().Result.FirstOrDefault(s => s.Id == Id_);   
            else 
                Selected = new Product();

            SelectedCategory = Selected.Category;
            PhotoPath = Selected.PathImage;

            if (SelectedCategory != null)
                picker.SelectedIndex = ListCatigories.IndexOf(ListCatigories.FirstOrDefault(s => s.Id == SelectedCategory.Id));
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Save(object sender, EventArgs e)
        {
            Selected.PathImage = PhotoPath;

            if (Id_ != 0)
            {
                
                DB.GetInstance().EditProduct(Selected, SelectedCategory);
            }

            else
            {
                List<Product> products = DB.GetInstance().GetProductList().Result;
                if (products!= null && products.Count != 0)
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
            await Shell.Current.GoToAsync("//MainPage");
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