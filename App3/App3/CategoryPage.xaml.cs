using Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage, INotifyPropertyChanged
    {
        private List<Category> categoriesList;
        private Category selectedCaterorie;

        public List<Category> CategoriesList { get => categoriesList; set { categoriesList = value; Signal(); } }
        public Category SelectedCaterorie { get => selectedCaterorie; set { selectedCaterorie = value; Signal(); } }
        public CategoryPage()
        {
            InitializeComponent();
            BindingContext = this;
            CategoriesList = DB.GetInstance().GetCategoryList().Result;

            Routing.RegisterRoute("EditCategory", typeof(EditCategory));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void Delete(object sender, EventArgs e)
        {
            if (SelectedCaterorie != null)
                DB.GetInstance().DeleteCategory(SelectedCaterorie);
            CategoriesList = DB.GetInstance().GetCategoryList().Result;
        }

        private async void Edit(object sender, EventArgs e)
        {
            if (SelectedCaterorie != null && SelectedCaterorie.Id != 0)
                await Shell.Current.GoToAsync($"EditCategory?id={SelectedCaterorie.Id}");

        }

        private async void AddCategorie(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"EditCategory?id=0");
        }

        private void Back(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
        }
    }
}