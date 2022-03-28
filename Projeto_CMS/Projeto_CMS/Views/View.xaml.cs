using Projeto_CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Projeto_CMS
{
    public partial class View : ContentPage
    {
        MenuViewModel viewmodel;
        public View()
        {
            InitializeComponent();
            viewmodel = new MenuViewModel();
            BindingContext = viewmodel;
        }
    }
}
