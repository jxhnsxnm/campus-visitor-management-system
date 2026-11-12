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
    public partial class FormUsuarios : Form
    {
        private readonly BusinessLogic businessLogic;

        // Campos del formulario
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtFechaNacimiento;
        private TextBox txtTipoUsuario;
        private TextBox txtUsuarioName;
        private TextBox txtContraseña;

        private Button btnAgregar;
        private Button btnModificar;
        private Button btnEliminar;

        private DataGridView dgvUsuarios;

        public FormUsuarios()
        {
            InitializeComponent();
            businessLogic = new BusinessLogic("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON");
            Begin();
        }

        private void Begin()
        {
            // Inicialización de los TextBoxes
            txtNombre = new TextBox { Location = new System.Drawing.Point(150, 30), Width = 200 };
            txtApellido = new TextBox { Location = new System.Drawing.Point(150, 70), Width = 200 };
            txtFechaNacimiento = new TextBox { Location = new System.Drawing.Point(150, 110), Width = 200 };
            txtTipoUsuario = new TextBox { Location = new System.Drawing.Point(150, 150), Width = 200 };
            txtUsuarioName = new TextBox { Location = new System.Drawing.Point(150, 190), Width = 200 };
            txtContraseña = new TextBox { Location = new System.Drawing.Point(150, 230), Width = 200 };

            // Inicialización de los Labels
            var lblNombre = new Label { Text = "Nombre:", Location = new System.Drawing.Point(50, 30) };
            var lblApellido = new Label { Text = "Apellido:", Location = new System.Drawing.Point(50, 70) };
            var lblFechaNacimiento = new Label { Text = "Fecha Nacimiento:", Location = new System.Drawing.Point(50, 110) };
            var lblTipoUsuario = new Label { Text = "Tipo de Usuario:", Location = new System.Drawing.Point(50, 150) };
            var lblUsuarioName = new Label { Text = "Nombre de Usuario:", Location = new System.Drawing.Point(50, 190) };
            var lblContraseña = new Label { Text = "Contraseña:", Location = new System.Drawing.Point(50, 230) };

            // Inicialización de los Botones
            btnAgregar = new Button { Text = "Agregar Usuario", Location = new System.Drawing.Point(50, 270) };
            btnModificar = new Button { Text = "Modificar Usuario", Location = new System.Drawing.Point(180, 270) };
            btnEliminar = new Button { Text = "Eliminar Usuario", Location = new System.Drawing.Point(310, 270) };

            // Inicialización del DataGridView
            dgvUsuarios = new DataGridView { Location = new System.Drawing.Point(50, 310), Width = 500, Height = 200 };

            // Configuración del DataGridView
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Columns.Add("UsuarioID", "ID");
            dgvUsuarios.Columns.Add("Nombre", "Nombre");
            dgvUsuarios.Columns.Add("Apellido", "Apellido");
            dgvUsuarios.Columns.Add("FechaNacimiento", "Fecha de Nacimiento");
            dgvUsuarios.Columns.Add("TipoUsuario", "Tipo de Usuario");
            dgvUsuarios.Columns.Add("UsuarioName", "Nombre de Usuario");
            dgvUsuarios.Columns.Add("Contraseña", "Contraseña");

            // Agregar eventos a los botones
            btnAgregar.Click += BtnAgregar_Click;
            btnModificar.Click += BtnModificar_Click;
            btnEliminar.Click += BtnEliminar_Click;

            // Agregar evento de selección en el DataGridView
            dgvUsuarios.SelectionChanged += DgvUsuarios_SelectionChanged;

            // Agregar los controles al formulario
            Controls.Add(txtNombre);
            Controls.Add(txtApellido);
            Controls.Add(txtFechaNacimiento);
            Controls.Add(txtTipoUsuario);
            Controls.Add(txtUsuarioName);
            Controls.Add(txtContraseña);
            Controls.Add(lblNombre);
            Controls.Add(lblApellido);
            Controls.Add(lblFechaNacimiento);
            Controls.Add(lblTipoUsuario);
            Controls.Add(lblUsuarioName);
            Controls.Add(lblContraseña);
            Controls.Add(btnAgregar);
            Controls.Add(btnModificar);
            Controls.Add(btnEliminar);
            Controls.Add(dgvUsuarios);

            // Configuración del formulario
            Text = "Usuarios";
            Size = new System.Drawing.Size(600, 600);

            // Cargar usuarios en el DataGridView
            CargarUsuarios();
        }

        // Método para cargar los usuarios en el DataGridView
        private void CargarUsuarios()
        {
            try
            {
                var usuarios = businessLogic.ObtenerUsuarios();

                dgvUsuarios.Rows.Clear();
                foreach (var usuario in usuarios)
                {
                    dgvUsuarios.Rows.Add(usuario.UsuarioID, usuario.Nombre, usuario.Apellido, usuario.FechaNacimiento.ToString("yyyy-MM-dd"), usuario.TipoUsuario, usuario.UsuarioName, usuario.Contraseña);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los usuarios: {ex.Message}");
            }
        }

        // Evento para cuando cambia la selección en el DataGridView
        private void DgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                // Obtener los datos de la fila seleccionada
                var filaSeleccionada = dgvUsuarios.SelectedRows[0];

                txtNombre.Text = filaSeleccionada.Cells["Nombre"].Value.ToString();
                txtApellido.Text = filaSeleccionada.Cells["Apellido"].Value.ToString();
                txtFechaNacimiento.Text = filaSeleccionada.Cells["FechaNacimiento"].Value.ToString();
                txtTipoUsuario.Text = filaSeleccionada.Cells["TipoUsuario"].Value.ToString();
                txtUsuarioName.Text = filaSeleccionada.Cells["UsuarioName"].Value.ToString();
                txtContraseña.Text = filaSeleccionada.Cells["Contraseña"].Value.ToString();
            }
        }

        // Evento para agregar un usuario
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                    TipoUsuario = txtTipoUsuario.Text,
                    UsuarioName = txtUsuarioName.Text,
                    Contraseña = txtContraseña.Text
                };

                businessLogic.AgregarUsuario(usuario);
                MessageBox.Show("Usuario agregado correctamente.");
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el usuario: {ex.Message}");
            }
        }

        // Evento para modificar un usuario
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = new Usuario
                {
                    UsuarioID = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                    TipoUsuario = txtTipoUsuario.Text,
                    UsuarioName = txtUsuarioName.Text,
                    Contraseña = txtContraseña.Text
                };

                businessLogic.ModificarUsuario(usuario);
                MessageBox.Show("Usuario modificado correctamente.");
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el usuario: {ex.Message}");
            }
        }

        // Evento para eliminar un usuario
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarioID = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);

                businessLogic.EliminarUsuario(usuarioID);
                MessageBox.Show("Usuario eliminado correctamente.");
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el usuario: {ex.Message}");
            }
        }
    }
}