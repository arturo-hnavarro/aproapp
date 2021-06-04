using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Approagro.Dao
{
    public static class CopiaDeSeguridad
    {
        #region Copia de Seguridad One Drive
        /// <summary>
        /// Sube el archivo desde Local Application Data a One Drive
        /// </summary>
        /// <param name="dbName">String con el nombre de la base de datos a copiar, archivo .db3</param>
        /// <returns></returns>
        public static async Task SubirCopia(string dbName)
        {
            var dbPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName));
            byte[] data = System.IO.File.ReadAllBytes(dbPath);
            Stream stream = new MemoryStream(data);
            //
            try
            {
                await App.GraphClient.Me
                    .Drive
                    .Special
                    .AppRoot
                    .ItemWithPath(dbName)
                    .Content
                    .Request()
                    .PutAsync<Microsoft.Graph.DriveItem>(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Descargar el archivo desde One Drive hasta Local Application Data
        /// </summary>
        /// <param name="dbName">String con el nombre de la base de datos a copiar, archivo .db3</param>
        /// <returns></returns>
        public static async Task BajarCopia(string dbName)
        {
            Stream contentStream = null;
            try
            {
                var path = await App.GraphClient
                            .Me
                            .Drive
                            .Special
                            .AppRoot
                            .ItemWithPath(dbName)
                            .Request()
                            .GetAsync();

                //foundFile = await path.Request().GetAsync();
                if (path == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    contentStream = await App.GraphClient.Me.Drive
                        .Special
                        .AppRoot
                        .ItemWithPath(dbName)
                        .Content
                        .Request()
                        .GetAsync();

                    var destPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName));
                    var driveItemFile = System.IO.File.Create(destPath);
                    contentStream.Seek(0, SeekOrigin.Begin);
                    contentStream.CopyTo(driveItemFile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
