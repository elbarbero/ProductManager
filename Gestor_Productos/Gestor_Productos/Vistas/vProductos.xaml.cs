using DevExpress.Xpo;
using Gestor_Productos.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestor_Productos.Vistas
{
    /// <summary>
    /// Lógica de interacción para vProductos.xaml
    /// </summary>
    public partial class vProductos : UserControl
    {
        public vProductos(string prueba)
        {
            InitializeComponent();
            MessageBox.Show("JHhkjh", prueba);
            ViewModelvProductos viewModel = new ViewModelvProductos(this);
            DataContext = viewModel;
        }

        public class ViewModelvProductos : INotifyPropertyChanged
        {
            #region variables globales
            private vProductos ventana;
            private vLogin viewLogin;
            public UnitOfWork uow;

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Constructor
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            //Constructor para visualizar albaranes
            public ViewModelvProductos(vProductos ventana)
            {
                this.ventana = ventana;
                uow = ConexionBBDD.getNewUnitOfWork();
            }
            #endregion
        }
    }
}
