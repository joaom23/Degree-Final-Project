using Newtonsoft.Json;
using Projeto_CMS.Helpers;
using Projeto_CMS.Models;
using Projeto_CMS.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Projeto_CMS.ViewModels
{
   public class LoginViewModel : BaseViewModel
    {

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
        
                username = value;
                OnPropertyChanged(nameof(Username));
                LoginCommand?.ChangeCanExecute();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {

                password = value;
                OnPropertyChanged(nameof(Password));
                LoginCommand?.ChangeCanExecute();
            }
        }

        public Command LoginCommand { get; }
        private readonly HttpClient _client;
        private readonly string _APIserver = "http://10.0.2.2:2626";
        //private readonly string _APIserver = "http://10.0.2.2:41597";

        public LoginViewModel()
        {
            _client = new HttpClient();
            //LoginCommand = new Command(Login,CanExecuteLogin);
            LoginCommand = new Command(async () => { await Login(); }, CanExecuteLogin);

        }

       public async Task Login()
        {
            UserLogin user = new UserLogin();

            user.Email = Username;
            user.Password = Password;

            var data = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented),
                    Encoding.UTF8,
                    "application/json");

            var response = await _client.PostAsync(_APIserver + "/api/utilizadores/login", data);

            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

           

            if (responseObject.IsSucess)
            {

                var token = responseObject.Message;

                var readToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var userId = readToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;

                var ImagemUser = readToken.Claims.FirstOrDefault(x => x.Type == "Foto").Value;

                Preferences.Set("ImagemUser", ImagemUser);
                Preferences.Set("Id", userId);
                Preferences.Set("Token", responseObject.Message);


                await App.Current.MainPage.Navigation.PushAsync(new Primeira());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Login falhou", "Ok");
                Password = "";
            }
        }

        private bool CanExecuteLogin()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
            {
                return false;
            }

            return true;
        }
    }
}
