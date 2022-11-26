using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Proyecto.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Proyecto.ViewModels.ViewEquipos;
using static Proyecto.ViewModels.ViewUsuarios;

namespace Proyecto.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEquipos : ContentPage
    {
        public IList<list_equipo> equipos { get; set; }
        public ViewEquipos()
        {
            InitializeComponent();
            data_list();
        }
        public class list_equipo
        {
            public int Id { get; set; }
            public string Serie { get; set; }
            public string Modelo { get; set; }
            public string Teclado { get; set; }
            public string Mouse { get; set; }
            //public string Correo { get; set; }
        }

        private void data_list()
        {
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();
            var cmd = new MySqlCommand("select * from equipo", conn);
            var rd = cmd.ExecuteReader();

            equipos = new List<list_equipo>();
            while (rd.Read())
            {
                equipos.Add(new list_equipo
                {
                    Id = rd.GetUInt16("id"),
                    Serie = rd.GetString("serie").ToString(),
                    Modelo = rd.GetString("modelo").ToString(),
                    Teclado = rd.GetString("teclado").ToString(),
                    Mouse = rd.GetString("mouse").ToString()
                    //Correo = rd.GetString("correo").ToString()


                }

              );
            }
            rd.Close();
            listEquipos.ItemsSource = equipos;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton x = (ImageButton)sender;
            string barcode_select = x.CommandParameter.ToString();
            Application.Current.Properties["View_Equipos_Id"] = barcode_select;
            Navigation.PushAsync(new EditEquipos());
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            ImageButton y = (ImageButton)sender;
            string barcode_select = y.CommandParameter.ToString();
            
            var ans = await DisplayAlert("Confirmacion", "Seguro desea eliminar los datos", "Si", "No");
            if (ans == true)
            {
                //condicion verdadera
                var conn = new MySqlConnection(Properties.Resources.dbcon);
                conn.Open();
                var cmd = new MySqlCommand("delete from equipo where id= '" + barcode_select + "'", conn);
                var rd = cmd.ExecuteReader();

                data_list();
                await DisplayAlert("Informacion", "Dato eliminado", "OK");
            }
            else
            {
                //condicion falsa
                return;
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Equipos());
        }
    }
}