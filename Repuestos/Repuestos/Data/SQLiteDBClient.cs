using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Repuestos.Models;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Repuestos.Data
{
    public class SQLiteDBClient
    {
        SQLiteAsyncConnection db;        
        public SQLiteDBClient(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync <Client>().Wait();            
        }        
        /// <summary>
        /// Guarda Cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public Task<int> SaveClienteAsync(Client cliente)
        {
            if (cliente.IdCliente != 0)
            {
                return db.UpdateAsync(cliente);
            }
            else
            {
                return db.InsertAsync(cliente);
            }
        }
        /// <summary>
        /// Elimina Cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public Task<int> DeleteClienteAsync(Client cliente)
        {
            return db.DeleteAsync(cliente);
        }
        /// <summary>
        ///     Recuperar todos los clientes
        /// </summary>
        /// <returns></returns>

        public Task<List<Client>> GetClientesAsync()
        {
            return db.Table<Client>().ToListAsync();
        }
        /// <summary>
        ///     Recuperar cliente por id
        /// </summary>
        /// <param name="idCliente">Id del cliente que se requiere</param>
        /// <returns></returns>
        public Task<Client> GetClienteByIdAsync(int idCliente)
        {
            return db.Table<Client>().Where(a => a.IdCliente == idCliente).FirstOrDefaultAsync();
        }               
    }
}
