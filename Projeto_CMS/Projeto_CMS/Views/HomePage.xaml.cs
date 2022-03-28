using Projeto_CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace Projeto_CMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly string _userName = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");
        private readonly HttpClient _client;
        private readonly string _APIserver = "http://10.0.2.2:2626";
        public HomePage()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            _client = new HttpClient();        
        }

        protected override void OnAppearing()
        {
            //base.OnAppearing();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            //var responsee = await _client.GetAsync(_APIserver + "/api/cliente/" + _userName);

            //var responsebody = await responsee.Content.ReadAsStringAsync();

            //Layout layout = JsonConvert.DeserializeObject<Layout1>(responsebody);

            //tituloLabel.Text = layout.Titulo;
            //botaoButton.Text = layout.Botao;

            //Image m = new Image()
            //{
            //    Source = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoN1))
            //};

            //fotoN1.Source = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoN1));
            //fotoN2.Source = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoN2));
            //fotoN3.Source = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoN3));
            //fotoN4.Source = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoN4));


        }
        protected override bool OnBackButtonPressed() //Desativar botao de voltar atras do android
        {
            return true;
        }

        private void Logout(object sender, EventArgs e)
        {
            Preferences.Remove("Id");
            Preferences.Remove("Token");
            App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        //public void DownloadImage(string filename)
        //{
        //    var documentsPath = System.Environment.GetFolderPath("~/Imagens"));

        //}
    }
}