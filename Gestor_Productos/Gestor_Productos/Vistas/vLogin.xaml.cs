using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpo;
using Gestor_Productos.Comun;
using Gestor_Productos.Database;
using Gestor_Productos.Database.ORM_GestProc;

namespace Gestor_Productos.Vistas
{
    /// <summary>
    /// Interaction logic for vLogin.xaml
    /// </summary>
    public partial class vLogin : ThemedWindow
    {
        public vLogin()
        {
            InitializeComponent();
            string conexionString = ConexionBBDD.CrearStringConexion("PORTATIL-MARIO", "BD_GESTPROC", "sa", "root");
            try
            {
                IDataLayer datalayer = ConexionBBDD.CrearConexion(conexionString, DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists);
                ConexionBBDD.CrearDataBase(conexionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema); //actualizamos la BBDD
                if (datalayer == null)
                {
                    datalayer = ConexionBBDD.CrearConexion(conexionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
                    ConexionBBDD.insertarDatosAdmin();
                }
            }
            catch (Exception ex)
            {
                IDataLayer datalayer = ConexionBBDD.CrearConexion(conexionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            }
            ViewModelvLogin viewModel = new ViewModelvLogin(this);
            DataContext = viewModel;
        }

        public class ViewModelvLogin : INotifyPropertyChanged
        {
            #region variables globales
            private vLogin ventana;
            public UnitOfWork uow;
            public bool _viewPass;
            public bool viewPass { get => _viewPass; set { _viewPass = value; OnPropertyChanged("viewPass"); } }

            private string _usuario;
            public string usuario { get => _usuario; set { _usuario = value; OnPropertyChanged("usuario"); } }
            private string _contrasena;
            public string contrasena { get => _contrasena; set { _contrasena = value; OnPropertyChanged("contrasena"); } }

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Constructor
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            //Constructor para visualizar albaranes
            public ViewModelvLogin(vLogin ventana)
            {
                this.ventana = ventana;
                uow = ConexionBBDD.getNewUnitOfWork();
                viewPass = true;
            }
            #endregion

            #region Eventos
            public void btnSiguiente_Click(object sender, RoutedEventArgs e)
            {
                USUARIOS user = uow.FindObject<USUARIOS>(CriteriaOperator.Parse("Username = ?", usuario));

                if (user != null && ComunClass.DesEncriptar(user.Password) == contrasena)
                {
                    //ventana.Visibility = Visibility.Collapsed;
                    //usuario = null;
                    //contrasena = null;
                    vMenu menu = new vMenu(user);
                    ventana.Close();
                    menu.ShowDialog();
                }
                else
                {
                    ThemedMessageBox.Show("Inicio de Sesión", "Usuario y/o contraseña incorrectos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            public void RegistroRecuperacion_MouseUp(object sender, MouseButtonEventArgs e)
            {
                TextBlock textedit = sender as TextBlock;
                string pulsado = "";
                if (textedit.Name == "btnRegistro")
                {
                    pulsado = "registro";
                }
                else
                {
                    pulsado = "recuperar";
                }
                vRegistroRecuperacion registroRec = new vRegistroRecuperacion(pulsado);
                registroRec.ShowDialog();
            }

            public void btnViewPassword_MouseDown(object sender, MouseButtonEventArgs e)
            {
                viewPass = viewPass ? false : true;
            }
            #endregion

        }

    }
}
