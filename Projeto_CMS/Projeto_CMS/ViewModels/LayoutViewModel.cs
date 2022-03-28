using Newtonsoft.Json;
using Projeto_CMS.Helpers;
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
    public class LayoutViewModel : BaseViewModel
    {
        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string titulo;
        public string Titulo
        {
            get { return titulo; }

            set
            {
                titulo = value;
                OnPropertyChanged(nameof(Titulo));
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }

            set
            {
                descricao = value;
                OnPropertyChanged(nameof(Descricao));
            }
        }

        private string corTitulo;
        public string CorTitulo
        {
            get { return corTitulo; }

            set
            {
                corTitulo = value;
                OnPropertyChanged(nameof(CorTitulo));
            }
        }

        private string corDescricao;
        public string CorDescricao
        {
            get { return corDescricao; }

            set
            {
                corDescricao = value;
                OnPropertyChanged(nameof(CorDescricao));
            }
        }

        private string morada;
        public string Morada
        {
            get { return morada; }

            set
            {
                morada = value;
                OnPropertyChanged(nameof(Morada));
            }
        }

        private string corMorada;
        public string CorMorada
        {
            get { return corMorada; }

            set
            {
                corMorada = value;
                OnPropertyChanged(nameof(CorMorada));
            }
        }

        private string horaAbertura;
        public string HoraAbertura
        {
            get { return horaAbertura; }

            set
            {
                horaAbertura = value;
                OnPropertyChanged(nameof(HoraAbertura));
            }
        }

        private string horaFecho;
        public string HoraFecho
        {
            get { return horaFecho; }

            set
            {
                horaFecho = value;
                OnPropertyChanged(nameof(HoraFecho));
            }
        }

        private string horario;
        public string Horario
        {
            get { return horario; }

            set
            {
                horario = value;
                OnPropertyChanged(nameof(Horario));
            }
        }

        private string corHorario;
        public string CorHorario
        {
            get { return corHorario; }

            set
            {
                corHorario = value;
                OnPropertyChanged(nameof(CorHorario));
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

        private string corDeFundo;
        public string CorDeFundo
        {
            get { return corDeFundo; }

            set
            {
                corDeFundo = value;
                OnPropertyChanged(nameof(CorDeFundo));
            }
        }

        public ObservableCollection<Produto> listaProdutos;

        public ObservableCollection<Produto> ListaProdutos
        {
            get { return listaProdutos; }
            set
            {
                listaProdutos = value;
                OnPropertyChanged(nameof(ListaProdutos));
            }
        }

        public bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public Command ShowLayoutCommand { get; }
        private readonly HttpClient _client;
        private readonly string _APIserver = "http://10.0.2.2:2626";
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");
       public Command<int> RefreshCommand { get; }
        public LayoutViewModel()
        {
            _client = new HttpClient();
            listaProdutos = new ObservableCollection<Produto>();
            RefreshCommand = new Command<int>((id) => RefreshLayout(id));
        }

        public void ShowLayout(Layout layout)
        {
            listaProdutos.Clear();

            Id = layout.Id;
            Titulo = layout.Titulo;
            CorTitulo = layout.CorTitulo;
            Descricao = layout.Descricao;
            CorDescricao = layout.CorDescricao;
            Morada = "Localização: " + layout.Morada;
            CorMorada = layout.CorMorada;
            HoraAbertura = string.Format("{0:00}: {1:00}", layout.HoraAbertura.Hours, layout.HoraAbertura.Minutes);
            HoraFecho = string.Format("{0:00}: {1:00}", layout.HoraFecho.Hours, layout.HoraFecho.Minutes);
            Horario = "Horário: " + HoraAbertura +" - " + HoraFecho;
            CorHorario = layout.CorHorario;
            CorDeFundo = layout.CorDeFundo;
            Banner = "http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoBanner;

            foreach (var produto in layout.Produtos)
            {
                listaProdutos.Add(new Produto { Imagem = "http://10.0.2.2:2626/api/cliente/imagegetlayout/" + produto.Foto, DescricaoProduto = produto.DescricaoProduto, Preco = "Preço: " + produto.Preco +" €", CorProduto = layout.CorTextoProdutos });
            }
            
            
        }

        public async void RefreshLayout(int Id)
        {
            IsRefreshing = true;
            listaProdutos.Clear();


            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await _client.GetAsync(_APIserver + "/api/cliente/Getlayoutnumero1/" + id);

            var responsebody = await response.Content.ReadAsStringAsync();

            var layout = JsonConvert.DeserializeObject<Layout>(responsebody);

            Id = layout.Id;
            Titulo = layout.Titulo;
            CorTitulo = layout.CorTitulo;
            Descricao = layout.Descricao;
            CorDescricao = layout.CorDescricao;
            Morada = "Localização: " + layout.Morada;
            CorMorada = layout.CorMorada;
            HoraAbertura = string.Format("{0:00}: {1:00}", layout.HoraAbertura.Hours, layout.HoraAbertura.Minutes);
            HoraFecho = string.Format("{0:00}: {1:00}", layout.HoraFecho.Hours, layout.HoraFecho.Minutes);
            Horario = "Horário: " + HoraAbertura + " - " + HoraFecho;
            CorHorario = layout.CorHorario;
            CorDeFundo = layout.CorDeFundo;
            Banner = "http://10.0.2.2:2626/api/cliente/imagegetlayout/" + layout.FotoBanner;

            foreach (var produto in layout.Produtos)
            {
                listaProdutos.Add(new Produto { Imagem = "http://10.0.2.2:2626/api/cliente/imagegetlayout/" + produto.Foto, DescricaoProduto = produto.DescricaoProduto, Preco = "Preço: " + produto.Preco + " €", CorProduto = layout.CorTextoProdutos });
            }

            OnPropertyChanged(nameof(IsRefreshing));
            IsRefreshing = false;

        }

        public void ShowLayoutDefault()
        {
            listaProdutos.Clear();

            Titulo = "Nome da empresa";
            Descricao = "Breve descrição da empresa";
            Morada = "Localização";
            Horario = "Horário: 00h - 00h";
            Banner = "http://10.0.2.2:2626/api/cliente/imagegetdefault/banner-default.jpg";
            corDeFundo = "White";

            listaProdutos.Add(new Produto { Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/produto-default.png", DescricaoProduto = "Produto 1", Preco = "Preço do produto" });
            listaProdutos.Add(new Produto { Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/produto-default.png", DescricaoProduto = "Produto 2", Preco = "Preço do produto" });
            listaProdutos.Add(new Produto { Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/produto-default.png", DescricaoProduto = "Produto 3", Preco = "Preço do produto" });

        }
    }
}
