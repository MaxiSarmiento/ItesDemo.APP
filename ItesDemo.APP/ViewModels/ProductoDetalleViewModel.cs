using CommunityToolkit.Mvvm.ComponentModel;
using ItesDemo.APP.Models;
using ItesDemo.APP.Views;
namespace ItesDemo.APP.ViewModels
{

    public partial class ProductoDetalleViewModel : ObservableObject
    {
        private ProductoModel productoModel;

        public ProductoModel ProductoModel
        {
            get { return productoModel; }
            set { SetProperty(ref productoModel, value); }
        }
        private async Task GoToBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
           

        }
    }
    }
