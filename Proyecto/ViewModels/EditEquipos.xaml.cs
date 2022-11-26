using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEquipos : ContentPage
    {
        string barcode = Convert.ToString(Application.Current.Properties["View_Equipos_Id"]);
        public EditEquipos()
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
            var cmd = new MySqlCommand("select * from equipo where id='" + t_id.Text + "'", conn);
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                
                t_id.Text = barcode;
                t_serie.Text = rd.GetString("serie").ToString();
                t_modelo.Text = rd.GetString("modelo").ToString();
                t_teclado.Text = rd.GetString("teclado").ToString();
                t_mouse.Text = rd.GetString("mouse").ToString();
                
            }
        }
        private void b_update_Clicked(object sender, EventArgs e)
        {
            if (t_serie.Text == null)
            {
                t_serie.Focus();
            }
            else if (t_modelo.Text == null)
            {
                t_modelo.Focus();
            }
            else if (t_teclado.Text == null)
            {
                t_teclado.Focus();
            }
            else if (t_mouse.Text == null)
            {
                t_mouse.Focus();
            }
            else
            {
                var conn = new MySqlConnection(Properties.Resources.dbcon);
                conn.Open();
                var cmd = new MySqlCommand("update equipo set serie='" + t_serie.Text + "',modelo='" + t_modelo.Text + "',teclado='" + t_teclado.Text + "',mouse='" + t_mouse.Text + "' where id='" + barcode + "'", conn);
                var rd = cmd.ExecuteReader();

                DisplayAlert("Informacion", "Datos actualizados correctamente", "OK");
                Navigation.PushAsync(new ViewEquipos());
            }
        }
    }
}