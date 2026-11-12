using CapaEntidad;
using CapaNegocio;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormAgregarAula : Form
    {
        private readonly BusinessLogic businessLogic;
        private int edificioId;

        public FormAgregarAula(int edificioId)
        {
            InitializeComponent();
            Begin();// Inicializa los componentes del formulario
            businessLogic = new BusinessLogic("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON");
            this.edificioId = edificioId;

            // Cargar los edificios en el ComboBox
            LoadEdificios();
        }

        private void Begin()
        {
            // Label para seleccionar el edificio
            lblSeleccionarEdificio = new Label
            {
                Text = "Seleccionar Edificio:",
                Location = new System.Drawing.Point(50, 30),
                Size = new System.Drawing.Size(120, 30)
            };

            // ComboBox para seleccionar el edificio
            cmbEdificios = new ComboBox
            {
                Location = new System.Drawing.Point(180, 30),
                Width = 200,
                Height = 30
            };

            // Label para el nombre del aula
            lblNombreAula = new Label
            {
                Text = "Nombre del Aula:",
                Location = new System.Drawing.Point(50, 70),
                Size = new System.Drawing.Size(120, 30)
            };

            // TextBox para el nombre del aula
            txtNombreAula = new TextBox
            {
                Location = new System.Drawing.Point(180, 70),
                Width = 200,
                Height = 30
            };

            // Botón para agregar el aula
            btnAgregarAula = new Button
            {
                Text = "Agregar Aula",
                Location = new System.Drawing.Point(50, 110),
                Width = 200,
                Height = 30
            };

            // Evento para el botón
            btnAgregarAula.Click += BtnAgregarAula_Click;

            // Agregar controles al formulario
            Controls.Add(lblSeleccionarEdificio);
            Controls.Add(cmbEdificios);
            Controls.Add(lblNombreAula);
            Controls.Add(txtNombreAula);
            Controls.Add(btnAgregarAula);

            // Configuración del formulario
            Text = "Agregar Aula";
            Size = new System.Drawing.Size(400, 200);
        }

        private void LoadEdificios()
        {
            try
            {
                // Obtener los edificios de la base de datos
                var edificios = businessLogic.ObtenerEdificios();

                // Verificar si la lista de edificios está vacía o nula
                if (edificios != null && edificios.Count > 0)
                {
                    cmbEdificios.DataSource = edificios;
                    cmbEdificios.DisplayMember = "Nombre";
                    cmbEdificios.ValueMember = "EdificioID";

                    // Seleccionar el edificio que se pasó al constructor
                    cmbEdificios.SelectedValue = this.edificioId;
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

        private void BtnAgregarAula_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones antes de agregar el aula
                string nombreAula = txtNombreAula.Text.Trim();
                if (string.IsNullOrEmpty(nombreAula))
                {
                    MessageBox.Show("El nombre del aula no puede estar vacío.");
                    return;
                }

                // Obtener el EdificioID seleccionado
                int selectedEdificioId = Convert.ToInt32(cmbEdificios.SelectedValue);

                // Llamar al método de negocio para agregar el aula
                businessLogic.AgregarAula(selectedEdificioId, nombreAula);

                MessageBox.Show("Aula agregada correctamente.");
                this.Close(); // Cerrar el formulario después de agregar el aula
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el aula: {ex.Message}");
            }
        }

        // Controles del formulario
        private Label lblSeleccionarEdificio, lblNombreAula;
        private ComboBox cmbEdificios;
        private TextBox txtNombreAula;
        private Button btnAgregarAula;
    }
}