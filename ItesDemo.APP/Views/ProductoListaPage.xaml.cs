using ItesDemo.APP.ViewModels;

namespace ItesDemo.APP.Views;

public partial class ProductoListaPage : ContentPage
{
	public ProductoListaPage()
	{
		InitializeComponent();

		// ProductoListaViewModel viewModel = new ProductoListaViewModel();
		// BindingContext = viewModel;
	}
    private async void OnVerListaClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InicioPage());
    }

}