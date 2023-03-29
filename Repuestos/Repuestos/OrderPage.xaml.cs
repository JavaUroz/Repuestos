using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repuestos.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repuestos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
            LlenarDatos();
        }
        public void LimpiarControles()
        {
            txtCantidad.Text = "";
            lstClientes.SelectedIndex = -1;
            lstProductos.SelectedIndex = -1;
        }
        public async void LlenarDatos()
        {
            var clienteList = await App.SQLiteDBClient.GetClientesAsync();
            if (clienteList != null)
            {
                lstClientes.ItemsSource = clienteList;
            }

            var productoList = await App.SQLiteDBProduct.GetProductoAsync();
            if (productoList != null)
            {
                lstProductos.ItemsSource = productoList;
            }

            var pedidosList = await App.SQLiteDBOrders.GetOrderAsync();
            if (pedidosList != null)
            {
                lstPedidos.ItemsSource = pedidosList;
            }           
        }

        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                respuesta = false;
            }            
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        private async void btnCargarPedido_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Order order = new Order
                {
                    Cliente = lstClientes.Items[lstClientes.SelectedIndex],
                    Producto = lstProductos.Items[lstProductos.SelectedIndex],
                    Cantidad = Convert.ToInt32(txtCantidad.Text),                    
                };
                await App.SQLiteDBOrders.SaveOrderAsync(order);

                await DisplayAlert("Atención", "Pedido cargado exitosamente", "OK");
                LimpiarControles();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingrese todos los datos", "OK");
            }
        }
        private async void btnActualizarPedido_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text))
            {
                Order order = new Order()
                {                    
                    IdOrder = Convert.ToInt32(txtIdOrder.Text),
                    Cliente = lstClientes.Items[lstClientes.SelectedIndex],
                    Producto = lstProductos.Items[lstProductos.SelectedIndex],
                    Cantidad = Convert.ToInt32(txtCantidad.Text),
                };
                await App.SQLiteDBOrders.SaveOrderAsync(order);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el pedido", "Ok");
                LimpiarControles();
                btnActualizarPedido.IsVisible = false;
                btnCargarPedido.IsVisible = true;
                LlenarDatos();
            }
        }
        private async void lstPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Order)e.SelectedItem;
            btnCargarPedido.IsVisible = false;
            btnActualizarPedido.IsVisible = true;
            btnEliminarPedido.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdOrder.ToString()))
            {
                var order = await App.SQLiteDBOrders.GetOrdersByIdAsync(obj.IdOrder);
                if (order != null)
                {
                    txtIdOrder.Text = order.IdOrder.ToString();
                    lstClientes.SelectedItem = order.Cliente;
                    lstProductos.SelectedItem = order.Producto;
                    txtCantidad.Text = order.Cantidad.ToString();                    
                }
            }
        }
        private async void btnEliminarPedido_Clicked(object sender, EventArgs e)
        {
            var order = await App.SQLiteDBOrders.GetOrdersByIdAsync(Convert.ToInt32(txtIdOrder.Text));
            if (order != null)
            {
                await App.SQLiteDBOrders.DeleteOrderAsync(order);
                await DisplayAlert("Pedido", "Se elimino de manera exitosa", "Ok");
                LimpiarControles();
                LlenarDatos();
                txtIdOrder.IsVisible = false;
                btnActualizarPedido.IsVisible = false;
                btnEliminarPedido.IsVisible = false;
                btnCargarPedido.IsVisible = true;
            }
        }    
    }
}

