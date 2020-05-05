using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core;
using DevExpress.Xpo;
using Gestor_Productos.Comun;
using Gestor_Productos.Database;
using Gestor_Productos.Database.ORM_GestProc;

namespace Gestor_Productos.Vistas
{
    /// <summary>
    /// Interaction logic for vRegistroRecuperacion.xaml
    /// </summary>
    public partial class vRegistroRecuperacion : ThemedWindow
    {
        public vRegistroRecuperacion(string pulsado)
        {
            InitializeComponent();
            ViewModelvRegRec viewModel = new ViewModelvRegRec(this, pulsado);
            DataContext = viewModel;
        }

        public class ViewModelvRegRec : INotifyPropertyChanged
        {
            #region variables globales
            private vRegistroRecuperacion ventana;
            public UnitOfWork uow;
            private string _nomVentana;
            public string nomVentana { get { return _nomVentana; } set { _nomVentana = value; OnPropertyChanged("nomVentana"); } }
            #region Variables de registro de usuario
            private string _nombre;
            public string nombre { get { return _nombre; } set { _nombre = value; OnPropertyChanged("nombre"); } }
            private string _apellidos;
            public string apellidos { get { return _apellidos; } set { _apellidos = value; OnPropertyChanged("apellidos"); } }
            private string _dni;
            public string dni { get { return _dni; } set { _dni = value; OnPropertyChanged("dni"); } }
            private string _tlfFijo;
            public string tlfFijo { get { return _tlfFijo; } set { _tlfFijo = value; OnPropertyChanged("tlfFijo"); } }
            private string _tlfMovil;
            public string tlfMovil { get { return _tlfMovil; } set { _tlfMovil = value; OnPropertyChanged("tlfMovil"); } }
            private string _email;
            public string email { get { return _email; } set { _email = value; OnPropertyChanged("email"); } }
            private string _username;
            public string username { get { return _username; } set { _username = value; OnPropertyChanged("username"); } }
            private string _password;
            public string password { get { return _password; } set { _password = value; OnPropertyChanged("password"); } }
            #endregion

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Constructor
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            //Constructor para visualizar albaranes
            public ViewModelvRegRec(vRegistroRecuperacion ventana, string pulsado)
            {
                this.ventana = ventana;
                uow = ConexionBBDD.getNewUnitOfWork();
                if (pulsado == "registro")
                {
                    nomVentana = "Registro nuevo usuario";
                }
                else
                {
                    nomVentana = "Recuperación de contraseña";
                }
            }
            #endregion

            #region Eventos

            public void btnAceptarCancel_Click(object sender, RoutedEventArgs e)
            {
                if ((sender as SimpleButton).Name == "btnAceptar")
                {
                    if (todosMetodosCorrectos())
                    {
                        try
                        {
                            USUARIOS user = new USUARIOS(uow);
                            user.Nombre = nombre;
                            user.Apellidos = apellidos;
                            user.DNI = dni;
                            user.TlfFijo = tlfFijo;
                            user.TlfMovil = tlfMovil;
                            user.Email = email;
                            user.Username = username;
                            user.Password = ComunClass.Encriptar(password);
                            uow.CommitChanges();
                            ThemedMessageBox.Show("Nuevo registro", "Usuario creado correctamente.", MessageBoxButton.OK, MessageBoxImage.Information);
                            ventana.Close();
                        }
                        catch (Exception ex)
                        {
                            ThemedMessageBox.Show("Nuevo registro", "Ha ocurrido un error a la hora de crear el nuevo usuario. Por favor, vuelva a intentarlo.", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    ventana.Close();
                }
            }

            #endregion

            #region Metodos

            private bool todosMetodosCorrectos()
            {
                Regex reg = new Regex(@"^[0-9]{8,8}[A-Za-z]$");
                if (nombre?.Length > 2 && apellidos?.Length > 2 &&
                    username?.Length >= 5 && password?.Length >= 8 &&
                    email?.Length >= 0 && dni?.Length >= 0)
                {
                    if (uow.FindObject<USUARIOS>(CriteriaOperator.Parse("Username == ?", username)) == null)
                    {
                        if (uow.FindObject<USUARIOS>(CriteriaOperator.Parse("Email == ?", email)) == null)
                        {
                            if (dni != null && reg.IsMatch(dni))
                            {
                                reg = new Regex(@"^[a-zA-Z0-9._%-]{5,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,4}$");
                                if (reg.IsMatch(email))
                                {
                                    string encriptado = ComunClass.Encriptar(password);
                                    Console.WriteLine(encriptado);
                                    Console.WriteLine(ComunClass.DesEncriptar(encriptado));
                                    return true;
                                }
                                else
                                {
                                    ThemedMessageBox.Show("Nuevo registro", "Por favor, rellene el email correctamente. Ej. ejmeplo@ejem.com.", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return false;
                                }
                            }
                            else
                            {
                                ThemedMessageBox.Show("Nuevo registro", "Por favor, rellene el DNI correctamente. Ej. 1111111-A.", MessageBoxButton.OK, MessageBoxImage.Information);
                                return false;
                            }
                        }
                        else
                        {
                            ThemedMessageBox.Show("Nuevo registro", "El email introducido ya está registrado. Por favor, introduzca otro.", MessageBoxButton.OK, MessageBoxImage.Information);
                            return false;
                        }
                    }
                    else
                    {
                        ThemedMessageBox.Show("Nuevo registro", "El username ya está registrado. Por favor, introduzca otro.", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                }
                else
                {
                    ThemedMessageBox.Show("Nuevo registro", "Debes rellenar todos los campos obligatorios.", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            #endregion
        }

    }
}
