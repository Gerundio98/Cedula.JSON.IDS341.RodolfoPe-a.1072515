using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json;

namespace RegistroDeDatos
{
    public partial class Form1 : Form
    {
        List<Ciudadano> ciudadanoList = new List<Ciudadano>();

        string path;

        public Form1()
        {
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\Ciudadanos.json";
            InitializeComponent();
            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                ciudadanoList = JsonConvert.DeserializeObject<List<Ciudadano>>(json);
                dgvDatosCiudadanos.DataSource = ciudadanoList;
            }
        }
        private void NuevoRegistro_Click(object sender, EventArgs e)
        {
            //Habilitación de botones.
            NuevoRegistro.Enabled = false;
            CancelarButtom.Enabled = true;
            GuardarButtom.Enabled = true;
            EliminarButtom.Enabled = false;
            CargarEditarImagenButtom.Enabled = true;

            BorradoCamposRellenables();
            VisibilidadCamposRellenables();
        }
        public void GuardarButtom_Click(object sender, EventArgs e)
        {

            Editar_Guardar();
            BorradoCamposRellenables();
            OcultamientoCamposRellenables();

            //Habilitación de botones.
            NuevoRegistro.Enabled = true;
            CancelarButtom.Enabled = false;
            GuardarButtom.Enabled = false;
            EliminarButtom.Enabled=false;
            CargarEditarImagenButtom.Enabled = false;
        }
        public void Editar_Guardar()
        {
            var Ciudadano =new Ciudadano()
            {
                ID = Guid.NewGuid(),
                NumeroCedula = txtNumeroCedula.Text,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                LugarNacimiento = txtLugarNacimiento.Text,
                FechaDeNacimiento = FechaDeNacimientoPicker.Value,
                Nacionalidad = txtNacionalidad.Text,
                Ocupacion = txtOcupacion.Text,
                Sexo = SexoComboBox.SelectedItem,
                Sangre = SangreComboBox.SelectedItem,
                EstadoCivil = EstadoCivilComboBox.SelectedItem,
                FechaEmision = FechaDeEmisionPicker.Value,
                Imagen = path,
            };
            int j = 0;
            var json=string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\Ciudadanos.json";
            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                ciudadanoList = JsonConvert.DeserializeObject<List<Ciudadano>>(json);
                dgvDatosCiudadanos.DataSource = ciudadanoList;
                for (int i = 0; i < ciudadanoList.Count; i++)
                {
                    if (ciudadanoList[i].NumeroCedula == Ciudadano.NumeroCedula)
                    {
                        ciudadanoList.Remove(ciudadanoList[i]);
                        ciudadanoList.Add(Ciudadano);
                        MessageBox.Show($"El ciudadano {txtNumeroCedula.Text} fue encontrado y actualizado.");
                        j = 1;
                        break;
                    }
                }
                if (j == 0)
                {
                    MessageBox.Show($"El ciudadano {txtNumeroCedula.Text}  fue agregado correctamente.");
                    ciudadanoList.Add(Ciudadano);
                }
            }
            else
            {
                ciudadanoList.Add(Ciudadano);
                MessageBox.Show($"El ciudadano {txtNumeroCedula.Text} fue agregado correctamente.");

            }
            json =JsonConvert.SerializeObject(ciudadanoList);
            var sv = new StreamWriter(pathFile, false, Encoding.UTF8);
            sv.Write(json);
            sv.Close();
            dgvDatosCiudadanos.DataSource = ciudadanoList;
        }
        public void Eliminar()
        {
            int j = 0;
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\Ciudadanos.json";
            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                ciudadanoList = JsonConvert.DeserializeObject<List<Ciudadano>>(json);
                    for(int j=0; j < ciudadanoList.Count; j++)
                    {
                        if (ciudadanoList[j].NumeroCedula == txtNumeroCedula.Text)
                        {
                            ciudadanoList.Remove(ciudadanoList[j]);
                        MessageBox.Show($"El ciudadano {txtNumeroCedula.Text} fue eliminado correctamente.");
                        j = 1;
                        break;
                        }
                    }
                    if(j== 0)
                {
                    MessageBox.Show($"No existe ningun ciudadano con la cedula {txtNumeroCedula.Text} que coincida en la lista.");
                }
            }
            else
            {
                MessageBox.Show($"Aún no se han ingresado ciudadanos.");
            }
            json = JsonConvert.SerializeObject(ciudadanoList);
            var sv = new StreamWriter(pathFile, false, Encoding.UTF8);
            sv.Write(json);
            sv.Close();
            dgvDatosCiudadanos.DataSource = ciudadanoList;
        }
        private void BorradoCamposRellenables() 
        {
            //Borrado de información previa.
            txtNumeroCedula.Text = string.Empty;
            txtLugarNacimiento.Text = string.Empty;
            txtNacionalidad.Text = string.Empty;
            txtOcupacion.Text = string.Empty;
            SangreComboBox.SelectedItem=null;
            SexoComboBox.SelectedItem = null;
            EstadoCivilComboBox.SelectedItem = null;
            txtNombres.Text= string.Empty;
            txtApellidos.Text= string.Empty;
            ImagenCiudadano.Image = null;
        }
        private void VisibilidadCamposRellenables()
        {
            var pathImage = $"{AppDomain.CurrentDomain.BaseDirectory}\\usuario.png";
            //Visibilidad de campos rellenables.
            txtLugarNacimiento.Visible = true;
            FechaDeNacimientoPicker.Visible = true;
            txtNacionalidad.Visible = true;
            FechaDeNacimientoPicker.Visible = true;
            SangreComboBox.Visible = true;
            SexoComboBox.Visible = true;
            EstadoCivilComboBox.Visible = true;
            FechaDeEmisionPicker.Visible = true;
            txtNombres.Visible=true;
            txtApellidos.Visible = true;
            txtOcupacion.Visible = true;

            ImagenCiudadano.Image = Image.FromFile(pathImage);
            ImagenCiudadano.Visible = true;
            path = pathImage;

        }
        //Metodo de guardado
        private void OcultamientoCamposRellenables()
        {
            //Ocultamiento de campos rellenables.

            txtLugarNacimiento.Visible = false;
            FechaDeNacimientoPicker.Visible = false;
            txtNacionalidad.Visible = false;
            FechaDeNacimientoPicker.Visible = false;
            SangreComboBox.Visible = false;
            SexoComboBox.Visible = false;
            EstadoCivilComboBox.Visible = false;
            FechaDeEmisionPicker.Visible = false;
            txtNombres.Visible = false;
            txtApellidos.Visible = false;
            txtOcupacion.Visible = false;
            ImagenCiudadano.Visible = false;

        }
        //Definición de la clase ciudadano.
        public class Ciudadano
        {
            public Guid ID { get; set; }
            public string NumeroCedula { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string LugarNacimiento { get; set; }
            public DateTime FechaDeNacimiento { get; set; }
            public string Nacionalidad { get; set; }
            public object Sexo { get; set; }
            public object Sangre { get; set; }
            public object EstadoCivil { get; set; }
            public string Ocupacion { get; set; }
            public DateTime FechaEmision { get; set; }
            public string Imagen { get; set; }
        }
        private void CancelarButtom_Click(object sender, EventArgs e)
        {
            BorradoCamposRellenables();
            OcultamientoCamposRellenables();
            NuevoRegistro.Enabled = true; 
            CancelarButtom.Enabled = false;
            GuardarButtom.Enabled = false;
            EliminarButtom.Enabled = false;
            CargarEditarImagenButtom.Enabled = false;

        }
        private  void BuscarButtom_Click(object sender, EventArgs e)
        {
            
            EliminarButtom.Enabled = true;
            CancelarButtom.Enabled = true;
            GuardarButtom.Enabled = true;
            CargarEditarImagenButtom.Enabled = true;
            VisibilidadCamposRellenables();
            txtNumeroCedula.Visible = true;
            int j = 0;
            var json = string.Empty;
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\Ciudadanos.json";
            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                ciudadanoList = JsonConvert.DeserializeObject<List<Ciudadano>>(json);
                dgvDatosCiudadanos.DataSource = ciudadanoList;
                for (int i = 0; i < ciudadanoList.Count; i++)
                {
                    if (ciudadanoList[i].NumeroCedula == txtNumeroCedula.Text.ToString())
                    {
                        txtNombres.Text = ciudadanoList[i].Nombres.ToString();
                        txtApellidos.Text = ciudadanoList[i].Apellidos.ToString();
                        txtLugarNacimiento.Text = ciudadanoList[i].LugarNacimiento.ToString();
                        FechaDeNacimientoPicker.Value = ciudadanoList[i].FechaDeNacimiento;
                        txtNacionalidad.Text = ciudadanoList[i].Nacionalidad.ToString();
                        txtOcupacion.Text = ciudadanoList[i].Ocupacion.ToString();
                        SexoComboBox.SelectedItem = ciudadanoList[i].Sexo;
                        SangreComboBox.SelectedItem = ciudadanoList[i].Sangre;
                        EstadoCivilComboBox.SelectedItem = ciudadanoList[i].EstadoCivil;
                        FechaDeEmisionPicker.Value = ciudadanoList[i].FechaEmision;
                        ImagenCiudadano.Image = Image.FromFile(ciudadanoList[i].Imagen);
                        j = 1;
                        break;
                    }
                }
                if (j == 0)
                {
                    MessageBox.Show($"El ciudadano con cedula {txtNumeroCedula.Text}, no existe en la base de datos");
                }
            }
            else
            {
                MessageBox.Show($"Aún no ha ingresado ningun dato.");
            }
            json = JsonConvert.SerializeObject(ciudadanoList);
            var sv = new StreamWriter(pathFile, false, Encoding.UTF8);
            sv.Write(json);
            sv.Close();

        }
        private void EliminarButtom_Click(object sender, EventArgs e)
        {
            CancelarButtom.Enabled = false;
            GuardarButtom.Enabled = false;
            EliminarButtom.Enabled = false;
            CargarEditarImagenButtom.Enabled = false;
            Eliminar();
            OcultamientoCamposRellenables();
        }
        private void dataGridViewCiudadanos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        public void GuardarImagenPath(string path)
        {

        }

        private void CargarEditarImagenButtom_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogImagen.ShowDialog() == DialogResult.OK)
                {
                    string imagen = openFileDialogImagen.FileName;
                    ImagenCiudadano.Image= Image.FromFile(imagen);
                    path = imagen;
                    txtNumeroCedula.Text = imagen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido");
            }

        }

        private void ImagenCiudadano_Click(object sender, EventArgs e)
        {

        }
    }

}