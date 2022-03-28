using Newtonsoft.Json;
using Projeto_CMS.Models;
using Projeto_CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projeto_CMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CarrosselPage : ContentPage
    {
        LayoutViewModel viewModel;

        public CarrosselPage()
        {
            InitializeComponent();
            viewModel = new LayoutViewModel();
            BindingContext = viewModel;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.ShowLayoutDefault();

        }

    }
}
