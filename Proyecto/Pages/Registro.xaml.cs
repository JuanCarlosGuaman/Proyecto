using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        private async void btn_Guardar_Clicked(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);

            conn.Open();
            MySqlCommand cmd = new MySqlCommand("insert into login (usuario,contrasena) values('" + username.Text + "','" + pass.Text + "')", conn);
            var rd = cmd.ExecuteReaderAsync();
               
            //conn.Close();
            DisplayAlert("Info", "Datos Guardados Correctamente", "OK");
            username.Text = null;
            pass.Text = null;
            await Navigation.PushAsync(new Login());





        }
    }
}