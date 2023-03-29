using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Repuestos.Data;
using System.IO;

namespace Repuestos
{
    public partial class App : Application
    {
        static SQLiteDBClient dbCliente;
        static SQLiteDBProduct dbProduct;
        static SQLiteDBOrders dbOrder;
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
        public static SQLiteDBClient SQLiteDBClient
        {
            get
            {
                if (dbCliente == null)
                {
                    dbCliente = new SQLiteDBClient(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Clientes.db3"));
                }
                return dbCliente;
            }
        }
        public static SQLiteDBProduct SQLiteDBProduct
        {
            get
            {
                if (dbProduct == null)
                {
                    dbProduct = new SQLiteDBProduct(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Productos.db3"));
                }
                return dbProduct;
            }
        }
        public static SQLiteDBOrders SQLiteDBOrders
        {
            get
            {
                if (dbOrder == null)
                {
                    dbOrder = new SQLiteDBOrders(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Orders.db3"));
                }
                return dbOrder;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
