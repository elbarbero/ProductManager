using DevExpress.Xpo;
using Gestor_Productos.Database;
using Gestor_Productos.Database.ORM_GestProc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestor_Productos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string conexionString = ConexionBBDD.CrearStringConexion("PORTATIL-MARIO", "BD_GESTPROC", "sa", "root");
            IDataLayer datalayer = ConexionBBDD.CrearConexion(conexionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

            UnitOfWork uow = ConexionBBDD.getNewUnitOfWork();
            XPCollection<USUARIOS> users = new XPCollection<USUARIOS>(uow);
            USUARIOS u = new USUARIOS(uow);
            u.Nombre = "hiuhihi";
            u.Username = "pppppp";
            uow.CommitChanges();

            UnitOfWork uow2 = new UnitOfWork();
            XPCollection<USUARIOS> users2 = new XPCollection<USUARIOS>(uow);
        }
    }
}
