using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Camara : ContentPage
    {
        private MediaFile _mediaFile;
        string directorio_local = "";
        string directorio_server = "";
        string name_file = "";
        public ImageSource ProfilePic { get; set; }
        public Camara()
        {
            InitializeComponent();
        }

        private async void B_Galeri_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Info", "galeria", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { });

            if (_mediaFile == null)
                return;

            directorio_local = _mediaFile.Path;
            name_file = Path.GetFileName(directorio_local);
            _ = DisplayAlert("Fiel info", name_file, "OK");

            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
                //var stream = _mediaFile.GetStream();
                //return stream;
            }
            );

        }

        private async void B_Camera_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Info", "Kamera", "OK");
                return;
            }
            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = DateTime.Now.ToString("yyyy-MM-dd") + "_MyImage.jpg"
            });

            if (_mediaFile == null)
                return;

            directorio_local = _mediaFile.Path;
            name_file = Path.GetFileName(directorio_local);
            _ = DisplayAlert("Fiel info", name_file, "OK");

            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
                //var stream = _mediaFile.GetStream();
                //return stream;
            }
            );
        }

        private async void B_Upload_Clicked(object sender, EventArgs e)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"", $"\"{_mediaFile.Path}\"");

            var client = new HttpClient();
            var upload_address = "http://192.168.18.56/RestApi/index.php/Upload";
            var http_respon = await client.PostAsync(upload_address, content);

            directorio_server = await http_respon.Content.ReadAsStringAsync();

            simpan_upload();
        }
        private void simpan_upload()
        {
            string url_file = "http://192.168.18.56/RestApi/images/" + name_file;
            var conectar = new MySqlConnection(Properties.Resources.dbcon);
            conectar.Close();
            conectar.Open();

            var cmd = new MySqlCommand("insert into media (name_file,url_file) values('" + name_file + "','" + url_file + "')", conectar);
            var rd = cmd.ExecuteReader();

            DisplayAlert("Info", "Foto" + name_file + "Upload", "OK");
        }
    }
}