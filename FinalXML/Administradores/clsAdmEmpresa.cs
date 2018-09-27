using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalXML.Interfaces;
using FinalXML.Administradores;
using FinalXML.InterMySql;
using FinalXML.Entidades;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;


namespace FinalXML.Administradores
{
    public class clsAdmEmpresa
    {
        IEmpresa CEmpresa = new MysqlEmpresa();

        public DataTable CargaEmpresa()
        {
            try
            {
                return CEmpresa.CargaEmpresa();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public Contribuyente LeerEmpresa(String NumRuc)
        {
            try
            {
                return CEmpresa.LeerEmpresa(NumRuc);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
