using Newtonsoft.Json;
using Projeto_CMS.Helpers;
using Projeto_CMS.Models;
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
    public class SeusLayoutsViewModel : BaseViewModel
    {
        private readonly HttpClient _client;
        private readonly string _APIserver = "http://10.0.2.2:2626";
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");

        public ObservableCollection<SeusLayoutsInformation> layouts;

        public ObservableCollection<SeusLayoutsInformation> Layouts
        {
            get { return layouts; }
            set
            {
                layouts = value;
                OnPropertyChanged(nameof(Layouts));
            }
        }

        public Command<SeusLayoutsInformation> GetLayoutCommand { get; }
        public Command TesteCommand { get; }

        public SeusLayoutsViewModel()
        {
            GetLayoutCommand = new Command<SeusLayoutsInformation>((obj) => GetLayout(obj));
            _client = new HttpClient();
            layouts = new ObservableCollection<SeusLayoutsInformation>();
            TesteCommand = new Command(Teste);
        }

        public async Task SeusLayouts()
        {
            layouts.Clear();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var responsee = await _client.GetAsync(_APIserver + "/api/cliente/SeusLayouts/" + _userId);

            var responsebody = await responsee.Content.ReadAsStringAsync();

            var layout = JsonConvert.DeserializeObject<ReturnLayoutsViewModel>(responsebody);

            foreach (var l in layout.LayoutNumero1s)
            {
                var seulayout = new SeusLayoutsInformation();

                if (l.NumeroLayout == 1)
                {
                    seulayout.Id = l.Id;
                    seulayout.Titulo = l.Titulo;
                    seulayout.Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/view-carousel.png";
                }
                else if (l.NumeroLayout == 2)
                {
                    seulayout.Id = l.Id;
                    seulayout.Titulo = l.Titulo;
                    seulayout.Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/view-list.png";
                }

                layouts.Add(seulayout);
            }

        }

        public async void GetLayout(SeusLayoutsInformation layout)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await _client.GetAsync(_APIserver + "/api/cliente/Getlayoutnumero1/" + layout.Id);

            var responsebody = await response.Content.ReadAsStringAsync();

            var seuLayout = JsonConvert.DeserializeObject<Layout>(responsebody);

            if (seuLayout.NumeroLayout == 1)
            {
                var novaPagina = new Layout1Page(seuLayout);
                await App.Current.MainPage.Navigation.PushAsync(novaPagina);
            }
            else if(seuLayout.NumeroLayout == 2)
            {
                var novaPagina = new Layout2Page(seuLayout);
                await App.Current.MainPage.Navigation.PushAsync(novaPagina);
            }         
        }

        public async void Teste()
        {
            await App.Current.MainPage.DisplayAlert("Erro", "Login falhou", "Ok");
        }
    }
}
