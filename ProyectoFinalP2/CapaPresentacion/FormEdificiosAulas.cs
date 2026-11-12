using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaDatos;

namespace CapaPresentacion
{
    public partial class FormEdificiosAulas : Form
    {
        private readonly BusinessLogic businessLogic;

        public FormEdificiosAulas()
        {
            Begin();
            businessLogic = new BusinessLogic("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON");
            this.Load += new System.EventHandler(this.FormEdificiosAulas_Load);
        }

        private void FormEdificiosAulas_Load(object sender, EventArgs e)
        {
            CargarEdificios();
        }

        private void CargarEdificios()
        {
            try
            {
                var edificios = businessLogic.ObtenerEdificios();
                if (edificios != null && edificios.Count > 0)
                {
                    dgvEdificios.DataSource = edificios;
                    dgvEdificios.Columns["EdificioID"].Visible = false; // Ocultar la columna de ID
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

        private void CargarAulas(int edificioId)
        {
            try
            {
                var aulas = businessLogic.ObtenerAulasPorEdificio(edificioId);
                if (aulas != null && aulas.Count > 0)
                {
                    dgvAulas.DataSource = aulas;
                    dgvAulas.Columns["AulaID"].Visible = false; // Ocultar la columna de ID
                }
                else
                {
                    MessageBox.Show("No se encontraron aulas para este edificio.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar aulas: {ex.Message}");
            }
        }

        private void dgvEdificios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cuando se selecciona un edificio, cargamos las aulas correspondientes
            if (e.RowIndex >= 0)
            {
                var edificio = (Edificio)dgvEdificios.Rows[e.RowIndex].DataBoundItem;
                CargarAulas(edificio.EdificioID);
            }
        }

        private void btnAgregarEdificio_Click(object sender, EventArgs e)
        {
            var frmAgregarEdificio = new FormAgregarEdificio();
            frmAgregarEdificio.ShowDialog();
            CargarEdificios(); // Recargar edificios después de agregar uno
        }

        private void btnAgregarAula_Click(object sender, EventArgs e)
        {
            if (dgvEdificios.SelectedRows.Count > 0)
            {
                var edificio = (Edificio)dgvEdificios.SelectedRows[0].DataBoundItem;
                var frmAgregarAula = new FormAgregarAula(edificio.EdificioID);
                frmAgregarAula.ShowDialog();
                CargarAulas(edificio.EdificioID); // Recargar aulas después de agregar una
            }
            else
            {
                MessageBox.Show("Debe seleccionar un edificio para agregar un aula.");
            }
        }

        private void btnEliminarEdificio_Click(object sender, EventArgs e)
        {
            if (dgvEdificios.SelectedRows.Count > 0)
            {
                var edificio = (Edificio)dgvEdificios.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"¿Está seguro de que desea eliminar el edificio '{edificio.Nombre}'?", "Eliminar Edificio", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    businessLogic.EliminarEdificio(edificio.EdificioID);
                    CargarEdificios(); // Recargar la lista de edificios
                    dgvAulas.DataSource = null; // Limpiar aulas
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un edificio para eliminar.");
            }
        }

        private void btnEliminarAula_Click(object sender, EventArgs e)
        {
            if (dgvAulas.SelectedRows.Count > 0)
            {
                var aula = (Aula)dgvAulas.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"¿Está seguro de que desea eliminar el aula '{aula.NombreAula}'?", "Eliminar Aula", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    businessLogic.EliminarAula(aula.AulaID);
                    var edificio = (Edificio)dgvEdificios.SelectedRows[0].DataBoundItem;
                    CargarAulas(edificio.EdificioID); // Recargar las aulas del edificio
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un aula para eliminar.");
            }
        }

        private void BtnModificarEdificio_Click(object sender, EventArgs e)
        {
            if (dgvEdificios.SelectedRows.Count == 1)
            {
                int edificioId = Convert.ToInt32(dgvEdificios.SelectedRows[0].Cells["EdificioID"].Value);
                string nuevoNombre = txtNuevoNombreEdificio.Text.Trim();

                if (string.IsNullOrEmpty(nuevoNombre))
                {
                    MessageBox.Show("Por favor, ingrese un nuevo nombre para el edificio.");
                    return;
                }

                try
                {
                    businessLogic.ModificarEdificio(edificioId, nuevoNombre);
                    MessageBox.Show("Edificio modificado exitosamente.");
                    CargarEdificios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al modificar edificio: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un edificio para modificar.");
            }
        }

        // Método para modificar el nombre de un aula
        private void BtnModificarAula_Click(object sender, EventArgs e)
        {
            if (dgvAulas.SelectedRows.Count == 1)
            {
                int aulaId = Convert.ToInt32(dgvAulas.SelectedRows[0].Cells["AulaID"].Value);
                string nuevoNombreAula = txtNuevoNombreAula.Text.Trim();

                if (string.IsNullOrEmpty(nuevoNombreAula))
                {
                    MessageBox.Show("Por favor, ingrese un nuevo nombre para el aula.");
                    return;
                }

                try
                {
                    businessLogic.ModificarAula(aulaId, nuevoNombreAula);
                    MessageBox.Show("Aula modificada exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al modificar aula: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un aula para modificar.");
            }
        }


        #region Controles y eventos de la interfaz

        private DataGridView dgvEdificios;
        private DataGridView dgvAulas;
        private Button btnAgregarEdificio;
        private Button btnAgregarAula;
        private Button btnEliminarEdificio;
        private Button btnEliminarAula;
        private Label lblNuevoNombreEdificio, lblNuevoNombreAula;
        private TextBox txtNuevoNombreEdificio, txtNuevoNombreAula;
        private Button btnModificarEdificio, btnModificarAula;


        private void Begin()
        {
            this.dgvEdificios = new DataGridView();
            this.dgvAulas = new DataGridView();
            this.btnAgregarEdificio = new Button();
            this.btnAgregarAula = new Button();
            this.btnEliminarEdificio = new Button();
            this.btnEliminarAula = new Button();

            // dgvEdificios
            this.dgvEdificios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdificios.Location = new Point(20, 20);
            this.dgvEdificios.Name = "dgvEdificios";
            this.dgvEdificios.Size = new Size(600, 200);
            this.dgvEdificios.CellClick += new DataGridViewCellEventHandler(this.dgvEdificios_CellClick);

            // dgvAulas
            this.dgvAulas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAulas.Location = new Point(20, 240);
            this.dgvAulas.Name = "dgvAulas";
            this.dgvAulas.Size = new Size(600, 200);

            // btnAgregarEdificio
            this.btnAgregarEdificio.Location = new Point(640, 20);
            this.btnAgregarEdificio.Name = "btnAgregarEdificio";
            this.btnAgregarEdificio.Size = new Size(120, 30);
            this.btnAgregarEdificio.Text = "Agregar Edificio";
            this.btnAgregarEdificio.Click += new EventHandler(this.btnAgregarEdificio_Click);

            // btnAgregarAula
            this.btnAgregarAula.Location = new Point(640, 60);
            this.btnAgregarAula.Name = "btnAgregarAula";
            this.btnAgregarAula.Size = new Size(120, 30);
            this.btnAgregarAula.Text = "Agregar Aula";
            this.btnAgregarAula.Click += new EventHandler(this.btnAgregarAula_Click);

            // btnEliminarEdificio
            this.btnEliminarEdificio.Location = new Point(640, 100);
            this.btnEliminarEdificio.Name = "btnEliminarEdificio";
            this.btnEliminarEdificio.Size = new Size(120, 30);
            this.btnEliminarEdificio.Text = "Eliminar Edificio";
            this.btnEliminarEdificio.Click += new EventHandler(this.btnEliminarEdificio_Click);

            // btnEliminarAula
            this.btnEliminarAula.Location = new Point(640, 140);
            this.btnEliminarAula.Name = "btnEliminarAula";
            this.btnEliminarAula.Size = new Size(120, 30);
            this.btnEliminarAula.Text = "Eliminar Aula";
            this.btnEliminarAula.Click += new EventHandler(this.btnEliminarAula_Click);

            // FormEdificiosAulas
            this.ClientSize = new Size(800, 500);
            this.Controls.Add(this.dgvEdificios);
            this.Controls.Add(this.dgvAulas);
            this.Controls.Add(this.btnAgregarEdificio);
            this.Controls.Add(this.btnAgregarAula);
            this.Controls.Add(this.btnEliminarEdificio);
            this.Controls.Add(this.btnEliminarAula);
            this.Text = "Gestión de Edificios y Aulas";

            lblNuevoNombreEdificio = new Label { Text = "Nuevo Nombre Edificio:", Location = new System.Drawing.Point(640, 220), Width = 120 };
            lblNuevoNombreAula = new Label { Text = "Nuevo Nombre Aula:", Location = new System.Drawing.Point(640, 350), Width = 120 };

            txtNuevoNombreEdificio = new TextBox { Location = new System.Drawing.Point(640, 250), Width = 120};
            txtNuevoNombreAula = new TextBox { Location = new System.Drawing.Point(640, 380), Width = 120 };

            btnModificarEdificio = new Button { Text = "Modificar Edificio", Location = new System.Drawing.Point(640, 270), Width = 120, Height = 30 };
            btnModificarAula = new Button { Text = "Modificar Aula", Location = new System.Drawing.Point(640, 400), Width = 120, Height = 30 };


            btnModificarEdificio.Click += BtnModificarEdificio_Click;
            btnModificarAula.Click += BtnModificarAula_Click;

            // Agregar controles al formulario
            Controls.Add(lblNuevoNombreEdificio);
            Controls.Add(txtNuevoNombreEdificio);
            Controls.Add(lblNuevoNombreAula);
            Controls.Add(txtNuevoNombreAula);
            Controls.Add(btnModificarEdificio);
            Controls.Add(btnModificarAula);
            Controls.Add(dgvEdificios);
            Controls.Add(dgvAulas);

        }

        #endregion
    }
}
