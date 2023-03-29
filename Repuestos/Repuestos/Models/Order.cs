using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Repuestos.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int IdOrder { get; set; }        
        public string Cliente { get; set; }        
        public string Producto { get; set; }
        public int Cantidad { get; set; }       
    }
}
