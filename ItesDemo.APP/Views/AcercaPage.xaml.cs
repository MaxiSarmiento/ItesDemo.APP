namespace ItesDemo.APP.Views;

public partial class AcercaPage : ContentPage
{
	public AcercaPage()
	{
		InitializeComponent();
	}
    private async void OnGitHubButtonClicked(object sender, EventArgs e)
    {    
        await Launcher.OpenAsync(new Uri("https://github.com/MaxiSarmiento"));
    }
    private async void OnLinkedinButtonClicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://www.linkedin.com/in/dsmaxi/"));
    }
}