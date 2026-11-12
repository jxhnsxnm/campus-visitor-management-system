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
    public partial class FormLogin : Form
    {
        private BusinessLogic businessLogic;

        public FormLogin()
        {
            InitializeComponent();
            businessLogic = new BusinessLogic("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON\r\n");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuarioName = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            try
            {
                // Implementar validación de usuario
                var usuario = businessLogic.ObtenerUsuario(usuarioName, contraseña); // Método a agregar en lógica de negocio
                if (usuario != null)
                {
                    FormPrincipal principal = new FormPrincipal(usuario);
                    this.Hide();
                    principal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
