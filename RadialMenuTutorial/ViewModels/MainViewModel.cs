using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RadialMenuTutorial.Data;
using RadialMenuTutorial.Model;
using RadialMenuTutorial.Pages;
using RadialMenuTutorial.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadialMenuTutorial.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MainMenuOptionsList))]
        ObservableCollection<MenuItemModel> _mainMenuOptions;

        public ObservableCollection<MenuItemModel> MainMenuOptionsList { get => _mainMenuOptions; set => _mainMenuOptions = value; }

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null) 
                return new MainViewModel();
            else
                return instance;
        }


        [RelayCommand]
        private async void OpenEditMenuPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditMenuPage());
        }

        public MainViewModel()
        {

            MenuTransactionsService menuTransactionsService = new MenuTransactionsService();

            var task = Task.Run( async () => await menuTransactionsService.GetMainMenuListAsync());

            if(task.Result != null)
            {
                var menuItems = task.Result;

                MainMenuOptionsList = new ObservableCollection<MenuItemModel>(menuItems);
            }
           instance = this;
        }
    }
}
