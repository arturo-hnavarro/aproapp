using Approagro.Dao;
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
    public partial class CopiaDeSeguridadPage : ContentPage
    {
        private bool _isWorking = false;
        public bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                OnPropertyChanged();
            }
        }
        public CopiaDeSeguridadPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void OnSignIn(object sender, EventArgs e)
        {
            IsWorking = true;
            try
            {
                await (Application.Current as App).SignIn();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error de autenticación.", ex.Message, "OK");
            }
            IsWorking = false;
        }
        async void OnSignOut(object sender, EventArgs e)
        {
            var signout = await DisplayAlert("Salir?", "Desea cerrar la sesión?", "Sí", "No");
            if (signout)
            {
                await (Application.Current as App).SignOut();
            }
        }
        async void OnBackUp(object sender, EventArgs e)
        {
            IsWorking = true;
            try
            {
                await CopiaDeSeguridad.SubirCopia("APROAGRO.db3");
                await DisplayAlert("Atención.", "Copia de seguridad creada correctamente.", "Aceptar");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ha ocurrido un problema al crear la copia de seguridad.\n\r{ex.Message}", "Aceptar");
            }
            IsWorking = false;
        }
        async void OnRestoreBackUp(object sender, EventArgs e)
        {
            IsWorking = true;
            try
            {
                await CopiaDeSeguridad.BajarCopia("APROAGRO.db3");
                await DisplayAlert("Atención", "Bases de datos recuperada desde One Drive.", "Aceptar");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ha ocurrido un problema al restaurar la copia de seguridad.\n\r{ex.Message}", "Aceptar");
            }
            IsWorking = false;
        }
    }
}