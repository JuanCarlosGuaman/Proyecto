using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySqlConnector;

namespace Proyecto.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Equipos : ContentPage
    {
        public Equipos()
        {
            InitializeComponent();
        }
        private void Validar()
        {

            MySqlConnection conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("insert into usuarios (serie,modelo,teclado,mouse,colocar) values('" + lb_serie.Text + "','" + lb_modelo.Text + "','" + lb_teclado.Text + "','" + lb_mouse.Text + "','" + lb_mouse.Text + "')", conn);
            var rd = cmd.ExecuteReaderAsync();

            //conn.Close();
            DisplayAlert("Informacion", "Datos Guardados Correctamente", "OK");

            lb_serie.Text = null;
            lb_modelo.Text = null;
            lb_teclado.Text = null;
            lb_mouse.Text = null;
            //lb_tipo.Text = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var datacat = new List<string>();
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();

            var cmd = new MySqlCommand("select categoria from tipo", conn);
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                datacat.Add(rd.GetString("categoria"));
            }

            Combo_cate.ItemsSource = datacat;

        }


        private void btn_Guardar_Equ_Clicked(object sender, EventArgs e)
        {
            if(ID.Text == null)
            {

            }
            else
            {
                simpan();
            }
        }

        private void Combo_cate_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            string opc = Combo_cate.SelectedItem.ToString();
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();
            var cmd = new MySqlCommand("select * from tipo where categoria='" + opc + "'", conn);
            var rd = cmd.ExecuteReader();

            if(rd.Read())
            {
                ID.Text = rd.GetInt32(0).ToString();
            }
            rd.Close();
        }
        private void simpan()
        {
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();

            var cmd = new MySqlCommand("insert into equipo (serie,modelo,teclado,mouse,tipo_id) values('" + lb_serie.Text + "','" + lb_modelo.Text + "','" + lb_teclado.Text + "','" + lb_mouse.Text + "','" + ID.Text + "')", conn);
            var rd = cmd.ExecuteReader();

            lb_serie.Text = null;
            lb_modelo.Text = null;
            lb_teclado.Text = null;
            lb_mouse.Text = null;
            ID.Text = null;

            DisplayAlert("Informacion", "Equipos Registrado", "ok");
        }
    }
}