using DevExpress.Xpo;
using Gestor_Productos.Database;
using Gestor_Productos.Database.ORM_GestProc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Gestor_Productos.Vistas
{
    /// <summary>
    /// Lógica de interacción para vMenu.xaml
    /// </summary>
    public partial class vMenu : DevExpress.Xpf.Core.ThemedWindow
    {
        public static USUARIOS usuario;
        private vLogin viewLogin;
        public vMenu(USUARIOS user, vLogin viewLogin)
        {
            InitializeComponent();
            usuario = user;
            this.viewLogin = viewLogin;
            ViewModelvMenu viewModel = new ViewModelvMenu(this);
            DataContext = viewModel;
        }

        private void ThemedWindow_Closing(object sender, CancelEventArgs e)
        {
            viewLogin.Close();
        }

        public class ViewModelvMenu : INotifyPropertyChanged
        {
            #region variables globales
            private vMenu ventana;
            public UnitOfWork uow;

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Constructor
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            //Constructor para visualizar albaranes
            public ViewModelvMenu(vMenu ventana)
            {
                this.ventana = ventana;
                uow = ConexionBBDD.getNewUnitOfWork();
            }
            #endregion
        }
    }
}
