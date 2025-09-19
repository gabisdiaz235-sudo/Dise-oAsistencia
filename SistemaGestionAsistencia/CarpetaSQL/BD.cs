using Microsoft.Data.SqlClient;
using ModeloAsistencia.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SistemaGestionAsistencia.VistaModelo;
using Catel.MVVM;
using ModeloAsistencia.Modelo;
using System.Diagnostics;

namespace SistemaGestionAsistencia.CarpetaSQL
{

    internal class BD
    {
        //------------------------Empledo---------------------------
        internal ObservableCollection<Empleado> Get()
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;

            ObservableCollection<Empleado> lstResult = new ObservableCollection<Empleado>();
            string query = "SELECT * FROM Empleados";
            using (SqlConnection cn = new SqlConnection(textconexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstResult.Add(new Empleado()
                    {
                        EmpleadoID = (int)reader["EmpleadoID"],
                        Nombre = (string)reader["Nombre"],
                        ApellidoPaterno = (string)reader["ApellidoPaterno"],
                        ApellidoMaterno = (string)reader["ApellidoMaterno"],
                        Sexo = (string)reader["Sexo"],
                        FechaNacimiento = ((DateTime)reader["FechaNacimiento"]),
                        CodigoPostal = (int)reader["CodigoPostal"],
                        Calle = (string)reader["Calle"],
                        NumeroExterior = (int)reader["NumeroExterior"],
                        Colonia = (string)reader["Colonia"],
                        Municipio = (string)reader["Municipio"],
                        Estado = (string)reader["Estado"],
                        Pais = (string)reader["Pais"],
                        Correo = (string)reader["Correo"],
                        Telefono = (string)reader["Telefono"],
                        ContraseñaHash = (string)reader["ContraseñaHash"]

                    }); ;

                }
                reader.Close();
                cn.Close();
            }
            return lstResult;

        }

        private bool nuevo = true;

        internal void Add(Empleado dato)
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;

            if (nuevo)
            {


                Debug.WriteLine("Iniciando el proceso de inserción en la base de datos.");

                // Definir la consulta SQL de inserción
                string sql = "INSERT INTO Empleados (Nombre, ApellidoPaterno, ApellidoMaterno, Sexo, FechaNacimiento, CodigoPostal, Calle, NumeroExterior, Colonia, Municipio, Estado, Pais, Telefono, Correo, ContraseñaHash) " +
                             "VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Sexo, @F, @CodigoPostal, @Calle, @NumeroExterior, @Colonia, @Municipio, @Estado, @Pais, @Telefono, @Correo, @ContraseñaHash)";

                // Establecer la conexión a la base de datos
                using (SqlConnection con = new SqlConnection(textconexion))
                {
                    // Crear un objeto SqlCommand para ejecutar la consulta SQL
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Especificar el tipo de comando como texto
                        cmd.CommandType = CommandType.Text;

                        // Configurar los parámetros de la consulta SQL
                        cmd.Parameters.AddWithValue("@Nombre", dato.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", dato.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", dato.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Sexo", dato.Sexo);
                        cmd.Parameters.AddWithValue("@F", dato.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@CodigoPostal", dato.CodigoPostal);
                        cmd.Parameters.AddWithValue("@Calle", dato.Calle);
                        cmd.Parameters.AddWithValue("@NumeroExterior", dato.NumeroExterior);
                        cmd.Parameters.AddWithValue("@Colonia", dato.Colonia);
                        cmd.Parameters.AddWithValue("@Municipio", dato.Municipio);
                        cmd.Parameters.AddWithValue("@Estado", dato.Estado);
                        cmd.Parameters.AddWithValue("@Pais", dato.Pais);
                        cmd.Parameters.AddWithValue("@Telefono", dato.Telefono);
                        cmd.Parameters.AddWithValue("@Correo", dato.Correo);
                        cmd.Parameters.AddWithValue("@ContraseñaHash", dato.ContraseñaHash);

                        // Abrir la conexión a la base de datos
                        con.Open();

                        try
                        {
                            // Ejecutar la consulta SQL y obtener el número de filas afectadas
                            int i = cmd.ExecuteNonQuery();

                            // Verificar si se insertó al menos una fila
                            if (i > 0)
                                MessageBox.Show("Registro ingresado correctamente.");
                            else
                                MessageBox.Show("Ninguna fila insertada en la base de datos.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error durante la inserción en la base de datos: " + ex.ToString());
                            // Maneja el error según tus necesidades
                        }
                    }
                }

            }
        }

        internal void Delete(Empleado dato)
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;

            // Definir la consulta SQL de eliminación
            string sql = "DELETE FROM Empleados WHERE EmpleadoID = @EmpleadoID";

            using (SqlConnection con = new SqlConnection(textconexion))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;

                    // Configurar el parámetro para el ID a eliminar
                    cmd.Parameters.AddWithValue("@EmpleadoID", dato.EmpleadoID);

                    con.Open();

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Registro eliminado correctamente !");
                        else
                            MessageBox.Show("No se encontró un registro con el ID proporcionado.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
            }
        }

        internal void Edit(Empleado dato)
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;
            // Definir la consulta SQL de actualización
            string sql = "UPDATE Empleados SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, " +
                "ApellidoMaterno = @ApellidoMaterno, Sexo = @Sexo, FechaNacimiento = @FechaNacimiento," +
                "CodigoPostal = @CodigoPostal, Calle = @Calle, NumeroExterior = @NumeroExterior, Colonia = @Colonia," +
                "Municipio = @Municipio, Estado = @Estado, Pais = @Pais, Correo = @Correo, Telefono = @Telefono, ContraseñaHash = @ContraseñaHash WHERE EmpleadoID = @EmpleadoID";

            // Establecer la conexión a la base de datos
            using (SqlConnection con = new SqlConnection(textconexion))
            {
                // Crear un objeto SqlCommand para ejecutar la consulta SQL
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Especificar el tipo de comando como texto
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@EmpleadoID", dato.EmpleadoID);
                    cmd.Parameters.AddWithValue("@Nombre",dato.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", dato.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", dato.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Sexo", dato.Sexo);
                    cmd.Parameters.AddWithValue("@FechaNacimiento",dato.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@CodigoPostal",dato.CodigoPostal);
                    cmd.Parameters.AddWithValue("@Calle",dato.Calle);
                    cmd.Parameters.AddWithValue("@NumeroExterior",dato.NumeroExterior);
                    cmd.Parameters.AddWithValue("@Colonia",dato.Colonia);
                    cmd.Parameters.AddWithValue("@Municipio",dato.Municipio);
                    cmd.Parameters.AddWithValue("@Estado",dato.Estado);
                    cmd.Parameters.AddWithValue("@Pais",dato.Pais);
                    cmd.Parameters.AddWithValue("@Correo", dato.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", dato.Telefono);
                    cmd.Parameters.AddWithValue("@ContraseñaHash", dato.ContraseñaHash);

                    // Abrir la conexión a la base de datos
                    con.Open();

                    try
                    {
                        // Ejecutar la consulta SQL y obtener el número de filas afectadas
                        int filasActualizadas = cmd.ExecuteNonQuery();

                        if (filasActualizadas > 0)
                        {
                            MessageBox.Show("Registro actualizado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró ningún registro con el ID especificado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.ToString());
                    }
                }
            }
        }

        internal Empleado RecuperarEmpleadoPorID(int empleadoID)
        {
            Debug.WriteLine("Iniciando RecuperarEmpleadoPorID");
            Empleado empleadoRecuperado = null;

            using (ModeloContext conexion = new ModeloContext())
            {
                string textConexion = conexion.MiString;

                string query = "SELECT * FROM Empleados WHERE EmpleadoID = @EmpleadoID";

                using (SqlConnection cn = new SqlConnection(textConexion))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Aquí, lees los valores de las columnas y creas un nuevo objeto Empleado
                                empleadoRecuperado = new Empleado
                                {
                                    EmpleadoID = (int)reader["EmpleadoID"],
                                    Nombre = reader["Nombre"].ToString(),
                                    ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                                    ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                                    Sexo = reader["Sexo"].ToString(),
                                    FechaNacimiento = (DateTime)reader["FechaNacimiento"],
                                    CodigoPostal = (int)reader["CodigoPostal"],
                                    Calle = reader["Calle"].ToString(),
                                    NumeroExterior = (int)reader["NumeroExterior"],
                                    Colonia = reader["Colonia"].ToString(),
                                    Municipio = reader["Municipio"].ToString(),
                                    Estado = reader["Estado"].ToString(),
                                    Pais = reader["Pais"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Telefono = reader["Telefono"].ToString()
                                };
                            }
                        }
                    }
                }
                Debug.WriteLine("Finalizado RecuperarEmpleadoPorID");
                return empleadoRecuperado;
                
            }

           
        }
        public Empleado ObtenerInformacionEmpleado(string correoEmpleado, string contraseñaHashEmpleado)
        {
            ModeloContext conexion = new ModeloContext();
            string textConexion = conexion.MiString;

            if (correoEmpleado != null && contraseñaHashEmpleado != null)
            {
                using (SqlConnection connection = new SqlConnection(textConexion))
                {
                    connection.Open();

                    // Consulta SQL parametrizada para prevenir inyección SQL
                    string consulta = "SELECT EmpleadoID, Nombre FROM Empleados WHERE Correo = @Correo AND ContraseñaHash = @Contraseña";

                    using (SqlCommand command = new SqlCommand(consulta, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@Correo", correoEmpleado);
                        command.Parameters.AddWithValue("@Contraseña", contraseñaHashEmpleado);

                        // Ejecutar la consulta y obtener el resultado
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Crear un objeto Empleado con la información necesaria
                                return new Empleado
                                {
                                    EmpleadoID = reader.GetInt32(reader.GetOrdinal("EmpleadoID")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                                };
                            }
                        }
                    }
                }
            }

            // Devolver null si las credenciales no son válidas o faltan
            return null;
        }



        //------------------------EntradaSalida---------------------------

        internal ObservableCollection<RegistroEntradaSalida> GetES(int dato)
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;

            ObservableCollection<RegistroEntradaSalida> lstResult = new ObservableCollection<RegistroEntradaSalida>();
            string query = "SELECT EntradaSalidaID, EmpleadoID, Fecha, HoraEntrada, HoraSalida, Latitud, Longitud FROM AsistenciaEmpleados.dbo.RegistroEntradaSalidas WHERE EmpleadoID = @EmpleadoID;";
            using (SqlConnection cn = new SqlConnection(textconexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@EmpleadoID", dato);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstResult.Add(new RegistroEntradaSalida()
                        {
                            EmpleadoID = (int)reader["EmpleadoID"],
                            Fecha = (DateTime)reader["Fecha"],
                            HoraEntrada = (DateTime)reader["HoraEntrada"],
                            HoraSalida = (DateTime)reader["HoraSalida"],
                            Latitud = (double)reader["Latitud"],
                            Longitud = (double)reader["Longitud"]
                        });
                    }
                }
                else
                {
                    // No se encontraron registros, puedes manejarlo aquí
                    MessageBox.Show("No se encontro el Empleado.");
                    // También podrías lanzar una excepción o realizar otra acción según tus necesidades
                }
                
                reader.Close();
                cn.Close();
            }
            return lstResult;

        }
        internal void AddR(RegistroEntradaSalida dato)
        {
            ModeloContext conexion = new ModeloContext();
            string textConexion = conexion.MiString;

            if (nuevo)
            {
                Debug.WriteLine("Iniciando el proceso de inserción/actualización en la base de datos.");

                // Verificar si ya existe un registro con la misma fecha y EmpleadoID
                bool registroExistente = CheckExistingRecord(dato);

                // Definir la consulta SQL de inserción o actualización
                string sql = registroExistente
                    ? "UPDATE RegistroEntradaSalidas SET HoraSalida = @HoraSalida WHERE EmpleadoID = @EmpleadoID AND Fecha = @Fecha"
                    : "INSERT INTO RegistroEntradaSalidas (EmpleadoID, Fecha, HoraEntrada, HoraSalida, Latitud, Longitud) " +
                      "VALUES (@EmpleadoID, @Fecha, @HoraEntrada, @HoraSalida, @Latitud, @Longitud)";

                // Establecer la conexión a la base de datos
                using (SqlConnection con = new SqlConnection(textConexion))
                {
                    // Crear un objeto SqlCommand para ejecutar la consulta SQL
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Especificar el tipo de comando como texto
                        cmd.CommandType = CommandType.Text;

                        // Configurar los parámetros de la consulta SQL
                        cmd.Parameters.AddWithValue("@EmpleadoID", dato.EmpleadoID);
                        cmd.Parameters.AddWithValue("@Fecha", dato.Fecha);
                        cmd.Parameters.AddWithValue("@HoraEntrada", dato.HoraEntrada);
                        cmd.Parameters.AddWithValue("@HoraSalida", dato.HoraSalida);
                        cmd.Parameters.AddWithValue("@Latitud", dato.Latitud);
                        cmd.Parameters.AddWithValue("@Longitud", dato.Longitud);

                        // Abrir la conexión a la base de datos
                        con.Open();

                        try
                        {
                            // Ejecutar la consulta SQL y obtener el número de filas afectadas
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Verificar si se insertó o actualizó al menos una fila
                            if (rowsAffected > 0)
                            {
                                Debug.WriteLine("Registro de entrada/salida ingresado/actualizado correctamente.");
                            }
                            else
                            {
                                Debug.WriteLine("Ninguna fila insertada/actualizada en la base de datos.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error durante la inserción/actualización en la base de datos: " + ex.ToString());
                            // Manejar el error según tus necesidades
                        }
                    }
                }
            }
        }
        internal bool CheckExistingRecord(RegistroEntradaSalida dato)
        {
            ModeloContext conexion = new ModeloContext();
            string textConexion = conexion.MiString;

            using (SqlConnection con = new SqlConnection(textConexion))
            {
                con.Open();

                // Verificar si existe un registro con la misma fecha y EmpleadoID
                string sql = "SELECT COUNT(*) FROM RegistroEntradaSalidas WHERE EmpleadoID = @EmpleadoID AND Fecha = @Fecha";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmpleadoID", dato.EmpleadoID);
                    cmd.Parameters.AddWithValue("@Fecha", dato.Fecha);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        //-----------------------Estados--------------------------------------------------------------
        internal ObservableCollection<Estados> GetEstadosPorPais(int paisId)
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;

            ObservableCollection<Estados> lstEstados = new ObservableCollection<Estados>();
            string query = "SELECT EstadoID, PaisID, EstadoNombre FROM Estados WHERE PaisID = @PaisID;";
            using (SqlConnection cn = new SqlConnection(textconexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@PaisID", paisId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstEstados.Add(new Estados()
                        {
                            EstadoID = (int)reader["EstadoID"],
                            PaisID = (int)reader["PaisID"],
                            EstadoNombre = reader["EstadoNombre"].ToString()
                        });
                    }
                }
                else
                {
                    // No se encontraron registros, puedes manejarlo aquí
                    MessageBox.Show("No se encontraron estados para el país seleccionado.");
                    // También podrías lanzar una excepción o realizar otra acción según tus necesidades
                }

                reader.Close();
                cn.Close();
            }
            return lstEstados;
        }

        //----------------------------------Pais------------------------------------------------------
        internal ObservableCollection<Pais> GetPaises()
        {
            ModeloContext conexion = new ModeloContext();
            string textconexion = conexion.MiString;

            ObservableCollection<Pais> lstPaises = new ObservableCollection<Pais>();
            string query = "SELECT PaisID, PaisNombre FROM Pais;";
            using (SqlConnection cn = new SqlConnection(textconexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstPaises.Add(new Pais()
                        {
                            PaisID = (int)reader["PaisID"],
                            PaisNombre = reader["PaisNombre"].ToString()
                        });
                    }
                }
                else
                {
                    // No se encontraron registros, puedes manejarlo aquí
                    MessageBox.Show("No se encontraron países.");
                    // También podrías lanzar una excepción o realizar otra acción según tus necesidades
                }

                reader.Close();
                cn.Close();
            }
            return lstPaises;
        }


        //--------------------------Administrador-----------------------------
        public bool ValidarCredenciales(string correoAdmin, string contraseñaHashAdmin)
        {
            ModeloContext conexion = new ModeloContext();
            string textConexion = conexion.MiString;
            if (correoAdmin != null || contraseñaHashAdmin != null)
            {
                using (SqlConnection connection = new SqlConnection(textConexion))
                {
                    connection.Open();

                    // Consulta SQL parametrizada para prevenir inyección SQL
                    string consulta = "SELECT COUNT(*) FROM Administradors WHERE CorreoAdmin = @Correo AND ContraseñaHashAdmin = @Contraseña";

                    using (SqlCommand command = new SqlCommand(consulta, connection))
                    {

                        // Agregar parámetros
                        command.Parameters.AddWithValue("@Correo", correoAdmin);
                        command.Parameters.AddWithValue("@Contraseña", contraseñaHashAdmin);

                        // Ejecutar la consulta y obtener el resultado

                        int resultado = (int)command.ExecuteScalar();

                        // Si el resultado es mayor que cero, las credenciales son válidas
                        return resultado > 0;
                    }
                }
            }
            else
            {
                return false;
            }        
        }
    }
}
