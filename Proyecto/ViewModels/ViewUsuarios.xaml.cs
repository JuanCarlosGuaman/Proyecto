using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Proyecto.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewUsuarios : ContentPage
    {
        public IList<list_usuario> usuarios { get; set; }
        public ViewUsuarios()
        {
            InitializeComponent();
            data_list();
        }
        public class list_usuario
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
        }
        private void data_list()
        {
            var conn = new MySqlConnection(Properties.Resources.dbcon);
            conn.Open();
            var cmd = new MySqlCommand("select * from usuarios", conn);
            var rd = cmd.ExecuteReader();

            usuarios = new List<list_usuario>();
            while (rd.Read())
            {
                usuarios.Add(new list_usuario
                {
                    Id = rd.GetUInt16("id"),
                    
                    Nombre = rd.GetString("nombre").ToString(),
                    Apellido = rd.GetString("apellido").ToString(),
                    Telefono = rd.GetString("telefono").ToString(),
                    Direccion = rd.GetString("direccion").ToString(),
                    Correo = rd.GetString("correo").ToString()


                }

              );
            }
            rd.Close();
            listUsuarios.ItemsSource = usuarios;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton x = (ImageButton)sender;
            string barcode_select = x.CommandParameter.ToString();
            Application.Current.Properties["View_Usuarios_Id"] = barcode_select;
            Navigation.PushAsync(new EditUsuarios());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Usuarios());
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
                var cmd = new MySqlCommand("delete from usuarios where id= '" + barcode_select + "'", conn);
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
    }
}