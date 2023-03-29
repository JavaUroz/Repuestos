using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repuestos.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int IdProducto { get; set; }
        [MaxLength(50)]
        public string CodigoProducto { get; set; }
        [MaxLength(50)]
        public string DescripcionProducto { get; set; }               
        public decimal PrecioProducto { get; set; }        
    }
}