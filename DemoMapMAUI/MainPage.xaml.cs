using DemoMapMAUI.ViewModel;

namespace DemoMapMAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext=mainPageViewModel;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		
	}
}

