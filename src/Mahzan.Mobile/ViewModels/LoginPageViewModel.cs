using Mahzan.Mobile.API.Interfaces.AspNetUsers;
using Mahzan.Mobile.API.Requests.AspNetUsers;
using Mahzan.Mobile.API.Results.AspNetUsers;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Mahzan.Mobile.Views;
using Mahzan.Mobile.Views.Members;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IAspNetUsersService _aspNetUsersService;

        private readonly IRepository<AspNetUsers> _repository;

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand { get; set; }

        public LoginPageViewModel(
            INavigationService navigationService,
            IAspNetUsersService aspNetUsersService,
            IRepository<AspNetUsers> repository)
            : base(navigationService)
        {
            Title = "Inicio de Sesión";

            //Services
            _aspNetUsersService = aspNetUsersService;

            //Repositories
            _repository = repository;

            //Navigation
            _navigationService = navigationService;

            //Commands
            LoginCommand = new Command(async () => await LogIn());
        }


        public async Task LogIn()
        {
            LogInResult logInResult = await _aspNetUsersService.LogIn(new LogInRequest
            {
                UserName = "mahzan",
                Password = "Mahzan20%"
            });

            if (logInResult.IsValid)
            {
                await InertSqlite(logInResult);

                switch (logInResult.Role)
                {
                    case "MEMBER":
                        //await NavigationService.NavigateAsync(nameof(MainPage) + "/" + nameof(NavigationPage) + "/" + nameof(SelectStorePage));
                        await NavigationService.NavigateAsync("SelectStorePage");
                        break;
                    case "EMPLOYEE_CASHIER":

                        break;
                    default:
 
                        break;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(logInResult.Title, logInResult.Message, "ok");
            }
        }

        private async Task InertSqlite(LogInResult logInResult)
        {
            //Elimina datos previos
            await _repository.DeleteAll();

            //Agrega Nuevos Datos
            await _repository.Insert(new AspNetUsers()
            {
                UserName = logInResult.UserName,
                Role = logInResult.Role,
                AspNetUsersId = logInResult.AspNetUsersId,
                Token = logInResult.Token,
                EmployeesId = logInResult.EmployeesId
            });

        }
    }
}
