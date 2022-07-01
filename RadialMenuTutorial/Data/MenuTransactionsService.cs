using Newtonsoft.Json;
using RadialMenuTutorial.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RadialMenuTutorial.Model.MenuItemModel;

namespace RadialMenuTutorial.Data
{
    public class MenuTransactionsService
    {
        public MenuTransactionsService()
        {
            bool mainMenuListKey = Preferences.ContainsKey("mainMenuList");
            if(!mainMenuListKey)
            {
                Preferences.Default.Set("mainMenuList", "[{\"name\":\"Menu 1\",\"icon\":\"bot_menu\"},{\"name\":\"Menu 2\",\"icon\":\"bot_menu\"},{\"name\":\"Menu 3\",\"icon\":\"bot_menu\"},{\"name\":\"Menu 4\",\"icon\":\"bot_menu\"},{\"name\":\"Menu 5\",\"icon\":\"bot_menu\"}]");
            }

            bool editOptionsListKey = Preferences.ContainsKey("editOptionsList");

            if (!editOptionsListKey)
            {
                Preferences.Default.Set("editOptionsList", "[{\"name\":\"Option 1\",\"icon\":\"bot_option\"},{\"name\":\"Option 2\",\"icon\":\"bot_option\"},{\"name\":\"Option 3\",\"icon\":\"bot_option\"},{\"name\":\"Option 4\",\"icon\":\"bot_option\"},{\"name\":\"Option 5\",\"icon\":\"bot_option\"}]");
            }
        }

        public void ChangeMainMenuList(ObservableCollection<MenuItemModel> mainMenuChanged)
        {
            bool hasKey = Preferences.ContainsKey("mainMenuList");

            if (hasKey && mainMenuChanged.Count > 0)
            {

                var mainMenuListModel = JsonConvert.SerializeObject(mainMenuChanged);

                Preferences.Default.Set("mainMenuList", mainMenuListModel);

            }
        }
        public void ChangeEditOptionsList(ObservableCollection<MenuItemModel> editOptionsChanged)
        {
            bool hasKey = Preferences.ContainsKey("editOptionsList");

            if (hasKey && editOptionsChanged.Count > 0)
            {

                var editOptionsListModel = JsonConvert.SerializeObject(editOptionsChanged);

                Preferences.Default.Set("editOptionsList", editOptionsListModel);

            }
        }

        public Task<List<MenuItemModel>> GetMainMenuListAsync()
        {
            bool hasKey = Preferences.ContainsKey("mainMenuList");

            if (hasKey)
            {
                string mainMenuString = Preferences.Get("mainMenuList", "Unknown");

                var mainMenuListModel = JsonConvert.DeserializeObject<List<MenuItemModel>>(mainMenuString);

                var mainMenuList = new List<MenuItemModel>(mainMenuListModel);

                return Task.Run(() => mainMenuList);
            }
            return null;
        }
        public Task<List<MenuItemModel>> GetEditOptionsListAsync()
        {
            bool hasKey = Preferences.ContainsKey("editOptionsList");

            if (hasKey)
            {
                string editOptionsString = Preferences.Get("editOptionsList", "Unknown");

                var editOptionsListModel = JsonConvert.DeserializeObject<List<MenuItemModel>>(editOptionsString);

                var EditOptionsList = new List<MenuItemModel>(editOptionsListModel);

                return Task.Run(() => EditOptionsList);
            }
            return null;
        }
    }
}
