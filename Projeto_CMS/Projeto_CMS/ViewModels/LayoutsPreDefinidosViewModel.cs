using Newtonsoft.Json;
using Projeto_CMS.Helpers;
using Projeto_CMS.Models;
using Projeto_CMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Projeto_CMS.ViewModels
{
    public class LayoutsPreDefinidosViewModel : BaseViewModel
    {
        private readonly HttpClient _client;
        private readonly string _APIserver = "http://10.0.2.2:2626";
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");

        private ObservableCollection<LayoutsPreDefinidosInformation> layoutsPreDefinidosList;

        public ObservableCollection<LayoutsPreDefinidosInformation> LayoutsPreDefinidosList
        {
            get { return layoutsPreDefinidosList; }
            set
            {
                layoutsPreDefinidosList = value;
                OnPropertyChanged(nameof(LayoutsPreDefinidosList));
            }
        }

        public Command LayoutsPreDefinidosCommand { get; }

        public Command MostrarLayoutCarrosselCommand { get; }

        public LayoutsPreDefinidosViewModel()
        {
            LayoutsPreDefinidosCommand = new Command(LayoutsPrefefinidos);
            MostrarLayoutCarrosselCommand = new Command<string>((obj) => MostrarLayoutCarrossel(obj));
            layoutsPreDefinidosList = new ObservableCollection<LayoutsPreDefinidosInformation>();
            _client = new HttpClient();

        }

        public void LayoutsPrefefinidos()
        {
            layoutsPreDefinidosList.Clear();

            var LayoutCarrossel = new LayoutsPreDefinidosInformation
            {
                Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/view-carousel.png",
                Label = "Layout em Carrossel"
            };

            var LayoutLista = new LayoutsPreDefinidosInformation
            {
                Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/view-list.png",
                Label = "Layout em Lista"
            };

            layoutsPreDefinidosList.Add(LayoutCarrossel);
            layoutsPreDefinidosList.Add(LayoutLista);


        }

        public async void MostrarLayoutCarrossel(string nome)
        {
            if(nome == "view-carousel.png")
            {
                await App.Current.MainPage.Navigation.PushAsync(new CarrosselPage());
            }else if(nome == "view-list.png")
            {
                await App.Current.MainPage.Navigation.PushAsync(new ListPage());
            }
            
        }

    }
}
