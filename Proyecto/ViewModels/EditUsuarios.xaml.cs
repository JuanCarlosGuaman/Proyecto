using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUsuarios : ContentPage
    {
        string barcode = Convert.ToString(Application.Current.Properties["View_Usuarios_Id"]);
        public EditUsuarios()
        {
            InitializeComponent();
            t_id.Text = barcode;
            
            Task.Delay(2000);
            data_barang();
        }
        private void data_barang()
        {
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();
            var cmd = new MySqlCommand("select * from usuarios where id='" + t_id.Text + "'", conn);
            var rd = cmd.ExecuteReader();

            if(rd.Read())
            {
                //t_id.Text = rd.GetUInt16("id");
                t_id.Text = barcode;

                t_nombre.Text = rd.GetString("nombre").ToString();
                t_apellido.Text = rd.GetString("apellido").ToString();
                t_telefono.Text = rd.GetString("telefono").ToString();
                t_direccion.Text = rd.GetString("direccion").ToString();
                t_correo.Text = rd.GetString("correo").ToString();
            }
        }

        private void b_update_Clicked(object sender, EventArgs e)
        {
            if(t_nombre.Text == null)
            {
                t_nombre.Focus();
            }
            else if (t_apellido.Text == null)
            {
                t_apellido.Focus();
            }
            else if (t_telefono.Text == null)
            {
                t_telefono.Focus();
            }
            else if (t_direccion.Text == null)
            {
                t_direccion.Focus();
            }
            else if (t_correo.Text == null)
            {
                t_correo.Focus();
            }
            else
            {
                var conn = new MySqlConnection(Properties.Resources.dbcon);
                conn.Open();
                var cmd = new MySqlCommand("update usuarios set nombre='" + t_nombre.Text + "',apellido='" + t_apellido.Text + "',telefono='" + t_telefono.Text + "',correo='" + t_direccion.Text + "',telefono='" + t_correo.Text + "' where id='" + barcode + "'", conn);
                var rd = cmd.ExecuteReader();

                DisplayAlert("Informacion", "Datos actualizados correctamente", "OK");
                Navigation.PushAsync(new ViewUsuarios());
            }

        }
    }
}