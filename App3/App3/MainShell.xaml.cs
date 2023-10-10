using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            InitializeComponent();
          //  Routing.RegisterRoute("Registration", typeof(Registration));
        }

        private void AddProductPage(object sender, EventArgs e)
        {

        }

        private async void ToRegistration(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Registration");
        }
    }
}