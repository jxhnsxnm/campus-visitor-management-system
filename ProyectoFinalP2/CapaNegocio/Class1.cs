using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Data.SqlClient;
using System.Data;

namespace CapaNegocio
{
    public class BusinessLogic
    {
        private DataAccess dataAccess;

        private string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ProyectoFinalP2;Data Source=JA-Johnson-M\\JOHNSON";
        public BusinessLogic(string connectionString)
        {
            dataAccess = new DataAccess(connectionString);
        }

        // Método para registrar un usuario
        public void RegistrarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                throw new ArgumentException("El nombre y apellido son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(usuario.UsuarioName) || string.IsNullOrWhiteSpace(usuario.Contraseña))
            {
                throw new ArgumentException("El usuario y la contraseña son obligatorios.");
            }

            dataAccess.RegistrarUsuario(usuario);
        }

        public Usuario ObtenerUsuario(string usuarioName, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(usuarioName) || string.IsNullOrWhiteSpace(contraseña))
            {
                throw new ArgumentException("El usuario y la contraseña son obligatorios.");
            }

            return dataAccess.ObtenerUsuario(usuarioName, contraseña);
        }

        public List<Edificio> ObtenerEdificios()
        {
            return dataAccess.ObtenerEdificios();
        }

        public List<Aula> ObtenerAulasPorEdificio(int edificioId)
        {
            return dataAccess.ObtenerAulasPorEdificio(edificioId);
        }

        // Método para registrar una visita
        public void RegistrarVisita(Visita visita)
        {
            if (string.IsNullOrWhiteSpace(visita.Nombre) || string.IsNullOrWhiteSpace(visita.Apellido))
            {
                throw new ArgumentException("El nombre y apellido del visitante son obligatorios.");
            }

            if (visita.EdificioID <= 0 || visita.AulaID <= 0)
            {
                throw new ArgumentException("Debe seleccionar un edificio y un aula válidos.");
            }

            if (visita.HoraEntrada == null || visita.HoraSalida == null)
            {
                throw new ArgumentException("La hora de entrada y salida son obligatorias.");
            }

            if (visita.HoraSalida <= visita.HoraEntrada)
            {
                throw new ArgumentException("La hora de salida debe ser posterior a la hora de entrada.");
            }

            dataAccess.RegistrarVisita(visita);
        }

        // Método para consultar visitas por edificio y rango de fechas
        public List<Visita> ConsultarVisitas(int? edificioId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (fechaInicio != null && fechaFin != null && fechaFin < fechaInicio)
            {
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");
            }

            return dataAccess.ConsultarVisitas(edificioId, fechaInicio, fechaFin);
        }

        public void AgregarEdificio(string nombre)
        {
            try
            {
                dataAccess.AgregarEdificio(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio: " + ex.Message);
            }
        }

        public void AgregarAula(int edificioID, string nombreAula)
        {
            try
            {
                dataAccess.AgregarAula(edificioID, nombreAula);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio: " + ex.Message);
            }
        }

        public void ModificarEdificio(int edificioID, string nuevoNombre)
        {
            try
            {
                dataAccess.ModificarEdificio(edificioID, nuevoNombre);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio: " + ex.Message);
            }
        }

        public void ModificarAula(int aulaID, string nuevoNombreAula)
        {
            try
            {
                dataAccess.ModificarAula(aulaID, nuevoNombreAula);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio: " + ex.Message);
            }
        }

        public void EliminarEdificio(int edificioID)
        {
            try
            {
                dataAccess.EliminarEdificio(edificioID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio: " + ex.Message);
            }
        }

        public void EliminarAula(int aulaID)
        {
            try
            {
                dataAccess.EliminarAula(aulaID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio: " + ex.Message);
            }
        }

        public void AgregarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("sp_AgregarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Agregar los parámetros usando las propiedades del objeto Usuario
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                command.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                command.Parameters.AddWithValue("@UsuarioName", usuario.UsuarioName);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void ModificarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("sp_ModificarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Agregar los parámetros usando las propiedades del objeto Usuario
                command.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                command.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                command.Parameters.AddWithValue("@UsuarioName", usuario.UsuarioName);
                command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public void EliminarUsuario(int usuarioID)
        {
            if (usuarioID <= 0)
                throw new ArgumentException("El ID del usuario no es válido.");

            dataAccess.EliminarUsuario(usuarioID);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return dataAccess.ObtenerUsuarios();
        }

    }
}
