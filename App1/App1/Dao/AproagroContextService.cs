using Approagro.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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
            database.CreateTableAsync<LaborRealizada>().Wait();
            database.CreateTableAsync<Enfermedades>().Wait();
        }

        #region CRUD TipoActividad
        public Task<List<TipoActividad>> GetTipoActividadesAsync()
        {
            //Get all TipoActividads
            return database.Table<TipoActividad>().ToListAsync();
        }

        public Task<TipoActividad> GetTipoActividadAsync(int id)
        {
            // Get a specific TipoActividad by id.
            return database.Table<TipoActividad>()
                            .Where(i => i.IdActividad == id)
                            .FirstOrDefaultAsync();
        }
        public Task<TipoActividad> GetTipoActividadAsync(string name)
        {
            // Get a specific TipoActividad by name.
            return database.Table<TipoActividad>()
                            .Where(i => i.Nombre == name)
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

                ActividadProductiva.Result.TipoActividad = GetTipoActividadAsync(ActividadProductiva.Result.Fk_TipoActividad).Result;
                ActividadProductiva.Result.LaboresRealizadas = GetLaboresRealizadasByActividadProductiva(id).Result;
                ActividadProductiva.Result.Enfermedades = GetEnfermedadesByActividadProductiva(id).Result;

                return ActividadProductiva;
            }
            catch
            {
                return null;
            }
        }

        public Task<ActividadProductiva> SaveActividadProductivaAsync(ActividadProductiva ActividadProductiva)
        {
            int rows = -1;
            if (ActividadProductiva.IdActividad != 0)
            {
                // Update an existing ActividadProductiva.
                rows = database.UpdateAsync(ActividadProductiva).Result;
            }
            else
            {
                // Save a new ActividadProductiva.
                rows = database.InsertAsync(ActividadProductiva).Result;
            }

            if (rows != -1)
            {
                return database.Table<ActividadProductiva>().Where(i => i.NombreActividad == ActividadProductiva.NombreActividad)
                        .FirstOrDefaultAsync();
            }
            return null;
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
        public Task<List<LaborRealizada>> GetLaboresRealizadasAsync()
        {
            return database.Table<LaborRealizada>().ToListAsync();
        }

        public Task<LaborRealizada> GetLaborRealizadAsync(int id)
        {
            try
            {
                var LaboresRealizadas = database.Table<LaborRealizada>()
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

        public Task<LaborRealizada> GetLaborRealizadaByNombreAsync(string nombre)
        {
            try
            {
                var LaboresRealizadas = database.Table<LaborRealizada>()
                                .Where(i => i.Nombre == nombre)
                                .FirstOrDefaultAsync();

                LaboresRealizadas.Result.Insumos = GetInsumosByLaborRealizada(LaboresRealizadas.Result.Id).Result;
                return LaboresRealizadas;
            }
            catch
            {
                return null;
            }
        }

        public Task<LaborRealizada> SaveLaborRealizadaAsync(LaborRealizada laborRealizada)
        {
            int rowsAfected = -1;
            if (laborRealizada.Id != 0)
            {
                rowsAfected = database.UpdateAsync(laborRealizada).Result;
                if(rowsAfected!= -1)
                {
                    return GetLaborRealizadAsync(laborRealizada.Id);
                }
            }
            else
            {
                return SaveNewLaborRealizada(laborRealizada);
            }

            return null;
        }

        

        public Task<int> DeleteLaboresRealizadasAsync(LaborRealizada LaboresRealizadas)
        {
            // Delete a LaboresRealizadas.
            return database.DeleteAsync(LaboresRealizadas);
        }

        public Task<List<LaborRealizada>> GetLaboresRealizadasByActividadProductiva(int id)
        {
            var labores = database.Table<LaborRealizada>()
                            .Where(i => i.FK_ActividadProductiva == id)
                            .ToListAsync();
            labores.Result.ForEach(x => x.Insumos = GetInsumosByLaborRealizada(x.Id).Result); //Add insumos used by specific labor
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

        #region CRUD Enfermedades
        public Task<List<Enfermedades>> GetEnfermedadesAsync()
        {
            return database.Table<Enfermedades>().ToListAsync();
        }

        public Task<Enfermedades> GetEnfermedadAsync(int id)
        {
            var Enfermedades = database.Table<Enfermedades>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
            return Enfermedades;
        }

        public Task<int> SaveEnfermedadesAsync(Enfermedades Enfermedades)
        {
            if (Enfermedades.Id != 0)
            {
                // Update an existing Enfermedades.
                return database.UpdateAsync(Enfermedades);
            }
            else
            {
                // Save a new Enfermedades.
                return database.InsertAsync(Enfermedades);
            }
        }

        public Task<int> DeleteEnfermedadesAsync(Enfermedades Enfermedades)
        {
            return database.DeleteAsync(Enfermedades);
        }

        private Task<List<Enfermedades>> GetEnfermedadesByActividadProductiva(int id)
        {
            return database.Table<Enfermedades>()
                            .Where(i => i.Fk_ActividadProductiva == id)
                            .ToListAsync();
        }
        #endregion

        #region Metodos utilitarios
        private Task<LaborRealizada> SaveNewLaborRealizada(LaborRealizada laborRealizada)
        {
            int rowsAfected = -1;
            laborRealizada.Nombre = $"AP{laborRealizada.FK_ActividadProductiva}-{Guid.NewGuid()}";
            rowsAfected = database.InsertAsync(laborRealizada).Result;

            if(rowsAfected != -1) {
                return GetLaborRealizadaByNombreAsync(laborRealizada.Nombre);
            }
            return null;
        }
        #endregion
    }
}
