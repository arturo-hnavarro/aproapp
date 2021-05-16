using App1.Domain;
using Approagro.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Approagro.Dao
{
    public class AproagroContextService
    {
        readonly SQLiteAsyncConnection database;
        

        public AproagroContextService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TipoActividad>().Wait();
            database.CreateTableAsync<ActividadProductiva>().Wait();
            database.CreateTableAsync<Insumos>().Wait();
            database.CreateTableAsync<LaboresRealizadas>().Wait();
        }

        #region CRUD TipoActividad
        public Task<List<TipoActividad>> GetTipoActividadesAsync()
        {
            //Get all TipoActividads
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
        #endregion

        #region CRUD ActividadProductiva
        public Task<List<ActividadProductiva>> GetActividadesProductivasAsync()
        {
            return database.Table<ActividadProductiva>().ToListAsync();
        }

        public Task<ActividadProductiva> GetActividadProductivaAsync(int id)
        {
            try
            {
                var ActividadProductiva = database.Table<ActividadProductiva>()
                            .Where(i => i.IdActividad == id)
                            .FirstOrDefaultAsync();

                ActividadProductiva.Result.TipoActividad = MapperTipoActividadAsync(ActividadProductiva.Result.Fk_TipoActividad).Result;
                ActividadProductiva.Result.LaboresRealizadas = GetLaboresRealizadasByActividadProductiva(id).Result;

                return ActividadProductiva;
            }
            catch 
            {
                return null;
            }
        }

        public Task<int> SaveActividadProductivaAsync(ActividadProductiva ActividadProductiva)
        {
            if (ActividadProductiva.IdActividad != 0)
            {
                // Update an existing ActividadProductiva.
                return database.UpdateAsync(ActividadProductiva);
            }
            else
            {
                // Save a new ActividadProductiva.
                return database.InsertAsync(ActividadProductiva);
            }
        }

        public Task<int> DeleteActividadProductivaAsync(ActividadProductiva ActividadProductiva)
        {
            // Delete a ActividadProductiva.
            return database.DeleteAsync(ActividadProductiva);
        }

        private Task<TipoActividad> MapperTipoActividadAsync(int fk_Tipo)
        {
            // Get a specific TipoActividad.
            return database.Table<TipoActividad>()
                            .Where(i => i.IdActividad == fk_Tipo)
                            .FirstOrDefaultAsync();
        }
        #endregion

        #region CRUD LaboresRealizadas
        public Task<List<LaboresRealizadas>> GetLaboresRealizadasAsync()
        {
            return database.Table<LaboresRealizadas>().ToListAsync();
        }

        public Task<LaboresRealizadas> GetLaborRealizadAsync(int id)
        {
            try
            {
                var LaboresRealizadas = database.Table<LaboresRealizadas>()
                                .Where(i => i.Id == id)
                                .FirstOrDefaultAsync();

                LaboresRealizadas.Result.Insumos = GetInsumosByLaborRealizada(id).Result;
                return LaboresRealizadas;
            }
            catch
            {
                return null;
            }
        }

        public Task<int> SaveLaboresRealizadasAsync(LaboresRealizadas LaboresRealizadas)
        {
            if (LaboresRealizadas.Id != 0)
            {
                // Update an existing LaboresRealizadas.
                return database.UpdateAsync(LaboresRealizadas);
            }
            else
            {
                // Save a new LaboresRealizadas.
                return database.InsertAsync(LaboresRealizadas);
            }
        }

        public Task<int> DeleteLaboresRealizadasAsync(LaboresRealizadas LaboresRealizadas)
        {
            // Delete a LaboresRealizadas.
            return database.DeleteAsync(LaboresRealizadas);
        }

        private Task<List<LaboresRealizadas>> GetLaboresRealizadasByActividadProductiva(int id)
        {
            var labores = database.Table<LaboresRealizadas>()
                            .Where(i => i.FK_ActividadProductiva == id)
                            .ToListAsync();
            labores.Result.ForEach(x => x.Insumos = GetInsumosByLaborRealizada(x.Id).Result ); //Add insumos used by specific labor
            return labores;
        }
        #endregion

        #region CRUD Insumos
        public Task<List<Insumos>> GetInsumosAplicadosAsync()
        {
            return database.Table<Insumos>().ToListAsync();
        }

        public Task<Insumos> GetInsumoAplicadoAsync(int id)
        {
            var Insumos = database.Table<Insumos>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
            return Insumos;
        }

        public Task<int> SaveInsumosAsync(Insumos Insumos)
        {
            if (Insumos.Id != 0)
            {
                // Update an existing Insumos.
                return database.UpdateAsync(Insumos);
            }
            else
            {
                // Save a new Insumos.
                return database.InsertAsync(Insumos);
            }
        }

        public Task<int> DeleteInsumosAsync(Insumos Insumos)
        {
            return database.DeleteAsync(Insumos);
        }

        private Task<List<Insumos>> GetInsumosByLaborRealizada(int id)
        {
            return database.Table<Insumos>()
                            .Where(i => i.Fk_LaborRealizada == id)
                            .ToListAsync();
        }
        #endregion
    }
}
