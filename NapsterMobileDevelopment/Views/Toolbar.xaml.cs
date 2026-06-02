using NapsterMobileDevelopment.Services;

namespace NapsterMobileDevelopment.Views;

public partial class Toolbar : ContentView
{

    public Toolbar()
	{
		InitializeComponent();
	}


    public async void SearchPageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//SearchPage");
    }

    public async void HomePageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//HomePage");
    }
}