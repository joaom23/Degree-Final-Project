using Projeto_CMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Projeto_CMS
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            var assembly = typeof(MainPage);
            //imagemAndroid.Source = ImageSource.FromResource("Projeto_CMS.Imagens.android.png", assembly);
            //imagemAndroid.Source = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetlayout/"));

        }

        protected override bool OnBackButtonPressed() //Desativar botao de voltar atras do android
        {
            return true;
        }

    }
}
