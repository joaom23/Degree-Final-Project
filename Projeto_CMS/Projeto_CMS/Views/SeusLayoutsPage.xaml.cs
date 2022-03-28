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
    public partial class SeusLayoutsPage : ContentPage
    {
        SeusLayoutsViewModel viewModel;

        public SeusLayoutsPage()
        {
            InitializeComponent();
            viewModel = new SeusLayoutsViewModel();
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.SeusLayouts();

        }
    }
}