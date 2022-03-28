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

    public partial class Layout2Page : ContentPage
    {
        LayoutViewModel viewModel;

        public Layout layoutId;
        public Layout2Page(Layout id)
        {
            InitializeComponent();
            viewModel = new LayoutViewModel();
            BindingContext = viewModel;
            layoutId = id;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.ShowLayout(layoutId);

        }

    }
}
