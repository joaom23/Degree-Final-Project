using Projeto_CMS.Helpers;
using Projeto_CMS.Models;
using Projeto_CMS.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Xamarin.Forms;

namespace Projeto_CMS.ViewModels
{
   public class RegistrationViewModel : BaseViewModel
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { 
                email = value;
                OnPropertyChanged(nameof(Email));
                RegistrationCommand?.ChangeCanExecute();
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
                RegistrationCommand?.ChangeCanExecute();
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
                RegistrationCommand?.ChangeCanExecute();
            }
        }
        public Command RegistrationCommand { get; }

        public RegistrationViewModel()
        {
        
            RegistrationCommand = new Command(Registar, CanExecuteRegister);
            //comando para registar aqui
        }

        private async void Registar()
        {
            if (!IsMailValid(Email))
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Email inválido", "Ok");

                Email = string.Empty;

                return;
            }

            User u = new User();

            u.Email = Email;
            u.Username = Username;
            u.Password = Password;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();

                User ut = conn.Table<User>().FirstOrDefault(x => x.Email == Email || x.Username == Username);

                if(ut != null)
                {
                    if(ut.Email == Email && ut.Username != Username)
                    {
                        await App.Current.MainPage.DisplayAlert("Falha no Registo", $"O email {Email} ja foi utilizado", "Ok");
                        Email = string.Empty;
                        return;
                    }

                    if(ut.Username == Username && ut.Email != Email)
                    {
                        await App.Current.MainPage.DisplayAlert("Falha no Registo", $"O username {Username} ja foi utilizado", "Ok");
                        Username = string.Empty;
                        return;
                    }

                    if (ut.Username == Username && ut.Email == Email)
                    {
                        await App.Current.MainPage.DisplayAlert("Falha no Registo", $"O username {Username} e o email {Email} ja foram utilizados", "Ok");
                        Email = string.Empty;
                        Username = string.Empty;
                        return;
                    }

                    
                    
                    //Password = string.Empty;

                   // return;
                }

                conn.CreateTable<User>();
                int rows = conn.Insert(u);
              
                if (rows > 0)
                {
                    //await App.Current.MainPage.DisplayAlert("Sucesso", "User adicionado", "Ok");
                   await  App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    
                }
            }
            //codigo para direcionar para a main page do user

        }

        private bool IsMailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool CanExecuteRegister()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email))
            {
                return false;
            }

            return true;
        }


    }
}
