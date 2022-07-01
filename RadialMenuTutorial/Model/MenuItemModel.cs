using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadialMenuTutorial.Model
{
    public class MenuItemModel
    {
        public MenuItemModel(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        public class MainMenuListModel
        {
            [JsonProperty("mainMenuList")]
            public List<MenuItemModel> MainMenuList { get; set; }
        }
    }
}
