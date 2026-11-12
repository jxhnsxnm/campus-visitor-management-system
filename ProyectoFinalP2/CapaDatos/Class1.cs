using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class DataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Usuario ObtenerUsuario(string usuarioName, string contraseña)
        {
            Usuario usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_ObtenerUsuario", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UsuarioName", usuarioName);
                    command.Parameters.AddWithValue("@Contraseña", contraseña);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            TipoUsuario = reader["TipoUsuario"].ToString(),
                            UsuarioName = reader["UsuarioName"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario: " + ex.Message);
            }

            return usuario;
        }

        // Método genérico para ejecutar un procedimiento almacenado
        private void ExecuteNonQuery(string storedProcedure, Action<SqlCommand> parameterizeCommand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                parameterizeCommand?.Invoke(cmd);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Método genérico para obtener datos de un procedimiento almacenado
        private DataTable ExecuteQuery(string storedProcedure, Action<SqlCommand> parameterizeCommand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(storedProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                parameterizeCommand?.Invoke(cmd);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }

        // Método para registrar un usuario
        public void RegistrarUsuario(Usuario usuario)
        {
            ExecuteNonQuery("spRegistrarUsuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                cmd.Parameters.AddWithValue("@Usuario", usuario.UsuarioName);
                cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
            });
        }

        // Método para obtener edificios
        public List<Edificio> ObtenerEdificios()
        {
            List<Edificio> edificios = new List<Edificio>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ObtenerEdificio", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        edificios.Add(new Edificio
                        {
                            EdificioID = Convert.ToInt32(reader["EdificioID"]),
                            Nombre = reader["Nombre"].ToString()
                        });
                    }
                }
            }
            return edificios;
        }

        // Método para obtener aulas por edificio
        public List<Aula> ObtenerAulasPorEdificio(int edificioId)
        {
            List<Aula> aulas = new List<Aula>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_ObtenerAulasPorEdificio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EdificioID", edificioId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aulas.Add(new Aula
                        {
                            AulaID = Convert.ToInt32(reader["AulaID"]),
                            NombreAula = reader["NombreAula"].ToString()
                        });
                    }
                }
            }
            return aulas;
        }

        // Método para registrar una visita
        public void RegistrarVisita(Visita visita)
        {
            ExecuteNonQuery("sp_RegistrarVisita", cmd =>
            {
                cmd.Parameters.AddWithValue("@Nombre", visita.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", visita.Apellido);
                cmd.Parameters.AddWithValue("@Carrera", visita.Carrera ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", visita.Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EdificioID", visita.EdificioID);
                cmd.Parameters.AddWithValue("@AulaID", visita.AulaID);
                cmd.Parameters.AddWithValue("@HoraEntrada", visita.HoraEntrada);
                cmd.Parameters.AddWithValue("@HoraSalida", visita.HoraSalida);
                cmd.Parameters.AddWithValue("@Motivo", visita.Motivo);
                cmd.Parameters.AddWithValue("@Foto", visita.Foto ?? (object)DBNull.Value);
            });
        }

        // Método para consultar visitas por edificio y rango de fechas
        public List<Visita> ConsultarVisitas(int? edificioId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            DataTable table = ExecuteQuery("sp_ConsultarVisitas", cmd =>
            {
                cmd.Parameters.AddWithValue("@EdificioID", edificioId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin ?? (object)DBNull.Value);
            });

            List<Visita> visitas = new List<Visita>();

            foreach (DataRow row in table.Rows)
            {
                visitas.Add(new Visita
                {
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Correo = row["Correo"].ToString(),
                    EdificioID = Convert.ToInt32(row["EdificioID"]),
                    AulaID = Convert.ToInt32(row["AulaID"]),
                    HoraEntrada = Convert.ToDateTime(row["HoraEntrada"]),
                    HoraSalida = Convert.ToDateTime(row["HoraSalida"]),
                    Motivo = row["Motivo"].ToString(),
                    Foto = row["Foto"] as byte[]
                });
            }

            return visitas;
        }

        public void AgregarEdificio(string nombre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_AgregarEdificio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar edificio: " + ex.Message);
            }
        }

        public void AgregarAula(int edificioID, string nombreAula)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_AgregarAula", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EdificioID", edificioID);
                    cmd.Parameters.AddWithValue("@NombreAula", nombreAula);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar aula: " + ex.Message);
            }
        }


        public void ModificarEdificio(int edificioID, string nuevoNombre)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarEdificio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EdificioID", edificioID);
                    cmd.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar edificio: " + ex.Message);
            }
        }

        public void ModificarAula(int aulaID, string nuevoNombreAula)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificarAula", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AulaID", aulaID);
                    cmd.Parameters.AddWithValue("@NuevoNombreAula", nuevoNombreAula);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar aula: " + ex.Message);
            }
        }

        public void EliminarEdificio(int edificioID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarEdificio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EdificioID", edificioID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar edificio: " + ex.Message);
            }
        }

        public void EliminarAula(int aulaID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarAula", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AulaID", aulaID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar aula: " + ex.Message);
            }
        }

        // Agregar Usuario
        public void AgregarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("sp_AgregarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

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

        // Modificar Usuario
        public void ModificarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("sp_ModificarUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;

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
            ExecuteNonQuery("sp_EliminarUsuario", cmd =>
            {
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
            });
        }

        public List<Usuario> ObtenerUsuarios()
        {
            DataTable table = ExecuteQuery("sp_ObtenerUsuarios", null);

            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow row in table.Rows)
            {
                usuarios.Add(new Usuario
                {
                    UsuarioID = Convert.ToInt32(row["UsuarioID"]),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                    TipoUsuario = row["TipoUsuario"].ToString(),
                    UsuarioName = row["UsuarioName"].ToString(),
                    Contraseña = row["Contraseña"].ToString()
                });
            }

            return usuarios;
        }

    }

}
