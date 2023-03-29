using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Repuestos.Models;
using Xamarin.Forms.Xaml.Diagnostics;

namespace Repuestos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuppliesPage : ContentPage
    {
        public SuppliesPage()
        {
            InitializeComponent();
            LlenarDatos();
        }
        public void LimpiarControles()
        {
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtIdProducto.Text = "";            
        }
        public async void LlenarDatos()
        {
            var productoList = await App.SQLiteDBProduct.GetProductoAsync();
            if (productoList != null)
            {
                lstRepuestos.ItemsSource = productoList;
            }
        }
        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;
            }            
            else if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                respuesta = false;
            }            
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        private async void btnCargar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Product producto = new Product
                {
                    CodigoProducto = txtCodigo.Text,
                    DescripcionProducto = txtDescripcion.Text,                    
                    PrecioProducto = Convert.ToDecimal(txtPrecio.Text),
                };
                await App.SQLiteDBProduct.SaveProductoAsync(producto);

                await DisplayAlert("Atención", "Nuevo repuesto cargado exitosamente, ya se encuentra disponible para hacer su pedido", "OK");
                LimpiarControles();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingrese todos los datos", "OK");
            }
        }
        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(txtIdProducto.Text))
                {
                    Product producto = new Product()
                    {
                        IdProducto = Convert.ToInt32(txtIdProducto.Text),
                        CodigoProducto = txtCodigo.Text,
                        DescripcionProducto = txtDescripcion.Text,                        
                        PrecioProducto = Convert.ToDecimal(txtPrecio.Text),                        
                    };
                    await App.SQLiteDBProduct.SaveProductoAsync(producto);
                    await DisplayAlert("Registro", "Se actualizo de manera exitosa el repuesto", "Ok");
                    LimpiarControles();
                    btnActualizar.IsVisible = false;
                    btnCargar.IsVisible = true;
                    LlenarDatos();
                }
            }
        }
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var producto = await App.SQLiteDBProduct.GetProductosByIdAsync(Convert.ToInt32(txtIdProducto.Text));
            if (producto != null)
            {
                await App.SQLiteDBProduct.DeleteProductoAsync(producto);
                await DisplayAlert("Producto", "Se elimino de manera exitosa", "Ok");
                LimpiarControles();
                LlenarDatos();
                txtIdProducto.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnCargar.IsVisible = true;
            }
        }
        private async void lstRepuestos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Product)e.SelectedItem;
            btnCargar.IsVisible = false;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdProducto.ToString()))
            {
                var producto = await App.SQLiteDBProduct.GetProductosByIdAsync(obj.IdProducto);
                if (producto != null)
                {
                    txtIdProducto.Text = producto.IdProducto.ToString();
                    txtCodigo.Text = producto.CodigoProducto;
                    txtDescripcion.Text = producto.DescripcionProducto;
                    txtPrecio.Text = producto.PrecioProducto.ToString();                    
                }
            }
        }
    }
}