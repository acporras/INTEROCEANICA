using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;

namespace FE.InterfaceConsole
{
    class Program
    {
        public static System.Timers.Timer ti_intejesrv = new System.Timers.Timer(); //Intervalo de ejecución del servicio.
        public static int i = 0;
        public static BaseDatos BD = new BaseDatos("BASPRVNAM", "BASCADCON"); //Conexión a BD Facturación

        static void Main(string[] args)
        {

            ti_intejesrv.Interval = 5000;
            ti_intejesrv.Elapsed += new System.Timers.ElapsedEventHandler(ti_intejesrv_Elapsed);
            ti_intejesrv.Enabled = true;
            ti_intejesrv.Start();

            Console.ReadLine();
        }

        public static void ti_intejesrv_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ThreadStart ts_srvprosun = new ThreadStart(ml_proceso_sunat);
            Thread.CurrentThread.Name = "SRVPROSUN";
            Thread th_srvprosun = new Thread(ts_srvprosun);
            th_srvprosun.Start();
            th_srvprosun.Join();

            ti_intejesrv.Enabled = true;
        }

        //public static void ml_proceso_sunat(object sender, EventArgs args)
        public static void ml_proceso_sunat()
        {
            //Obtener Lista de Emisores electronicos
            BD.Conectar();
            IDataReader dr_emidocele = BD.Dame_Datos_DR("SPS_MAE_EMIDOCELE", false, "P");
            ListBEMaeemiele lst_maeemiele = new ListBEMaeemiele();
            while (dr_emidocele.Read())
            {
                var oBEMaeemiele = new BEMaeemiele();
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NID_EMIDOCELE"))))
                    oBEMaeemiele.nid_maeemiele = dr_emidocele.GetInt32(dr_emidocele.GetOrdinal("NID_EMIDOCELE"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NU_EMINUMRUC"))))
                    oBEMaeemiele.nu_eminumruc = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NU_EMINUMRUC"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_EMIRAZSOC"))))
                    oBEMaeemiele.no_emirazsoc = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_EMIRAZSOC"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_ESTEMIELE"))))
                    oBEMaeemiele.no_estemiele = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_ESTEMIELE"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_CONEMIELE"))))
                    oBEMaeemiele.no_conemiele = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_CONEMIELE"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_EMIUBIGEO"))))
                    oBEMaeemiele.no_emiubigeo = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_EMIUBIGEO"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_EMIDEPART"))))
                    oBEMaeemiele.no_emidepart = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_EMIDEPART"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_EMIPROVIN"))))
                    oBEMaeemiele.no_emiprovin = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_EMIPROVIN"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_EMIDISTRI"))))
                    oBEMaeemiele.no_emidistri = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_EMIDISTRI"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_EMIDIRFIS"))))
                    oBEMaeemiele.no_emidirfis = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_EMIDIRFIS"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_BASTIPBAS"))))
                    oBEMaeemiele.no_bastipbas = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_BASTIPBAS"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_BASNOMSRV"))))
                    oBEMaeemiele.no_basnomsrv = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_BASNOMSRV"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_BASNOMBAS"))))
                    oBEMaeemiele.no_basnombas = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_BASNOMBAS"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_BASUSRBAS"))))
                    oBEMaeemiele.no_basusrbas = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_BASUSRBAS"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_BASUSRPAS"))))
                    oBEMaeemiele.no_basusrpas = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_BASUSRPAS"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_TABFACCAB"))))
                    oBEMaeemiele.no_tabfaccab = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_TABFACCAB"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_TABFACDET"))))
                    oBEMaeemiele.no_tabfacdet = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_TABFACDET"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("NO_RUTCERDIG"))))
                    oBEMaeemiele.no_rutcerdig = dr_emidocele.GetString(dr_emidocele.GetOrdinal("NO_RUTCERDIG"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("FE_REGCREACI"))))
                    oBEMaeemiele.fe_regcreaci = dr_emidocele.GetDateTime(dr_emidocele.GetOrdinal("FE_REGCREACI"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("FE_REGMODIFI"))))
                    oBEMaeemiele.fe_regmodifi = dr_emidocele.GetDateTime(dr_emidocele.GetOrdinal("FE_REGMODIFI"));
                if ((!dr_emidocele.IsDBNull(dr_emidocele.GetOrdinal("FL_REGINACTI"))))
                    oBEMaeemiele.fl_reginacti = dr_emidocele.GetString(dr_emidocele.GetOrdinal("FL_REGINACTI"));
                lst_maeemiele.Add(oBEMaeemiele);
            }
            dr_emidocele.Close();
            //Recorre la lista de emisores
            BD.Desconectar();
            foreach (BEMaeemiele item in lst_maeemiele)
            {
                //Inicia el proceso de migración por cada compañia de forma independiente
                Thread th_srvpromig = new Thread(() => ml_migracion_documentos_cliente(item)) { Name = "SRVPROMIG" };
                th_srvpromig.Start();
            }
        }
        //Se encarga de realizar la migración de documentos a BD Facturación
        public static void ml_migracion_documentos_cliente(BEMaeemiele oBEMaeemiele)
        {
            //Se iniciliza la conexión de la BD
            BaseDatos BDFact = new BaseDatos("BASPRVNAM", "BASCADCON");
            Boolean ProcessException = false;
            String MessageException = "";
            //Se verifica que no exista un proceso de migración en ejecución para la empresa
            BDFact.Conectar();
            BDFact.Añadir_Parametro(0, "NID_EMIDOCELE", "I", oBEMaeemiele.nid_maeemiele.ToString());
            BDFact.Añadir_Parametro(1, "CO_ESTPROINT", "S", "EJ"); //Ejecutando
            BDFact.Añadir_Parametro(2, "CO_TIPPROFAC", "S", "MI"); //Migración
            IDataReader dr_proejemig = BDFact.Dame_Datos_DR("SPS_TL_PROFACINT_BY_EMIDOCELE", true, "P");
            Boolean fl_proejemig = false;
            while (dr_proejemig.Read())
            {
                fl_proejemig = true;
            }
            BDFact.Desconectar();
            //Si no existe proceso en ejecución se procede a hacer el volcado de información de la base cliente a la base de de facturación
            if (!fl_proejemig)
            {
                //Se crea un registro identificador de la tarea en ejecución
                BDFact.Conectar();
                BDFact.Añadir_Parametro(0, "CO_TIPPROFAC", "S", "MIG"); //Migración
                BDFact.Añadir_Parametro(1, "CO_ESTPROINT", "S", "EJ"); //Ejecutando
                BDFact.Añadir_Parametro(2, "NID_EMIDOCELE", "I", oBEMaeemiele.nid_maeemiele.ToString());
                BDFact.Ejecutar_PA("SPI_TL_PROFACINT", true);
                BDFact.Desconectar();
                //Se identifica el tipo de base de datos registrada
                BaseDatos.BBDD BBDD = 0;
                switch (oBEMaeemiele.no_bastipbas)
                {
                    case "SQL":
                        BBDD = BaseDatos.BBDD.SQL;
                        break;
                    case "ODBC":
                        BBDD = BaseDatos.BBDD.ODBC;
                        break;
                    case "OLEDB":
                        BBDD = BaseDatos.BBDD.OLEDB;
                        break;
                    case "MySQL":
                        BBDD = BaseDatos.BBDD.MySQL;
                        break;
                }
                //Crear conexión con base de datos cliente
                BaseDatos BDClient = new BaseDatos(oBEMaeemiele.no_basnomsrv, BBDD, oBEMaeemiele.no_basnombas,
                    oBEMaeemiele.no_basusrbas, oBEMaeemiele.no_basusrpas);
                BDClient.Conectar();
                BDClient.Añadir_Parametro(0, "TX_ESTDOCELE", "S", "2,3"); //Pendiente y Por enviar
                BDClient.Añadir_Parametro(1, "NO_DOCELECAB", "S", oBEMaeemiele.no_tabfaccab); //Pendiente y Por enviar
                IDataReader dr_clidoccab = BDClient.Dame_Datos_DR("SPS_TABFACCAB_BY_ESTDOCELE", true, "P");
                //Se recorre los datos de cabecera
                while (dr_clidoccab.Read())
                {
                    var co_doctipdoc = dr_clidoccab.GetString(dr_clidoccab.GetOrdinal("F5_CTD"));
                    var nu_docsersun = dr_clidoccab.GetString(dr_clidoccab.GetOrdinal("F5_CNUMSER"));
                    var nu_docnumsun = dr_clidoccab.GetString(dr_clidoccab.GetOrdinal("F5_CNUMDOC"));
                    var fe_docfecemi = dr_clidoccab.GetDateTime(dr_clidoccab.GetOrdinal("F5_DFECDOC")).ToString("dd/MM/yyyy");

                    //Insertando Cabecera
                    try
                    {
                        //Se estructura el xml de detalle

                        BDFact.Conectar();

                        BDFact.Desconectar();
                    }
                    catch (Exception ex)
                    {
                        ProcessException = true;
                        MessageException = ex.Message.ToString();
                    }

                    //Obteniendo el detalle de la factura
                    BDClient.Añadir_Parametro(0, "CO_DETALTIDO", "S", co_doctipdoc);
                    BDClient.Añadir_Parametro(0, "NU_DETSERSUN", "S", nu_docsersun);
                    BDClient.Añadir_Parametro(0, "NU_DETNUMSUN", "S", nu_docnumsun);
                    BDClient.Añadir_Parametro(0, "NO_DOCELEDET", "S", oBEMaeemiele.no_tabfacdet);
                    IDataReader dr_clidocdet = BDClient.Dame_Datos_DR("SPS_TABFACDET_BY_TABFACCAB", true, "S");
                    while (dr_clidocdet.Read())
                    {
                        //Insertando Detalle
                        try
                        {
                            //Se estructura el xml de detalle
                            BDFact.Conectar();

                            BDFact.Desconectar();
                        }
                        catch (Exception ex)
                        {
                            ProcessException = true;
                            MessageException = ex.Message.ToString();
                        }
                    }
                }
                BDClient.Desconectar();
                //Se actualiza el registro identificador de la tarea que ha finalizado
                BDFact.Conectar();
                BDFact.Añadir_Parametro(0, "CO_TIPPROFAC", "S", "MIG"); //Migración
                BDFact.Añadir_Parametro(1, "CO_ESTPROINT", "S", (ProcessException) ? "EX" : "CO"); //Excepción - Correcto
                BDFact.Añadir_Parametro(2, "NID_EMIDOCELE", "I", oBEMaeemiele.nid_maeemiele.ToString());
                BDFact.Añadir_Parametro(3, "TX_DESCRIPCI", "S", MessageException);
                BDFact.Ejecutar_PA("SPU_TL_PROFACINT", true);
                BDFact.Desconectar();
            }
        }

    }
}
