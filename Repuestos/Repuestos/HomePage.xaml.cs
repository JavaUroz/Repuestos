using Repuestos;
using Repuestos.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Repuestos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            LlenarDatos();
        }
        public void LimpiarControles()
        {
            txtIdCliente.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtCuit.Text = "";
            txtLocalidad.Text = "";
            txtEmail.Text = "";
            txtRazonSocial.Text = "";
            txtTelefono.Text = "";
            txtVendedor.Text = "";
        }
        public async void LlenarDatos()
        {
            var clienteList = await App.SQLiteDBClient.GetClientesAsync();
            if (clienteList != null)
            {
                lstClientes.ItemsSource = clienteList;
            }
        }
        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellido.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtLocalidad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtVendedor.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Client cliente = new Client
                {
                    CuitCliente = txtCuit.Text,
                    NombreCliente = txtNombre.Text,
                    ApellidoCliente = txtApellido.Text,
                    RazonSocial = txtRazonSocial.Text,
                    Email = txtEmail.Text,
                    Telefono = txtTelefono.Text,
                    Localidad = txtLocalidad.Text,
                    Vendedor = txtVendedor.Text,
                };
                await App.SQLiteDBClient.SaveClienteAsync(cliente);

                await DisplayAlert("Atención", "Cliente cargado exitosamente, haga ahora su pedido de repuestos", "OK");
                LimpiarControles();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingrese todos los datos", "OK");
            }
        }
        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                Client cliente = new Client()
                {
                    IdCliente = Convert.ToInt32(txtIdCliente.Text),
                    CuitCliente = txtCuit.Text,
                    RazonSocial = txtRazonSocial.Text,
                    NombreCliente = txtNombre.Text,
                    ApellidoCliente = txtApellido.Text,
                    Email = txtEmail.Text,
                    Telefono = txtTelefono.Text,
                    Localidad = txtLocalidad.Text,
                    Vendedor = txtVendedor.Text,
                };
                await App.SQLiteDBClient.SaveClienteAsync(cliente);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el cliente", "Ok");
                LimpiarControles();
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                LlenarDatos();
            }
        }
        private async void lstClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Client)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdCliente.ToString()))
            {
                var cliente = await App.SQLiteDBClient.GetClienteByIdAsync(obj.IdCliente);
                if (cliente != null)
                {
                    txtIdCliente.Text = cliente.IdCliente.ToString();
                    txtCuit.Text = cliente.CuitCliente;
                    txtRazonSocial.Text = cliente.RazonSocial;
                    txtNombre.Text = cliente.NombreCliente;
                    txtApellido.Text = cliente.ApellidoCliente;
                    txtEmail.Text = cliente.Email;
                    txtTelefono.Text = cliente.Telefono;
                    txtLocalidad.Text = cliente.Localidad;
                    txtVendedor.Text = cliente.Vendedor;
                }
            }
        }
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var cliente = await App.SQLiteDBClient.GetClienteByIdAsync(Convert.ToInt32(txtIdCliente.Text));
            if (cliente != null)
            {
                await App.SQLiteDBClient.DeleteClienteAsync(cliente);
                await DisplayAlert("Cliente", "Se elimino de manera exitosa", "Ok");
                LimpiarControles();
                LlenarDatos();
                txtIdCliente.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;
            }
        }
    }
}