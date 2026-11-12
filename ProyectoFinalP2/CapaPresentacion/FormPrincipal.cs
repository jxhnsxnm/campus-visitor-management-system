using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;
using CapaDatos;

namespace CapaPresentacion
{
    public partial class FormPrincipal : Form
    {
        private Usuario usuarioActual;

        public FormPrincipal(Usuario usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;

            // Personaliza opciones según el tipo de usuario
            if (usuarioActual.TipoUsuario != "Administrador")
            {
                btnConsultaVisitas.Enabled = false;
                btnGestionUsuarios.Enabled = false;
                btnGestionEdificios.Enabled = false;

            }
        }

        private void btnRegistrarVisitas_Click(object sender, EventArgs e)
        {
            FormVisitas visitasForm = new FormVisitas();
            visitasForm.Show();
        }

        private void btnConsultaVisitas_Click(object sender, EventArgs e)
        {
            FormConsultarVisitas consultasForm = new FormConsultarVisitas();
            consultasForm.Show();
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuarios usuariosForm = new FormUsuarios();
            usuariosForm.Show();
        }

        private void btnGestionEdificios_Click(object sender, EventArgs e)
        {
            FormEdificiosAulas edificiosForm = new FormEdificiosAulas();
            edificiosForm.Show();
        }
    }
}
