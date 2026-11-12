using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TipoUsuario { get; set; }  // 'Administrador' o 'General'
        public string UsuarioName { get; set; }  // Nombre de usuario
        public string Contraseña { get; set; }   // Contraseña encriptada
    }

    public class Edificio
    {
        public int EdificioID { get; set; }
        public string Nombre { get; set; }
    }

    public class Aula
    {
        public int AulaID { get; set; }
        public int EdificioID { get; set; }
        public string NombreAula { get; set; }
        // Propiedad de navegación (para usarla en la capa de lógica de negocio si es necesario)
        public Edificio Edificio { get; set; }
    }

    public class Visita
    {
        public int VisitaID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Carrera { get; set; }
        public string Correo { get; set; }
        public int EdificioID { get; set; }
        public int AulaID { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public string Motivo { get; set; }
        public byte[] Foto { get; set; }  // Imagen almacenada como un array de bytes

        // Propiedades de navegación
        public Edificio Edificio { get; set; }
        public Aula Aula { get; set; }
    }

}
