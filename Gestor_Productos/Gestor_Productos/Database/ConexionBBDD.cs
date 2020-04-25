using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Gestor_Productos.Database.ORM_GestProc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Productos.Database
{
    class ConexionBBDD
    {
        public static IDataLayer datalayer;
        public static UnitOfWork getNewUnitOfWork() => new UnitOfWork(datalayer);
        
        
        public static string CrearStringConexion(string direccionBBDD, string nombreDB, string usuarioBBDD, string passwordBBDD)
        {
            string sqlConn;
            sqlConn = "XpoProvider=MSSqlServer;Data Source=" + direccionBBDD + ";User ID=" + usuarioBBDD + ";Password=" + passwordBBDD + ";Initial Catalog=" + nombreDB + ";Persist Security Info=true";
            return sqlConn;
        }

        public static IDataLayer CrearConexion(string stringConexion, DevExpress.Xpo.DB.AutoCreateOption option)
        {
            try
            {
                return datalayer = XpoDefault.GetDataLayer(stringConexion, AutoCreateOption.None);
            }
            catch (Exception ex)
            {
                //Poner mensaje de error
                return null;
            }
        }

        public static bool CrearDataBase(string stringConexion, DevExpress.Xpo.DB.AutoCreateOption option) // Crea o Actualiza la BBDD
        {
            try
            {
                using (Session session = new Session(XpoDefault.GetDataLayer(stringConexion, option)))
                {
                    session.UpdateSchema(ConnectionHelper.GetPersistentTypes());
                    session.CreateObjectTypeRecords();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ConectarConBBDDUsuario(string direccion, string puerto, string usuario, string password, string database)
        {
            try
            {
                using (var connection = GetSQLServerInstance(direccion, puerto, usuario, password, database))
                {
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception es2)
            {
                return false;
            }
        }

        public static SqlConnection GetSQLServerInstance(string direccion, string puerto, string usuario, string password, string database)
        {
            if (direccion.ToLower() != "localhost" && direccion != "127.0.0.1" && (puerto != null && puerto != ""))//Si es localhost no necesita puerto
            {
                direccion = "Data Source = " + direccion + "," + puerto + ";User ID=" + usuario + ";Password=" + password + ";";
            }
            else
            {
                direccion = "Data Source = " + direccion + ";User ID=" + usuario + ";Password=" + password + ";";
            }

            if (database != null)
            {
                direccion += "Database=" + database + ";";
            }
            var db = new SqlConnection(direccion);
            db.Open();
            return db;
        }

    }
}
