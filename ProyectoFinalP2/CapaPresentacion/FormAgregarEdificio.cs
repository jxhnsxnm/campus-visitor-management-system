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

namespace CapaPresentacion
{
    public partial class FormAgregarEdificio : Form
    {
        private readonly BusinessLogic businessLogic;

        public FormAgregarEdificio()
        {
            Begin();
            businessLogic = new BusinessLogic("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON");
        }

        private void Begin()
        {
            // Label para el nombre del edificio
            lblNombreEdificio = new Label
            {
                Text = "Nombre del Edificio:",
                Location = new System.Drawing.Point(50, 30),
                Size = new System.Drawing.Size(120, 30)
            };

            // TextBox para el nombre del edificio
            txtNombreEdificio = new TextBox
            {
                Location = new System.Drawing.Point(180, 30),
                Width = 200,
                Height = 30
            };

            // Botón para agregar el edificio
            btnAgregarEdificio = new Button
            {
                Text = "Agregar Edificio",
                Location = new System.Drawing.Point(50, 70),
                Width = 200,
                Height = 30
            };

            // Evento para el botón
            btnAgregarEdificio.Click += BtnAgregarEdificio_Click;

            // Agregar controles al formulario
            Controls.Add(lblNombreEdificio);
            Controls.Add(txtNombreEdificio);
            Controls.Add(btnAgregarEdificio);

            // Configuración del formulario
            Text = "Agregar Edificio";
            Size = new System.Drawing.Size(400, 150);
        }

        private void BtnAgregarEdificio_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el nombre del edificio desde el TextBox
                string nombreEdificio = txtNombreEdificio.Text.Trim();

                if (string.IsNullOrEmpty(nombreEdificio))
                {
                    MessageBox.Show("El nombre del edificio no puede estar vacío.");
                    return;
                }

                // Llamar al método de negocio para agregar el edificio
                businessLogic.AgregarEdificio(nombreEdificio);

                MessageBox.Show("Edificio agregado correctamente.");
                this.Close(); // Cerrar el formulario después de agregar el edificio
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el edificio: {ex.Message}");
            }
        }

        // Controles del formulario
        private Label lblNombreEdificio;
        private TextBox txtNombreEdificio;
        private Button btnAgregarEdificio;
    }
}
