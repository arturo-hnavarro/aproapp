﻿using App1;
using Approagro.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Approagro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRegistrarTipo : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public PageRegistrarTipo()
        {
            InitializeComponent();

            /*Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;*/
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void OnRegisterTypeActivityClick(object sender, EventArgs e)
        {
            string Name = EntryActivityName.Text;
            string Description = EntryActivityDescription.Text;

            if (!string.IsNullOrWhiteSpace(Name))
            {
                await App.AproagroDB.SaveTipoActividadAsync(new TipoActividad
                {
                    Nombre = Name,
                    Descripcion = Description
                });
            }
        }
    }
}
