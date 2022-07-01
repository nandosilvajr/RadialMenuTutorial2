using RadialMenuTutorial.Model;
using RadialMenuTutorial.ViewModels;
using System.Collections.ObjectModel;

namespace RadialMenuTutorial;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (MainViewModel.GetInstance().MainMenuOptionsList != null)
        {
            InitializeComponent();
        }
    }

}
