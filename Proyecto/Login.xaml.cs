using MySqlConnector;
using Proyecto.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btn_guardar_Clicked(object sender, EventArgs e)
        {
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();

            var opc = new MySqlCommand("select * from login where usuario='" + txtusuario.Text + "' and contrasena='" + txtcontrasena.Text + "'",conn);
            var rpt = opc.ExecuteReader();

            if (rpt.Read())
            {
                DisplayAlert("Informacion", "Login Correcto", "OK");
                //DisplayAlert("Alerta", "USUARIO CORRECTO", "Cerrar");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Advertencia", "Usuario / Contraseña Incorrecto", "OK");
            }
        }

        private async void btn_registrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
                        
        }
    }
}