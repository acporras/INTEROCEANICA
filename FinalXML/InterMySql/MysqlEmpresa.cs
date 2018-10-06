using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using FinalXML.Entidades;
using FinalXML.Interfaces;
using FinalXML.Conexion;
using System.Data.Sql;
using System.Data.SqlClient;
namespace FinalXML.InterMySql
{
    public class MysqlEmpresa : IEmpresa
    {
        clsConexionMysql con = new clsConexionMysql();
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        SqlDataAdapter adap = null;
        DataTable tabla = null;

        public DataTable CargaEmpresa()
        {
            try
            {
                string consulta = @"SELECT * FROM MAE_EMIDOCELE WHERE FL_REGINACTI = '0' ORDER BY FE_REGCREACI";

                tabla = new DataTable();
                con.conectarBD();
                cmd = new SqlCommand(consulta, con.conector);
                cmd.CommandType = CommandType.Text;
                adap = new SqlDataAdapter(cmd);
                adap.Fill(tabla);
                return tabla;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public Contribuyente LeerEmpresa(String NumRuc)
        {
            Contribuyente cont = null;
            try
            {
                string consulta = @"SELECT * FROM MAE_EMIDOCELE WHERE FL_REGINACTI = '0' AND NU_EMINUMRUC = @numruc ORDER BY FE_REGCREACI";

                con.conectarBD();
                cmd = new SqlCommand(consulta, con.conector);
                cmd.Parameters.AddWithValue("@numruc", SqlDbType.Char).Value = NumRuc;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cont = new Contribuyente();
                        cont.NroDocumento = dr.GetString(1);
                        cont.TipoDocumento = "6";
                        cont.NombreLegal = dr.GetString(2);
                        cont.NombreComercial = dr.GetString(2);
                        cont.Ubigeo = dr.GetString(6);
                        cont.Direccion = dr.GetString(10);
                        cont.Urbanizacion = "";
                        cont.Departamento = dr.GetString(7);
                        cont.Provincia = dr.GetString(8);
                        cont.Distrito = dr.GetString(9);
                        cont.UsuarioSol = dr.GetString(19);
                        cont.ClaveSol = dr.GetString(20);
                    }

                }
                return cont;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
    }
}
