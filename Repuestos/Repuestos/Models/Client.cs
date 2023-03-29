using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Repuestos.Models
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int IdCliente { get; set; }
        [MaxLength(50)]
        public string CuitCliente { get; set; }
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        [MaxLength(50)]
        public string NombreCliente { get; set; }
        [MaxLength(50)]
        public string ApellidoCliente { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Telefono { get; set; }
        [MaxLength(50)]
        public string Localidad { get; set; }
        [MaxLength(50)]
        public string Vendedor { get; set; }
    }
}
