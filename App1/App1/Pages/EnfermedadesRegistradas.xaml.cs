﻿using Approagro.Domain;
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
    public partial class EnfermedadesRegistradas : ContentPage
    {
        private ActividadProductiva actividadProductiva;
        public EnfermedadesRegistradas()
        {
            InitializeComponent();
        }

        public EnfermedadesRegistradas(ActividadProductiva actividad)
        {
            InitializeComponent();
            actividadProductiva = actividad;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                MyListView.ItemsSource = await GetEnfermedades();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error.", ex.Message, "Aceptar");
            }
        }

        async void OnListViewItemSelected(object sender, EventArgs e)
        {
            if (MyListView.SelectedItem != null)
            {
                Enfermedad enfermedades= (Enfermedad)MyListView.SelectedItem;
                await DisplayAlert(enfermedades.Nombre.ToUpper(), "Observaciones: " +enfermedades.Observacion + "\nFecha: " + enfermedades.Fecha.ToString("dd/MM/yyyy"), "Aceptar");
            }
        }

        async void OnRegisterEnfermedadClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EnfermedadesAdd(actividadProductiva));
        }

        private Task<List<Enfermedad>> GetEnfermedades()
        {
            return App.AproagroDB.GetEnfermedadesByActividadProductiva(actividadProductiva.IdActividad);
        }
    }
}