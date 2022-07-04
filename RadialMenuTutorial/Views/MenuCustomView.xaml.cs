using RadialMenuTutorial.Data;
using RadialMenuTutorial.Model;
using RadialMenuTutorial.Pages;
using RadialMenuTutorial.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RadialMenuTutorial.Views;

public partial class MenuCustomView : ContentView
{
    public static BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource),
    typeof(ObservableCollection<MenuItemModel>),
    typeof(MenuCustomView),
    defaultValue: new ObservableCollection<MenuItemModel>(),
    defaultBindingMode: BindingMode.TwoWay,
    propertyChanged: ItemSourcePropertyChanged);

    public ObservableCollection<MenuItemModel> ItemSource
    {
        get => (ObservableCollection<MenuItemModel>)GetValue(ItemSourceProperty);
        set
        {
            SetValue(ItemSourceProperty, value);
        }
    }

    private static void ItemSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (MenuCustomView)bindable;
        control.ItemSource = (ObservableCollection<MenuItemModel>)newValue;
    }

    public static readonly BindableProperty RadiusProperty = BindableProperty.Create(nameof(Radius),
    typeof(int),
    typeof(MenuCustomView),
    300);

    public int Radius
    {
        get => (int)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    public MenuCustomView()
	{
		InitializeComponent();

        ItemSource = MainViewModel.GetInstance().MainMenuOptionsList;

        UpdateViews();
    }

    private void UpdateViews()
    {
        const int offset = 210;

        var sweepingAngle = 360 / ItemSource.Count;

        var gridSize = 100;

        var radius = 0.2;

        double x = 0;
        double y = 0;

        double centerX = 0.5;
        double centerY = 0.5;

        AbsoluteLayout views = new AbsoluteLayout();

        for (int i = 0; i < 5; i++)
        {
            var degrees = i * sweepingAngle;

            x = radius * Math.Cos((degrees - offset) * (Math.PI / 180)) + centerX;

            y = radius * Math.Sin((degrees - offset) * (Math.PI / 180)) + centerY;

            Grid grid = new Grid();
            grid.HorizontalOptions = LayoutOptions.Center;

            var dropGestureRecognizer = new DropGestureRecognizer();

            dropGestureRecognizer.AllowDrop = true;
            dropGestureRecognizer.Drop += (s, e) =>
            {
                DropGestureRecognizer_Drop(s, e);
            };

            grid.GestureRecognizers.Add(dropGestureRecognizer);

            Label label = new Label();

            label.VerticalOptions = LayoutOptions.End;
            label.WidthRequest = 100;
            label.LineBreakMode = LineBreakMode.WordWrap;
            label.HorizontalTextAlignment = TextAlignment.Center;

            Image image = new Image();
            image.Source = this.ItemSource[i].Icon;
            image.HeightRequest = 60;
            image.WidthRequest = 60;

            grid.Add(image);
            grid.Add(label);
            grid.HeightRequest = 100;
            grid.WidthRequest = 100;

            label.Text = this.ItemSource[i].Name;
            label.TextColor = Colors.Black;

            AbsoluteLayout.SetLayoutBounds(grid, new Rect(x, y , gridSize, gridSize));
            AbsoluteLayout.SetLayoutFlags(grid, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);

            views.Add(grid);

           
            

            Debug.WriteLine($"{x}:{y}");

        }

        views.WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width * 0.6;
        views.HeightRequest  = DeviceDisplay.Current.MainDisplayInfo.Width * 0.6;
        views.SetLayoutBounds(views, new Rect(0.5, 0.5, gridSize, gridSize));
        views.SetLayoutFlags(views, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        Content = new AbsoluteLayout
        {
            Children = { views }
        };
    }

    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {
        var obj = (sender as Element)?.Parent as Grid;

        if (obj != null)
        {

            var getImage = obj.Children[0] as Image;
            var imageSource = getImage.Source as FileImageSource;

            var getLabel = obj.Children[1] as Label;
            // That receive on Top
            var oldMenuItem = new MenuItemModel(getLabel.Text, imageSource.File);

            int getIndex = -1;

            for (int i = 0; i < this.ItemSource.Count; i++)
            {
                if (this.ItemSource[i].Name == oldMenuItem.Name && this.ItemSource[i].Icon == oldMenuItem.Icon) getIndex = i;
            }

            // That give from bottom 
            MenuItemModel newMenuItem = new MenuItemModel(e.Data.Properties["Name"].ToString(), e.Data.Properties["Icon"].ToString());

            // List of top items
            var mainMenuItemList = MainViewModel.GetInstance().MainMenuOptionsList;

            // List of bottom items
            var editMenuItemList = EditMenuViewModel.GetInstance().EditMenuOptionsList;

            int getIndexFromCollection = -1;

            for (int i = 0; i < editMenuItemList.Count; i++)
            {
                if (editMenuItemList[i].Name == newMenuItem.Name && editMenuItemList[i].Icon == newMenuItem.Icon) getIndexFromCollection = i;
            }


            var checkItem = mainMenuItemList.Where(x => x.Name == newMenuItem.Name && x.Icon == newMenuItem.Icon).FirstOrDefault();

            if (checkItem == null)
            {
                // The top top receive the update
                getImage.Source = newMenuItem.Icon;
                getLabel.Text = newMenuItem.Name;

                try
                {

                    MenuTransactionsService menuTransactionsService = new MenuTransactionsService();

                    mainMenuItemList.RemoveAt(getIndex);
                    mainMenuItemList.Insert(getIndex, newMenuItem);

                    // Update preferences to main menu
                    menuTransactionsService.ChangeMainMenuList(mainMenuItemList);

                    editMenuItemList.RemoveAt(getIndexFromCollection);
                    editMenuItemList.Insert(getIndexFromCollection, oldMenuItem);
                    
                    // Update preferences to edit options
                    menuTransactionsService.ChangeEditOptionsList(editMenuItemList);

                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

    }


}