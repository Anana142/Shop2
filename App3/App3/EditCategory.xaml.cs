using Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Id_), "id")]
    public partial class EditCategory : ContentPage, INotifyPropertyChanged
    {
        private int id_;
        private Category categoryItem;

        public int Id_ { get => id_; set { id_ = value;
                GetCategoryItem();
            } }

        public Category CategoryItem { get => categoryItem; set { categoryItem = value; Signal();} }
        public EditCategory()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public void GetCategoryItem()
        {
            if (Id_ == 0)
                CategoryItem = new Category();
            else
                CategoryItem = DB.GetInstance().GetCategoryList().Result.FirstOrDefault(s => s.Id == Id_);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private async void Back(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Category?update=true");
        }

        private void Save(object sender, EventArgs e)
        {
             if (CategoryItem != null && CategoryItem.Id != 0)
             {
                 DB.GetInstance().EditCategory(CategoryItem);
             }

             else
             {
                 List<Category> categories = DB.GetInstance().GetCategoryList().Result;

                 if (categories != null && categories.Count > 0)
                     CategoryItem.Id = categories.Last().Id + 1;
                 else
                     CategoryItem.Id = 1;
                 categories.Add(CategoryItem);
                 DB.GetInstance().AddCatigorie(categories);
             }
        }
    }
}