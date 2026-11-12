using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormConsultarVisitas : Form
    {
        private readonly BusinessLogic businessLogic;

        public FormConsultarVisitas()
        {
            InitializeComponent();
            businessLogic = new BusinessLogic("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON\r\n");
            ConfigureForm();
        }

        private void ConfigureForm()
        {
            // Label y ComboBox para seleccionar edificio
            lblEdificio = new Label
            {
                Text = "Edificio:",
                Location = new Point(20, 20),
                Size = new Size(100, 30)
            };
            cmbEdificio = new ComboBox
            {
                Location = new Point(130, 20),
                Width = 200
            };

            // Label y DateTimePicker para rango de fechas
            lblFechaInicio = new Label
            {
                Text = "Fecha Inicio:",
                Location = new Point(20, 60),
                Size = new Size(100, 30)
            };
            dtpFechaInicio = new DateTimePicker
            {
                Location = new Point(130, 60),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                ShowCheckBox = true // Para habilitar o deshabilitar el filtro de fecha
            };

            lblFechaFin = new Label
            {
                Text = "Fecha Fin:",
                Location = new Point(20, 100),
                Size = new Size(100, 30)
            };
            dtpFechaFin = new DateTimePicker
            {
                Location = new Point(130, 100),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                ShowCheckBox = true
            };

            // Botón para consultar visitas
            btnConsultar = new Button
            {
                Text = "Consultar",
                Location = new Point(350, 60),
                Width = 120,
                Height = 30
            };
            btnConsultar.Click += BtnConsultar_Click;

            // DataGridView para mostrar resultados
            dgvVisitas = new DataGridView
            {
                Location = new Point(20, 140),
                Size = new Size(750, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Configuración del formulario
            Text = "Consultar Visitas";
            Size = new Size(800, 500);

            // Cargar los edificios en el ComboBox
            LoadEdificios();

            // Agregar controles al formulario
            Controls.Add(lblEdificio);
            Controls.Add(cmbEdificio);
            Controls.Add(lblFechaInicio);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(dtpFechaFin);
            Controls.Add(btnConsultar);
            Controls.Add(dgvVisitas);
        }

        private void LoadEdificios()
        {
            try
            {
                var edificios = businessLogic.ObtenerEdificios();

                if (edificios != null && edificios.Count > 0)
                {
                    cmbEdificio.DataSource = edificios;
                    cmbEdificio.DisplayMember = "Nombre";
                    cmbEdificio.ValueMember = "EdificioID";
                    cmbEdificio.SelectedIndex = -1; // Para que inicialmente no haya ningún edificio seleccionado
                }
                else
                {
                    MessageBox.Show("No se encontraron edificios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar edificios: {ex.Message}");
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores del filtro
                int? edificioId = cmbEdificio.SelectedIndex >= 0 ? (int?)cmbEdificio.SelectedValue : null;
                DateTime? fechaInicio = dtpFechaInicio.Checked ? (DateTime?)dtpFechaInicio.Value : null;
                DateTime? fechaFin = dtpFechaFin.Checked ? (DateTime?)dtpFechaFin.Value : null;

                // Validar que la fecha fin no sea anterior a la fecha inicio
                if (fechaInicio != null && fechaFin != null && fechaFin < fechaInicio)
                {
                    MessageBox.Show("La fecha fin debe ser posterior a la fecha inicio.");
                    return;
                }

                // Consultar visitas a través de la capa de negocio
                var visitas = businessLogic.ConsultarVisitas(edificioId, fechaInicio, fechaFin);

                // Mostrar resultados en el DataGridView
                dgvVisitas.DataSource = visitas;

                if (visitas.Count == 0)
                {
                    MessageBox.Show("No se encontraron visitas con los filtros aplicados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar visitas: {ex.Message}");
            }
        }

        // Controles del formulario
        private Label lblEdificio, lblFechaInicio, lblFechaFin;
        private ComboBox cmbEdificio;
        private DateTimePicker dtpFechaInicio, dtpFechaFin;
        private Button btnConsultar;
        private DataGridView dgvVisitas;
    }
}
