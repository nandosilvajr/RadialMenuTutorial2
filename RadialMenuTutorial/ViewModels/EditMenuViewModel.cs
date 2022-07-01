using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RadialMenuTutorial.Data;
using RadialMenuTutorial.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadialMenuTutorial.ViewModels
{
    public partial class EditMenuViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MainMenuOptionsList))]
        ObservableCollection<MenuItemModel> _mainMenuOptions;

        public ObservableCollection<MenuItemModel> MainMenuOptionsList { get => _mainMenuOptions; set => _mainMenuOptions = value; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EditMenuOptionsList))]
        ObservableCollection<MenuItemModel> _editMenuOptions;

        public ObservableCollection<MenuItemModel> EditMenuOptionsList { get => _editMenuOptions; set => _editMenuOptions = value; }

        public EditMenuViewModel()
        {
            _mainMenuOptions = MainViewModel.GetInstance().MainMenuOptionsList;

            LoadData();

            instance = this;

        }
        [RelayCommand]
        void DragOption(object obj)
        {

        }

        private static EditMenuViewModel instance;

        public static EditMenuViewModel GetInstance()
        {
            if (instance == null) 
                return new EditMenuViewModel();
            else
                return instance;
        }

        private void LoadData()
        {

            MenuTransactionsService menuTransactionsService = new MenuTransactionsService();

            var task = Task.Run(async () => await menuTransactionsService.GetEditOptionsListAsync());
            task.Wait();

            if (task.Result != null)
            {
                var optionItems = task.Result;
                EditMenuOptions = new ObservableCollection<MenuItemModel>(optionItems);
            }
        }
    }
}
