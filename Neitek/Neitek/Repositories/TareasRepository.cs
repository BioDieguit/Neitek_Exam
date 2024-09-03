using Character.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Neitek.IRepository;
using Neitek.Shared.Models;
using System.Data;
using System.Text.Json;

namespace Neitek.Repositories
{
    public class TareasRepository : ITareasRepository
    {
        private readonly string _conexionSQL;
        private readonly DataContext _context;
        public TareasRepository(DataContext context, IConfiguration config)
        {
            _conexionSQL = config.GetConnectionString("DefaultConnection");
            _context = context;
        }
  
        public async Task<DbResponse<IEnumerable<Tareas>>> All(short metaId)
        {
            DbResponse<IEnumerable<Tareas>> response = new();
            try
            {
                string jsonString;
                string sp = String.Format("Exec sp_Se_Tareas {0}", metaId);
                var task = _context.Tareas.FromSqlRaw(sp);
                jsonString = JsonSerializer.Serialize(task);
                var result = JsonSerializer.Deserialize<IEnumerable<Tareas>>(jsonString);

                if (result.Count() == 0)
                {
                    response.data = null;
                    response.success = true;
                    return response;
                }

                response.success = true;
                response.data = result;
                return response;
            }
            catch (Exception ex)
            {
                response.success = true;
                response.data = new List<Tareas>();
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Tareas>> GetByDsc(Tareas tareas)
        {
            DbResponse<Tareas> response = new();
            try
            {
                string jsonString;
                string sp = String.Format("Exec sp_Se_Tarea_Unica '{0}', {1}", tareas.Tarea_Id, tareas.Meta_Id);
                var task = _context.Tareas.FromSqlRaw(sp);
                jsonString = JsonSerializer.Serialize(task);
                var result = JsonSerializer.Deserialize<IEnumerable<Tareas>>(jsonString);

                if (result.Count() == 0)
                {
                    response.data = null;
                    response.success = true;
                    return response;
                }

                response.success = true;
                response.data = result.FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                response.success = true;
                response.data = new Tareas();
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Add(Tareas tareas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_In_Tareas";
                    cmd.Parameters.Add("@pstrTareaId", SqlDbType.VarChar).Value = tareas.Tarea_Id;
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = tareas.Meta_Id;
                    cmd.Parameters.Add("@psdtmFechaAlta", SqlDbType.DateTime).Value = tareas.Fecha_Alta;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Tarea registrada con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Update(Tareas[] tareas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Up_Tareas";
                    cmd.Parameters.Add("@pstrTareaId", SqlDbType.VarChar).Value = tareas[0].Tarea_Id;
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = tareas[0].Meta_Id;
                    cmd.Parameters.Add("@pstrNuevaTareaId", SqlDbType.VarChar).Value = tareas[1].Tarea_Id;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Tarea modificado con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Complete(Tareas tareas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Up_Completar_Tarea";
                    cmd.Parameters.Add("@pstrTareaId", SqlDbType.VarChar).Value = tareas.Tarea_Id;
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = tareas.Meta_Id;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Tarea completada con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Importancia(Tareas tareas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Up_Tarea_Importancia";
                    cmd.Parameters.Add("@pstrTareaId", SqlDbType.VarChar).Value = tareas.Tarea_Id;
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = tareas.Meta_Id;
                    cmd.Parameters.Add("@psbitImportancia", SqlDbType.Bit).Value = tareas.Importancia;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Importancia de tarea modificada con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> DeleteAll(short metaId)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "sp_De_Tareas_All";
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = metaId;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Tareas borrada con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> DeleteOne(string tareaId, short metaId)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "sp_De_Tareas_Unica";
                    cmd.Parameters.Add("@pstrTareaId", SqlDbType.VarChar).Value = tareaId;
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = metaId;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Tarea borrada con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
    }
}
