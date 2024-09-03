using Character.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Neitek.IRepository;
using Neitek.Shared.Models;
using System.Data;
using System.Text.Json;

namespace Neitek.Repositories
{
    public class MetasRepository : IMetasRepository
    {
        private readonly string _conexionSQL;
        private readonly DataContext _context;
        public MetasRepository(DataContext context, IConfiguration config)
        {
            _conexionSQL = config.GetConnectionString("DefaultConnection");
            _context = context;
        }

        public async Task<DbResponse<IEnumerable<Metas>>> All()
        {
            DbResponse<IEnumerable<Metas>> response= new ();

            try
            {
                string jsonString;
                var metas = _context.Metas.FromSqlRaw("Exec sp_Se_Metas '' "); 
                jsonString = JsonSerializer.Serialize(metas);
                var result = JsonSerializer.Deserialize<IEnumerable<Metas>>(jsonString);
                response.success = true;
                response.data = result;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.data = new List<Metas>();
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Metas>> GetByDsc(string metaDsc)
        {
            DbResponse<Metas> response = new();
            try
            {
                string jsonString;
                string sp = String.Format("Exec sp_Se_Metas '{0}'", metaDsc);
                var task = _context.Metas.FromSqlRaw(sp);
                jsonString = JsonSerializer.Serialize(task);
                var result = JsonSerializer.Deserialize<IEnumerable<Metas>>(jsonString);

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
                response.data = new Metas();
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Add(Metas metas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_In_Metas";
                    cmd.Parameters.Add("@pstrMetaDsc", SqlDbType.VarChar).Value = metas.Meta_Dsc;
                    cmd.Parameters.Add("@psdtmFechaAlta", SqlDbType.DateTime).Value = metas.Fecha_Alta;  
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Registro insertado con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Update(Metas metas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Up_Metas";
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = metas.Meta_Id;
                    cmd.Parameters.Add("@pstrMetaDsc", SqlDbType.VarChar).Value = metas.Meta_Dsc;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Registro modificado con exito" };
                return response;            
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> UpdateProgreso(Metas metas)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Up_Metas_Progreso";
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = metas.Meta_Id;
                    cmd.Parameters.Add("@psintProgreso", SqlDbType.SmallInt).Value = metas.Progreso;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Poregreso modificado con exito" };
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.error = ex.Message;
                return response;
            }
        }
        public async Task<DbResponse<Mensaje>> Delete(short id)
        {
            DbResponse<Mensaje> response = new();
            try
            {
                using (SqlConnection conection = new SqlConnection(_conexionSQL))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "sp_De_Metas";
                    cmd.Parameters.Add("@psintMetaId", SqlDbType.SmallInt).Value = id;
                    cmd.Connection = conection;
                    conection.Open();
                    cmd.ExecuteNonQuery();
                }
                response.success = true;
                response.data = new Mensaje() { TipoMensaje = (byte)TipoMensaje.Exitoso, Texto = "Meta borrada con exito" };
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
