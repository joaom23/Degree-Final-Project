using Projeto_CMS.Helpers;
using Projeto_CMS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Projeto_CMS.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public bool IsDebug = false;
        public Command LoginPageCommnad { get; }
        public Command RegisterPageCommnad { get; }

        private ImageSource imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/smartphone.jpg";

        public ImageSource Imagem
        {
            get { return imagem; }
            set
            {
                imagem = value;
                OnPropertyChanged(nameof(Imagem));
            }
        }

        public MainPageViewModel()
        {
            LoginPageCommnad = new Command(LoginPage);
            RegisterPageCommnad = new Command(RegisterPage);
            Imagem = "http://10.0.2.2:2626/api/cliente/imagegetdefault/smartphone.jpg";
        }

        private async void LoginPage()
        {
            //Apenas para debug 
            if (IsDebug == true)
            {
                Preferences.Set("Id", "591ec429-d8dc-4238-9269-c03528fb8074");
                Preferences.Set("Token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImptZmVybmFuZGVzOEBzYXBvLnB0IiwiSWQiOiJjYTY0NjJiZS1lMTBjLTQ3NzMtOGE0NS1jMmE2NjIyMDQ4YzYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiY2E2NDYyYmUtZTEwYy00NzczLThhNDUtYzJhNjYyMjA0OGM2IiwiRm90byI6ImxldmljYTY0NjJiZS1lMTBjLTQ3NzMtOGE0NS1jMmE2NjIyMDQ4YzYyMTAxMDk5MzEuanBnIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQ2xpZW50ZSIsImV4cCI6MTYyNjQ2MzMwMiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDoxMDM4Mi8iLCJhdWQiOiJVc2VyIn0.tacLze5Pegbj2__SDEd8s5ospgoPe4oahnYHKHy4fig");
                Preferences.Set("ImagemUser", "leviba92356a-b607-4673-9dc8-b8aa84a1f946211233323.jpg");

                await App.Current.MainPage.Navigation.PushAsync(new Primeira());
            }
            else
            {
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
          
        } 

        private async void RegisterPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
        }
    }
}
