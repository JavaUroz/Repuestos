using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Repuestos.Models;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Repuestos.Data
{
    public class SQLiteDBProduct
    {
        SQLiteAsyncConnection db;
        public SQLiteDBProduct(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Product>().Wait();
        }
        /// <summary>
        /// Guarda Producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public Task<int> SaveProductoAsync(Product producto)
        {
            if (producto.IdProducto != 0)
            {
                return db.UpdateAsync(producto);
            }
            else
            {
                return db.InsertAsync(producto);
            }
        }
        /// <summary>
        /// Elimina Producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public Task<int> DeleteProductoAsync(Product producto)
        {
            return db.DeleteAsync(producto);
        }
        /// <summary>
        ///     Recuperar todos los productos
        /// </summary>
        /// <returns></returns>

        public Task<List<Product>> GetProductoAsync()
        {
            return db.Table<Product>().ToListAsync();
        }
        /// <summary>
        ///     Recuperar producto por id
        /// </summary>
        /// <param name="idProducto">Id del producto que se requiere</param>
        /// <returns></returns>
        public Task<Product> GetProductosByIdAsync(int idProducto)
        {
            return db.Table<Product>().Where(a => a.IdProducto == idProducto).FirstOrDefaultAsync();
        }
    }
}