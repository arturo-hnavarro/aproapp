using Approagro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsumosAdd : ContentPage
    {
        private readonly LaborRealizada laborRealizada;
        private List<Insumos> insumos;
        public InsumosAdd()
        {
            InitializeComponent();
            if (insumos == null)
                insumos = new List<Insumos>();
        }
        public InsumosAdd(LaborRealizada labor)
        {
            InitializeComponent();
            laborRealizada = labor;
            if (insumos == null)
                insumos = new List<Insumos>();
        }

        async void AddInsumo(object sender, EventArgs e)
        {
            try
            {
                insumos.Add(Mapper());
                if (await DisplayAlert("Insumo agregado", "Insumo agregado ¿Desea registrar otro?", "Sí", "No"))
                {
                    cleanEntry();
                }
                else
                {
                    App.AproagroDB.SaveInsumosListAsync(insumos);
                    await DisplayAlert("Actualizado", "Insumos registrados", "Aceptar");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups, no fue posible realizar la operación", ex.Message, "Aceptar");
            }
        }

        #region utilitarios
        private Insumos Mapper()
        {
            string Nombre = EntryNombre.Text;
            string Precio = EntryPrecio.Text;
            string Cantidad = EntryCantidad.Text;
            string Observacion = EntryObservaciones.Text;

            if (string.IsNullOrWhiteSpace(Nombre))
                throw new ArgumentException("Nombre del producto no puede ser vacío.");

            if (string.IsNullOrWhiteSpace(Precio))
                throw new ArgumentException("Precio del producto no puede ser vacío.");

            if (string.IsNullOrWhiteSpace(Cantidad))
                throw new ArgumentException("Cantidad aplicadano puede ser vacío.");

            return new Insumos
            {
                Fk_LaborRealizada = laborRealizada.Id,
                Nombre = Nombre,
                PrecioTotal = double.Parse(Precio),
                Observacion = Observacion,
                CantidadUsada = double.Parse(Cantidad)
            };
        }

        private void cleanEntry()
        {
            EntryNombre.Text = "";
            EntryPrecio.Text = "";
            EntryObservaciones.Text = "";
        }
        #endregion

    }
}