using Approagro.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Approagro.Dao
{
    public class TipoActividadDao
    {
        readonly SQLiteAsyncConnection database;

        public TipoActividadDao(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TipoActividad>().Wait();
        }

        public Task<List<TipoActividad>> GetTipoActividadesAsync()
        {
            //Get all TipoActividads.
            return database.Table<TipoActividad>().ToListAsync();
        }

        public Task<TipoActividad> GetTipoActividadAsync(int id)
        {
            // Get a specific TipoActividad.
            return database.Table<TipoActividad>()
                            .Where(i => i.IdActividad == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTipoActividadAsync(TipoActividad TipoActividad)
        {
            if (TipoActividad.IdActividad != 0)
            {
                // Update an existing TipoActividad.
                return database.UpdateAsync(TipoActividad);
            }
            else
            {
                // Save a new TipoActividad.
                return database.InsertAsync(TipoActividad);
            }
        }

        public Task<int> DeleteTipoActividadAsync(TipoActividad TipoActividad)
        {
            // Delete a TipoActividad.
            return database.DeleteAsync(TipoActividad);
        }
    }
}
