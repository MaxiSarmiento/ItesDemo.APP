using ItesDemo.APP.Models;
using ItesDemo.APP.Services;
using ItesDemo.APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItesDemo.APP.ViewModels
{
    public class ProductoListaViewModel : BaseViewModel
    {
        private bool isRefreshing;
        private ProductoModel productoSeleccion;
        private ObservableCollection<ProductoModel> productos = new ObservableCollection<ProductoModel>();
        private ObservableCollection<ProductoModel> originalProductos = new ObservableCollection<ProductoModel>();
        private string searchText = "";
     


        public ProductoListaViewModel()
        {
            Title = "Lista Productos";

            IsRefreshing = true; 

            Task.Run(async () => { await ConsultarApi(); }).Wait();

            RefreshCommand = new Command(async () =>
            {
                if (IsBusy)
                {
                    return;
                }

                await ConsultarApi();
            });
            PerformSearchCommand = new Command(PerformSearch);
        }


        public ObservableCollection<ProductoModel> Productos
        {
            get { return productos; }
            set { SetProperty(ref productos, value); }
        }

        public ProductoModel ProductoSeleccion
        {
            get { return productoSeleccion; }
            set { SetProperty(ref productoSeleccion, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (SetProperty(ref searchText, value))
                {
                    PerformSearchCommand.Execute(null); 
                }
            }
        }


        public ICommand GoToCancelarCommand => new Command(() => GoToCancelar());
        public ICommand GoToDetailCommand => new Command(async () => await GoToDetailAsync());
        public ICommand RefreshCommand { get; set; }
        public ICommand PerformSearchCommand { get; internal set; }

        private async Task ConsultarApi()
        {
            IsBusy = IsRefreshing = true;
            Productos.Clear();
            var apiClient = new ApiClient();
            var lista = await apiClient.ObtenerProductos();
            originalProductos = new ObservableCollection<ProductoModel>(lista);
            PerformSearch();
            IsBusy = IsRefreshing = false;
        }

        private void GoToCancelar()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task GoToDetailAsync()
        {
            if (ProductoSeleccion != null)
            {
                var productoDetalleViewModel = new ProductoDetalleViewModel();
                productoDetalleViewModel.ProductoModel = ProductoSeleccion;
                var productoDetallePage = new ProductoDetallePage { BindingContext = productoDetalleViewModel };
                await Application.Current.MainPage.Navigation.PushAsync(productoDetallePage);
            }
        }

        private void PerformSearch()
        {
            var filteredProducts = originalProductos.ToList();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.nombre.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Productos.Clear();
            foreach (var product in filteredProducts)
            {
                Productos.Add(product);
            }
        }
    }
}
