using CapaNegocio;
using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CapaPresentacion
{
    public partial class FormVisitas : Form
    {
        private readonly BusinessLogic businessLogic;

        public FormVisitas()
        {
            Begin(); // Método para inicializar componentes
            businessLogic = new BusinessLogic("Server=JA-Johnson-M\\JOHNSON;Database=ProyectoFinalP2;Integrated Security=True;");
            this.Load += new System.EventHandler(this.FormVisitas_Load);
        }

        private void FormVisitas_Load(object sender, EventArgs e)
        {
            CargarEdificios();
        }

        private void Begin()
        {
            // Inicializar controles existentes
            lblEdificio = new Label { Text = "Edificio:", Location = new Point(20, 20) };
            lblAula = new Label { Text = "Aula:", Location = new Point(20, 60) };
            lblNombre = new Label { Text = "Nombre:", Location = new Point(20, 100) };
            lblNombre = new Label { Text = "Nombre:", Location = new Point(20, 100) };
            lblApellido = new Label { Text = "Apellido:", Location = new Point(20, 140) };
            lblCarrera = new Label { Text = "Carrera:", Location = new Point(20, 180) };
            lblCorreo = new Label { Text = "Correo:", Location = new Point(20, 220) };
            lblMotivo = new Label { Text = "Motivo:", Location = new Point(20, 260) };
            lblHoraEntrada = new Label { Text = "Hora Entrada:", Location = new Point(20, 300) };
            lblHoraSalida = new Label { Text = "Hora Salida:", Location = new Point(20, 340) };
            lblFoto = new Label { Text = "Foto:", Location = new Point(20, 380) };

            cmbEdificios = new ComboBox { Location = new Point(120, 20), Width = 200 };
            cmbAulas = new ComboBox { Location = new Point(120, 60), Width = 200 };
            txtNombre = new TextBox { Location = new Point(120, 100), Width = 200 };
            txtApellido = new TextBox { Location = new Point(120, 140), Width = 200 };
            txtCarrera = new TextBox { Location = new Point(120, 180), Width = 200 }; // Carrera
            txtCorreo = new TextBox { Location = new Point(120, 220), Width = 200 };  // Correo
            txtMotivo = new TextBox { Location = new Point(120, 260), Width = 200 };
            dtpHoraEntrada = new DateTimePicker { Location = new Point(120, 300), Format = DateTimePickerFormat.Time };
            dtpHoraSalida = new DateTimePicker { Location = new Point(120, 340), Format = DateTimePickerFormat.Time };

            picFoto = new PictureBox
            {
                Location = new Point(120, 380),
                Size = new Size(100, 100),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            btnCargarFoto = new Button { Text = "Cargar Foto", Location = new Point(230, 380), Width = 90 };
            btnRegistrar = new Button { Text = "Registrar Visita", Location = new Point(120, 500) };

            // Eventos
            btnRegistrar.Click += BtnRegistrar_Click;
            cmbEdificios.SelectedIndexChanged += CmbEdificios_SelectedIndexChanged;
            btnCargarFoto.Click += BtnCargarFoto_Click;

            // Agregar controles al formulario
            Controls.Add(lblEdificio);
            Controls.Add(cmbEdificios);
            Controls.Add(lblAula);
            Controls.Add(cmbAulas);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblCarrera);
            Controls.Add(txtCarrera); // Carrera
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);  // Correo
            Controls.Add(lblMotivo);
            Controls.Add(txtMotivo);
            Controls.Add(lblHoraEntrada);
            Controls.Add(dtpHoraEntrada);
            Controls.Add(lblHoraSalida);
            Controls.Add(dtpHoraSalida);
            Controls.Add(lblFoto);
            Controls.Add(picFoto);
            Controls.Add(btnCargarFoto);
            Controls.Add(btnRegistrar);

            // Configuración del formulario
            Text = "Registro de Visitas";
            Size = new Size(400, 600); // Ajustar tamaño para nuevos controles
        }


        // Controles del formulario
        private Label lblEdificio, lblAula, lblNombre, lblApellido, lblCarrera, lblCorreo, lblMotivo, lblHoraEntrada, lblHoraSalida, lblFoto;
        private ComboBox cmbEdificios, cmbAulas;
        private TextBox txtNombre, txtApellido, txtCarrera, txtCorreo, txtMotivo;
        private DateTimePicker dtpHoraEntrada, dtpHoraSalida;
        private PictureBox picFoto;
        private Button btnRegistrar, btnCargarFoto;

        private void CargarEdificios()
        {
            try
            {
                var edificios = businessLogic.ObtenerEdificios();

                Console.WriteLine($"Cantidad de edificios obtenidos: {edificios.Count}");
                if (edificios != null && edificios.Count > 0)
                {
                    cmbEdificios.DataSource = edificios;
                    cmbEdificios.DisplayMember = "Nombre";
                    cmbEdificios.ValueMember = "EdificioID";
                    cmbEdificios.SelectedIndex = -1; // Sin selección inicial
                }
                else
                {
                    cmbEdificios.DataSource = null;
                    MessageBox.Show("No se encontraron edificios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar edificios: {ex.Message}");
            }
        }

        private void CargarAulas(int edificioId)
        {
            try
            {
                var aulas = businessLogic.ObtenerAulasPorEdificio(edificioId);

                Console.WriteLine($"Cantidad de aulas obtenidas: {aulas.Count}");
                if (aulas != null && aulas.Count > 0)
                {
                    cmbAulas.DataSource = aulas;
                    cmbAulas.DisplayMember = "NombreAula";
                    cmbAulas.ValueMember = "AulaID";
                    cmbAulas.SelectedIndex = -1; // Sin selección inicial
                }
                else
                {
                    cmbAulas.DataSource = null;
                    MessageBox.Show("No se encontraron aulas para el edificio seleccionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar aulas: {ex.Message}");
            }
        }

        private void CmbEdificios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEdificios.SelectedItem != null)
            {
                var edificio = (Edificio)cmbEdificios.SelectedItem;
                Console.WriteLine($"Edificio seleccionado: {edificio.Nombre}, ID: {edificio.EdificioID}");
                CargarAulas(edificio.EdificioID);
            }
            else
            {
                cmbAulas.DataSource = null;
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEdificios.SelectedValue == null || cmbAulas.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un edificio y un aula válidos.");
                    return;
                }

                var visita = new Visita
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Carrera = txtCarrera.Text.Trim(), // Nuevo campo
                    Correo = txtCorreo.Text.Trim(),   // Nuevo campo
                    Motivo = txtMotivo.Text.Trim(),
                    HoraEntrada = dtpHoraEntrada.Value,
                    HoraSalida = dtpHoraSalida.Value,
                    EdificioID = Convert.ToInt32(cmbEdificios.SelectedValue),
                    AulaID = Convert.ToInt32(cmbAulas.SelectedValue),
                    Foto = ObtenerFoto() // Método para obtener la foto
                };

                businessLogic.RegistrarVisita(visita);
                MessageBox.Show("Visita registrada exitosamente.");
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la visita: {ex.Message}");
            }
        }

        private byte[] ObtenerFoto()
        {
            if (picFoto.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    picFoto.Image.Save(ms, picFoto.Image.RawFormat);
                    return ms.ToArray();
                }
            }
            return null;
        }

        private void BtnCargarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picFoto.Image = Image.FromFile(ofd.FileName);
                }
            }
        }


        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtCarrera.Clear(); // Limpiar carrera
            txtCorreo.Clear();  // Limpiar correo
            txtMotivo.Clear();
            cmbEdificios.SelectedIndex = -1;
            cmbAulas.DataSource = null;
            dtpHoraEntrada.Value = DateTime.Now;
            dtpHoraSalida.Value = DateTime.Now;
            picFoto.Image = null; // Limpiar la imagen
        }

    }
}
