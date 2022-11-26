using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Usuarios : ContentPage
    {
        public Usuarios()
        {
            InitializeComponent();
        }
        private void Validar()
        {

            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("insert into usuarios (nombre,apellido,telefono,direccion,correo) values('" + lb_nombre.Text + "','" + lb_apellido.Text + "','" + lb_telefono.Text + "','" + lb_direccion.Text + "','" + lb_correo.Text + "')", conn);
            var rd = cmd.ExecuteReaderAsync();

            //conn.Close();
            DisplayAlert("Info", "Datos Guardados Correctamente", "OK");

            lb_nombre.Text = null;
            lb_apellido.Text = null;
            lb_telefono.Text = null;
            lb_direccion.Text = null;
            lb_correo.Text = null;
        }

        private async void btn_Guardar_Usu_Clicked(object sender, EventArgs e)
        {
            if (lb_nombre.Text == null)
            {
                lb_nombre.Focus();
            }
            else if (lb_apellido == null)
            {
                lb_apellido.Focus();
            }
            else if (lb_telefono == null)
            {
                lb_apellido.Focus();
            }
            else if (lb_direccion == null)
            {
                lb_direccion.Focus();
            }
            else if (lb_correo == null)
            {
                lb_correo.Focus();
            }
            else
            {
                Validar();
            }


        }
    }
}