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
    public partial class Registration : ContentPage,INotifyPropertyChanged 
    {
        private string login;
        private string password;

        public string Login { get => login; set { login = value; Signal(); } }
        public string Password { get => password; set { password = value; Signal(); } }

        public List<User> Users { get; set; }

        public Registration()
        {
            InitializeComponent();
            BindingContext = this;
            
           // DB.GetInstance().u();
            Users = DB.GetInstance().GetUsersList().Result;
            
            
        }
        private async void Inter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
            {
                if (DB.GetInstance().GetUsersList().Result.FirstOrDefault(s => s.Login == Login && s.Password == Password) != null)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                    Login = null;
                    Password = null;
                }
                   
                else
                    await DisplayAlert("Ой-ей", "Неверный логин или пароль", "Ок");
                
                   
                
            }
                
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private async void Registr(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Registr");
        }
    }
}