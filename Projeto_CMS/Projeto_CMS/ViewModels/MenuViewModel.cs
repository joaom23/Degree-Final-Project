using Newtonsoft.Json;
using Projeto_CMS.Helpers;
using Projeto_CMS.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Projeto_CMS.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly string _fotoUser = Preferences.Get("ImagemUser", "NULL");
        private readonly HttpClient _client;
        private readonly string _APIserver = "http://10.0.2.2:2626";
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");

        private ImageSource fotoUser;
        public ImageSource FotoUser
        {
            get { return fotoUser; }
            set
            {
                fotoUser = value;
                OnPropertyChanged(nameof(FotoUser));
            }
        }

        private ImageSource banner;
        public ImageSource Banner
        {
            get { return banner; }
            set
            {
                banner = value;
                OnPropertyChanged(nameof(Banner));
            }
        }
        public Command LogoutCommand { get; }
        public Command SeusLayouts1Commnad { get; }
        public Command LayoutsPredefinidosCommnad { get; }
        public MenuViewModel()
        {
            LogoutCommand = new Command(Logout);
            SeusLayouts1Commnad = new Command(SeusLayoutsNumero1);
            LayoutsPredefinidosCommnad = new Command(LayoutsPredefinidos);
            Banner = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/imagegetdefault/homepage-banner.jpg"));
            FotoUser = ImageSource.FromUri(new Uri("http://10.0.2.2:2626/api/cliente/ImageGetUser/" + _fotoUser));
            _client = new HttpClient();
        }

        public async void SeusLayoutsNumero1()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SeusLayoutsPage());

        }

        public async void LayoutsPredefinidos()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LayoutsPredefinidosPage());

        }

        public void Logout()
        {
            Preferences.Remove("Id");
            Preferences.Remove("Token");
            App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
