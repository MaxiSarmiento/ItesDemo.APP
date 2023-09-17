using ItesDemo.APP.ViewModels;

namespace ItesDemo.APP.Views
{
    public partial class ProductoDetallePage : ContentPage
    {
        public ProductoDetallePage()
        {
            InitializeComponent();
            BindingContext = new ProductoDetalleViewModel();
        }
        private async void OnVerListaClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductoListaPage());
        }
    }
}