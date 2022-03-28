using Projeto_CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projeto_CMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        LayoutViewModel viewModel;

        public ListPage()
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