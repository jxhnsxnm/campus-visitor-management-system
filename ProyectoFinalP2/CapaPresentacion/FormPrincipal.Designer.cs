namespace CapaPresentacion
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRegistrarVisitas = new System.Windows.Forms.Button();
            this.btnGestionUsuarios = new System.Windows.Forms.Button();
            this.btnGestionEdificios = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConsultaVisitas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegistrarVisitas
            // 
            this.btnRegistrarVisitas.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarVisitas.Location = new System.Drawing.Point(165, 152);
            this.btnRegistrarVisitas.Name = "btnRegistrarVisitas";
            this.btnRegistrarVisitas.Size = new System.Drawing.Size(202, 39);
            this.btnRegistrarVisitas.TabIndex = 0;
            this.btnRegistrarVisitas.Text = "Registrar Visitas";
            this.btnRegistrarVisitas.UseVisualStyleBackColor = true;
            this.btnRegistrarVisitas.Click += new System.EventHandler(this.btnRegistrarVisitas_Click);
            // 
            // btnGestionUsuarios
            // 
            this.btnGestionUsuarios.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionUsuarios.Location = new System.Drawing.Point(165, 269);
            this.btnGestionUsuarios.Name = "btnGestionUsuarios";
            this.btnGestionUsuarios.Size = new System.Drawing.Size(202, 39);
            this.btnGestionUsuarios.TabIndex = 1;
            this.btnGestionUsuarios.Text = "Gestion Usuarios";
            this.btnGestionUsuarios.UseVisualStyleBackColor = true;
            this.btnGestionUsuarios.Click += new System.EventHandler(this.btnGestionUsuarios_Click);
            // 
            // btnGestionEdificios
            // 
            this.btnGestionEdificios.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionEdificios.Location = new System.Drawing.Point(165, 327);
            this.btnGestionEdificios.Name = "btnGestionEdificios";
            this.btnGestionEdificios.Size = new System.Drawing.Size(202, 39);
            this.btnGestionEdificios.TabIndex = 2;
            this.btnGestionEdificios.Text = "Gestion Edificios";
            this.btnGestionEdificios.UseVisualStyleBackColor = true;
            this.btnGestionEdificios.Click += new System.EventHandler(this.btnGestionEdificios_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seleccione una Opcion";
            // 
            // btnConsultaVisitas
            // 
            this.btnConsultaVisitas.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultaVisitas.Location = new System.Drawing.Point(165, 210);
            this.btnConsultaVisitas.Name = "btnConsultaVisitas";
            this.btnConsultaVisitas.Size = new System.Drawing.Size(202, 39);
            this.btnConsultaVisitas.TabIndex = 1;
            this.btnConsultaVisitas.Text = "Consulta Visitas";
            this.btnConsultaVisitas.UseVisualStyleBackColor = true;
            this.btnConsultaVisitas.Click += new System.EventHandler(this.btnConsultaVisitas_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 397);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGestionEdificios);
            this.Controls.Add(this.btnConsultaVisitas);
            this.Controls.Add(this.btnGestionUsuarios);
            this.Controls.Add(this.btnRegistrarVisitas);
            this.Name = "FormPrincipal";
            this.Text = "FormPrincipal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegistrarVisitas;
        private System.Windows.Forms.Button btnGestionUsuarios;
        private System.Windows.Forms.Button btnGestionEdificios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConsultaVisitas;
    }
}