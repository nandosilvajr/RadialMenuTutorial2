using RadialMenuTutorial.Data;

namespace RadialMenuTutorial;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MenuTransactionsService menuTransactionsService = new MenuTransactionsService();

		

		MainPage = new NavigationPage(new MainPage());



	}
}
