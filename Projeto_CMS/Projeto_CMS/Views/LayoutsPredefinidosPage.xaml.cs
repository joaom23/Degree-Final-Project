using Projeto_CMS.Models;
using Projeto_CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Projeto_CMS
{
    public partial class LayoutsPredefinidosPage : ContentPage
    {
        LayoutsPreDefinidosViewModel viewModel;

        public LayoutsPredefinidosPage()
        {
            InitializeComponent();
            viewModel = new LayoutsPreDefinidosViewModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LayoutsPrefefinidos();

        }

        private void Mostrar(object sender, EventArgs e)
        {
            var imageSender = (Image)sender;

            viewModel.MostrarLayoutCarrossel(imageSender.Source.ToString().Substring(54));
        }
    }
}
