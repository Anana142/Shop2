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
    public partial class RegistrationUser : ContentPage, INotifyPropertyChanged
    {
        private string login;
        private string password;

        public string Login { get => login; set { login = value; Signal(); } }
        public string Password { get => password; set { password = value; Signal(); } }

        public List<User> Users { get; set; }
        public RegistrationUser()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private async void Registr(object sender, EventArgs e)
        {
            DB.GetInstance().AddUser(Login, Password);
            await DisplayAlert("Сообщение", "Вы зарегистрированы", "Ок");
            await Shell.Current.GoToAsync("//Registration");
            Login = null;
            Password = null;    
        }
    }
}