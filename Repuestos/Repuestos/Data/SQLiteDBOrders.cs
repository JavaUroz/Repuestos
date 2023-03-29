using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Repuestos.Models;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Repuestos.Data
{
    public class SQLiteDBOrders
    {
        SQLiteAsyncConnection db;
        public SQLiteDBOrders(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Order>().Wait();
        }
        /// <summary>
        /// Guarda Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Task<int> SaveOrderAsync(Order order)
        {
            if (order.IdOrder != 0)
            {
                return db.UpdateAsync(order);
            }
            else
            {
                return db.InsertAsync(order);
            }
        }
        /// <summary>
        /// Elimina Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Task<int> DeleteOrderAsync(Order order)
        {
            return db.DeleteAsync(order);
        }
        /// <summary>
        ///     Recuperar todos las Orders
        /// </summary>
        /// <returns></returns>

        public Task<List<Order>> GetOrderAsync()
        {
            return db.Table<Order>().ToListAsync();
        }
        /// <summary>
        ///     Recuperar Order por id
        /// </summary>
        /// <param name="idOrder">Id de Order que se requiere</param>
        /// <returns></returns>
        public Task<Order> GetOrdersByIdAsync(int idOrder)
        {
            return db.Table<Order>().Where(a => a.IdOrder == idOrder).FirstOrDefaultAsync();
        }
    }
}